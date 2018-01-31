#include "MeetingManageCB.h"
#include "MeetingSdkClient.h"
//#include <iostream>

// ���캯��
MeetingManageCB::MeetingManageCB(PFunc_Callback cb)
{
	m_cb = cb;
	std::cout << "constructor MeetingManageCB" << std::endl;
}

MeetingManageCB::~MeetingManageCB()
{
	std::cout << "destroy MeetingManageCB" << std::endl;
}

void MeetingManageCB::SetCallback(PFunc_Callback cb)
{
	m_cb = cb;
}

/*
*  ģ�������ص�
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  descLen�� ��������
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnStart(int statusCode, char * description, int descLen, Context context)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(start, &startResult, 0, 0);
}

/*
*  ��¼����ص�
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  descLen�� ��������
*  info:	 ��¼��Ϣ
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnLogin(int statusCode, char * description, int descLen, LoginInfo info, Context context)
{
		LoginResult loginResult;
		loginResult.m_result.m_statusCode = statusCode;
		strncpy_s(loginResult.m_result.m_message, description, sizeof(loginResult.m_result.m_message) - 1);
		loginResult.m_loginInfo = info;
		//memcpy(&loginResult.m_loginInfo, &info, sizeof(loginResult.m_loginInfo));
		cb(login, &loginResult, 0, context);
}

/*
*  ��Token���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnBindToken(int statusCode, char * description, int descLen, Context context)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(bind_token, &result, 0, 0);
}

/*
*  ��������Ƿ���ڽ��
*  statusCode: ״̬�룬0 - �������  ��0 - ���鲻���ڣ�������
*  description: ��Ӧ״̬�����ʾ
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnCheckMeetExist(int statusCode, char * description, int descLen, Context context)
{
		AsyncCallResult result;
		result.m_statusCode = statusCode;
		strcpy_s(result.m_message, sizeof(result.m_message), description);
		cb(check_meeting_exist, &result, 0, context);
}

/*
*  ȡ�û����б���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  meetList: �����б�
*  meetCount: �����б��еĻ�����
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnGetMeetingList(int statusCode, char * description, int descLen, MeetingInfo * meetList, int meetCount, Context context)
{
		GetMeetingListResult result;
		result.m_result.m_statusCode = statusCode;
		strcpy_s(result.m_result.m_message, sizeof(result.m_result.m_message), description);

		result.m_meetingCount = meetCount;
		result.m_meetingList = meetList;
		cb(get_meeting_list, &result, 0, context);
}

/*
*	���û���������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  descLen: ��ʾ��Ϣ����
*/
void MeetingManageCB::OnResetMeetingPassword(int statusCode, char * description, int descLen)
{
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
	cb(reset_meeting_password, &startResult, 0, 0);
}

/*
*	��ȡ����������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  descLen: ��ʾ��Ϣ����
*  password: ����
*  pwdLen: ���볤��
*  hostId: ������ID
*  hostIdLen: ID����
*/
void MeetingManageCB::OnGetMeetingPassword(int statusCode, char * description, int descLen, char * password, int pwdLen, char * hostId, int hostIdLen)
{
	MeetingPasswordResult meetingPwdResult;
	meetingPwdResult.m_result.m_statusCode = statusCode;
	strcpy_s(meetingPwdResult.m_result.m_message, sizeof(meetingPwdResult.m_result.m_message), description);
	strcpy_s(meetingPwdResult.password, sizeof(meetingPwdResult.password), password);
	strcpy_s(meetingPwdResult.hostId, sizeof(meetingPwdResult.hostId), hostId);

	cb(get_meeting_password, &meetingPwdResult, 0, 0);
}

