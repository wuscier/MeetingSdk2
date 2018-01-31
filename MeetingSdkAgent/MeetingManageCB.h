#pragma once

#include "MeetingManageCallBack.h"
#include "MeetingStruct.h"

typedef void(*FUN_CALLBACK)(char* cbType, void* sType);

class MeetingManageCB : public IMeetingManageCB
{
private:
	PFunc_Callback m_cb;

public:
	enum CallbackType
	{

	};

	MeetingManageCB(PFunc_Callback cb);
	~MeetingManageCB();
	void SetCallback(PFunc_Callback cb);
	void OnStart(int statusCode, char * description, int descLen, Context context);
	void OnLogin(int statusCode, char * description, int descLen, LoginInfo info, Context context);
	void OnBindToken(int statusCode, char * description, int descLen, Context context);
	void OnCheckMeetExist(int statusCode, char * description, int descLen, Context context);
	void OnGetMeetingList(int statusCode, char * description, int descLen, MeetingInfo * meetList, int meetCount, Context context);
	void OnResetMeetingPassword(int statusCode, char * description, int descLen);
	void OnGetMeetingPassword(int statusCode, char * description, int descLen, char * password, int pwdLen, char * hostId, int hostIdLen);
	void OnCheckMeetingHasPassword(int statusCode, char * description, int descLen, int hasPassword, char * hostId, int hostIdLen);
	void OnCheckMeetingPasswordValid(int statusCode, char * description, int descLen);
	void OnCreateMeeting(int statusCode, int meetingId, MM_MeetingType meetType, Context context);
	void OnJoinMeeting(int statusCode, JoinMeetingInfo * meetingInfo, Context context);
	void OnGetUserList(ParticipantInfo * participants, int userCount);
	void OnUserJoinEvent(int accountId, char * accountName, int accoundNameLen);
	void OnUserLeaveEvent(int accountId, char * accountName, int accountNameLen);
	void OnAskForSpeak(int statusCode, Context context);
	void OnStartSpeakEvent(int speakReason, char * accountName, int accountNameLen);
	void OnUserStartSpeakEvent(int speakReason, char * accountName, int accountNameLen, char * newSpeakerAcountName, int newSpeakerAccountId);
	void OnAskForStopSpeak(int statusCode, Context context);
	void OnStopSpeakEvent(int reason, char * accountName, int accountNameLen, int accountNameID);
	void OnUserStopSpeakEvent(int reason, char * accountName, int accountNameLen, char * speakerAcountName, int speakerAccountNameLen, int speakerAccountId);

	void OnPublishCameraVideo(int statusCode, int resourceId, Context context);
	void OnPublishWinCaptureVideo(int statusCode, int resourceId, Context context);
	void OnPublishDataCardVideo(int statusCode, int resourceId, Context context);
	void OnPublishMicAudio(int statusCode, int resourceId, Context context);

	void OnUnPublishCameraVideo(int statusCode, Context context);
	void OnUnPublishWinCaptureVideo(int statusCode, Context context);
	void OnUnPublishDataCardVideo(int statusCode, Context context);
	void OnUnPublishMicAudio(int statusCode, Context context);
	
#pragma region ÏûÏ¢
	
	void OnUserPublishCameraVideoEvent(int resourceId, int syncId, int accountId, char * accountName, int accountNameLen, char * extraInfo, int extraInfoLen);
	void OnUserPublishDataVideoEvent(int resourceId, int syncId, int accountId, char * accountName, int accountNameLen, char * extraInfo, int extraInfoLen);
	void OnUserPublishMicAudioEvent(int resourceId, int syncId, int accountId, char * accountName, int accoundNameLen, char * extraInfo, int extraInfoLen);

	void OnUserUnPublishCameraVideoEvent(int resourceId, int accountId, char * accountName, int accountNameLen);
	void OnUserUnPublishDataCardVideoEvent(int resourceId, int accountId, char * accountName, int accountNameLen);
	void OnUserUnPublishMicAudioEvent(int resourceId, int accountId, char * accountName, int accountNameLen);

#pragma endregion

	void OnYUVData(int accountId, int resourceId, char * yuvBuffer, int yuvBufferSize, int width, int height, int orientation);
	void OnHostChangeMeetingMode(int statusCode, char * description, int descLen);
	void OnHostChangeMeetingModeEvent(int meetingStyle);
	void OnHostKickoutUser(int statusCode, char * description, int descLen);
	void OnHostKickoutUserEvent(int meetId, char * kickedUserId, int idLen);
	void OnRaiseHandReq(int statusCode, char * description, int descLen);
	void OnRaiseHandReqEvent(int accountId, char * accountName, int nameLen);
	void OnAskForMeetingLock(int statusCode, char * description, int descLen);
	void OnHostOrderOneSpeak(int statusCode, char * description, int descLen);
	void OnHostOrderOneStopSpeak(int statusCode, char * description, int descLen);
	void OnLockStatusChangedEvent(int statusCode, char * description, int descLen);
	void OnMeetingManageExecptionEvent(SDKExceptionType exceptionType, char * exceptionDesc, int descLen, char * extraInfo, int infoLen);
	void OnDeviceStatusEvent(DevStatusChangeType type, char * devName, int nameLen);
	void OnNetDiagnosticCheck(NetDiagnosticType type, int statusCode, char * description, int descLen);
	void OnPlayVideoTest(int statusCode, char * description, int descLen);
	void OnPlaySoundTest(int statusCode, char * description, int descLen);
	void OnTransparentMsgEvent(int accountId, char * data, int dataLen);
	void OnMicSendResponse(int statusCode);
	void OnNetworkStatusLevelNoticeEvent(int netlevel);
	void OnDeviceLostNoticeEvent(int accountid, int resourceid);
	void OnMeetingSDKCallback(MEETINGMANAGE_SMSDK_CBTYPE e, char * description, int descLen);
	void OnModifyNickName(int statusCode,
		const char * desc, int descLen, Context context);
	void OnGetMeetingInfo(int statusCode, char * description,
		int descLen, MeetingInfo &meetInfo, Context context);
	void OnModifyMeetingInviters(int statusCode, char * description,
		int descLen, Context context);
	void OnHostOrderOneDoOpration(int statusCode, char * description,
		int descLen, Context context);
	void OnHostOrderDoOpratonEvent(int opType);
	void OnOtherChangeAudioSpeakerStatusEvent(int accountId, int opType);
	void OnGetMeetingInvitationSMS(int statusCode, char* description, int descLen,
		char* invitationSMS, int smsLen, char* yyURL, int urlLen, Context context);
	void OnSendASpeakerStatus(int statusCode, char * description,
		int descLen, Context context);
	void OnSubscribrVideo(int accountid, int statusCode);
	void OnSendUIMsgRespone(int status);
	void OnRecvUImsgEvent(int msgid, int srcUserid, char* msg, int msgLen);
	void Custom();

private:
	void cb(int cmdId, void * pData, int dataLen, Context context);
};
