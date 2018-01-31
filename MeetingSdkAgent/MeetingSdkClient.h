#pragma once

#include "MeetingManageCB.h"
#include "MeetingManage.h"
#include "HostManage.h"
#include "HostManageCB.h"

class MeetingSdkClient
{
private:
	static MeetingSdkClient* m_instance;

	MeetingManageCB* m_callback;
	IMeetingManage* m_manage;

	IHostManage* m_host;
	HostManageCB* m_hostCallback;

	PFunc_Callback m_cb;

protected:
	MeetingSdkClient(void);
	~MeetingSdkClient(void);

public:
	static MeetingSdkClient* Instance();
	static void DestroyInstance();

	void SetCallback(PFunc_Callback cb);

#pragma region MeetingManage
	
	int Start(const char * devmodel, char * configPath, int pathLen, Context context);
	int SetNpsUrl(StringStruct * npsUrlList, int urlSize);
	int Stop();
	int LoginThirdParty(const char* nube, const char* appkey, const char * uid, Context context);
	int Login(const char * nube, int nubeLen, const char * pass, int passLen, const char * deviceType, int dtLen, Context context);
	int LoginViaImei(const char * imei, int imeiLen, Context context);

#pragma region ??????

	int IsMeetingExist(int meetingId, Context context);
	int GetMeetingList(Context context);
	int GetMeetingInfo(int meetId, Context context);

#pragma endregion

	int GetAudioCaptureDeviceList(StringStruct* devicelist, int listsize);
	int GetAudioRenderDeviceList(StringStruct* devicelist, int listsize);
	int GetVideoDeviceList(MEETINGMANAGE_VideoDeviceInfo *devInfo, int maxCount);
	int CreateMeeting(char * appType, int typeLen, Context context);
	int JoinMeeting(int meetingId, bool autoSpeak, Context context);

	int GetJoinMeetingInfo(int meetingId, JoinMeetingInfo * joinMeetingInfo);

	int GenericSyncId();

