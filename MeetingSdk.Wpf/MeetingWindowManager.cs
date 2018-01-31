using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;
using Prism.Events;
using MeetingSdk.Wpf.Interfaces;
using System.Windows;

namespace MeetingSdk.Wpf
{  
    /// <summary>
    ///  meeting:MeetingWindow.VideoType="DataCard"  meeting:MeetingWindow.BindAccout="3200123"
    /// </summary>
    public class MeetingWindowManager : IMeetingWindowManager, ILayoutWindow, IGetLiveVideoCoordinate
    {
        private readonly ConcurrentDictionary<int, Participant> _participants =
            new ConcurrentDictionary<int, Participant>();

        public Participant Participant { get; private set; }

        public int HostId { get; set; }

        public IEnumerable<Participant> Participants => _participants.Values;

        private bool _firstTimeToSpeak = true;
        private readonly object _syncRoot = new object();

        private readonly IEventAggregator _eventAggregator;
        private readonly IMeetingSdkAgent _meetingSdkWrapper;
        private readonly IDeviceNameAccessor _deviceNameAccessor;

        public MeetingWindowManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _meetingSdkWrapper = IoC.Get<IMeetingSdkAgent>();
            _deviceNameAccessor = IoC.Get<IDeviceNameAccessor>();

            LayoutRendererStore = new LayoutRendererStore();
            ModeDisplayerStore = new ModeDisplayerStore();

            _layoutWindows = new List<ILayoutWindow>();
            _layoutWindows.Add(this);
        }

        private int _joinStatus;

        private bool _creating;
        private bool _autoPublish;
        private bool _autoSubscribe;

        public async Task Join(int meetingId, bool creating, bool autoPublish = true, bool autoSubscribe = true)
        {
            if (_init == 0)
                throw new Exception("");

            if (Participant == null)
                throw new Exception("");

            if (Interlocked.CompareExchange(ref _joinStatus, 1, 0) != 0)
                throw new Exception("连接失败，状态错误。");

            _creating = creating;
            _autoPublish = autoPublish;
            _autoSubscribe = autoSubscribe;
            await Task.Run(() =>
            {
                GetParticipants();
                GetJoinMeetingInfo(meetingId);
            });

            if (_autoSubscribe)
            {
                AutoSubscribeStreams();
            }

            StartSpeak(new SpeakModel());
        }

        private IVideoBoxManager _videoBoxManager;
        public IVideoBoxManager VideoBoxManager => _videoBoxManager;

        private IList<ILayoutWindow> _layoutWindows;
        public IEnumerable<ILayoutWindow> LayoutWindows => _layoutWindows;

        public async Task Leave()
        {
            if (Interlocked.CompareExchange(ref _joinStatus, 0, 1) == 1)
            {
                // 执行必要的清理工作
                _firstTimeToSpeak = true;

                Participant.Resources.Clear();
                _participants.Clear();

                if (_videoBoxManager != null)
                {
                    foreach (var item in _videoBoxManager.Items.Where(m => m.AccountResource != null))
                    {
                        _videoBoxManager.Release(item.AccountResource.AccountModel.AccountId);
                    }
                }

                await Task.Run(() =>
                {
                    _eventAggregator.GetEvent<ParticipantCollectionChangeEvent>()
                        .Publish(Enumerable.Empty<Participant>());
                });
            }
        }

        private int _init;
        public void Initialize()
        {
            _videoBoxManager = IoC.Get<IVideoBoxManager>();
            if (Interlocked.CompareExchange(ref _init, 1, 0) == 0)
            {
                _eventAggregator.GetEvent<UserPublishCameraVideoEvent>().Subscribe(UserPublishCamera);
                _eventAggregator.GetEvent<UserPublishDataVideoEvent>().Subscribe(UserPublishDataVideo);
                _eventAggregator.GetEvent<UserPublishMicAudioEvent>().Subscribe(UserPublishMicAudio);
                _eventAggregator.GetEvent<UserUnpublishCameraVideoEvent>().Subscribe(UserUnpublishCamera);
                _eventAggregator.GetEvent<UserUnpublishDataCardVideoEvent>().Subscribe(UserUnpublishDataVideo);
                _eventAggregator.GetEvent<UserUnpublishMicAudioEvent>().Subscribe(UserUnpublishMicAudio);

                _eventAggregator.GetEvent<UserJoinEvent>().Subscribe(UserJoin);
                _eventAggregator.GetEvent<UserLeaveEvent>().Subscribe(UserLeave);

                _eventAggregator.GetEvent<UserStartSpeakEvent>().Subscribe(UserStartSpeak);
                _eventAggregator.GetEvent<UserStopSpeakEvent>().Subscribe(UserStopSpeak);

                _eventAggregator.GetEvent<StartSpeakEvent>().Subscribe(StartSpeak);
                _eventAggregator.GetEvent<StopSpeakEvent>().Subscribe(StopSpeak);
                _eventAggregator.GetEvent<UserLoginEvent>().Subscribe(UserLogin);
                _eventAggregator.GetEvent<UserLogoutEvent>().Subscribe(UserLogout);
            }
        }

        private void UserLogout(UserInfo userInfo)
        {
            Participant = null;
        }

        private void UserLogin(UserInfo userInfo)
        {
            var account = new AccountModel(userInfo.UserId, userInfo.UserName);
            Participant = new Participant(account);
        }

