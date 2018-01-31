using System;
using System.Runtime.InteropServices;

namespace MeetingSdk.NetAgent
{
    internal static class MeetingSdkAgent
    {
        const string DllName = @"MeetingSdkAgent.dll";

        #region 设置，启动及鉴权接口

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetCallback(PFuncCallBack pFuncCallBack);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetNpsUrl(IntPtr npmUrlList, int urlSize);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetRkPath(string rkPath, int pathLen);

        /// <summary>
        /// 回调名称OnStart[Self]
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Start(string devModel, string configPath, int pathLen, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Stop();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Login(string nube, int nubeLen, string pass, int passLen, string deviceType, int dtLen, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int LoginThirdParty(string nube, string appkey, string uid, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int LoginViaImei(string imei, int imeiLen, IntPtr ctx);

        /// <summary>
        /// 回调名称OnBindToken(Self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int BindToken(string token, int tokenLen,
            int accountId, string accountName, int accountNameLen, IntPtr ctx);

        #endregion

        #region 获取会议相关信息，音视频设备信息接口

        /// <summary>
        /// 回调名称OnCheckMeetExist(Self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int IsMeetingExist(int meetingId, IntPtr ctx);

        /// <summary>
        /// 回调名称OnGetMeetingList(Self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetMeetingList(IntPtr ctx);

        /// <summary>
        /// 回调名称OnGetMeetingInfo(Self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetMeetingInfo(int meetId, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetJoinMeetingInfo(int meetingId, IntPtr joinMeetingInfo);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetSpeakerList(IntPtr speakerInfos, int maxCount);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetSpeakerInfo(int accountId, IntPtr speakerInfo);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetCurMeetingMode();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetMeetingLockStatus();

        /// <summary>
        /// 回调名称OnGetMeetingPassword(self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetMeetingPassword(int meetingid);


        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetUserPublishStreamInfo(int accountId,
            IntPtr streamsInfo, int maxCount);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetCurrentSubscribleStreamInfo(
            IntPtr streamsInfo, int maxCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="resourceID"></param>
        /// <param name="data">SpeakerVideoStreamParamData</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetSpeakerVideoStreamParam(int accountId, int resourceID, IntPtr data);


        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetParticipants(IntPtr participants, int maxCount);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetParticipantsByPage(IntPtr participants, int pageNum, int countPerPage);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetMeetingInvitationSMS(int meetId, int inviterPhoneId, string inviterName, int inviterNameLen,
            int meetingType, string app, int appLen, int urlType, IntPtr context);

        /// <summary>
        /// 获取Qos数据
        /// </summary>
        /// <param name="outdata">StringStruct</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetMeetingQos(IntPtr outdata);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetMicSendList(IntPtr participants, int maxCount);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetVideoDeviceList(IntPtr devInfo, int maxCount);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetAudioCaptureDeviceList(IntPtr devicelist, int listsize);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetAudioRenderDeviceList(IntPtr devicelist, int listsize);


        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetSerialNo(IntPtr imei);

        #endregion

        #region 音视频，网络测试接口

        /// <summary>
        /// 回调名称OnPlayVideoTest(Self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AsynPlayVideoTest(int colorsps, int fps, int width, int height, IntPtr previewWindow, string videoCapName);

        /// <summary>
        /// 同步接口
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void StopVideoTest();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
            int height, string videoCapName, FUN_VIDEO_PREVIEW funVideoPreviewCallback);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopVideoTestYUVCB();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AsynPlaySoundTest(string wavFile, string renderName);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void StopPlaySoundTest();

        /// <summary>
        /// 同步接口
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RecordSoundTest(string micName, string wavFile);

        /// <summary>
        /// 同步接口
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void StopRecordSoundTest();

        /// <summary>
        /// 回调名称OnNetworkStatusLevelNoticeEvent(self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AsynStartNetDiagnosticSerialCheck();

        /// <summary>
        /// 同步接口
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopNetBandDetect();

        /// <summary>
        /// 同步接口
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetNetBandDetectResult(IntPtr bandWidthDataPtr);

        #endregion

        #region 会议相关接口

        /// <summary>
        /// 回调名称OnResetMeetingPassword（self）
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ResetMeetingPassword(int meetingid, string encode);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CheckMeetingHasPassword(int meetingid);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CheckMeetingPasswordValid(int meetingid, string encryptcode);

        /// <summary>
        /// 回调名称OnGetMeetingPassword(self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CreateMeeting(string appType, int typeLen, IntPtr ctx);

        /// <summary>
        /// 回调名称OnGetMeetingPassword(self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CreateAndInviteMeeting(string appType, int typeLen, IntPtr inviteeList, int inviteeCount, IntPtr context);

        /// <summary>
        /// 回调名称OnGetMeetingPassword(self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CreateDatedMeeting(string appType, int typeLen, uint year,
            uint month, uint day, uint hour,
            uint minute, uint second, string endtime,
            string topic, string encryptcode);

        /// <summary>
        /// 回调名称OnGetMeetingPassword(self)
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CreateAndInviteDatedMeeting(string appType, int typeLen, uint year,
            uint month, uint day, uint hour,
            uint minute, uint second, string endtime,
        string topic, IntPtr inviteeList, int inviteeCount, string encryptcode);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ModifyMeetingInviters(int meetId, string appType, int smsType, IntPtr accountList, int accountNum, IntPtr context);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int JoinMeeting(int meetingId, bool autoSpeak, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int LeaveMeeting();

        #endregion

        #region 音视频发布和订阅

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GenericSyncId();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PublishCameraVideo(MEETINGMANAGE_PublishCameraParam param, bool isNeedCallBackMedia, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam param, bool isNeedCallBackMedia, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam param,
            bool isNeedCallBackMedia, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PublishMicAudio(MEETINGMANAGE_publishMicParam param, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UnpublishCameraVideo(int resourceId, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UnpublishDataCardVideo(int resourceId, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UnpublishWinCaptureVideo(int resourceId, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UnpublishMicAudio(int resourceId, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SubscribeVideo(MEETINGMANAGE_subscribeVideoParam param, bool isNeedCallBackMedia);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SubscribeAudio(MEETINGMANAGE_subscribeAudioParam param);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Unsubscribe(int accountId, int resourceId);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PushMediaFrameData(int resourceId,
            MEETINGMANAGE_FrameType frameType, string frameData, int dataLen, int orientation);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StartYUVDataCallBack(int accountId, int resourceId);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopYUVDataCallBack(int accountId, int resourceId);

        [Obsolete]
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StartLocalVideoRender(int resourceId, IntPtr displayWindow, int aspx, int aspy);

        [Obsolete]
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopLocalVideoRender(int resourceId);

        [Obsolete]
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StartRemoteVideoRender(int accountId, int resourceId, IntPtr displayWindow, int aspx, int aspy);

        [Obsolete]
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopRemoteVideoRender(int accountId, int resourceId);

        #endregion

        #region 会议中相关操作

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AskForSpeak(string speakerId, int speakerIdLen, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AskForStopSpeak(IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SendUiTransparentMsg(int destAccount, IntPtr data);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AsynSendUIMsg(int msgId, int dstUserAccount, string msgData);


        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AsynMicSendReq(int beSpeakedUserId);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int HostChangeMeetingMode(int toMode);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int HostKickoutUser(int accountId);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RaiseHandReq();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AskForMeetingLock(bool bToLock);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int HostOrderOneSpeak(string toAccountId, int toLen,
            string kickAccountId, int kickLen);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int HostOrderOneStopSpeak(string toAccountId, int toLen);

        /// <summary>
        /// 发送本地扬声器状态
        /// </summary>
        /// <param name="isOpen"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SendAudioSpeakerStatus(int isOpen, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int HostOrderOneDoOpration(int toUserId, int oprateType, IntPtr ctx);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ModifyNickName(string accountName, int nameLen, IntPtr ctx);

        #endregion

        #region 录制，推流和双屏渲染

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopMp4Record(int streamID);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StartMp4Record(int streamID, string filepath);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam param);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UnpublishLiveStream(int streamID);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StartLiveRecord(int streamID, string url);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopLiveRecord(int streamID);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UpdateLiveStreamVideoInfo(int streamID, IntPtr streamInfosPtr, int streamnum);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UpdateLiveStreamAudioInfo(int streamID, IntPtr streamInfosPtr, int streamnum);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AddDisplayWindow(int accountId, int resourceId, IntPtr displayWindow, int aspx = 0, int aspy = 0);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RemoveDisplayWindow(int accountId, int resourceId, IntPtr displayWindow, int aspx = 0, int aspy = 0);

        #endregion

        #region 自适应接口

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetAudioMixRecvBufferNum(int audioMaxBufferNum, int audioStartVadBufferNum, int audioStopVadBufferNum);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate, int frameRate);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetPublishDoubleVideoStreamStatus(int isEnabled);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetAutoAdjustEnableStatus(int isEnabled);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetVideoClarity(int accountId, int resourceId, int clarityLevel);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetVideoDisplayMode(int videoDisplayMode);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetCurCpuInfo(int processCpu, int totalCpu);

        #endregion


        #region HostManage相关
        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StartHost(string devmodel, string configPath,int pathLen, IntPtr context);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int StopHost();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ConnectMeetingVDN(int accountId, string accountName,int nameLen, string token, int tokenLen, IntPtr context);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetAccountInfo(string accountName, int nameLen);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ConnectMeetingVDNAfterMeetingInstStarted(IntPtr context);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DisConnectMeetingVDN();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int MeetingInvite(int meetingId, IntPtr accountIdList,
            int accountSize);


        #endregion
    }
}