/*
*	��ѯ�����Ƿ���������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  descLen: ��ʾ��Ϣ����
*  hasPassword: �Ƿ������� 1��ʾ�����룬������ʾ������
*  hostId: ������ID
*  hostIdLen: ID����
*/
void MeetingManageCB::OnCheckMeetingHasPassword(int statusCode, char * description, int descLen, int hasPassword, char * hostId, int hostIdLen)
{
	MeetingHasPasswordResult meetingHasPwdResult;
	meetingHasPwdResult.m_result.m_statusCode = statusCode;
	strcpy_s(meetingHasPwdResult.m_result.m_message, sizeof(meetingHasPwdResult.m_result.m_message), description);
	meetingHasPwdResult.hasPwd = hasPassword;
	strcpy_s(meetingHasPwdResult.hostId, sizeof(meetingHasPwdResult.hostId), hostId);

	cb(check_meeting_has_password, &meetingHasPwdResult, 0, 0);
}

/*
*	�����������Ƿ���Ч���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ��Ӧ״̬�����ʾ
*  descLen: ��ʾ��Ϣ����
*/
void MeetingManageCB::OnCheckMeetingPasswordValid(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);
	cb(check_meeting_password_valid, &result, 0, 0);
}

/*
*  ����������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  meetingId: �´����Ļ���ID��statusCode Ϊ0ʱ��Ч
*  meetType : ��������
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnCreateMeeting(int statusCode, int meetingId, MM_MeetingType meetType, Context context)
{
		CreateMeetingResult createMeetingResult;

		createMeetingResult.m_result.m_statusCode = statusCode;
		createMeetingResult.m_meetingInfo.m_meetingId = meetingId;
		createMeetingResult.m_meetingInfo.m_meetingType = meetType;

		cb(create_meeting, &createMeetingResult, 0, 0);
}

/*
*  ���������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  meetingInfo: ������Ϣ��statusCode Ϊ0ʱ��Ч
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnJoinMeeting(int statusCode, JoinMeetingInfo * meetingInfo, Context context)
{
		JoinMeetingResult joinMeetingResult;
		joinMeetingResult.m_statusCode = statusCode;
		joinMeetingResult.m_joinMeetingInfo = meetingInfo;
		cb(join_meeting, &joinMeetingResult, 0, 0);
}

/*
*  ȡ�òλ����б�֪ͨ
*  participants: �λ����б�
*  userCount: �λ�����
*/
void MeetingManageCB::OnGetUserList(ParticipantInfo * participants, int userCount)
{

}

/*
*  �����û��������֪ͨ
*  accountId: �¼����û�����Ѷ��
*  accountName: �¼����û����ǳ�
*/
void MeetingManageCB::OnUserJoinEvent(int accountId, char * accountName, int accoundNameLen)
{
		AttendeeInfo attendeeInfo;
		attendeeInfo.m_accountId = accountId;
		strcpy_s(attendeeInfo.m_accountName, sizeof(attendeeInfo.m_accountName), accountName);

		cb(user_join_event, &attendeeInfo, 0, 0);
}

/*
*  �����û��뿪����
*  accountId: �뿪�û�����Ѷ��
*  accountName: �뿪�û����ǳ�
*/
void MeetingManageCB::OnUserLeaveEvent(int accountId, char * accountName, int accountNameLen)
{
		AttendeeInfo attendeeInfo;
		attendeeInfo.m_accountId = accountId;
		strcpy_s(attendeeInfo.m_accountName, sizeof(attendeeInfo.m_accountName), accountName);

		cb(user_leave_event, &attendeeInfo, 0, 0);
}

/*
*  ���뷢�Խ��
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnAskForSpeak(int statusCode, Context context) 
{
		AsyncCallResult asyncCallResult;
		asyncCallResult.m_statusCode = statusCode;

		cb(ask_for_speak, &asyncCallResult, 0, 0);
}

/*
*  ������֪ͨ��ʼ����
*  speakReason: ��ʼ���Ե�ԭ��0��������������,1��ʾ�������ɴ������,2��ʾ��������������ָ�����Բ���
*  accountName: ԭʼ�����˵����ƣ����speakReason��1�����ֶα�ʾ�����ߵ����ƣ����speakReason��2�����ֶα�ʾ�����˵�����
*/
void MeetingManageCB::OnStartSpeakEvent(int speakReason, char * accountName, int accountNameLen)
{
		SpeakResult startSpeakResult;
		startSpeakResult.m_speakReason = speakReason;
		strcpy_s(startSpeakResult.m_accountName, sizeof(startSpeakResult.m_accountName), accountName);

		cb(start_speak_event, &startSpeakResult, 0, 0);
}

