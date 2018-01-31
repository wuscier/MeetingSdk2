using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Prism.Mvvm;
using Caliburn.Micro;
using MeetingSdkTestWpf.Views;
using Prism.Events;
using Prism.Regions;
using MessageBox = System.Windows.MessageBox;
using Prism.Commands;
using System.Windows.Input;
using System.Windows.Threading;
using Newtonsoft.Json;
using System.IO;
using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;
using MeetingSdk.Wpf;
using System.Reflection;

namespace MeetingSdkTestWpf.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware, IViewAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeetingWindowManager _meetingWindowManager;

        private readonly IMeetingSdkAgent _meetingSdkWrapper;
        private readonly IDeviceNameAccessor _deviceNameAccessor;

        private int _recordStreamId;
        private int _liveStreamId;
        private ExtendedView _extendedView;

        public MainViewModel(IEventAggregator eventAggregator,
            IMeetingSdkAgent meetingSdkWrapper,
            IMeetingWindowManager meetingWindowManager,
            IDeviceNameAccessor deviceNameAccessor)
        {
            _eventAggregator = eventAggregator;
            _meetingSdkWrapper = meetingSdkWrapper;
            _meetingWindowManager = meetingWindowManager;
            _deviceNameAccessor = deviceNameAccessor;

            this.Participants = new BindableCollection<Participant>();
            _eventAggregator.GetEvent<ParticipantCollectionChangeEvent>().Subscribe(ParticipantCollectionChange);

            _eventAggregator.GetEvent<StartSpeakEvent>().Subscribe(StartSpeak);
            _eventAggregator.GetEvent<StopSpeakEvent>().Subscribe(StopSpeak);

            //_eventAggregator.GetEvent<LayoutChangeEvent>().Subscribe(LayoutChanged);
            _eventAggregator.GetEvent<UserRaiseHandRequestEvent>().Subscribe(UserRaiseHandRequest);
            _eventAggregator.GetEvent<TransparentMsgReceivedEvent>().Subscribe(TransparentMsgReceived);
            _eventAggregator.GetEvent<HostOperationReceivedEvent>().Subscribe(HostOperationReceived);
            _eventAggregator.GetEvent<HostKickoutUserEvent>().Subscribe(HostKickoutUserReceived);

            _eventAggregator.GetEvent<DeviceLostNoticeEvent>().Subscribe(DeviceLostNoticeEventHandler);
            _eventAggregator.GetEvent<DeviceStatusChangedEvent>().Subscribe(DeviceStatusChangedEventHandler);
            _eventAggregator.GetEvent<LockStatusChangedEvent>().Subscribe(LockStatusChangedEventHandler);
            _eventAggregator.GetEvent<MeetingManageExceptionEvent>().Subscribe(MeetingManageExceptionEventHandler);
            _eventAggregator.GetEvent<SdkCallbackEvent>().Subscribe(SdkCallbackEventHandler);



            SubscribeVideoCommand = new DelegateCommand<Participant>(BtnSubscribeVideo);
            UnsubscribeVideoCommand = new DelegateCommand<Participant>(BtnUnsubscribeVideo);
            SubscribeAudioCommand = new DelegateCommand<Participant>(BtnSubscribeAudio);
            UnsubscribeAudioCommand = new DelegateCommand<Participant>(BtnUnsubscribeAudio);
            RequireSpeakCommand = new DelegateCommand<Participant>(BtnRequireSpeak);
            RequireStopSpeakCommand = new DelegateCommand<Participant>(BtnRequireStopSpeak);
            GetSpeakerInfoCommand = new DelegateCommand<Participant>(BtnGetSpeakerInfo);
            GetUserPublishStreamInfoCommand = new DelegateCommand<Participant>(BtnGetUserPublishStreamInfo);
            GetSpeakerVideoStreamParamCommand = new DelegateCommand<Participant>(BtnGetSpeakerVideoStreamParam);
            SendUiTransparentMsgCommand = new DelegateCommand<Participant>(SendUiTransparentMsg);
            AsynMicSendReqCommand = new DelegateCommand<Participant>(AsynMicSendReq);
            HostKickoutUserCommand = new DelegateCommand<Participant>(HostKickoutUser);

            HostOrderOneDoOpration1Command = new DelegateCommand<Participant>(HostOrderOneDoOpration1);
            HostOrderOneDoOpration2Command = new DelegateCommand<Participant>(HostOrderOneDoOpration2);
            HostOrderOneDoOpration3Command = new DelegateCommand<Participant>(HostOrderOneDoOpration3);
            HostOrderOneDoOpration4Command = new DelegateCommand<Participant>(HostOrderOneDoOpration4);
            HostOrderOneDoOpration5Command = new DelegateCommand<Participant>(HostOrderOneDoOpration5);
            HostOrderOneDoOpration6Command = new DelegateCommand<Participant>(HostOrderOneDoOpration6);
            HostOrderOneDoOpration7Command = new DelegateCommand<Participant>(HostOrderOneDoOpration7);
            HostOrderOneDoOpration8Command = new DelegateCommand<Participant>(HostOrderOneDoOpration8);

            SetVideoClarityCommand = new DelegateCommand<Participant>(SetVideoClarity);

            

            VideoDevices = new BindableCollection<string>();
            AudioInputDevices = new BindableCollection<string>();
            AudioInputDevice2s = new BindableCollection<string>();
            AudioOutputDevices = new BindableCollection<string>();

            _userPublishVideoDatas = new Dictionary<int, UserPublishModel>();
            _userPublishAudioDatas = new Dictionary<int, UserPublishModel>();
            _userPublishDocDatas = new Dictionary<int, UserPublishModel>();

            this.Imeis = new BindableCollection<NameValue>()
            {
                new NameValue("吴叙", "BOX408D5CAF922E"),
                new NameValue("秦学良PC", "BOX408D5CBBCA17"),
                new NameValue("秦学良笔记本", "BOX68F728B76F4F"),
                new NameValue("潘桂龙笔记本", "BOX3417EB96377B"),
            };
        }

        private void SdkCallbackEventHandler(SdkCallbackModel obj)
        {
            throw new NotImplementedException();
        }

        private void MeetingManageExceptionEventHandler(ExceptionModel obj)
        {
            throw new NotImplementedException();
        }

        private void LockStatusChangedEventHandler(MeetingResult obj)
        {
            throw new NotImplementedException();
        }

        private void DeviceStatusChangedEventHandler(DeviceStatusModel obj)
        {
            throw new NotImplementedException();
        }

        private void DeviceLostNoticeEventHandler(ResourceModel obj)
        {
            throw new NotImplementedException();
        }


        private void HostKickoutUserReceived(KickoutUserModel obj)
        {
            if (_meetingWindowManager.Participant.Account.AccountId.ToString() == obj.KickedUserId)
            {
                App.Current.Dispatcher.BeginInvoke(new System.Action(async () =>
                {
                    IsSpeaking = false;
                    MeetingResult result = await _meetingSdkWrapper.LeaveMeeting();
                    ShowResult(result);

                    await _meetingWindowManager.Leave();

                }));
            }
        }

        private void HostOperationReceived(HostOprateType hostOprateType)
        {
            switch (hostOprateType)
            {
                case HostOprateType.OpenCamera:
                    BtnPublishVideo();
                    break;
                case HostOprateType.CloseCamera:
                    BtnUnpublishVideo();
                    break;
                case HostOprateType.OpenMic:
                    BtnPublishAudio();
                    break;
                case HostOprateType.CloseMic:
                    BtnUnpublishAudio();
                    break;
                case HostOprateType.OpenSharedScreen:
                    OpenSharedScreen();
                    break;
                case HostOprateType.CloseSharedScreen:
                    CloseSharedScreen();
                    break;
                case HostOprateType.OpenAudio:
                    SubscribeAudios();
                    break;
                case HostOprateType.CloseAudio:
                    UnSubscribeAudios();
                    break;
            }
        }

        private async void CloseSharedScreen()
        {
            foreach (var res in _meetingWindowManager.Participant.Resources)
            {
                if (res.MediaType == MediaType.VideoDoc)
                {
                    var result = await _meetingWindowManager.Unpublish(MediaType.VideoDoc, res.ResourceId);
                    res.IsUsed = false;
                    ShowResult(result);
                }
            }
        }

        private async void OpenSharedScreen()
        {
            var result = await _meetingWindowManager.Publish(MediaType.VideoDoc, "");
            ShowResult(result);
        }

        private void UnSubscribeAudios()
        {
            foreach (var p in _meetingWindowManager.Participants)
            {
                foreach (var res in p.Resources)
                {
                    if (res.MediaType == MediaType.Microphone)
                    {
                        var result = _meetingWindowManager.Unsubscribe(p.Account.AccountId, res.ResourceId, MediaType.Microphone);
                        if (result.StatusCode == 0)
                        {
                            res.IsUsed = false;
                        }
                    }
                }
            }
        }

        private void SubscribeAudios()
        {
            foreach (var p in _meetingWindowManager.Participants)
            {
                foreach (var res in p.Resources)
                {
                    if (res.MediaType == MediaType.Microphone)
                    {
                        var result = _meetingWindowManager.Subscribe(p.Account.AccountId, res.ResourceId, MediaType.Microphone);
                        if (result.StatusCode == 0)
                        {
                            res.IsUsed = true;
                        }
                    }
                }
            }
        }

        private void TransparentMsgReceived(TransparentMsg obj)
        {
            MessageBox.Show($"收到来自{obj.SenderAccountId}的如下消息：\r\n{obj.Data}");
        }

        private void UserRaiseHandRequest(AccountModel obj)
        {
            App.Current.Dispatcher.BeginInvoke(new System.Action(async () =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"视讯号：{obj.AccountId}，名称：{obj.AccountName} 举手了！\r\n是否同意发言？", "消息", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var result = await _meetingSdkWrapper.HostOrderOneSpeak(obj.AccountId.ToString(), "");
                    ShowResult(result);
                }
            }));
        }

        private void StopSpeak(SpeakModel obj)
        {
            IsSpeaking = false;
        }

        private void StartSpeak(SpeakModel obj)
        {
            IsSpeaking = true;
        }

        private void LayoutChanged()
        {
            try
            {
                //LayoutRendererStore.Factory.Create()?.Render(_meetingWindowManager.VideoBoxManager);
            }
            catch (Exception ex)
            {

            }
        }

        private Dictionary<int, UserPublishModel> _userPublishVideoDatas;
        private Dictionary<int, UserPublishModel> _userPublishAudioDatas;
        private Dictionary<int, UserPublishModel> _userPublishDocDatas;

        private BindableCollection<Participant> _participants;

        public BindableCollection<Participant> Participants
        {
            get => _participants;
            set => SetProperty(ref _participants, value);
        }

        private void ParticipantCollectionChange(IEnumerable<Participant> participants)
        {
            this.Participants.Clear();
            this.Participants.AddRange(participants);
        }


        private BindableCollection<NameValue> _imeis;

        public BindableCollection<NameValue> Imeis
        {
            get => _imeis;
            set { SetProperty(ref _imeis, value); }
        }

        private string _imei;

        public string Imei
        {
            get => _imei;
            set { SetProperty(ref _imei, value); }
        }

        private int _publishVideoId;
        private int _publishAudioId;


        private bool _isSpeaking;

        public bool IsSpeaking
        {
            get { return _isSpeaking; }

            set { SetProperty(ref _isSpeaking, value); }
        }

        private string _textBlockPhoneId;

        public string TextBlockPhoneId
        {
            get { return _textBlockPhoneId; }
            set { SetProperty(ref _textBlockPhoneId, value); }
        }

        private string _textBlockName;

        public string TextBlockName
        {
            get { return _textBlockName; }
            set { SetProperty(ref _textBlockName, value); }
        }


        private string _textMeetingId;

        public string TextBlockMeetingId
        {
            get { return _textMeetingId; }
            set { SetProperty(ref _textMeetingId, value); }
        }

        private string _joinMeetingId;

        public string TBJoinMeetingId
        {
            get { return _joinMeetingId; }
            set { SetProperty(ref _joinMeetingId, value); }
        }


        public BindableCollection<string> VideoDevices { get; private set; }

        private string _selectedVideoDevice;

        public string SelectedVideoDevice
        {
            get { return _selectedVideoDevice; }
            set
            {
                if (SetProperty(ref _selectedVideoDevice, value))
                {
                    _deviceNameAccessor.SetName(DeviceName.Camera, "");
                    _deviceNameAccessor.SetName(DeviceName.Camera, value, "first");
                }
            }
        }



        public BindableCollection<string> AudioInputDevices { get; private set; }
        public BindableCollection<string> AudioInputDevice2s { get; private set; }

        private string _selectedAudioInputDevice;

        public string SelectedAudioInputDevice
        {
            get { return _selectedAudioInputDevice; }
            set
            {
                if (SetProperty(ref _selectedAudioInputDevice, value))
                {
                    _deviceNameAccessor.SetName(DeviceName.Microphone, "");
                    _deviceNameAccessor.SetName(DeviceName.Microphone, value, "first");
                    _deviceNameAccessor.SetName(DeviceName.Microphone, SelectedAudioInputDevice2, "second");
                }
            }
        }

        private string _selectedAudioInputDevice2;

        public string SelectedAudioInputDevice2
        {
            get { return _selectedAudioInputDevice2; }
            set
            {
                if (SetProperty(ref _selectedAudioInputDevice2, value))
                {
                    _deviceNameAccessor.SetName(DeviceName.Microphone, "");
                    _deviceNameAccessor.SetName(DeviceName.Microphone, SelectedAudioInputDevice, "first");
                    _deviceNameAccessor.SetName(DeviceName.Microphone, value,"second");
                }
            }
        }


        public BindableCollection<string> AudioOutputDevices { get; private set; }

        private string _selectedAudioOutputDevice;

        public string SelectedAudioOutputDevice
        {
            get { return _selectedAudioOutputDevice; }
            set
            {
                if (SetProperty(ref _selectedAudioOutputDevice, value))
                {
                    _deviceNameAccessor.SetSingleName(DeviceName.Speaker, value);
                }
            }
        }

        public async void BtnStart()
        {
            string configPath = Environment.CurrentDirectory;
            int configPathLen = configPath.Length;

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var startResult = await _meetingSdkWrapper.Start("PCJM", path);
            var startHostResult = await _meetingSdkWrapper.StartHost("PCJM", path);

            MessageBox.Show($"start result: {startResult.StatusCode}");
        }

        public async void BtnLoginViaImei()
        {
            try
            {
                if (string.IsNullOrEmpty(Imei))
                {
                    MessageBox.Show("请输入设备号！");
                    return;
                }

                string imei = Imei;

              var loginResult = await _meetingSdkWrapper.LoginViaImei(imei);

                if (loginResult.StatusCode == 0)
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = loginResult.Result.Account.AccountId,
                        UserName = loginResult.Result.Account.AccountName,
                    };
                    _eventAggregator.GetEvent<UserLoginEvent>().Publish(userInfo);

                    TextBlockName = $"名称：{loginResult.Result.Account.AccountName}";
                    TextBlockPhoneId = $"视讯号：{loginResult.Result.Account.AccountId}";
                }
                else
                {
                    ShowResult(loginResult);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public async void BtnLoginViaNube()
        {
            try
            {
                var loginResult = await _meetingSdkWrapper.LoginThirdParty("67005003", "46fb264854534e2a8ea5620d28f44db7", "28ae6fdb9dac41fb8606bb1b185ea3bb");

                if (loginResult.StatusCode == 0)
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = loginResult.Result.Account.AccountId,
                        UserName = loginResult.Result.Account.AccountName,
                    };
                    _eventAggregator.GetEvent<UserLoginEvent>().Publish(userInfo);
                }


                TextBlockName = $"名称：{loginResult.Result.Account.AccountName}";
                TextBlockPhoneId = $"视讯号：{loginResult.Result.Account.AccountId}";
                //MessageBox.Show(
                //    $"login result: 视讯号：{loginResult.Account.AccountId}, 名称：{loginResult.Account.AccountName}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        public void BtnSaveDeviceSettings()
        {
            var accessor = IoC.Get<IDeviceNameAccessor>();
            IoC.Get<IDeviceConfigLoader>().SaveConfig(accessor);
        }


        public async Task BtnCreateMeeting()
        {
            MeetingResult<MeetingModel> createMeetingResult = await _meetingSdkWrapper.CreateMeeting("");

            if (createMeetingResult.StatusCode == 0)
            {
                await _meetingWindowManager.Join(createMeetingResult.Result.MeetingId, true);
            }

            TextBlockMeetingId = createMeetingResult.Result.MeetingId.ToString();
            //MessageBox.Show(
            //    $"create meeting result, status code:{createMeetingResult.StatusCode}, message:{createMeetingResult.Message}");
        }

        public async Task BtnCreateAndInviteMeeting()
        {
            MeetingResult<MeetingModel> createMeetingResult = await _meetingSdkWrapper.CreateAndInviteMeeting("", new int[] { 90531246, 61000356 });

            if (createMeetingResult.StatusCode == 0)
            {
                await _meetingWindowManager.Join(createMeetingResult.Result.MeetingId, true);
            }

            TextBlockMeetingId = createMeetingResult.Result.MeetingId.ToString();
            //MessageBox.Show(
            //    $"create meeting result, status code:{createMeetingResult.StatusCode}, message:{createMeetingResult.Message}");
        }

        public async Task BtnCreateDatedMeeting()
        {
            DatedMeetingModel datedMeetingModel = new DatedMeetingModel()
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                Password = "123456",
                Topic = "中共十九大",
            };
            MeetingResult<MeetingModel> createMeetingResult = await _meetingSdkWrapper.CreateDatedMeeting("", datedMeetingModel);

            if (createMeetingResult.StatusCode == 0)
            {
                await _meetingWindowManager.Join(createMeetingResult.Result.MeetingId, true);
            }

            TextBlockMeetingId = createMeetingResult.Result.MeetingId.ToString();
            ShowResult(createMeetingResult, createMeetingResult.Result);
            //MessageBox.Show(
            //    $"create meeting result, status code:{createMeetingResult.StatusCode}, message:{createMeetingResult.Message}");
        }

        public async Task BtnCreateAndInviteDatedMeeting()
        {
            DatedMeetingModel datedMeetingModel = new DatedMeetingModel()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3),
                Password = "000000",
                Topic = "党的先进性",
            };

            MeetingResult<MeetingModel> createMeetingResult = await _meetingSdkWrapper.CreateAndInviteDatedMeeting("", datedMeetingModel, new int[] { 90531246, 61000356 });

            if (createMeetingResult.StatusCode == 0)
            {
                await _meetingWindowManager.Join(createMeetingResult.Result.MeetingId, true);
            }

            TextBlockMeetingId = createMeetingResult.Result.MeetingId.ToString();

            ShowResult(createMeetingResult, createMeetingResult.Result);

            //MessageBox.Show(
            //    $"create meeting result, status code:{createMeetingResult.StatusCode}, message:{createMeetingResult.Message}");
        }


        public async Task ModifyMeetingInviters()
        {
            int meetingId;
            if (int.TryParse(TextBlockMeetingId, out meetingId))
            {
                MeetingResult result = await _meetingSdkWrapper.ModifyMeetingInviters(meetingId, "", 1, new int[] { 90531246, 61000356 });
                ShowResult(result);
            }
            else if (int.TryParse(TBJoinMeetingId, out meetingId))
            {
                MeetingResult result = await _meetingSdkWrapper.ModifyMeetingInviters(meetingId, "", 1, new int[] { 90531246, 61000356 });
                ShowResult(result);
            }
            else
            {
                MessageBox.Show("会议号为空！");
            }
        }

        public async Task BtnJoinMeeting()
        {
            if (string.IsNullOrEmpty(TBJoinMeetingId))
            {
                MessageBox.Show("请输入要进入的会议号！");
                return;
            }

            int meetingId;
            if (!int.TryParse(TBJoinMeetingId, out meetingId))
            {
                MessageBox.Show("会议号格式不正确！");
                return;
            }

            try
            {
                var syncContext = SynchronizationContext.Current;
                MeetingResult<JoinMeetingModel> joinMeetingResult =
                    await _meetingSdkWrapper.JoinMeeting(meetingId, true);

                if (joinMeetingResult.StatusCode == 0)
                {
                    await _meetingWindowManager.Join(meetingId, false);
                    syncContext.Post(AutoSubscribe, joinMeetingResult.Result.MeetingSpeakerModels);
                }
                else
                {
                    MessageBox.Show($"join meeting result: status code={joinMeetingResult.StatusCode}, message={joinMeetingResult.Message}");
                }
            }
            catch (Exception exception)
            {
                MeetingLogger.Logger.LogError(exception, "JoinMeeting调用错误。");
            }
        }

        private void AutoSubscribe(object obj)
        {
            var speakerModels = (IEnumerable<MeetingSpeakerModel>)obj;
            foreach (var speakerModel in speakerModels)
            {
                foreach (var streamModel in speakerModel.MeetingUserStreamInfos)
                {
                    _meetingWindowManager.Subscribe(
                        speakerModel.Account.AccountId,
                        streamModel.ResourceId,
                        (MediaType)streamModel.MediaType);
                }
            }
        }

        public void BtnGetVideoDevices()
        {
            GetVideoDevices();
        }

        private void GetVideoDevices()
        {
            MeetingResult<IList<VideoDeviceModel>> videoDevices =
                _meetingSdkWrapper.GetVideoDevices();

            VideoDevices.Clear();

            foreach (var videoDevice in videoDevices.Result)
            {
                VideoDevices.Add(videoDevice?.DeviceName);
            }
        }

        public void BtnGetAudioInputDevices()
        {
            GetAudioInputDevices();
        }

        private void GetAudioInputDevices()
        {
            MeetingResult<IList<string>> mics = _meetingSdkWrapper.GetMicrophones();
            AudioInputDevices.Clear();
            AudioInputDevice2s.Clear();

            foreach (var mic in mics.Result)
            {
                AudioInputDevices.Add(mic);
                AudioInputDevice2s.Add(mic);
            }

        }

        public void BtnGetAudioOutputDevices()
        {
            GetAudioOutputDevices();
        }

        private void GetAudioOutputDevices()
        {
            MeetingResult<IList<string>> speakers = _meetingSdkWrapper.GetLoudSpeakers();
            AudioOutputDevices.Clear();
            foreach (var speaker in speakers.Result)
            {
                AudioOutputDevices.Add(speaker);
            }
        }

        public async Task BtnExitMeeting()
        {
            IsSpeaking = false;
            MeetingResult leaveTaskResult = await _meetingSdkWrapper.LeaveMeeting();

            await _meetingWindowManager.Leave();

            MessageBox.Show(
                $"leave meeting, status code={leaveTaskResult.StatusCode}, message={leaveTaskResult.Message}");
        }

        public async void BtnStartSpeak()
        {
            try
            {
                MeetingResult result = await _meetingSdkWrapper.AskForSpeak();

                MessageBox.Show($"申请发言，statuscode={result.StatusCode}, message={result.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public async void BtnStopSpeak()
        {
            try
            {
                MeetingResult result = await _meetingSdkWrapper.AskForStopSpeak();
                MessageBox.Show($"申请停止发言，statuscode={result.StatusCode}, message={result.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async void GetSpeakerList()
        {
            var result = await _meetingSdkWrapper.GetSpeakerList();
            ShowResult(result, result.Result);
        }

        public async void GetCurMeetingMode()
        {
            var result = await _meetingSdkWrapper.GetCurMeetingMode();
            ShowResult(result, result.Result.ToString());
        }
        public async void GetMeetingLockStatus()
        {
            var result = await _meetingSdkWrapper.GetMeetingLockStatus();
            ShowResult(result, result.Result);
        }
        public async void GetCurrentSubscribleStreamInfo()
        {
            var result = await _meetingSdkWrapper.GetCurrentSubscribleStreamInfo();
            ShowResult(result, result.Result);
        }

        public async void GetMicSendList()
        {
            var result = await _meetingSdkWrapper.GetMicSendList();
            ShowResult(result, result.Result);
        }

        public async void HostChangeMeetingMode()
        {
            var result1 = await _meetingSdkWrapper.GetCurMeetingMode();
            MeetingMode curMeetingMode = result1.Result;

            MeetingMode targetMeetingMode = curMeetingMode == MeetingMode.FreeMode ? MeetingMode.HostMode : MeetingMode.FreeMode;

            var result2 = await _meetingSdkWrapper.HostChangeMeetingMode(targetMeetingMode);

            ShowResult(result2, $"{curMeetingMode}=>{targetMeetingMode}");
        }


        public async void AskForMeetingLock()
        {
            var result1 = await _meetingSdkWrapper.GetMeetingLockStatus();
            bool hasLock = result1.Result;

            bool target = !hasLock;

            try
            {
                var result2 = await _meetingSdkWrapper.AskForMeetingLock(target);

                ShowResult(result2, $"{hasLock}=>{target}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void StartMp4Record()
        {
            PublishLiveStreamParameter publishLiveStreamParameter = new PublishLiveStreamParameter()
            {
                LiveParameter = new LiveParameter()
                {
                    AudioBitrate = 64,
                    BitsPerSample = 16,
                    Channels = 1,
                    SampleRate = 8000,
                    FilePath = Environment.CurrentDirectory,
                    Height = 480,
                    IsLive = false,
                    IsRecord = true,
                    VideoBitrate = 800,
                    Width = 640,
                },
                MediaType = MediaType.StreamMedia,
                StreamType = StreamType.Live,

            };

            MeetingResult<int> publishLiveResult = _meetingSdkWrapper.PublishLiveStream(publishLiveStreamParameter);

            if (publishLiveResult.StatusCode == 0)
            {
                _recordStreamId = publishLiveResult.Result;

                List<VideoStreamModel> videoStreamModels = new List<VideoStreamModel>();
                List<AudioStreamModel> audioStreamModels = new List<AudioStreamModel>();

                var videoResource = _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

                if (videoResource != null)
                {
                    VideoStreamModel videoStreamModel = new VideoStreamModel()
                    {
                        Height = 480,
                        Width = 640,
                        StreamId = videoResource.ResourceId,
                        X = 0,
                        Y = 0,
                        AccountId = _meetingWindowManager.Participant.Account.AccountId.ToString(),
                        VideoType = VideoType.VideoType,
                    };

                    videoStreamModels.Add(videoStreamModel);

                    MeetingResult updateLiveResult = _meetingSdkWrapper.UpdateLiveStreamVideoInfo(publishLiveResult.Result, videoStreamModels.ToArray());

                }

                var audioResource = _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Microphone);

                if (audioResource != null)
                {
                    AudioStreamModel audioStreamModel = new AudioStreamModel()
                    {
                        AccountId = _meetingWindowManager.Participant.Account.AccountId.ToString(),
                        BitsPerSameple = 16,
                        Channels = 1,
                        SampleRate = 8000,
                        StreamId = audioResource.ResourceId,
                    };

                    audioStreamModels.Add(audioStreamModel);

                    MeetingResult updateLiveResult = _meetingSdkWrapper.UpdateLiveStreamAudioInfo(publishLiveResult.Result, audioStreamModels.ToArray());
                }

                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".mp4");

                MeetingResult startRecordResult = _meetingSdkWrapper.StartMp4Record(publishLiveResult.Result, filename);
                ShowResult(startRecordResult);

            }
        }

        public void StopMp4Record()
        {
            var stopRecordResult = _meetingSdkWrapper.StopMp4Record(_recordStreamId);
            var unpublishLiveResult = _meetingSdkWrapper.UnpublishLiveStream(_recordStreamId);
            ShowResult(stopRecordResult);
        }

        public void StartLiveRecord()
        {
            PublishLiveStreamParameter publishLiveStreamParameter = new PublishLiveStreamParameter()
            {
                LiveParameter = new LiveParameter()
                {
                    AudioBitrate = 64,
                    BitsPerSample = 16,
                    Channels = 1,
                    SampleRate = 8000,
                    FilePath = Environment.CurrentDirectory,
                    Height = 480,
                    IsLive = true,
                    IsRecord = false,
                    VideoBitrate = 800,
                    Width = 640,
                },
                MediaType = MediaType.StreamMedia,
                StreamType = StreamType.Live,

            };

            MeetingResult<int> publishLiveResult = _meetingSdkWrapper.PublishLiveStream(publishLiveStreamParameter);

            if (publishLiveResult.StatusCode == 0)
            {
                _liveStreamId = publishLiveResult.Result;

                List<VideoStreamModel> videoStreamModels = new List<VideoStreamModel>();
                List<AudioStreamModel> audioStreamModels = new List<AudioStreamModel>();


                var videoResource = _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

                if (videoResource != null)
                {
                    VideoStreamModel videoStreamModel = new VideoStreamModel()
                    {
                        Height = 480,
                        Width = 640,
                        StreamId = videoResource.ResourceId,
                        X = 0,
                        Y = 0,
                        AccountId = _meetingWindowManager.Participant.Account.AccountId.ToString(),
                    };

                    videoStreamModels.Add(videoStreamModel);


                    MeetingResult updateLiveResult = _meetingSdkWrapper.UpdateLiveStreamVideoInfo(publishLiveResult.Result, videoStreamModels.ToArray());
                }

                var audioResource = _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Microphone);

                if (audioResource != null)
                {
                    AudioStreamModel audioStreamModel = new AudioStreamModel()
                    {
                        AccountId = _meetingWindowManager.Participant.Account.AccountId.ToString(),
                        BitsPerSameple = 16,
                        Channels = 1,
                        SampleRate = 8000,
                        StreamId = audioResource.ResourceId,
                    };

                    audioStreamModels.Add(audioStreamModel);

                    MeetingResult updateLiveResult = _meetingSdkWrapper.UpdateLiveStreamAudioInfo(publishLiveResult.Result, audioStreamModels.ToArray());
                }

                var startLiveResult = _meetingSdkWrapper.StartLiveRecord(_liveStreamId, "http://gslb.butel.com/live/live.butel.com/39ff");
                ShowResult(startLiveResult);

            }
        }

        public void StopLiveRecord()
        {
            var stopLiveResult = _meetingSdkWrapper.StopLiveRecord(_liveStreamId);
            var unpublishLiveResult = _meetingSdkWrapper.UnpublishLiveStream(_liveStreamId);
            ShowResult(stopLiveResult);

        }


        public void AddDisplayWindow()
        {
            var resource = _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

            if (resource != null)
            {
                _extendedView = new ExtendedView();
                _extendedView.Show();
                var result = _meetingSdkWrapper.AddDisplayWindow(_meetingWindowManager.Participant.Account.AccountId, resource.ResourceId, _extendedView.DisplayHanlde, 0, 0);
                ShowResult(result);
            }
        }

        public void RemoveDisplayWindow()
        {
            var resource = _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

            if (resource != null)
            {
                var result = _meetingSdkWrapper.RemoveDisplayWindow(_meetingWindowManager.Participant.Account.AccountId, resource.ResourceId, _extendedView.DisplayHanlde, 0, 0);
                ShowResult(result);
                _extendedView?.Close();
                _extendedView = null;
            }
        }


        public void SetVideoDisplayMode()
        {
            var result = _meetingSdkWrapper.SetVideoDisplayMode(VideoDisplayMode.Flunet);
            ShowResult(result);
        }
        public void SetAudioMixRecvBufferNum()
        {
            var result = _meetingSdkWrapper.SetAudioMixRecvBufferNum(65536, 65536, 65536);
            ShowResult(result);
        }
        public void SetAutoAdjustEnableStatus()
        {
            var result = _meetingSdkWrapper.SetAutoAdjustEnableStatus(1);
            ShowResult(result);
        }
        public void SetPublishDoubleVideoStreamStatus()
        {
            var result = _meetingSdkWrapper.SetPublishDoubleVideoStreamStatus(0);
            ShowResult(result);
        }
        public void SetLowVideoStreamCodecParam()
        {
            var result = _meetingSdkWrapper.SetLowVideoStreamCodecParam(640, 480, 800, 15);
            ShowResult(result);
        }
        public void SetCurCpuInfo()
        {
            var result = _meetingSdkWrapper.SetCurCpuInfo(10, 20);
            ShowResult(result);
        }

        private void  SelectDevicesBasedOnConfig()
        {
            GetVideoDevices();
            GetAudioInputDevices();
            GetAudioOutputDevices();

            string cameraDeviceName;
            if (_deviceNameAccessor.TryGetSingleName(DeviceName.Camera, out cameraDeviceName))
            {
                if (VideoDevices.Contains(cameraDeviceName))
                {
                    SelectedVideoDevice = cameraDeviceName;
                }
            }
            else
            {
                if (VideoDevices.Count >= 1)
                {
                    SelectedVideoDevice = VideoDevices[0];
                }
            }




            IEnumerable<string> micDeviceName;
            if (_deviceNameAccessor.TryGetName(DeviceName.Microphone, (deviceName) => { return deviceName.Option == "first"; }, out micDeviceName))
            {
                string firstMicDeviceName = micDeviceName.FirstOrDefault();
                if (AudioInputDevices.Contains(firstMicDeviceName))
                {
                    SelectedAudioInputDevice = firstMicDeviceName;
                }
            }
            else
            {
                if (AudioInputDevices.Count >= 1)
                {
                    SelectedAudioInputDevice = AudioInputDevices[0];
                }
            }

            IEnumerable<string> micDeviceName2;
            if (_deviceNameAccessor.TryGetName(DeviceName.Microphone, (deviceName) => { return deviceName.Option == "second"; }, out micDeviceName2))
            {
                string firstMicDeviceName2 = micDeviceName2.FirstOrDefault();
                if (AudioInputDevice2s.Contains(firstMicDeviceName2))
                {
                    SelectedAudioInputDevice2 = firstMicDeviceName2;
                }
            }
            else
            {
                var audioInput2 = AudioInputDevice2s.FirstOrDefault(audioInput => audioInput != SelectedAudioInputDevice);

                if (audioInput2 != null)
                {
                    SelectedAudioInputDevice2 = audioInput2;
                }
            }

            string speakerDeviceName;
            if (_deviceNameAccessor.TryGetSingleName(DeviceName.Speaker, out speakerDeviceName))
            {
                if (AudioOutputDevices.Contains(speakerDeviceName))
                {
                    SelectedAudioOutputDevice = speakerDeviceName;
                }
            }
            else
            {
                if (AudioOutputDevices.Count >= 1)
                {
                    SelectedAudioOutputDevice = AudioOutputDevices[0];
                }
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectDevicesBasedOnConfig();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void BtnPublishVideo()
        {
            if (string.IsNullOrEmpty(SelectedVideoDevice))
            {
                MessageBox.Show("请选择摄像头！");
                return;
            }

            MeetingResult<int> result =
                await _meetingWindowManager.Publish(MediaType.Camera, SelectedVideoDevice);

            if (result.StatusCode == 0)
            {
                _publishVideoId = result.Result;
                MessageBox.Show($"发布视频流{_publishVideoId}成功！");
            }
            else
            {
                MessageBox.Show($"发布视频流失败！statusCode={result.StatusCode}, msg={result.Message}");
            }

        }

        public async void BtnUnpublishVideo()
        {
            var streamResource =
                _meetingWindowManager.Participant.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

            if (streamResource == null)
            {
                MessageBox.Show("没有可取消的视频流！");
                return;
            }

            _publishVideoId = streamResource.ResourceId;

            MeetingResult result = await _meetingWindowManager.Unpublish(MediaType.Camera, _publishVideoId);
            if (result.StatusCode == 0)
            {
                MessageBox.Show($"取消发布视频流{_publishVideoId}成功！");
                _publishVideoId = 0;

            }
            else
            {
                MessageBox.Show($"取消发布视频流{_publishVideoId}失败！");
            }
        }

        public async void BtnPublishAudio()
        {
            if (string.IsNullOrEmpty(SelectedAudioInputDevice))
            {
                MessageBox.Show("请选择麦克风！");
                return;
            }

            MeetingResult<int> result =
                await _meetingWindowManager.Publish(MediaType.Microphone, SelectedAudioInputDevice);

            if (result.StatusCode == 0)
            {
                _publishAudioId = result.Result;
                MessageBox.Show($"发布音频流{_publishAudioId}成功！");
            }
            else
            {
                MessageBox.Show($"发布音频流失败！statusCode={result.StatusCode}, msg={result.Message}");
            }

        }

        public async void BtnUnpublishAudio()
        {
            var streamResource =
                _meetingWindowManager.Participant.Resources
                    .FirstOrDefault(res => res.MediaType == MediaType.Microphone);

            if (streamResource == null)
            {
                MessageBox.Show("没有可取消的音频流！");
                return;
            }

            _publishAudioId = streamResource.ResourceId;

            MeetingResult result = await _meetingWindowManager.Unpublish(MediaType.Microphone, _publishAudioId);
            if (result.StatusCode == 0)
            {
                MessageBox.Show($"取消发布音频流{_publishAudioId}成功！");
                _publishAudioId = 0;
            }
            else
            {
                MessageBox.Show($"取消发布音频流{_publishAudioId}失败！");
            }

        }


        public async void RaiseHandReq()
        {
            try
            {
                var result = await _meetingSdkWrapper.RaiseHandReq();
                ShowResult(result);
            }
            catch (Exception e)
            {

            }
        }

        public async void SendAudioSpeakerStatus()
        {

            var result = await _meetingSdkWrapper.SendAudioSpeakerStatus(true);
            ShowResult(result);
        }



        public void BtnSubscribeVideo(Participant p)
        {
            if (!p.IsSpeaking)
            {
                MessageBox.Show("用户不在发言状态！");
                return;
            }

            var streamResource = p.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);
            if (streamResource == null)
            {
                MessageBox.Show("用户没有可订阅的视频流！");
                return;
            }

            MeetingResult result = _meetingWindowManager.Subscribe(p.Account.AccountId,
                streamResource.ResourceId,
                MediaType.Camera);

            MessageBox.Show(result.StatusCode == 0 ? "订阅视频流成功！" : "订阅视频流失败！");

        }

        public void BtnUnsubscribeVideo(Participant p)
        {
            if (!p.IsSpeaking)
            {
                MessageBox.Show("用户不在发言状态！");
                return;
            }

            var streamResource = p.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);
            if (streamResource == null)
            {
                MessageBox.Show("用户没有可取消订阅的视频流！");
                return;
            }


            MeetingResult result =
                _meetingWindowManager.Unsubscribe(p.Account.AccountId, streamResource.ResourceId,
                    MediaType.Camera);

            MessageBox.Show(result.StatusCode == 0 ? "取消订阅视频流成功！" : "取消订阅视频流失败！");

        }

        public void BtnSubscribeAudio(Participant p)
        {
            if (!p.IsSpeaking)
            {
                MessageBox.Show("用户不在发言状态！");
                return;
            }


            var streamResource = p.Resources.FirstOrDefault(res => res.MediaType == MediaType.Microphone);
            if (streamResource == null)
            {
                MessageBox.Show("用户没有可订阅的音频流！");
                return;
            }

            MeetingResult result = _meetingWindowManager.Subscribe(p.Account.AccountId,
                streamResource.ResourceId,
                MediaType.Microphone);

            MessageBox.Show(result.StatusCode == 0 ? "订阅音频流成功！" : "订阅音频流失败！");


        }

        public void BtnUnsubscribeAudio(Participant p)
        {
            if (!p.IsSpeaking)
            {
                MessageBox.Show("用户不在发言状态！");
                return;
            }

            var streamResource = p.Resources.FirstOrDefault(res => res.MediaType == MediaType.Microphone);
            if (streamResource == null)
            {
                MessageBox.Show("用户没有可取消订阅的音频流！");
                return;
            }


            MeetingResult result =
                _meetingWindowManager.Unsubscribe(p.Account.AccountId, streamResource.ResourceId,
                    MediaType.Microphone);

            MessageBox.Show(result.StatusCode == 0 ? "取消订阅音频流成功！" : "取消订阅音频流失败！");

        }

        public async void BtnRequireSpeak(Participant p)
        {
            if (p.IsSpeaking)
            {
                MessageBox.Show("用户已在发言状态！");
                return;
            }

            MeetingResult result =
                await _meetingSdkWrapper.HostOrderOneSpeak(p.Account.AccountId.ToString(), "");
            MessageBox.Show(result.StatusCode == 0 ? "指定发言成功！" : "指定发言失败！");
        }

        public async void BtnRequireStopSpeak(Participant p)
        {
            if (!p.IsSpeaking)
            {
                MessageBox.Show("用户已不在发言状态！");
                return;
            }

            MeetingResult result =
                await _meetingSdkWrapper.HostOrderOneStopSpeak(p.Account.AccountId.ToString());
            MessageBox.Show(result.StatusCode == 0 ? "指定停止发言成功！" : "指定停止发言失败！");
        }

        private async void BtnGetSpeakerInfo(Participant p)
        {
            var result = await _meetingSdkWrapper.GetSpeakerInfo(p.Account.AccountId);
            ShowResult(result, result.Result);
        }

        private async void BtnGetUserPublishStreamInfo(Participant p)
        {
            var result = await _meetingSdkWrapper.GetUserPublishStreamInfo(p.Account.AccountId);
            ShowResult(result, result.Result);
        }

        private async void BtnGetSpeakerVideoStreamParam(Participant p)
        {
            Participant speaker = _meetingWindowManager.Participants.FirstOrDefault(participant => participant.Account.AccountId == p.Account.AccountId);

            if (speaker != null)
            {
                StreamResource<IStreamParameter> streamResource = speaker.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

                if (streamResource != null)
                {
                    var result = await _meetingSdkWrapper.GetSpeakerVideoStreamParam(p.Account.AccountId, streamResource.ResourceId);
                    ShowResult(result, result.Result);
                }
            }
        }

        private void SendUiTransparentMsg(Participant p)
        {
            var result = _meetingSdkWrapper.SendUiTransparentMsg(p.Account.AccountId, "测试消息");
            ShowResult(result);
        }

        private async void AsynMicSendReq(Participant p)
        {
            var result = await _meetingSdkWrapper.AsynMicSendReq(p.Account.AccountId);
            ShowResult(result);
        }

        private async void HostKickoutUser(Participant p)
        {
            var result = await _meetingSdkWrapper.HostKickoutUser(p.Account.AccountId);
            ShowResult(result);
        }


        private async void HostOrderOneDoOpration1(Participant p)
        {
            try
            {
                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)1);
                ShowResult(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void HostOrderOneDoOpration2(Participant p)
        {
            try
            {
                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)2);
                ShowResult(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async void HostOrderOneDoOpration3(Participant p)
        {
            try
            {

                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)3);
                ShowResult(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void HostOrderOneDoOpration4(Participant p)
        {
            try
            {

                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)4);
                ShowResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void HostOrderOneDoOpration5(Participant p)
        {
            try
            {

                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)5);
                ShowResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async void HostOrderOneDoOpration6(Participant p)
        {
            try
            {

                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)6);
                ShowResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void HostOrderOneDoOpration7(Participant p)
        {
            try
            {

                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)7);
                ShowResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void HostOrderOneDoOpration8(Participant p)
        {
            try
            {
                var result = await _meetingSdkWrapper.HostOrderOneDoOpration(p.Account.AccountId, (HostOprateType)8);
                ShowResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetVideoClarity(Participant p)
        {
            var targetP = _meetingWindowManager.Participants.FirstOrDefault(participant => participant.Account.AccountId == p.Account.AccountId);

            if (targetP != null)
            {
                var resource = targetP.Resources.FirstOrDefault(res => res.MediaType == MediaType.Camera);

                if (resource != null)
                {
                    var result = _meetingSdkWrapper.SetVideoClarity(p.Account.AccountId, resource.ResourceId, 3);
                    ShowResult(result);
                }
            }
        }


        public ICommand SubscribeVideoCommand { get; set; }
        public ICommand UnsubscribeVideoCommand { get; set; }
        public ICommand SubscribeAudioCommand { get; set; }
        public ICommand UnsubscribeAudioCommand { get; set; }
        public ICommand RequireSpeakCommand { get; set; }
        public ICommand RequireStopSpeakCommand { get; set; }
        public ICommand GetSpeakerInfoCommand { get; set; }
        public ICommand GetUserPublishStreamInfoCommand { get; set; }
        public ICommand GetSpeakerVideoStreamParamCommand { get; set; }
        public ICommand SendUiTransparentMsgCommand { get; set; }
        public ICommand AsynMicSendReqCommand { get; set; }
        public ICommand HostKickoutUserCommand { get; set; }


        public ICommand HostOrderOneDoOpration1Command { get; set; }
        public ICommand HostOrderOneDoOpration2Command { get; set; }
        public ICommand HostOrderOneDoOpration3Command { get; set; }
        public ICommand HostOrderOneDoOpration4Command { get; set; }
        public ICommand HostOrderOneDoOpration5Command { get; set; }
        public ICommand HostOrderOneDoOpration6Command { get; set; }
        public ICommand HostOrderOneDoOpration7Command { get; set; }
        public ICommand HostOrderOneDoOpration8Command { get; set; }

        public ICommand SetVideoClarityCommand { get; set; }

        

        #region IViewAware

        private FrameworkElement _view;

        public void AttachView(object view, object context = null)
        {
            _view = view as FrameworkElement;
            ViewAttached?.Invoke(this, new ViewAttachedEventArgs()
            {
                Context = context,
                View = view
            });
        }

        public object GetView(object context = null)
        {
            return _view;
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        private void ShowResult(MeetingResult result, object info = null)
        {
            string sInfo = info == null ? "" : JsonConvert.SerializeObject(info);
            MessageBox.Show($"statusCode={result.StatusCode}, msg={result.Message}, info:{sInfo}");
        }

        #endregion
    }
}
