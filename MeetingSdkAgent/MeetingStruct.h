#pragma once

#include <string.h>
#include <MeetingManageDefine.h>

typedef void(*PFunc_Callback)(int cmdId, void * pData, int dataLen, Context ctx);

typedef struct _tagAsyncCallResult {
	int m_statusCode;
	char m_message[128];

	_tagAsyncCallResult() {
		m_statusCode = 0;
		memset(m_message, 0, sizeof(m_message));
	}
}AsyncCallResult;


typedef struct _tagStringStruct {
	char string[256];
}StringStruct;

typedef struct _tagLongStringStruct {
	char string[4096];
}LongStringStruct;


typedef struct _tagAttendeeInfo {
	char m_accountName[MEETINGMANAGE_USERNAME_LEN];
	int m_accountId;
}AttendeeInfo;

typedef struct _tagLoginResult {
	AsyncCallResult m_result;
	LoginInfo m_loginInfo;
}LoginResult;


typedef struct _tagCreateMeetingResult {
	AsyncCallResult m_result;
	MeetingInfo m_meetingInfo;
}CreateMeetingResult;

typedef struct _tagJoinMeetingResult {
	int m_statusCode;
	JoinMeetingInfo* m_joinMeetingInfo;
}JoinMeetingResult;

typedef struct _tagGetMeetingListResult {
	AsyncCallResult m_result;
	MeetingInfo * m_meetingList;
	int m_meetingCount;
}GetMeetingListResult;

typedef struct _tagSpeakResult {
	int m_speakReason;
	char m_accountName[MEETINGMANAGE_USERNAME_LEN];
}SpeakResult;


typedef struct _tagUserSpeakResult {
	int m_speakeReason;
	char m_accountName[MEETINGMANAGE_USERNAME_LEN];
	int m_newSpeakerAccountId;
	char m_newSpeakerAccountName[MEETINGMANAGE_USERNAME_LEN];
}UserSpeakResult;

typedef struct _tagPublishStreamResult {
	int m_statusCode;
	int m_streamId;
}PublishStreamResult;

typedef struct _tagUserPublishData {
	int m_accountId;
	char m_accountName[MEETINGMANAGE_USERNAME_LEN];
	int m_syncId;
	int m_resourceId;
	char m_extraInfo[MEETINGMANAGE_EXTRAINFO_LEN];
}UserPublishData;

typedef struct _tagUserUnpublishData {
	int m_resourceId;
	int m_accountId;
	char m_accountName[MEETINGMANAGE_USERNAME_LEN];
}UserUnpublishData;

typedef struct _tagYUVData {
	int m_accountId;
	int m_resourceId;
	char m_yuvBuffer[MEETINGMANAGE_sourceName_len];
	int m_width;
	int m_height;
	int m_orientation;
}YUVData;

typedef struct _tagKickoutUserData
{
	int meetingId;
	char kickedUserId[MEETINGMANAGE_USERID_LEN];
}KickoutUserData;

typedef struct _tagMeetingInvitationSMSData
{
	AsyncCallResult m_result;
	char invitationSMS[MEETINGMANAGE_sourceName_len];
	char yyURL[MEETINGMANAGE_EXTRAINFO_LEN];
}MeetingInvitationSMSData;

typedef struct _tagSpeakerVideoStreamParamData 
{
	int width;
	int height;
}SpeakerVideoStreamParamData;

typedef struct _tagBandWidthData
{
	int upWidth;
	int downWidth;
}BandWidthData;


typedef struct _tagOtherChangeAudioSpeakerStatusData
{
	int accountId;
	int opType;
}OtherChangeAudioSpeakerStatusData;

typedef struct _tagNetStatusResult{
	NetDiagnosticType netStatusType;
	AsyncCallResult m_result;
}NetStatusResult;

typedef struct _tagNetLevelResult {
	int NetLevel;
}NetLevelResult;