/*
*  ������֪ͨ���������߲���
*  speakReason: ��ʼ���Ե�ԭ��0��������������,1��ʾ�������ɴ������,2��ʾ��������������ָ�����Բ���
*  accountName: ԭʼ�����˵����ƣ����speakReason��1�����ֶα�ʾ�����ߵ����ƣ����speakReason��2�����ֶα�ʾ�����˵�����
*  newSpeakerAcountName���·������ǳ�
*  newSpeakerAccountId: �·�������Ѷ��
*/
void MeetingManageCB::OnUserStartSpeakEvent(int speakReason, char * accountName, int accountNameLen, char * newSpeakerAcountName, int newSpeakerAccountId)
{
		UserSpeakResult userStartSpeakResult;
		userStartSpeakResult.m_speakeReason = speakReason;
		userStartSpeakResult.m_newSpeakerAccountId = newSpeakerAccountId;
		strcpy_s(userStartSpeakResult.m_accountName, sizeof(userStartSpeakResult.m_accountName), accountName);
		strcpy_s(userStartSpeakResult.m_newSpeakerAccountName, sizeof(userStartSpeakResult.m_newSpeakerAccountName), newSpeakerAcountName);

		cb(user_start_speak_event, &userStartSpeakResult, 0, 0);
}

/*
*  ����ֹͣ���Խ��
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnAskForStopSpeak(int statusCode, Context context)
{
		AsyncCallResult asyncCallResult;
		asyncCallResult.m_statusCode = statusCode;

		cb(ask_for_stop_speak, &asyncCallResult, 0, 0);
}

/*
*  ������ֹ֪ͨͣ����
*  reason: ֹͣ���Ե�ԭ��0����������ʧЧ���Լ�����ֹͣ���˳�����ȣ���1������ʧЧ�ɴ�����������2������ʧЧ�������˲�������
*  accountName: ԭʼ�����˵����ƣ����reason��1�����ֶα�ʾ�����ߵ����ƣ����reason��2�����ֶα�ʾ�����˵�����
*/
void MeetingManageCB::OnStopSpeakEvent(int reason, char * accountName, int accountNameLen, int accountNameID)
{
		SpeakResult stopSpeakResult;
		stopSpeakResult.m_speakReason = reason;
		strcpy_s(stopSpeakResult.m_accountName, sizeof(stopSpeakResult.m_accountName), accountName);

		cb(stop_speak_event, &stopSpeakResult, 0, 0);
}

/*
*  ������֪ͨ����������ֹͣ����
*  reason: ֹͣ���Ե�ԭ��0����������ʧЧ���Լ�����ֹͣ���˳�����ȣ���1������ʧЧ�ɴ�����������2������ʧЧ�������˲�������
*  accountName: ԭʼ�����˵����ƣ����reason��1�����ֶα�ʾ�����ߵ����ƣ����reason��2�����ֶα�ʾ�����˵�����
*  speakerAcountName: ֹͣ�����û����ǳ�
*  speakerAccountId: ֹͣ�����û�����Ѷ��
*/
void MeetingManageCB::OnUserStopSpeakEvent(int reason, char * accountName, int accountNameLen, char * speakerAcountName, int speakerAccountNameLen, int speakerAccountId)
{
		UserSpeakResult userStopSpeakResult;
		userStopSpeakResult.m_speakeReason = reason;
		userStopSpeakResult.m_newSpeakerAccountId = speakerAccountId;
		strcpy_s(userStopSpeakResult.m_accountName, sizeof(userStopSpeakResult.m_accountName), accountName);
		strcpy_s(userStopSpeakResult.m_newSpeakerAccountName, sizeof(userStopSpeakResult.m_newSpeakerAccountName), speakerAcountName);

		cb(user_stop_speak_event, &userStopSpeakResult, 0, 0);
}