        private void GetParticipants()
        {
            MeetingResult<IList<ParticipantModel>> result = _meetingSdkWrapper.GetParticipants();
            if (result.StatusCode == 0)
            {
                var models = result.Result;
                foreach (var model in models)
                {
                    if (model.AccountId != Participant.Account.AccountId)
                    {
                        var account = new AccountModel(model.AccountId, model.AccountName);
                        var participant = new Participant(account)
                        {
                            IsSpeaking = model.IsSpeaking,
                        };

                        _participants.TryAdd(model.AccountId, participant);
                    }
                }
                _eventAggregator.GetEvent<ParticipantCollectionChangeEvent>().Publish(_participants.Values);
            }
        }

        private void GetJoinMeetingInfo(int meetingId)
        {
            MeetingResult<JoinMeetingModel> joinMeetingResult = _meetingSdkWrapper.GetJoinMeetingInfo(meetingId);

            if (joinMeetingResult.StatusCode == 0)
            {
                foreach (var speakerInfo in joinMeetingResult.Result.MeetingSpeakerModels)
                {
                    Participant participant;
                    if (_participants.TryGetValue(speakerInfo.Account.AccountId, out participant))
                    {
                        foreach (var streamInfo in speakerInfo.MeetingUserStreamInfos)
                        {
                            StreamResource<IStreamParameter> stream = new StreamResource<IStreamParameter>()
                            {
                                MediaType = (MediaType)streamInfo.MediaType,
                                ResourceId = streamInfo.ResourceId,
                                SyncId = streamInfo.SyncGroupId,
                            };
                            participant.Resources.Add(stream);
                        }
                    }
                }
            }
        }

        #region 发布与订阅

