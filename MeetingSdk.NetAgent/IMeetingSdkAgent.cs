using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.NetAgent
{
    public interface IMeetingSdkAgent
    {
        bool IsStarted { get; }

        Action<MeetingResult<UserPublishModel>> OnUserPublishCameraVideoEvent { get; set; }
        Action<MeetingResult<UserPublishModel>> OnUserPublishDataVideoEvent { get; set; }
        Action<MeetingResult<UserPublishModel>> OnUserPublishMicAudioEvent { get; set; }

        Action<MeetingResult<UserUnpublishModel>> OnUserUnpublishCameraVideoEvent { get; set; }
        Action<MeetingResult<UserUnpublishModel>> OnUserUnpublishDataCardVideoEvent { get; set; }
        Action<MeetingResult<UserUnpublishModel>> OnUserUnpublishMicAudioEvent { get; set; }

        Action<MeetingResult<SpeakModel>> OnStartSpeakEvent { get; set; }
        Action<MeetingResult<SpeakModel>> OnStopSpeakEvent { get; set; }

        Action<MeetingResult<UserSpeakModel>> OnUserStartSpeakEvent { get; set; }
        Action<MeetingResult<UserSpeakModel>> OnUserStopSpeakEvent { get; set; }

        Action<MeetingResult<AccountModel>> OnUserJoinEvent { get; set; }
        Action<MeetingResult<AccountModel>> OnUserLeaveEvent { get; set; }

        /// <summary>
        /// 主持人修改会议模式
        /// </summary>
        Action<MeetingResult<MeetingMode>> OnHostChangeMeetingModeEvent { get; set; }
        /// <summary>
        /// 主持人踢出用户
        /// </summary>
        Action<MeetingResult<KickoutUserModel>> OnHostKickoutUserEvent { get; set; }
        /// <summary>
        /// 收到用户举手请求通知（主持人收到）
        /// </summary>
        Action<MeetingResult<AccountModel>> OnRaiseHandRequestEvent { get; set; }

        /// <summary>
        /// 收到主持人指定打开/关闭麦克风、扬声器、摄像头 事件通知
        /// </summary>
        Action<MeetingResult<HostOprateType>> OnHostOrderDoOpratonEvent { get; set; }
        /// <summary>
        /// 收到其他参会者打开/关闭麦克风、扬声器、摄像头 事件通知
        /// </summary>
        Action<MeetingResult<OtherChangeAudioSpeakerStatusModel>> OnOtherChangeAudioSpeakerStatusEvent { get; set; }

        Action<MeetingResult<int>> OnNetworkStatusLevelNoticeEvent { get; set; }
        Action<MeetingResult<NetType>> OnNetworkCheckedEvent { get; set; }

        Action<MeetingResult<TransparentMsg>> OnTransparentMsgReceivedEvent { get; set; }
        Action<MeetingResult<UiTransparentMsg>> OnUiTransparentMsgReceivedEvent { get; set; }

        Action<MeetingResult> OnLockStatusChangedEvent { get; set; }
        Action<MeetingResult<ExceptionModel>> OnMeetingManageExceptionEvent { get; set; }
        Action<MeetingResult<DeviceStatusModel>> OnDeviceStatusChangedEvent { get; set; }
        Action<MeetingResult<ResourceModel>> OnDeviceLostNoticeEvent { get; set; }
        Action<MeetingResult<SdkCallbackModel>> OnSdkCallbackEvent { get; set; }

        Action<MeetingResult<MeetingInvitationModel>> OnMeetingInviteEvent { get; set; }
        Action<MeetingResult<RecommendContactModel>> OnContactRecommendEvent { get; set; }
        Action<MeetingResult<ForcedOfflineModel>> OnForcedOfflineEvent { get; set; }


        #region 设置，启动及鉴权接口

        MeetingResult SetNpsUrl(params string[] npsUrlList);

        MeetingResult SetRkPath(string rkPath);

        Task<MeetingResult> Start(string devModel, string configPath);

        Task<MeetingResult> Stop();

        Task<MeetingResult<LoginModel>> LoginViaImei(string imei);

        Task<MeetingResult<LoginModel>> LoginThirdParty(string nube, string appkey, string uid);

        Task<MeetingResult> BindToken(string token, AccountModel account);

        #endregion

        #region 获取会议相关信息，音视频设备信息接口

        Task<MeetingResult> IsMeetingExist(int meetingId);
        Task<MeetingResult<IList<MeetingModel>>> GetMeetingList();
        Task<MeetingResult<MeetingModel>> GetMeetingInfo(int meetingId);
        MeetingResult<JoinMeetingModel> GetJoinMeetingInfo(int meetingId);
        Task<MeetingResult<IList<MeetingSpeakerModel>>> GetSpeakerList();
        Task<MeetingResult<MeetingSpeakerModel>> GetSpeakerInfo(int accountId);
        Task<MeetingResult<MeetingMode>> GetCurMeetingMode();
        Task<MeetingResult<bool>> GetMeetingLockStatus();
        Task<MeetingResult<MeetingPasswordModel>> GetMeetingPassword(int meetingId);
        Task<MeetingResult<IList<MeetingUserStreamModel>>> GetUserPublishStreamInfo(int accountId);
        Task<MeetingResult<IList<MeetingUserStreamModel>>> GetCurrentSubscribleStreamInfo();
        Task<MeetingResult<SpeakerVideoStreamParamModel>> GetSpeakerVideoStreamParam(int accountId, int resourceId);
        MeetingResult<IList<ParticipantModel>> GetParticipants();
        MeetingResult<IList<ParticipantModel>> GetParticipantsByPage(int pageNum, int countPerPage);
        Task<MeetingResult<MeetingInvitationSMSModel>> GetMeetingInvitationSMS(int meetingId, int inviterPhoneId,
            string inviterName, MeetingType meetingType, string app, InviterUrlType urlType);
        MeetingResult<string> GetMeetingQos();
        Task<MeetingResult<IList<ParticipantModel>>> GetMicSendList();
        MeetingResult<IList<VideoDeviceModel>> GetVideoDevices();
        MeetingResult<IList<string>> GetMicrophones();
        MeetingResult<IList<string>> GetLoudSpeakers();
        MeetingResult<string> GetSerialNo();

        #endregion

        #region 音视频，网络测试接口

        Task<MeetingResult> PlayVideoTest(int colorsps, int fps, int width, int height, IntPtr previewWindow, string videoCapName);
        Task<MeetingResult> StopVideoTest();

        Task<MeetingResult> PlayVideoTestYUVCB(int colorsps, int fps, int width, int height, string videoCapName);
        Task<MeetingResult> StopVideoTestYUVCB();

        Task<MeetingResult> AsynPlaySoundTest(string wavFile, string renderName);
        MeetingResult StopPlaySoundTest();

        MeetingResult<int> RecordSoundTest(string micName, string wavFile);
        MeetingResult StopRecordSoundTest();

        MeetingResult AsynStartNetDiagnosticSerialCheck();
        MeetingResult<int> StopNetBandDetect();

        Task<MeetingResult<BandWidthModel>> GetNetBandDetectResult();

        #endregion

        #region 会议相关接口

        Task<MeetingResult> ResetMeetingPassword(int meetingId, string password);
        Task<MeetingResult<MeetingHasPwdModel>> CheckMeetingHasPassword(int meetingId);
        Task<MeetingResult> CheckMeetingPasswordValid(int meetingId, string encryptedPwd);
        Task<MeetingResult<MeetingModel>> CreateMeeting(string appType);
        Task<MeetingResult<MeetingModel>> CreateAndInviteMeeting(string appType, int[] inviteeList);
        Task<MeetingResult<MeetingModel>> CreateDatedMeeting(string appType, DatedMeetingModel datedMeetingModel);
        Task<MeetingResult<MeetingModel>> CreateAndInviteDatedMeeting(string appType, DatedMeetingModel datedMeetingModel,
            int[] inviteeList);
        Task<MeetingResult> ModifyMeetingInviters(int meetingId, string appType, int smsType, int[] inviters);
        Task<MeetingResult<JoinMeetingModel>> JoinMeeting(int meetingId, bool autoSpeak);
        Task<MeetingResult> LeaveMeeting();

        #endregion

        #region 音视频发布和订阅

        Task<MeetingResult<int>> GenericSyncId();
        Task<MeetingResult<int>> PublishCameraVideo(PublishVideoModel publishVideoModel);
        Task<MeetingResult<int>> PublishDataCardVideo(PublishVideoModel publishVideoModel);
        Task<MeetingResult<int>> PublishWinCaptureVideo(PublishVideoModel publishVideoModel);
        Task<MeetingResult<int>> PublishMicAudio(PublishAudioModel publishAudioModel);

        Task<MeetingResult> UnpublishCameraVideo(int resoureId);
        Task<MeetingResult> UnpublishDataCardVideo(int resoureId);
        Task<MeetingResult> UnpublishWinCaptureVideo(int resoureId);
        Task<MeetingResult> UnpublishMicAudio(int resoureId);

        MeetingResult SubscribeVideo(SubscribeVideoModel subscribeVideoModel);
        MeetingResult SubscribeAudio(SubscribeAudioModel subscribeVideoModel);
        MeetingResult Unsubscribe(UserUnpublishModel userUnpublishModel);

        MeetingResult PushMediaFrameData(int resourceId, FrameType frameType, string frameData,
            int orientation);
        MeetingResult StartYUVDataCallBack(int accountId, int resourceId);
        MeetingResult StopYUVDataCallBack(int accountId, int resourceId);

        [Obsolete]
        MeetingResult StartLocalVideoRender(int resourceId, IntPtr displayWindow, int aspx, int aspy);
        [Obsolete]
        MeetingResult StopLocalVideoRender(int resourceId);
        [Obsolete]
        MeetingResult StartRemoteVideoRender(int accountId, int resourceId, IntPtr displayWindow,
            int aspx, int aspy);
        [Obsolete]
        MeetingResult StopRemoteVideoRender(int accountId, int resourceId);

        #endregion

        #region 会议中相关操作

        Task<MeetingResult> AskForSpeak(string speakerId = null);
        Task<MeetingResult> AskForStopSpeak();
        MeetingResult SendUiTransparentMsg(int destAccount, string message);
        Task<MeetingResult> AsynSendUIMsg(int msgId, int targetAccountId, string data);
        Task<MeetingResult> AsynMicSendReq(int toBeSpeakerAccountId);
        Task<MeetingResult> HostChangeMeetingMode(MeetingMode toMode);
        Task<MeetingResult> HostKickoutUser(int accountId);
        Task<MeetingResult> RaiseHandReq();
        Task<MeetingResult> AskForMeetingLock(bool bToLock);
        Task<MeetingResult> HostOrderOneSpeak(string toAccountId, string kickAccountId);
        Task<MeetingResult> HostOrderOneStopSpeak(string toAccountId);
        Task<MeetingResult> SendAudioSpeakerStatus(bool isOpen);
        Task<MeetingResult> HostOrderOneDoOpration(int toUserId, HostOprateType oprateType);
        Task<MeetingResult> ModifyNickName(string accountName);

        #endregion

        #region 录制，推流和双屏渲染

        MeetingResult StopMp4Record(int streamId);
        MeetingResult StartMp4Record(int streamId, string filepath);
        MeetingResult<int> PublishLiveStream(PublishLiveStreamParameter parameter);
        MeetingResult<int> UnpublishLiveStream(int streamId);
        MeetingResult StartLiveRecord(int streamId, string url);
        MeetingResult StopLiveRecord(int streamId);
        MeetingResult UpdateLiveStreamVideoInfo(int streamId, VideoStreamModel[] streamModels);
        MeetingResult UpdateLiveStreamAudioInfo(int streamId, AudioStreamModel[] streamModels);
        MeetingResult AddDisplayWindow(int accountId, int resourceId, IntPtr displayPtr, int aspx,
            int aspy);
        MeetingResult RemoveDisplayWindow(int accountId, int resourceId, IntPtr displayPtr, int aspx,
            int aspy);

        #endregion

        #region 自适应接口

        MeetingResult SetAudioMixRecvBufferNum(
            int audioMaxBufferNum,
            int audioStartVadBufferNum,
            int audioStopVadBufferNum);

        MeetingResult SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate,
            int frameRate);

        MeetingResult SetPublishDoubleVideoStreamStatus(int isEnabled);

        MeetingResult SetAutoAdjustEnableStatus(int isEnabled);

        MeetingResult SetVideoClarity(int accountId, int resourceId, int clarityLevel);

        MeetingResult SetVideoDisplayMode(VideoDisplayMode videoDisplayMode);

        MeetingResult SetCurCpuInfo(int processCpu, int totalCpu);

        #endregion

        #region HostManage相关

        Task<MeetingResult> StartHost(string devModel, string configPath);

        MeetingResult StopHost();

        Task<MeetingResult> ConnectMeetingVDN(int accountId, string accountName, string token);

        MeetingResult DisConnectMeetingVDN();

        Task<MeetingResult> SetAccountInfo(string accountName);

        Task<MeetingResult> ConnectMeetingVDNAfterMeetingInstStarted();

        Task<MeetingResult> MeetingInvite(int meetingId, int[] inviters);


        #endregion

    }
}