/*
*  ��������ͷ��Ƶ���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnPublishCameraVideo(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_camera_video, &publishStreamResult, 0, context);
}

/*
*  �����û���������ͷ��Ƶ
*  resourceId�� ��ԴID
*  syncId�� ����Ƶͬ��ID
*  accountId�� ��������ͷ�û�����Ѷ��
*  accountName: ��������ͷ�û����ǳ�
*  accountNameLen: �ǳƳ���
*  extraInfo: ��������չ��Ϣ
*  extraInfoLen: ��չ��Ϣ����
*/
void MeetingManageCB::OnUserPublishCameraVideoEvent(int resourceId, int syncId, int accountId, char * accountName, int accountNameLen, char * extraInfo, int extraInfoLen)
{
		UserPublishData userPublishData;
		userPublishData.m_resourceId = resourceId;
		userPublishData.m_syncId = syncId;
		userPublishData.m_accountId = accountId;

		strcpy_s(userPublishData.m_accountName, sizeof(userPublishData.m_accountName), accountName);
		strcpy_s(userPublishData.m_extraInfo, sizeof(userPublishData.m_extraInfo), extraInfo);

		cb(user_publish_camera_video_event, &userPublishData, 0, 0);
}

/*
*  ��������ɼ���Ƶ���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnPublishWinCaptureVideo(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_win_capture_video, &publishStreamResult, 0, context);
}

/*
*  �����ɼ�����Ƶ���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnPublishDataCardVideo(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_data_card_video, &publishStreamResult, 0, context);
}

/*
*  �����û������ĵ���Ƶ���ɼ��� �� ����ɼ��� �ĵ���Ƶ��ý��������һ����ͬʱֻ����һ���Ǵ�״̬
*  resourceId�� ��ԴID
*  syncId�� ����Ƶͬ��ID
*  accountId�� �����ĵ����û�����Ѷ��
*  accountName: �����ĵ����û����ǳ�
*  accountNameLen: �ǳƳ���
*  extraInfo: ��������չ��Ϣ
*  extraInfoLen: ��չ��Ϣ����
*/
void MeetingManageCB::OnUserPublishDataVideoEvent(int resourceId, int syncId, int accountId, char * accountName, int accountNameLen, char * extraInfo, int extraInfoLen)
{
		UserPublishData userPublishData;
		userPublishData.m_resourceId = resourceId;
		userPublishData.m_syncId = syncId;
		userPublishData.m_accountId = accountId;

		strcpy_s(userPublishData.m_accountName, sizeof(userPublishData.m_accountName), accountName);
		strcpy_s(userPublishData.m_extraInfo, sizeof(userPublishData.m_extraInfo), extraInfo);

		cb(user_publish_data_video_event, &userPublishData, 0, 0);
}

/*
*  ������˷���Ƶ���
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnPublishMicAudio(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_mic_audio, &publishStreamResult, 0, context);
}

/*
*  �����û�������˷���Ƶ
*  resourceId�� ��ԴID
*  syncId�� ����Ƶͬ��ID
*  accountId�� ������˷���Ƶ�û�����Ѷ��
*  accountName: ������˷���Ƶ�û����ǳ�
*  accountNameLen: �ǳƳ���
*  extraInfo: ��������չ��Ϣ
*  extraInfoLen: ��չ��Ϣ����
*/
void MeetingManageCB::OnUserPublishMicAudioEvent(int resourceId, int syncId, int accountId, char * accountName, int accoundNameLen, char * extraInfo, int extraInfoLen)
{
		UserPublishData userPublishData;
		userPublishData.m_resourceId = resourceId;
		userPublishData.m_syncId = syncId;
		userPublishData.m_accountId = accountId;

		strcpy_s(userPublishData.m_accountName, sizeof(userPublishData.m_accountName), accountName);
		strcpy_s(userPublishData.m_extraInfo, sizeof(userPublishData.m_extraInfo), extraInfo);

		cb(user_publish_mic_audio_event, &userPublishData, 0, 0);
}