        public async Task<MeetingResult<int>> Publish(MediaType mediaType, string deviceName)
        {
            ThrowIfPublishVerify();
            var result = MeetingResult.Error<int>("未实现的媒体类型。");
            VideoBox videoBox;
            switch (mediaType)
            {
                case MediaType.Camera:

                    var cameraParam = StreamParameterProviders.GetParameter<PublishCameraStreamParameter>(deviceName);

                    PublishVideoModel publishCameraModel = cameraParam.GetPublishVideoModel();
                    publishCameraModel.VideoSendModel.SourceName = deviceName;

                    if (VideoBoxManager.TryGet(Participant.Account, VideoBoxType.Camera, mediaType, out videoBox))
                    {
                        publishCameraModel.VideoSendModel.DisplayWindow = videoBox.Handle;
                    }

                    var publishCameraResult = await _meetingSdkWrapper.PublishCameraVideo(publishCameraModel);
                    if (publishCameraResult.StatusCode == 0)
                    {
                        var publishStreamResource = new StreamResource<IStreamParameter>
                        {
                            MediaType = mediaType,
                            ResourceId = publishCameraResult.Result,
                            SyncId = publishCameraModel.AvSyncGroupId,
                            StreamParameter = cameraParam,
                            IsUsed = true
                        };
                        Participant.Resources.Add(publishStreamResource);
                        if (videoBox != null)
                        {
                            videoBox.AccountResource.ResourceId = publishStreamResource.ResourceId;
                            videoBox.AccountResource.MediaType = mediaType;

                            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Publish(videoBox);

                            //await _meetingSdkWrapper.StartLocalVideoRender(
                            //    publishStreamResource.ResourceId,
                            //    videoBox.Handle,
                            //    (int)videoBox.Host.ActualWidth,
                            //    (int)videoBox.Host.ActualHeight);
                        }
                    }

                    result = publishCameraResult;

                    break;
                case MediaType.Microphone:

                    PublishMicStreamParameter micParam = StreamParameterProviders.GetParameter<PublishMicStreamParameter>(deviceName);

                    PublishAudioModel publishMicModel = micParam.GetPublishAudioModel();
                    publishMicModel.AudioSendModel.SourceName = deviceName;

                    MeetingResult<int> publishMicResult =
                        await _meetingSdkWrapper.PublishMicAudio(publishMicModel);

                    if (publishMicResult.StatusCode == 0)
                    {
                        StreamResource<IStreamParameter> publishStreamResource =
                            new StreamResource<IStreamParameter>
                            {
                                MediaType = mediaType,
                                ResourceId = publishMicResult.Result,
                                SyncId = publishMicModel.AvSyncGroupId,
                                StreamParameter = micParam,
                                IsUsed = true
                            };
                        Participant.Resources.Add(publishStreamResource);
                    }

                    result = publishMicResult;

                    break;
                case MediaType.AudioCaptureCard:
                case MediaType.AudioDoc:

                    PublishMicStreamParameter docMicParam = StreamParameterProviders.GetParameter<PublishMicStreamParameter>(deviceName);

                    PublishAudioModel publishDocMicModel = docMicParam.GetPublishAudioModel();
                    publishDocMicModel.AudioSendModel.SourceName = deviceName;

                    MeetingResult<int> publishDocMicResult =
                        await _meetingSdkWrapper.PublishMicAudio(publishDocMicModel);

                    if (publishDocMicResult.StatusCode == 0)
                    {
                        StreamResource<IStreamParameter> publishStreamResource =
                            new StreamResource<IStreamParameter>
                            {
                                MediaType = mediaType,
                                ResourceId = publishDocMicResult.Result,
                                SyncId = publishDocMicModel.AvSyncGroupId,
                                StreamParameter = docMicParam,
                                IsUsed = true
                            };
                        Participant.Resources.Add(publishStreamResource);
                    }

                    result = publishDocMicResult;

                    break;

                case MediaType.VideoDoc:
                    PublishDataCardStreamParameter dataCardParam = StreamParameterProviders.GetParameter<PublishDataCardStreamParameter>(deviceName);

                    PublishVideoModel publishDataCardModel = dataCardParam.GetPublishVideoModel();
                    publishDataCardModel.VideoSendModel.SourceName = deviceName;

                    if (VideoBoxManager.TryGet(Participant.Account, VideoBoxType.DataCard, mediaType, out videoBox))
                    {
                        publishDataCardModel.VideoSendModel.DisplayWindow = videoBox.Handle;
                    }

                    MeetingResult<int> publishDataCardResult =
                        await _meetingSdkWrapper.PublishDataCardVideo(publishDataCardModel);

                    if (publishDataCardResult.StatusCode == 0)
                    {
                        StreamResource<IStreamParameter> publishStreamResource =
                            new StreamResource<IStreamParameter>
                            {
                                MediaType = mediaType,
                                ResourceId = publishDataCardResult.Result,
                                SyncId = publishDataCardModel.AvSyncGroupId,
                                StreamParameter = dataCardParam,
                                IsUsed = true
                            };


                        Participant.Resources.Add(publishStreamResource);
                        if (videoBox != null)
                        {
                            videoBox.AccountResource.ResourceId = publishStreamResource.ResourceId;
                            videoBox.AccountResource.MediaType = mediaType;

                            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Publish(videoBox);

                            //await _meetingSdkWrapper.StartLocalVideoRender(
                            //    publishStreamResource.ResourceId,
                            //    videoBox.Handle,
                            //    (int)videoBox.Host.ActualWidth,
                            //    (int)videoBox.Host.ActualHeight);
                        }
                    }

                    result = publishDataCardResult;

                    break;
                case MediaType.VideoCaptureCard:


                    PublishWinCaptureStreamParameter winCapParam = StreamParameterProviders.GetParameter<PublishWinCaptureStreamParameter>(deviceName);

                    PublishVideoModel publishWinCapModel = winCapParam.GetPublishVideoModel();
                    publishWinCapModel.VideoSendModel.SourceName = "DesktopCapture";

                    if (VideoBoxManager.TryGet(Participant.Account, VideoBoxType.WinCapture, mediaType, out videoBox))
                    {
                        publishWinCapModel.VideoSendModel.DisplayWindow = videoBox.Handle;
                    }

                    MeetingResult<int> publishWinCapResult =
                        await _meetingSdkWrapper.PublishWinCaptureVideo(publishWinCapModel);

                    if (publishWinCapResult.StatusCode == 0)
                    {
                        StreamResource<IStreamParameter> publishStreamResource =
                            new StreamResource<IStreamParameter>
                            {
                                MediaType = mediaType,
                                ResourceId = publishWinCapResult.Result,
                                SyncId = publishWinCapModel.AvSyncGroupId,
                                StreamParameter = winCapParam,
                                IsUsed = true
                            };
                        Participant.Resources.Add(publishStreamResource);
                        if (videoBox != null)
                        {
                            videoBox.AccountResource.ResourceId = publishStreamResource.ResourceId;
                            videoBox.AccountResource.MediaType = mediaType;

                            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Publish(videoBox);

                            //await _meetingSdkWrapper.StartLocalVideoRender(
                            //    publishStreamResource.ResourceId,
                            //    videoBox.Handle,
                            //    (int)videoBox.Host.ActualWidth,
                            //    (int)videoBox.Host.ActualHeight);
                        }
                    }

                    result = publishWinCapResult;

                    break;
                case MediaType.StreamMedia:
                    break;
                case MediaType.File:
                    break;
                case MediaType.WhiteBoard:
                    break;
                case MediaType.RemoteControl:
                    break;
                case MediaType.MediaTypeMax:
                    break;
            }
            LayoutChanged(mediaType);
            return result;
        }

        public async Task<MeetingResult> Unpublish(MediaType mediaType, int resourceId)
        {
            MeetingResult result = new MeetingResult();

            switch (mediaType)
            {
                case MediaType.Camera:
                    MeetingResult unpublishCameraResult = await _meetingSdkWrapper.UnpublishCameraVideo(resourceId);
                    if (unpublishCameraResult.StatusCode == 0)
                    {
                        Participant.Resources.RemoveWhere(m => m.ResourceId == resourceId);
                        VideoBoxManager.Release(Participant.Account.AccountId, VideoBoxType.Camera);
                    }

                    result = unpublishCameraResult;

                    break;
                case MediaType.Microphone:
                    MeetingResult unpublishMicResult = await _meetingSdkWrapper.UnpublishMicAudio(resourceId);

                    if (unpublishMicResult.StatusCode == 0)
                    {
                        Participant.Resources.RemoveWhere(res => res.ResourceId == resourceId);
                    }

                    result = unpublishMicResult;

                    break;
                case MediaType.VideoDoc:
                    MeetingResult unpublishWinCapResult = await _meetingSdkWrapper.UnpublishDataCardVideo(resourceId);

                    if (unpublishWinCapResult.StatusCode == 0)
                    {
                        Participant.Resources.RemoveWhere(m => m.ResourceId == resourceId);
                        VideoBoxManager.Release(Participant.Account.AccountId, VideoBoxType.DataCard);
                    }

                    result = unpublishWinCapResult;

                    break;
                case MediaType.AudioDoc:
                case MediaType.AudioCaptureCard:

                    MeetingResult unpublishAudioResult = await _meetingSdkWrapper.UnpublishMicAudio(resourceId);

                    if (unpublishAudioResult.StatusCode == 0)
                    {
                        Participant.Resources.RemoveWhere(m => m.ResourceId == resourceId);
                    }

                    result = unpublishAudioResult;

                    break;
                case MediaType.VideoCaptureCard:
                    MeetingResult unpublishDataCardResult = await _meetingSdkWrapper.UnpublishWinCaptureVideo(resourceId);

                    if (unpublishDataCardResult.StatusCode == 0)
                    {
                        Participant.Resources.RemoveWhere(m => m.ResourceId == resourceId);
                        VideoBoxManager.Release(Participant.Account.AccountId, VideoBoxType.WinCapture);
                    }

                    result = unpublishDataCardResult;

                    break;
                case MediaType.StreamMedia:
                    break;
                case MediaType.File:
                    break;
                case MediaType.WhiteBoard:
                    break;
                case MediaType.RemoteControl:
                    break;
                case MediaType.MediaTypeMax:
                    break;
            }
            LayoutChanged(mediaType);
            return result;
        }

