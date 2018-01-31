#pragma once

#ifdef MEETINGSDK_EXPORTS
#define MEETINGSDKAGENT_API __declspec(dllexport)
#else
#define MEETINGSDKAGENT_API __declspec(dllimport)
#endif // MEETINGSDK_EXPORTS

#include "test.h"
//#include <string>

extern "C"
{
	MEETINGSDKAGENT_API int GetDeviceNum();

	MEETINGSDKAGENT_API int GetDeviceNum2();

	MEETINGSDKAGENT_API int GetSerialNo(StringStruct* imei);

	MEETINGSDKAGENT_API void CallCB(ManageCB * cb);

	MEETINGSDKAGENT_API void SetCallback(PFunc_Callback pFunc_Callback);

	MEETINGSDKAGENT_API void Destroy();

	MEETINGSDKAGENT_API int Start(const char * devmodel, char * configPath, int pathLen, Context context);

	MEETINGSDKAGENT_API int SetNpsUrl(StringStruct * npsUrlList, int urlSize);

	MEETINGSDKAGENT_API int Stop();

	MEETINGSDKAGENT_API int Login(const char * nube, int nubeLen, const char * pass, int passLen, const char * deviceType, int dtLen, Context context);

	MEETINGSDKAGENT_API int LoginThirdParty(const char* nube, const char* appkey, const char * uid, Context context);

	MEETINGSDKAGENT_API int LoginViaImei(const char * imei, int imeiLen, Context context);

	MEETINGSDKAGENT_API int IsMeetingExist(int meetingId, Context context);

	MEETINGSDKAGENT_API int GetMeetingList(Context context);

	MEETINGSDKAGENT_API int GetMeetingInfo(int meetId, Context context);

	MEETINGSDKAGENT_API int GetVideoDeviceList(MEETINGMANAGE_VideoDeviceInfo *devInfo, int maxCount);

	MEETINGSDKAGENT_API int GetAudioCaptureDeviceList(StringStruct* devicelist, int listsize);

	MEETINGSDKAGENT_API int GetAudioRenderDeviceList(StringStruct* devicelist, int listsize);

	MEETINGSDKAGENT_API int CreateMeeting(char * appType, int typeLen, Context context);

	MEETINGSDKAGENT_API int JoinMeeting(int meetingId, bool autoSpeak, Context context);

	MEETING_MANAGE_API int GetJoinMeetingInfo(int meetingId, JoinMeetingInfo * joinMeetingInfo);

	MEETING_MANAGE_API int GenericSyncId();

	MEETINGSDKAGENT_API int LeaveMeeting();

	MEETINGSDKAGENT_API int PublishCameraVideo(MEETINGMANAGE_PublishCameraParam param,
		bool isNeedCallBackMedia, Context context);

	MEETINGSDKAGENT_API int PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam param,
		bool isNeedCallBackMedia, Context context);

	MEETINGSDKAGENT_API int PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam param,
		bool isNeedCallBackMedia, Context context);

	MEETINGSDKAGENT_API int PublishMicAudio(MEETINGMANAGE_publishMicParam param,
		Context context);

	MEETINGSDKAGENT_API int UnpublishCameraVideo(int resourceId, Context context);

	MEETINGSDKAGENT_API int UnpublishDataCardVideo(int resourceId, Context context);

	MEETINGSDKAGENT_API int UnpublishWinCaptureVideo(int resourceId, Context context);

	MEETINGSDKAGENT_API int UnpublishMicAudio(int resourceId, Context context);

	MEETINGSDKAGENT_API int	SubscribeVideo(MEETINGMANAGE_subscribeVideoParam param,
		bool isNeedCallBackMedia);

	MEETINGSDKAGENT_API int SubscribeAudio(MEETINGMANAGE_subscribeAudioParam param);

	MEETINGSDKAGENT_API int	Unsubscribe(int accountId, int resourceId);

	MEETINGSDKAGENT_API int AskForSpeak(char * speakerId, int speakerIdLen, Context context);

	MEETINGSDKAGENT_API int AskForStopSpeak(Context context);

	MEETINGSDKAGENT_API int GetSpeakerList(MeetingSpeakerInfo * speakerInfos, int maxCount);

	MEETINGSDKAGENT_API int GetSpeakerInfo(int accountId, MeetingSpeakerInfo * speakerInfo);

	MEETINGSDKAGENT_API int GetMeetingQos(LongStringStruct * outdata);

	MEETINGSDKAGENT_API int StartLocalVideoRender(int resourceId, void* displayWindow, int aspx, int aspy);

	MEETINGSDKAGENT_API int StopLocalVideoRender(int resourceId);

	MEETINGSDKAGENT_API int StartRemoteVideoRender(int accountId, int resourceId, void* displayWindow, int aspx, int aspy);

	MEETINGSDKAGENT_API int StopRemoteVideoRender(int accountId, int resourceId);

	MEETINGSDKAGENT_API int StartYUVDataCallBack(int accountId, int resourceId);

	MEETINGSDKAGENT_API int StopYUVDataCallBack(int accountId, int resourceId);

	MEETINGSDKAGENT_API int SendUiTransparentMsg(int destAccount, StringStruct * data);

	MEETINGSDKAGENT_API int AsynSendUIMsg(int msgId, int dstUserAccount, const char *msgData);

	MEETINGSDKAGENT_API int HostChangeMeetingMode(int toMode);

	MEETINGSDKAGENT_API int GetCurMeetingMode();

	MEETINGSDKAGENT_API int HostKickoutUser(int accountId);

	MEETINGSDKAGENT_API int RaiseHandReq();

	MEETINGSDKAGENT_API int AskForMeetingLock(bool bToLock);

	MEETINGSDKAGENT_API bool GetMeetingLockStatus();

	MEETINGSDKAGENT_API int GetParticipants(ParticipantInfo* participants, int maxCount);

	MEETINGSDKAGENT_API int SendAudioSpeakerStatus(int isOpen, Context context);

	MEETINGSDKAGENT_API int GetMeetingInvitationSMS(int meetId, int inviterPhoneId, const char* inviterName, int inviterNameLen, int meetingType, const char* app, int appLen, int urlType, Context context);

	MEETINGSDKAGENT_API int GetSpeakerVideoStreamParam(int accountId, int resourceID, SpeakerVideoStreamParamData * data);

	MEETINGSDKAGENT_API int HostOrderOneDoOpration(int toUserId, int oprateType, Context context);

	MEETINGSDKAGENT_API int ModifyNickName(const char * accountName, int nameLen, Context context);

	MEETINGSDKAGENT_API int BindToken(const char* token, int tokenLen,
		int accountId, const char* accountName,
		int accountNameLen, Context context);

	MEETINGSDKAGENT_API int AsynPlayVideoTest(int colorsps, int fps,
		int width, int height, void * previewWindow, char videoCapName[256]);

	MEETINGSDKAGENT_API void StopVideoTest();

	MEETINGSDKAGENT_API int AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
		int height, char videoCapName[256], FUN_VIDEO_PREVIEW fun);

	MEETINGSDKAGENT_API int StopVideoTestYUVCB();

	MEETINGSDKAGENT_API int AsynPlaySoundTest(char wavFile[256], char renderName[256]);

	MEETINGSDKAGENT_API void StopPlaySoundTest();

	MEETINGSDKAGENT_API int RecordSoundTest(char micName[256], char wavFile[256]); 

	MEETINGSDKAGENT_API void StopRecordSoundTest();

	MEETINGSDKAGENT_API int AsynStartNetDiagnosticSerialCheck();

	MEETINGSDKAGENT_API int StopNetBandDetect();

	MEETINGSDKAGENT_API int GetNetBandDetectResult(BandWidthData * bandWidthData);

	MEETINGSDKAGENT_API int ResetMeetingPassword(int meetingid, const char* encode = NULL);

	MEETINGSDKAGENT_API int GetMeetingPassword(int meetingid);

	MEETINGSDKAGENT_API int CheckMeetingHasPassword(int meetingid);

	MEETINGSDKAGENT_API int CheckMeetingPasswordValid(int meetingid,
		const char* encryptcode);

	MEETINGSDKAGENT_API int CreateAndInviteMeeting(char * appType, int typeLen,
		int * inviteeList, int inviteeCount, Context context);

	MEETINGSDKAGENT_API int CreateDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour,
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, const char * encryptcode);

	MEETINGSDKAGENT_API int CreateAndInviteDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour,
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, int * inviteeList, int inviteeCount, const char * encryptcode);

	MEETINGSDKAGENT_API int ModifyMeetingInviters(int meetId, const char * appType, int smsType, int *accountList, int accountNum, Context context);

	MEETINGSDKAGENT_API int HostOrderOneSpeak(char * toAccountId, int toLen,
		char * kickAccountId, int kickLen);
	MEETINGSDKAGENT_API int SetAudioMixRecvBufferNum(int AudioMaxBufferNum, int AudioStartVadBufferNum, int AudioStopVadBufferNum);


	MEETINGSDKAGENT_API int HostOrderOneStopSpeak(char * toAccountId, int toLen);

	MEETINGSDKAGENT_API int GetUserPublishStreamInfo(int accountId,
		MeetingUserStreamInfo * streamsInfo, int maxCount);

	MEETINGSDKAGENT_API int GetCurrentSubscribleStreamInfo(
		MeetingUserStreamInfo * streamsInfo, int maxCount);

	MEETINGSDKAGENT_API int GetParticipantsByPage(ParticipantInfo * participants,
		int pageNum, int countPerPage);

	MEETINGSDKAGENT_API int SetCurCpuInfo(int processCpu, int totalCpu);

	MEETINGSDKAGENT_API int PushMediaFrameData(int resourceId,
		MEETINGMANAGE_FrameType frameType, char * frameData, int dataLen, int orientation);

	MEETINGSDKAGENT_API int GetMicSendList(ParticipantInfo* participants, int maxCount);

	MEETINGSDKAGENT_API int SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate, int frameRate);

	MEETINGSDKAGENT_API int AsynMicSendReq(int beSpeakedUserId);

	MEETINGSDKAGENT_API int AddDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx = 0, int aspy = 0);


	MEETINGSDKAGENT_API int RemoveDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx = 0, int aspy = 0);

	MEETINGSDKAGENT_API int PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam param);

	MEETINGSDKAGENT_API int UnpublishLiveStream(int streamID);

	MEETINGSDKAGENT_API int StartLiveRecord(int streamID, char *url);

	MEETINGSDKAGENT_API int StopLiveRecord(int streamID);

	MEETINGSDKAGENT_API int UpdateLiveStreamVideoInfo(int streamID, MEETINGMANAGE_VideoStreamInfo* streamInfo, int streamnum);

	MEETINGSDKAGENT_API int UpdateLiveStreamAudioInfo(int streamID, MEETINGMANAGE_AudioStreamInfo *streamInfo, int streamnum);


	MEETINGSDKAGENT_API int  SetPublishDoubleVideoStreamStatus(int isEnabled);

	MEETINGSDKAGENT_API int  SetAutoAdjustEnableStatus(int isEnabled);

	MEETINGSDKAGENT_API int SetVideoClarity(int accountId, int resourceId, int clarityLevel);

	MEETINGSDKAGENT_API int SetVideoDisplayMode(int videoDisplayMode);

	MEETINGSDKAGENT_API int StopMp4Record(int streamID);

	MEETINGSDKAGENT_API int StartMp4Record(int streamID, char *filepath);

	MEETINGSDKAGENT_API int SetRkPath(const char* rkPath, int pathLen);


	MEETINGSDKAGENT_API int StartHost(const char * devmodel, char * configPath,
		int pathLen, Context context);

	MEETINGSDKAGENT_API int StopHost();

	MEETINGSDKAGENT_API int ConnectMeetingVDN(int accountId, char * accountName,
		int nameLen, char * token, int tokenLen, Context context);

	MEETINGSDKAGENT_API int SetAccountInfo(const char * accountName, int nameLen);

	MEETINGSDKAGENT_API int ConnectMeetingVDNAfterMeetingInstStarted(Context context);
	MEETINGSDKAGENT_API int DisConnectMeetingVDN();

	MEETINGSDKAGENT_API int MeetingInvite(int meetingId, int * accountIdList,
		int accountSize);





}