/*
*  ȡ������ͷ��Ƶ�������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnUnPublishCameraVideo(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_camera_video, &publishStreamResult, 0, context);
}

/*
*  ȡ������������������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnUnPublishWinCaptureVideo(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_win_capture_video, &publishStreamResult, 0, context);
}

/*
*  ȡ���ɼ�����Ƶ�������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnUnPublishDataCardVideo(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_data_card_video, &publishStreamResult, 0, context);
}

/*
*  ȡ����Ƶ�������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  context�� �����Ĳ���
*/
void MeetingManageCB::OnUnPublishMicAudio(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_mic_audio, &publishStreamResult, 0, context);
}

/*
*  �����û�ȡ����������ͷ��Ƶ��
*  resourceId�� ��ԴID
*  accountId�� ȡ���������û�����Ѷ��
*  accountName: ȡ���������û����ǳ�
*/
void MeetingManageCB::OnUserUnPublishCameraVideoEvent(int resourceId, int accountId, char * accountName, int accountNameLen)
{
		UserUnpublishData userUnpublishData;
		userUnpublishData.m_resourceId = resourceId;
		userUnpublishData.m_accountId = accountId;
		strcpy_s(userUnpublishData.m_accountName, sizeof(userUnpublishData.m_accountName), accountName);

		cb(user_unpublish_camera_video_event, &userUnpublishData, 0, 0);
}

/*
*  �����û�ȡ�������ĵ���
*  resourceId�� ��ԴID
*  accountId�� ȡ���������û�����Ѷ��
*  accountName: ȡ���������û����ǳ�
*/
void MeetingManageCB::OnUserUnPublishDataCardVideoEvent(int resourceId, int accountId, char * accountName, int accountNameLen)
{
		UserUnpublishData userUnpublishData;
		userUnpublishData.m_resourceId = resourceId;
		userUnpublishData.m_accountId = accountId;
		strcpy_s(userUnpublishData.m_accountName, sizeof(userUnpublishData.m_accountName), accountName);

		cb(user_unpublish_data_card_video_event, &userUnpublishData, 0, 0);
}

/*
*  �����û�ȡ��������Ƶ��
*  resourceId�� ��ԴID
*  accountId�� ȡ���������û�����Ѷ��
*  accountName: ȡ���������û����ǳ�
*/
void MeetingManageCB::OnUserUnPublishMicAudioEvent(int resourceId, int accountId, char * accountName, int accountNameLen)
{
		UserUnpublishData userUnpublishData;
		userUnpublishData.m_resourceId = resourceId;
		userUnpublishData.m_accountId = accountId;
		strcpy_s(userUnpublishData.m_accountName, sizeof(userUnpublishData.m_accountName), accountName);

		cb(user_unpublish_mic_audio_event, &userUnpublishData, 0, 0);
}

/*
*  �ص�YUV����
*  accountId�� YUV���������û�����Ѷ��
*  resourceId: ��ԴID
*  yuvBuffer�� YUV���ݴ洢��buffer
*  yuvBufferSize�� YUV���ݳ���
*  width�� ��Ƶ�Ŀ��
*  height�� ��Ƶ�ĸ߶�
*  orientation: ��Ƶ����ص�ֵ
*/
void MeetingManageCB::OnYUVData(int accountId, int resourceId, char * yuvBuffer, int yuvBufferSize, int width, int height, int orientation)
{
		YUVData data;
		data.m_accountId = accountId;
		data.m_resourceId = resourceId;
		data.m_width = width;
		data.m_height = height;
		
		strcpy_s(data.m_yuvBuffer, MEETINGMANAGE_sourceName_len, yuvBuffer);
		cb(yuv_data, &data, 0, nullptr);
}