        public MeetingResult Subscribe(int accountId, int resourceId, MediaType mediaType)
        {
            MeetingResult result = new MeetingResult();

            Participant participant = null;
            switch (mediaType)
            {
                case MediaType.Camera:

                    var cameraParam = StreamParameterProviders.GetParameter<SubscribeCameraStreamParameter>(string.Empty);
                    if (_participants.TryGetValue(accountId, out participant))
                    {
                        StreamResource<IStreamParameter> streamResource =
                            participant.Resources.SingleOrDefault(res => res.ResourceId == resourceId);

                        if (streamResource == null)
                            throw new NullReferenceException("流缓存不存在。");

                        SubscribeVideoModel subscribeCameraModel = cameraParam.GetSubscribeVideoModel();
                        //subscribeCameraModel.AvSyncGroupId = (uint)streamResource.SyncId;
                        subscribeCameraModel.ResourceId = resourceId;
                        subscribeCameraModel.UserId = accountId.ToString();

                        VideoBox videoBox;
                        if (VideoBoxManager.TryGet(participant.Account, VideoBoxType.None, mediaType, out videoBox))
                        {
                            subscribeCameraModel.VideoRecvModel.DisplayWindow = videoBox.Handle;
                            videoBox.AccountResource.ResourceId = resourceId;
                            videoBox.AccountResource.MediaType = mediaType;
                        }

                        MeetingResult subscribeCameraResult = _meetingSdkWrapper.SubscribeVideo(subscribeCameraModel);

                        if (subscribeCameraResult.StatusCode == 0)
                        {
                            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Publish(videoBox);

                            streamResource.IsUsed = true;
                            if (videoBox != null)
                            {
                                //await _meetingSdkWrapper.StartRemoteVideoRender(accountId, resourceId, videoBox.Handle,
                                //    (int)videoBox.Host.ActualWidth,
                                //    (int)videoBox.Host.ActualHeight);
                            }
                        }

                        result = subscribeCameraResult;
                    }

                    break;
                case MediaType.Microphone:

                    var micParam = StreamParameterProviders.GetParameter<SubscribeMicStreamParameter>(string.Empty);
                    if (_participants.TryGetValue(accountId, out participant))
                    {
                        var streamResource =
                            participant.Resources.SingleOrDefault(res => res.ResourceId == resourceId);

                        if (streamResource == null)
                            throw new NullReferenceException("流缓存不存在。");

                        SubscribeAudioModel subscribeMicModel = micParam.GetSubscribeAudioModel();

                        subscribeMicModel.UserId = accountId.ToString();
                        //subscribeMicModel.AvSyncGroupId = (uint)streamResource.SyncId;
                        subscribeMicModel.ResourceId = resourceId;
                        string audioOutputDeviceName;
                        if (!_deviceNameAccessor.TryGetSingleName(DeviceName.Speaker, out audioOutputDeviceName))
                        {
                            throw new Exception("扬声器未设置！");
                        }
                        subscribeMicModel.AudioRecvModel.SourceName = audioOutputDeviceName;

                        var subscribeMicResult = _meetingSdkWrapper.SubscribeAudio(subscribeMicModel);
                        if (subscribeMicResult.StatusCode == 0)
                        {
                            streamResource.IsUsed = true;
                        }

                        result = subscribeMicResult;
                    }


                    break;
                case MediaType.VideoDoc:

                    var winCapParam = StreamParameterProviders.GetParameter<SubscribeDataCardStreamParameter>(string.Empty);
                    if (_participants.TryGetValue(accountId, out participant))
                    {
                        var streamResource =
                            participant.Resources.SingleOrDefault(res => res.ResourceId == resourceId);

                        if (streamResource == null)
                            throw new NullReferenceException("流缓存不存在。");

                        SubscribeVideoModel subscribeWinCapModel = winCapParam.GetSubscribeVideoModel();
                        //subscribeWinCapModel.AvSyncGroupId = (uint)streamResource.SyncId;
                        subscribeWinCapModel.ResourceId = resourceId;
                        subscribeWinCapModel.UserId = accountId.ToString();

                        VideoBox videoBox;
                        var user = Participants.FirstOrDefault(p => p.Account.AccountId == accountId);

                        if (VideoBoxManager.TryGet(user.Account, VideoBoxType.DataCard, mediaType, out videoBox))
                        {
                            subscribeWinCapModel.VideoRecvModel.DisplayWindow = videoBox.Handle;
                            videoBox.AccountResource.ResourceId = resourceId;
                            videoBox.AccountResource.MediaType = mediaType;
                        }

                        MeetingResult subscribeWinCapResult = _meetingSdkWrapper.SubscribeVideo(subscribeWinCapModel);

                        if (subscribeWinCapResult.StatusCode == 0)
                        {
                            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Publish(videoBox);

                            streamResource.IsUsed = true;
                            if (videoBox != null)
                            {
                                //await _meetingSdkWrapper.StartRemoteVideoRender(accountId, resourceId, videoBox.Handle,
                                //    (int)videoBox.Host.ActualWidth,
                                //    (int)videoBox.Host.ActualHeight);
                            }
                        }

                        result = subscribeWinCapResult;
                    }


                    break;
                case MediaType.AudioDoc:
                case MediaType.AudioCaptureCard:

                    var docMicParam = StreamParameterProviders.GetParameter<SubscribeMicStreamParameter>(string.Empty);
                    if (_participants.TryGetValue(accountId, out participant))
                    {
                        var streamResource =
                            participant.Resources.SingleOrDefault(res => res.ResourceId == resourceId);

                        if (streamResource == null)
                            throw new NullReferenceException("流缓存不存在。");

                        SubscribeAudioModel subscribeMicModel = docMicParam.GetSubscribeAudioModel();

                        subscribeMicModel.UserId = accountId.ToString();
                        //subscribeMicModel.AvSyncGroupId = (uint)streamResource.SyncId;
                        subscribeMicModel.ResourceId = resourceId;
                        string audioOutputDeviceName;
                        if (!_deviceNameAccessor.TryGetSingleName(DeviceName.Speaker, out audioOutputDeviceName))
                        {
                            throw new Exception("扬声器未设置！");
                        }
                        subscribeMicModel.AudioRecvModel.SourceName = audioOutputDeviceName;

                        var subscribeMicResult = _meetingSdkWrapper.SubscribeAudio(subscribeMicModel);
                        if (subscribeMicResult.StatusCode == 0)
                        {
                            streamResource.IsUsed = true;
                        }

                        result = subscribeMicResult;
                    }

                    break;
                case MediaType.VideoCaptureCard:

                    var dataCardParam = StreamParameterProviders.GetParameter<SubscribeWinCaptureStreamParameter>(string.Empty);
                    if (_participants.TryGetValue(accountId, out participant))
                    {
                        var streamResource =
                            participant.Resources.SingleOrDefault(res => res.ResourceId == resourceId);

                        if (streamResource == null)
                            throw new NullReferenceException("流缓存不存在。");

                        SubscribeVideoModel subscribeDataCardModel = dataCardParam.GetSubscribeVideoModel();
                        //subscribeDataCardModel.AvSyncGroupId = (uint)streamResource.SyncId;
                        subscribeDataCardModel.ResourceId = resourceId;
                        subscribeDataCardModel.UserId = accountId.ToString();

                        VideoBox videoBox;
                        Participant user = Participants.FirstOrDefault(p => p.Account.AccountId == accountId);

                        if (VideoBoxManager.TryGet(user.Account, VideoBoxType.WinCapture, mediaType, out videoBox))
                        {
                            subscribeDataCardModel.VideoRecvModel.DisplayWindow = videoBox.Handle;
                            videoBox.AccountResource.ResourceId = resourceId;
                            videoBox.AccountResource.MediaType = mediaType;
                        }

                        MeetingResult subscribeCameraResult =
                            _meetingSdkWrapper.SubscribeVideo(subscribeDataCardModel);

                        if (subscribeCameraResult.StatusCode == 0)
                        {
                            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Publish(videoBox);

                            streamResource.IsUsed = true;
                            if (videoBox != null)
                            {
                                //await _meetingSdkWrapper.StartRemoteVideoRender(accountId, resourceId, videoBox.Handle,
                                //    (int)videoBox.Host.ActualWidth,
                                //    (int)videoBox.Host.ActualHeight);
                            }
                        }

                        result = subscribeCameraResult;
                    }

                    break;
                case MediaType.StreamMedia:
                    break;
                case MediaType.File:
                    break;
                case MediaType.WhiteBoard:
                    break;
                case MediaType.RemoteControl:
                    break;
                case MediaType.MediaTypeMax:
                    break;
            }
            LayoutChanged(mediaType);
            return result;
        }