typedef struct _tagMeetingPasswordResult {
	char password[MEETINGMANAGE_USERID_LEN];
	char hostId[MEETINGMANAGE_USERID_LEN];
	AsyncCallResult m_result;
}MeetingPasswordResult;


typedef struct _tagMeetingHasPasswordResult {
	int hasPwd;
	char hostId[MEETINGMANAGE_USERID_LEN];
	AsyncCallResult m_result;
}MeetingHasPasswordResult;


typedef struct _tagTransparentMsg {
	int senderAccountId;
	char data[256];
}TransparentMsg;


typedef struct _tagUiTransparentMsg {
	int msgId;
	int toAccountId;
	char data[256];
}UiTransparentMsg;

typedef struct _tagHostOrderDoOpratonResult {
	int OperationType;
}HostOrderDoOpratonResult;

typedef struct _tagExceptionResult {
	SDKExceptionType exceptionType;
	char description[128];
	char extraInfo[MEETINGMANAGE_EXTRAINFO_LEN];
}ExceptionResult;

typedef struct _tagSdkCallbackResult {
	MEETINGMANAGE_SMSDK_CBTYPE type;
	char description[128];
}SdkCallbackResult;


typedef struct _tagDeviceStatusResult {
	DevStatusChangeType type;
	char devName[MEETINGMANAGE_DEVICENAME_LEN];
}DeviceStatusResult;

typedef struct _tagResourceResult {
	int accountId;
	int resourceId;
}ResourceResult;

typedef struct _tagMeetingInvitationResult {
	int inviterAccountId;
	char inviterAccountName[MEETINGMANAGE_USERNAME_LEN];
	int meetingId;
}MeetingInvitationResult;

typedef struct _tagForcedOfflineResult {
	int accountId;
	char token[MEETINGMANAGE_TOKEN_LEN];
}ForcedOfflineResult;





//回调类型定义
enum CallbackType
{
	start,
	login,
	bind_token,
	check_meeting_exist,
	get_meeting_list,
	get_meeting_info,
	reset_meeting_password,
	get_meeting_password,
	check_meeting_has_password,
	check_meeting_password_valid,
	create_meeting,
	join_meeting,
	get_user_list,
	user_join_event,
	user_leave_event,
	ask_for_speak,
	start_speak_event,
	user_start_speak_event,
	ask_for_stop_speak,
	stop_speak_event,
	user_stop_speak_event,
	send_ui_msg,

	publish_camera_video,
	publish_win_capture_video,
	publish_data_card_video,
	publish_mic_audio,

	unpublish_camera_video,
	unpublish_win_capture_video,
	unpublish_data_card_video,
	unpublish_mic_audio,

	user_publish_camera_video_event,
	user_publish_data_video_event,
	user_publish_mic_audio_event,

	user_unpublish_camera_video_event,
	user_unpublish_data_card_video_event,
	user_unpublish_mic_audio_event,

	yuv_data,
	host_change_meeting_mode,
	host_change_meeting_mode_event,
	host_kickout_user,
	host_kickout_user_event,
	raise_hand_request,
	raise_hand_request_event,
	ask_for_meeting_lock,
	host_order_one_speak,
	host_order_one_stop_speak,
	lock_status_changed_event,
	meeting_manage_exception_event,
	device_status_event,
	net_diagnostic_check,
	play_video_test,
	play_sound_test,
	transparent_msg_event,
	receive_ui_msg_event,
	mic_send_response,
	network_status_level_notice_event,
	device_lost_notice_event,
	meeting_sdk_callback,
	send_audio_speaker_status,
	get_meeting_invitation_sms,
	host_order_one_do_opration,
	host_order_do_opraton_event,
	other_change_audio_speaker_status_event,
	modify_nick_name,
	modify_meeting_inviters,

	start_host,
	connect_meeting_vdn,
	connect_meeting_vdn_after_instance_started,
	set_account_info,
	meeting_invite,
	meeting_invite_event,
	contact_recommend_event,
	force_offline_event,
};