/*
*	�����˸ı䵱ǰ����ģʽ���(�����˲����յ�)
*  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnHostChangeMeetingMode(int statusCode, char * description, int descLen)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(host_change_meeting_mode, &startResult, 0, 0);
}

/*
*	�����˸ı䵱ǰ����ģʽ֪ͨ
*  meetingStyle : ��ǰ����ģʽ
*/
void MeetingManageCB::OnHostChangeMeetingModeEvent(int meetingStyle)
{

		cb(host_change_meeting_mode_event, &meetingStyle, 0, 0);
}

/*
*	�������߳��û����
*  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnHostKickoutUser(int statusCode, char * description, int descLen)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(host_kickout_user, &startResult, 0, 0);
}

/*
*	�������߳��û�֪ͨ
*  meetId : �����
*  kickedUserId : ���߳��û���Ѷ��
*  descLen : ��Ѷ�ų���
*/
void MeetingManageCB::OnHostKickoutUserEvent(int meetId, char * kickedUserId, int idLen)
{
		KickoutUserData data;
		data.meetingId = meetId;
		strcpy_s(data.kickedUserId, MEETINGMANAGE_USERID_LEN, kickedUserId);

		cb(host_kickout_user_event, &data, 0, 0);
}

/*
*	�û�����������
*  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnRaiseHandReq(int statusCode, char * description, int descLen)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(raise_hand_request, &startResult, 0, 0);
}

/*
*	�յ��û���������֪ͨ���������յ���
*  accountId: �����û�ID
*  accountName: �����û�����
*  nameLen: ���Ƴ���
*/
void MeetingManageCB::OnRaiseHandReqEvent(int accountId, char * accountName, int nameLen)
{
	AttendeeInfo info;
	info.m_accountId = accountId;
	strcpy_s(info.m_accountName, MEETINGMANAGE_USERNAME_LEN, accountName);

	cb(raise_hand_request_event, &info, 0, 0);
}

/*
*	���������������������
*  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnAskForMeetingLock(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(ask_for_meeting_lock, &result, 0, 0);
}

/*
*	������ָ���û����Խ��
*  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnHostOrderOneSpeak(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(host_order_one_speak, &result, 0, 0);
}

/*
*	������ָ���û�ֹͣ���Խ��
*  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnHostOrderOneStopSpeak(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(host_order_one_stop_speak, &result, 0, 0);
}

/*
*	��������״̬�ı�֪ͨ
*  statusCode : ����״̬�룬0 - ����  1 - ����
*  description : ������Ϣ
*  descLen : ������Ϣ����
*/
void MeetingManageCB::OnLockStatusChangedEvent(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(lock_status_changed_event, &result, 0, 0);
}

/*
*	SDK�쳣֪ͨ
*  exceptionType: �쳣����
*  exceptionDesc: �쳣������Ϣ
*  descLen: ������Ϣ����
*  extraInfo: ��չ��Ϣ
*  infoLen: ��չ��Ϣ����
*/
void MeetingManageCB::OnMeetingManageExecptionEvent(SDKExceptionType exceptionType, char * exceptionDesc, int descLen, char * extraInfo, int infoLen)
{
	ExceptionResult exceptionResult;

	exceptionResult.exceptionType = exceptionType;
	strcpy_s(exceptionResult.description, sizeof(exceptionResult.description), exceptionDesc);
	strcpy_s(exceptionResult.extraInfo, sizeof(exceptionResult.extraInfo), extraInfo);
	cb(meeting_manage_exception_event, &exceptionResult, 0, 0);
}

/*
*	�豸״̬�仯֪ͨ
*  type : �豸״̬�仯����
*  devName �� �豸����
*  nameLen �� ���Ƴ���
*/
void MeetingManageCB::OnDeviceStatusEvent(DevStatusChangeType type, char * devName, int nameLen)
{
	DeviceStatusResult deviceStatusResult;

	deviceStatusResult.type = type;
	strcpy_s(deviceStatusResult.devName, sizeof(deviceStatusResult.devName), devName);

	cb(device_status_event, &deviceStatusResult, 0, 0);
}