        public MeetingResult Unsubscribe(int accountId, int resourceId, MediaType mediaType)
        {
            UserUnpublishModel userUnpublishModel = new UserUnpublishModel()
            {
                AccountId = accountId,
                ResourceId = resourceId,
            };

            MeetingResult result = _meetingSdkWrapper.Unsubscribe(userUnpublishModel);
            if (result.StatusCode == 0)
            {
                Participant participant;
                if (_participants.TryGetValue(accountId, out participant))
                {
                    var resource = participant.Resources.SingleOrDefault(m => m.ResourceId == resourceId);
                    if (resource != null)
                    {
                        resource.IsUsed = false;
                    }
                }

                VideoBoxType videoBoxType;
                if (mediaType.TryConvertVideoBoxType(out videoBoxType))
                {
                    VideoBoxManager.Release(
                        accountId, videoBoxType == VideoBoxType.Camera
                            ? VideoBoxType.None
                            : videoBoxType);
                }
            }
            LayoutChanged(mediaType);
            return result;
        }

        #endregion

        #region 其它用户发布事件

        void UserPublishCamera(UserPublishModel model)
        {
            Participant participant;
            if (_participants.TryGetValue(model.AccountId, out participant))
            {
                StreamResource<IStreamParameter> userPublishStreamResource = new StreamResource<IStreamParameter>();
                SubscribeCameraStreamParameter parameter = StreamParameterProviders.GetParameter<SubscribeCameraStreamParameter>(string.Empty);

                userPublishStreamResource.MediaType = MediaType.Camera;
                userPublishStreamResource.ResourceId = model.ResourceId;
                userPublishStreamResource.SyncId = model.SyncId;
                userPublishStreamResource.StreamParameter = parameter;

                participant.Resources.Add(userPublishStreamResource);

                if (_autoSubscribe)
                {
                    MeetingResult result = Subscribe(model.AccountId, model.ResourceId, MediaType.Camera);
                    if (result.StatusCode == 0)
                    {
                        userPublishStreamResource.IsUsed = true;
                    }
                }
            }
        }