	int LeaveMeeting();
	int PublishCameraVideo(MEETINGMANAGE_PublishCameraParam param,
		bool isNeedCallBackMedia, Context context);
	int PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam param,
		bool isNeedCallBackMedia, Context context);

	int PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam param,
		bool isNeedCallBackMedia, Context context);

	int PublishMicAudio(MEETINGMANAGE_publishMicParam param,
		Context context);

	int UnpublishCameraVideo(int resourceId, Context context);

	int UnpublishDataCardVideo(int resourceId, Context context);

	int UnpublishWinCaptureVideo(int resourceId, Context context);

	int UnpublishMicAudio(int resourceId, Context context);


	int	SubscribeVideo(MEETINGMANAGE_subscribeVideoParam param,
		bool isNeedCallBackMedia);

	int SubscribeAudio(MEETINGMANAGE_subscribeAudioParam param);

	int	Unsubscribe(int accountId, int resourceId);

	int AskForSpeak(char * speakerId, int speakerIdLen, Context context);

	int AskForStopSpeak(Context context);
	
	int GetSpeakerList(MeetingSpeakerInfo * speakerInfos, int maxCount);
		
	int GetSpeakerInfo(int accountId, MeetingSpeakerInfo * speakerInfo);

	int GetMeetingQos(LongStringStruct * outdata);

	int StartLocalVideoRender(int resourceId, void* displayWindow, int aspx, int aspy);
	
	int StopLocalVideoRender(int resourceId);
	
	int StartRemoteVideoRender(int accountId, int resourceId, void* displayWindow, int aspx, int aspy);

	int StopRemoteVideoRender(int accountId, int resourceId);

	int StartYUVDataCallBack(int accountId, int resourceId);

	int StopYUVDataCallBack(int accountId, int resourceId);

	int SendUiTransparentMsg(int destAccount, StringStruct * data);

	int AsynSendUIMsg(int msgId, int dstUserAccount, const char *msgData);

	int HostChangeMeetingMode(int toMode);

	int GetCurMeetingMode();

	int HostKickoutUser(int accountId);

	int RaiseHandReq();

	int AskForMeetingLock(bool bToLock); 
	
	bool GetMeetingLockStatus();

	int GetParticipants(ParticipantInfo* participants, int maxCount);

	int SendAudioSpeakerStatus(int isOpen, Context context);

	int GetMeetingInvitationSMS(int meetId, int inviterPhoneId, const char* inviterName, int inviterNameLen, int meetingType, const char* app, int appLen, int urlType, Context context);

	int GetSpeakerVideoStreamParam(int accountId, int resourceID, SpeakerVideoStreamParamData * data);

	int HostOrderOneDoOpration(int toUserId, int oprateType, Context context);

	int ModifyNickName(const char * accountName, int nameLen, Context context);

	int BindToken(const char* token, int tokenLen,
		int accountId, const char* accountName,
		int accountNameLen, Context context);

	int AsynPlayVideoTest(int colorsps, int fps,
		int width, int height, void * previewWindow, char videoCapName[256]);

	void StopVideoTest();

	int AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
		int height, char videoCapName[256], FUN_VIDEO_PREVIEW fun);

	int StopVideoTestYUVCB();

	int AsynPlaySoundTest(char wavFile[256], char renderName[256]);

	void StopPlaySoundTest();

	int RecordSoundTest(char micName[256], char wavFile[256]); 

	void StopRecordSoundTest();

	int AsynStartNetDiagnosticSerialCheck();

	int StopNetBandDetect();

	int GetNetBandDetectResult(BandWidthData* bandWidthData);

	int ResetMeetingPassword(int meetingid, const char* encode);

	int GetMeetingPassword(int meetingid);

	int CheckMeetingHasPassword(int meetingid);

	int CheckMeetingPasswordValid(int meetingid,
		const char* encryptcode);

	int CreateAndInviteMeeting(char * appType, int typeLen,
		int * inviteeList, int inviteeCount, Context context);


	int CreateDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour,
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, const char * encryptcode);

	int CreateAndInviteDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour,
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, int * inviteeList, int inviteeCount, const char * encryptcode);

	int ModifyMeetingInviters(int meetId, const char * appType, int smsType, int *accountList, int accountNum, Context context);

	int SetAudioMixRecvBufferNum(int AudioMaxBufferNum, int AudioStartVadBufferNum, int AudioStopVadBufferNum);
	int HostOrderOneSpeak(char * toAccountId, int toLen,
		char * kickAccountId, int kickLen);

	int HostOrderOneStopSpeak(char * toAccountId, int toLen);

	int GetUserPublishStreamInfo(int accountId,
		MeetingUserStreamInfo * streamsInfo, int maxCount);

	int GetCurrentSubscribleStreamInfo(
		MeetingUserStreamInfo * streamsInfo, int maxCount);


	int GetParticipants(ParticipantInfo * participants,
		int pageNum, int countPerPage);

	int PushMediaFrameData(int resourceId,
		MEETINGMANAGE_FrameType frameType, char * frameData, int dataLen, int orientation);


	int SetCurCpuInfo(int processCpu, int totalCpu);

	int SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate, int frameRate);

	int GetMicSendList(ParticipantInfo* participants, int maxCount);

	int AsynMicSendReq(int beSpeakedUserId);

	int AddDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx = 0, int aspy = 0);

	int RemoveDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx = 0, int aspy = 0);

	int PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam param);

	int UnpublishLiveStream(int streamID);

	int SetPublishDoubleVideoStreamStatus(int isEnabled);

	int  SetAutoAdjustEnableStatus(int isEnabled);

	int StartLiveRecord(int streamID, char *url);

	int StopLiveRecord(int streamID);

	int UpdateLiveStreamVideoInfo(int streamID, MEETINGMANAGE_VideoStreamInfo* streamInfo, int streamnum);

	int UpdateLiveStreamAudioInfo(int streamID, MEETINGMANAGE_AudioStreamInfo *streamInfo, int streamnum);

	int SetVideoClarity(int accountId, int resourceId, int clarityLevel);

	int SetVideoDisplayMode(int videoDisplayMode);

	int StopMp4Record(int streamID);

	int StartMp4Record(int streamID, char *filepath);

	int SetRkPath(const char* rkPath, int pathLen);


	int StartHost(const char * devmodel, char * configPath,
		int pathLen, Context context);

	int StopHost();

	int ConnectMeetingVDN(int accountId, char * accountName,
		int nameLen, char * token, int tokenLen, Context context);

	int SetAccountInfo(const char * accountName, int nameLen);

	int ConnectMeetingVDNAfterMeetingInstStarted(Context context);
	int DisConnectMeetingVDN();

	int MeetingInvite(int meetingId, int * accountIdList,
		int accountSize);



#pragma endregion

};