/*
*	����̽����
*  type: ����̽������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ������Ϣ
*  descLen: ������Ϣ����
*
*/
void MeetingManageCB::OnNetDiagnosticCheck(NetDiagnosticType type, int statusCode, char * description, int descLen)
{
	NetStatusResult netStatusResult;
	netStatusResult.netStatusType = type;
	netStatusResult.m_result.m_statusCode = statusCode;
	strcpy_s(netStatusResult.m_result.m_message, sizeof(netStatusResult.m_result.m_message), description);

	cb(net_diagnostic_check, &netStatusResult, 0, 0);
}

/*
*	����ͷ�����������
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ������Ϣ
*  descLen: ������Ϣ����
*
*/
void MeetingManageCB::OnPlayVideoTest(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(play_video_test, &result, 0, 0);
}

/*
*	������Ƶ�����
*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
*  description: ������Ϣ
*  descLen: ������Ϣ����
*
*/
void MeetingManageCB::OnPlaySoundTest(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(play_sound_test, &result, 0, 0);
}

/*
*	͸����Ϣ����֪ͨ
*  accountId:	����͸����Ϣ����Ѷ��
*  data:		͸����Ϣ����
*  dataLen:	��Ϣ����
*/
void MeetingManageCB::OnTransparentMsgEvent(int accountId, char * data, int dataLen)
{
	TransparentMsg msg;
	msg.senderAccountId = accountId;
	strcpy_s(msg.data, sizeof(msg.data), data);

	cb(transparent_msg_event, &msg, 0, 0);
}

/*
*	������Ӧ����
*  statusCode:	������
*/
void MeetingManageCB::OnMicSendResponse(int statusCode)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), "");
	cb(mic_send_response, &result, 0, 0);
}

/**
* ����״���ȼ�
*
* @see lossrateLevel_NULL = 0;		//û�з�����
* @see lossrateLevel_one = 1;		//�ȼ�1
* @see lossrateLevel_two = 2;		//�ȼ�2
* @see lossrateLevel_three = 3;		//�ȼ�3
* @see lossrateLevel_four =4;		//�ȼ�4
*
*/
void MeetingManageCB::OnNetworkStatusLevelNoticeEvent(int netlevel)
{
	NetLevelResult result;
	result.NetLevel = netlevel;

	cb(network_status_level_notice_event, &result, 0, 0);
}

/**
* �豸��ʧ�ص� ,
* android�豸�ײ�ý�����ⲻ֧���豸�б���豸������
* û���豸�ָ���˵�������Եײ�����id֪ͨ�����豸��ʧ����ֱ��֪ͨUI�� ��
* ֻ����� �ֻ���M1S��
* @param  resourceid ��ʧ�豸��Ӧ����Դid
*
*/
void MeetingManageCB::OnDeviceLostNoticeEvent(int accountid, int resourceid)
{
	ResourceResult resourceResult;

	resourceResult.accountId = accountid;
	resourceResult.resourceId = resourceid;

	cb(device_lost_notice_event, &resourceResult, 0, 0);
}

/*
*	MeetingSDK�ص�֪ͨ
*  statusCode: ״̬�룬���MEETINGMANAGE_SMSDK_CBTYPE
*  description: ������Ϣ
*  descLen: ������Ϣ����
*/
void MeetingManageCB::OnMeetingSDKCallback(MEETINGMANAGE_SMSDK_CBTYPE e, char * description, int descLen)
{
	SdkCallbackResult sdkCallbackResult;
	sdkCallbackResult.type = e;
	strcpy_s(sdkCallbackResult.description, sizeof(sdkCallbackResult.description), description);

	cb(meeting_sdk_callback, &sdkCallbackResult, 0, 0);
}

/*
*	�û���Ϣ�޸Ļص�
*  statusCode : ״̬�룬0 - �ɹ������� - ʧ��
*/
void MeetingManageCB::OnModifyNickName(int statusCode,
	const char * desc, int descLen, Context context) 
{
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), desc);
	cb(modify_nick_name, &startResult, 0, 0);
}