        void UserPublishDataVideo(UserPublishModel model)
        {
            Participant participant;
            if (_participants.TryGetValue(model.AccountId, out participant))
            {
                StreamResource<IStreamParameter> userPublishStreamResource = new StreamResource<IStreamParameter>();
                SubscribeDataCardStreamParameter parameter = StreamParameterProviders.GetParameter<SubscribeDataCardStreamParameter>(string.Empty);

                userPublishStreamResource.MediaType = MediaType.VideoDoc;
                userPublishStreamResource.ResourceId = model.ResourceId;
                userPublishStreamResource.SyncId = model.SyncId;
                userPublishStreamResource.StreamParameter = parameter;

                participant.Resources.Add(userPublishStreamResource);

                if (_autoSubscribe)
                {
                    MeetingResult result = Subscribe(model.AccountId, model.ResourceId, MediaType.VideoDoc);
                    if (result.StatusCode == 0)
                    {
                        userPublishStreamResource.IsUsed = true;
                    }
                }
            }

        }

        void UserPublishMicAudio(UserPublishModel model)
        {
            Participant participant;
            if (_participants.TryGetValue(model.AccountId, out participant))
            {
                StreamResource<IStreamParameter> userPublishStreamResource = new StreamResource<IStreamParameter>();
                SubscribeMicStreamParameter parameter = StreamParameterProviders.GetParameter<SubscribeMicStreamParameter>(string.Empty);

                userPublishStreamResource.MediaType = MediaType.Microphone;
                userPublishStreamResource.ResourceId = model.ResourceId;
                userPublishStreamResource.SyncId = model.SyncId;
                userPublishStreamResource.StreamParameter = parameter;

                participant.Resources.Add(userPublishStreamResource);

                if (_autoSubscribe)
                {
                    MeetingResult result = Subscribe(model.AccountId, model.ResourceId, MediaType.Microphone);
                    if (result.StatusCode == 0)
                    {
                        userPublishStreamResource.IsUsed = true;
                    }
                }
            }

        }

        void UserUnpublishCamera(UserUnpublishModel model)
        {
            Participant participant;
            if (_participants.TryGetValue(model.AccountId, out participant))
            {
                participant.Resources.RemoveWhere(res => res.ResourceId == model.ResourceId);
                MeetingResult result = Unsubscribe(model.AccountId, model.ResourceId, MediaType.Camera);
                if (result.StatusCode == 0)
                {
                    VideoBoxManager.Release(model.AccountId, VideoBoxType.None);
                }
            }
        }

        void UserUnpublishDataVideo(UserUnpublishModel model)
        {
            Participant participant;
            if (_participants.TryGetValue(model.AccountId, out participant))
            {
                participant.Resources.RemoveWhere(res => res.ResourceId == model.ResourceId);
                MeetingResult result = Unsubscribe(model.AccountId, model.ResourceId, MediaType.VideoDoc);
                if (result.StatusCode == 0)
                {
                    VideoBoxManager.Release(model.AccountId, VideoBoxType.DataCard);
                }
            }
        }

        void UserUnpublishMicAudio(UserUnpublishModel model)
        {
            Participant participant;
            if (_participants.TryGetValue(model.AccountId, out participant))
            {
                participant.Resources.RemoveWhere(res => res.ResourceId == model.ResourceId);
                MeetingResult result = Unsubscribe(model.AccountId, model.ResourceId, MediaType.Microphone);
                if (result.StatusCode == 0)
                {

                }
            }
        }

        private void UserStopSpeak(UserSpeakModel obj)
        {
            Participant participant;
            if (_participants.TryGetValue(obj.Account.AccountId, out participant))
            {
                participant.IsSpeaking = false;

                var resources = participant.Resources.ToList();
                foreach (var streamResource in resources)
                {
                    Unsubscribe(participant.Account.AccountId, streamResource.ResourceId, streamResource.MediaType);
                    //Async.Create(() => Unsubscribe(participant.Account.AccountId, streamResource.ResourceId, streamResource.MediaType))
                    //    .TryRun($"用户停止发言 - 取消订阅失败{streamResource.MediaType}。");
                }
                participant.Resources.Clear();
                VideoBoxManager.Release(obj.Account.AccountId);
            }
        }

        private void UserStartSpeak(UserSpeakModel obj)
        {
            Participant p;
            if (_participants.TryGetValue(obj.Account.AccountId, out p))
            {
                p.IsSpeaking = true;
                VideoBoxManager.Release(obj.Account.AccountId);
            }
        }

        private void UserLeave(AccountModel obj)
        {
            Participant participant;
            if (_participants.TryRemove(obj.AccountId, out participant))
            {
                _eventAggregator.GetEvent<ParticipantCollectionChangeEvent>().Publish(_participants.Values);
                VideoBoxManager.Release(participant.Account.AccountId);
                MeetingLogger.Logger.LogMessage($"{obj.AccountId} 退出会议, 移除成功？");
            }
        }

        private void UserJoin(AccountModel obj)
        {
            var account = new AccountModel(obj.AccountId, obj.AccountName);
            Participant participant = new Participant(account);
            if (_participants.TryAdd(obj.AccountId, participant))
            {
                _eventAggregator.GetEvent<ParticipantCollectionChangeEvent>().Publish(_participants.Values);
                MeetingLogger.Logger.LogMessage($"{obj.AccountId} 加入会议, 添加成功？");
            }
        }

        #endregion

        private async void StopSpeak(SpeakModel obj)
        {
            ThrowIfPublishVerify();

            Participant.IsSpeaking = false;
            var resources = Participant.Resources.ToList();
            foreach (var streamResource in resources)
            {
                await Async.Create(() => Unpublish(streamResource.MediaType, streamResource.ResourceId))
                    .TryRun("取消发布摄像头。");
                VideoBoxType videoBoxType;
                if (streamResource.MediaType.TryConvertVideoBoxType(out videoBoxType))
                {
                    VideoBoxManager.Release(Participant.Account.AccountId, videoBoxType);
                }
            }
            Participant.Resources.Clear();
            LayoutChanged(MediaType.Camera);
        }

        private async void StartSpeak(SpeakModel obj)
        {
            if (_firstTimeToSpeak)
            {
                Monitor.Enter(_syncRoot);
                if (_firstTimeToSpeak)
                {
                    _firstTimeToSpeak = false;
                }
                Monitor.Exit(_syncRoot);
                return;
            }

            ThrowIfPublishVerify();

            Participant.IsSpeaking = true;

            Console.WriteLine($"_autoPublish:{_autoPublish}");

            if (_autoPublish)
            {
                IEnumerable<string> micDeviceName;
                if (!_deviceNameAccessor.TryGetName(DeviceName.Microphone, new Func<DeviceName, bool>(d => { return d.Option == "first"; }), out micDeviceName))
                {
                    throw new Exception("麦克风未设置！");
                }
                await Async.Create(() => Publish(MediaType.Microphone, micDeviceName.FirstOrDefault())).TryRun("发布Microphone失败。");

                IEnumerable<string> cameraDeviceName;
                if (!_deviceNameAccessor.TryGetName(DeviceName.Camera, new Func<DeviceName, bool>(d => { return d.Option == "first"; }), out cameraDeviceName))
                {
                    throw new Exception("人像摄像头未设置！");
                }
                await Async.Create(() => Publish(MediaType.Camera, cameraDeviceName.FirstOrDefault())).TryRun("发布Camera失败。");
            }
        }

        private void LayoutChanged(MediaType mediaType)
        {
            VideoBoxType videoBoxType;
            if (mediaType.TryConvertVideoBoxType(out videoBoxType))
            {
                LayoutChange(LayoutRendererStore.CurrentLayoutRenderType);
            }
        }

        private void AutoSubscribeStreams()
        {
            foreach (var participant in Participants)
            {
                foreach (var resource in participant.Resources)
                {
                    Subscribe(participant.Account.AccountId, resource.ResourceId, resource.MediaType);
                }
            }
        }

        void ThrowIfPublishVerify()
        {
            if (_init == 0)
                throw new Exception("初始化失败。");

            if (Participant == null)
                throw new Exception("未设置登录信息。");
        }


        public string WindowName => "MainWindow";

        public bool LayoutChange(string windowName, LayoutRenderType layoutRenderType)
        {
            ILayoutWindow layoutWindow = LayoutWindows.FirstOrDefault(window => window.WindowName == windowName);

            if (layoutWindow != null)
            {
                return layoutWindow.LayoutChange(layoutRenderType);
            }

            return false;
        }