/*
*  ��ȡ������ϸ��Ϣ���
*  statusCode: ״̬�룬0 - �ɹ������� - ʧ��
*  description: ����ֵ������Ϣ
*  descLen: ������Ϣ����
*  meetInfo: ������ϸ��Ϣ
*  context: �����Ĳ���
*/
void MeetingManageCB::OnGetMeetingInfo(int statusCode, char * description,
	int descLen, MeetingInfo &meetInfo, Context context)
{
		CreateMeetingResult result;
		result.m_result.m_statusCode = statusCode;
		strcpy_s(result.m_result.m_message, sizeof(result.m_result.m_message), description);

		result.m_meetingInfo = meetInfo;
		cb(get_meeting_info, &result, 0, context);
}

/*
*  ���Ӳλ���Ա���
*/
void MeetingManageCB::OnModifyMeetingInviters(int statusCode, char * description,
	int descLen, Context context) {
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
	cb(modify_meeting_inviters, &startResult, 0, 0);
}

/*
*  ������ָ����/�ر���˷硢������������ͷ ���
*/
void MeetingManageCB::OnHostOrderOneDoOpration(int statusCode, char * description,
	int descLen, Context context)
{
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
	cb(host_order_one_do_opration, &startResult, 0, 0);
}

/*
*  �յ�������ָ����/�ر���˷硢������������ͷ �¼�֪ͨ
*/
void MeetingManageCB::OnHostOrderDoOpratonEvent(int opType)
{
	HostOrderDoOpratonResult opTypeResult;
	opTypeResult.OperationType = opType;
	cb(host_order_do_opraton_event, &opTypeResult, 0, 0);
}

/*
*  �յ������λ��ߴ�/�ر���˷硢������������ͷ �¼�֪ͨ
*/
void MeetingManageCB::OnOtherChangeAudioSpeakerStatusEvent(int accountId, int opType)
{
	OtherChangeAudioSpeakerStatusData data;
	data.accountId = accountId;
	data.opType = opType;
	cb(other_change_audio_speaker_status_event, &data, 0, 0);
}

/*
*  ��ȡ΢�ŷ���ķ������ݽ��
*/
void MeetingManageCB::OnGetMeetingInvitationSMS(int statusCode, char* description, int descLen,
	char* invitationSMS, int smsLen, char* yyURL, int urlLen, Context context)
{
	MeetingInvitationSMSData result;
	result.m_result.m_statusCode = statusCode;
	strcpy_s(result.m_result.m_message, sizeof(result.m_result.m_message), description);
	strcpy_s(result.invitationSMS, sizeof(result.invitationSMS), invitationSMS);
	strcpy_s(result.yyURL, sizeof(result.yyURL), yyURL);
	cb(get_meeting_invitation_sms, &result, 0, context);
}

void MeetingManageCB::OnSendASpeakerStatus(int statusCode, char * description, int descLen, Context context)
{
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
	cb(send_audio_speaker_status, &startResult, 0, 0);
}

void MeetingManageCB::OnSubscribrVideo(int accountid, int statusCode) {

}



void MeetingManageCB::OnSendUIMsgRespone(int status) {
	AsyncCallResult result;
	result.m_statusCode = status;
	cb(send_ui_msg, &result, 0, 0);
}


void MeetingManageCB::OnRecvUImsgEvent(int msgId, int srcUserId, char* msg, int msgLen) {
	UiTransparentMsg uiTrangsparentMsg;
	uiTrangsparentMsg.msgId = msgId;
	uiTrangsparentMsg.toAccountId = srcUserId;
	strcpy_s(uiTrangsparentMsg.data, sizeof(uiTrangsparentMsg.data), msg);

	cb(receive_ui_msg_event, &uiTrangsparentMsg, 0, 0);
}




void MeetingManageCB::cb(int cmdId, void * pData, int dataLen, Context context)
{
	if (m_cb)
	{
		try {
			m_cb(cmdId, pData, dataLen, context);
		}
		catch (...) {
			// logging
		}
	}
}


void MeetingManageCB::Custom()
{
	if (m_cb)
	{
		m_cb(1, nullptr, 1, 0);
	}
}