        public bool LayoutChange(LayoutRenderType layoutRenderType)
        {
            IScreen screen = VideoBoxManager as IScreen;

            if (layoutRenderType == LayoutRenderType.AutoLayout)
            {
                LayoutRendererStore.CurrentLayoutRenderType = layoutRenderType;

                if (ModeChange(ModeDisplayerStore.CurrentModeDisplayerType))
                {
                    //LayoutRendererStore.CurrentLayoutRenderType = layoutRenderType;
                    _eventAggregator.GetEvent<LayoutChangedEvent>().Publish(layoutRenderType);

                    return true;
                }

                return false;
            }
            else
            {
                try
                {
                    _eventAggregator.GetEvent<RefreshCanvasEvent>().Publish();

                    if (LayoutRendererStore.Create(layoutRenderType).Render(GetVisibleVideoBoxs(), screen.Size, GetSpecialVideoBoxName(layoutRenderType)))
                    {
                        LayoutRendererStore.CurrentLayoutRenderType = layoutRenderType;

                        _eventAggregator.GetEvent<LayoutChangedEvent>().Publish(layoutRenderType);

                        return true;
                    }
                    else if (LayoutRendererStore.Create(LayoutRenderType.AverageLayout).Render(GetVisibleVideoBoxs(), screen.Size, GetSpecialVideoBoxName(LayoutRenderType.AverageLayout)))
                    {
                        LayoutRendererStore.CurrentLayoutRenderType = LayoutRenderType.AverageLayout;

                        _eventAggregator.GetEvent<LayoutChangedEvent>().Publish(LayoutRenderType.AverageLayout);

                        return false;
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    MeetingLogger.Logger.LogError(ex, ex.Message);
                }

                return false;
            }
        }

        private string GetSpecialVideoBoxName(LayoutRenderType layoutRenderType)
        {
            string specialVideoBoxName = VideoBoxManager.Properties.GetValueOrDefault(layoutRenderType.ToString()) as string;
            return specialVideoBoxName;
        }

        public bool ModeChange(ModeDisplayerType modeDisplayerType)
        {
            _eventAggregator.GetEvent<RefreshCanvasEvent>().Publish();

            IScreen screen = VideoBoxManager as IScreen;

            try
            {
                if (modeDisplayerType != ModeDisplayerStore.CurrentModeDisplayerType)
                {
                    LayoutRendererStore.CurrentLayoutRenderType = LayoutRenderType.AutoLayout;
                }


                if (ModeDisplayerStore.Create(modeDisplayerType).Display(GetVisibleVideoBoxs(), screen.Size))
                {
                    ModeDisplayerStore.CurrentModeDisplayerType = modeDisplayerType;

                    _eventAggregator.GetEvent<ModeDisplayerTypeChangedEvent>().Publish(modeDisplayerType);

                    if (LayoutRendererStore.CurrentLayoutRenderType != LayoutRenderType.AutoLayout)
                    {
                        LayoutChange(LayoutRendererStore.CurrentLayoutRenderType);
                    }

                    return true;
                }
                else if (ModeDisplayerStore.Create(ModeDisplayerType.InteractionMode).Display(GetVisibleVideoBoxs(), screen.Size))
                {
                    ModeDisplayerStore.CurrentModeDisplayerType = ModeDisplayerType.InteractionMode;

                    _eventAggregator.GetEvent<ModeDisplayerTypeChangedEvent>().Publish(ModeDisplayerType.InteractionMode);

                    if (LayoutRendererStore.CurrentLayoutRenderType != LayoutRenderType.AutoLayout)
                    {
                        LayoutChange(LayoutRendererStore.CurrentLayoutRenderType);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                MeetingLogger.Logger.LogError(ex, ex.Message);
            }

            return false;
        }

        private IList<IVideoBox> GetVisibleVideoBoxs()
        {
            return VideoBoxManager.Items.Where(v => v.Visible).OrderBy(v => v.Sequence).ToList<IVideoBox>();
        }

        public LayoutRendererStore LayoutRendererStore { get; set; }
        public ModeDisplayerStore ModeDisplayerStore { get; set; }

        public void RemoveLayoutWindow(string windowName)
        {
            var window = LayoutWindows.FirstOrDefault(win => win.WindowName == windowName);

            if (window != null)
            {
                _layoutWindows.Remove(window);
            }
        }

        public void AddLayoutWindow(ILayoutWindow layoutWindow)
        {
            if (!_layoutWindows.Any(win => win.WindowName == layoutWindow.WindowName))
            {
                _layoutWindows.Add(layoutWindow);
            }
        }

        public VideoStreamModel[] GetVideoStreamModels(Size canvasSize)
        {
            List<IVideoBox> copiedVideoBoxs = new List<IVideoBox>();

            foreach (IVideoBox item in GetVisibleVideoBoxs())
            {
                VideoBox videoBox = item as VideoBox;
                var copiedItem = videoBox.Copy();
                copiedVideoBoxs.Add(copiedItem);
            }


            if (LayoutRendererStore.CurrentLayoutRenderType == LayoutRenderType.AutoLayout)
            {
                ModeDisplayerStore.Create().Display(copiedVideoBoxs, canvasSize);
            }
            else
            {
                LayoutRendererStore.Create().Render(copiedVideoBoxs, canvasSize, GetSpecialVideoBoxName(LayoutRendererStore.CurrentLayoutRenderType));
            }

            List<VideoStreamModel> videoStreamModels = new List<VideoStreamModel>();

            foreach (var copiedVideoBox in copiedVideoBoxs)
            {
                VideoStreamModel videoStreamModel = new VideoStreamModel()
                {
                    AccountId = copiedVideoBox.AccountResource.AccountModel.AccountId.ToString(),
                    Height = (int)Math.Round(copiedVideoBox.Height),
                    StreamId = copiedVideoBox.AccountResource.ResourceId,
                    Width = (int)Math.Round(copiedVideoBox.Width),
                    X = (int)Math.Round(copiedVideoBox.PosX),
                    Y = (int)Math.Round(copiedVideoBox.PosY),
                };

                VideoBox videoBox = copiedVideoBox as VideoBox;
                videoStreamModel.VideoType = videoBox.AccountResource.MediaType == MediaType.VideoDoc ? VideoType.DataType : VideoType.VideoType;

                videoStreamModels.Add(videoStreamModel);
            }

            return videoStreamModels.ToArray();
        }
    }
}
