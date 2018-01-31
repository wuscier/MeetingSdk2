#ifndef _MEETINGMANAGECALLBACK_H_
#define _MEETINGMANAGECALLBACK_H_

#include "MeetingManageDefine.h"

class IMeetingManageCB
{
public:
	/*
	*  ģ�������ص�
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*  descLen�� ��������
	*  context�� �����Ĳ���
	*/
	virtual void OnStart(int statusCode, char * description,
		int descLen, Context context) = 0;

	/*
	*  ��¼����ص�
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*  descLen�� ��������
	*  info:	 ��¼��Ϣ
	*  context�� �����Ĳ���
	*/
	virtual void OnLogin(int statusCode, char * description,
		int descLen, LoginInfo info, Context context) = 0;
	
	/*
	*  ��Token���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*  context�� �����Ĳ���
	*/
	virtual void OnBindToken(int statusCode, char * description,
		int descLen, Context context) = 0;
	
	/*
	*  ��������Ƿ���ڽ��
	*  statusCode: ״̬�룬0 - �������  ��0 - ���鲻���ڣ�������
	*  description: ��Ӧ״̬�����ʾ
	*  context�� �����Ĳ���
	*/
	virtual void OnCheckMeetExist(int statusCode, char * description,
		int descLen, Context context) = 0;
	
	/*
	*  ȡ�û����б���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*  meetList: �����б�
	*  meetCount: �����б��еĻ�����
	*  context�� �����Ĳ���
	*/
	virtual void OnGetMeetingList(int statusCode, char * description, int descLen,
		MeetingInfo * meetList, int meetCount, Context context) = 0;

	/*
	 *	���û���������
	 *  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description: ��Ӧ״̬�����ʾ
	 *  descLen: ��ʾ��Ϣ���� 
	 */
	virtual void OnResetMeetingPassword(int statusCode, char * description, int descLen) = 0;

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
	virtual void OnGetMeetingPassword(int statusCode,
		char * description, int descLen,
		char * password, int pwdLen,
		char * hostId, int hostIdLen) = 0;

	/*
	 *	��ѯ�����Ƿ���������
	 *  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description: ��Ӧ״̬�����ʾ
	 *  descLen: ��ʾ��Ϣ����
	 *  hasPassword: �Ƿ������� 1��ʾ�����룬������ʾ������
	 *  hostId: ������ID
	 *  hostIdLen: ID����
	 */
	virtual void OnCheckMeetingHasPassword(int statusCode, char * description,
		int descLen, int hasPassword, char * hostId, int hostIdLen) = 0;

	/*
	 *	�����������Ƿ���Ч���
	 *  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description: ��Ӧ״̬�����ʾ
	 *  descLen: ��ʾ��Ϣ���� 
	 */
	virtual void OnCheckMeetingPasswordValid(int statusCode, char * description,
		int descLen) = 0;
	
	/*
	*  ����������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  meetingId: �´����Ļ���ID��statusCode Ϊ0ʱ��Ч
	*  meetType : ��������
	*  context�� �����Ĳ���
	*/
	virtual void OnCreateMeeting(int statusCode,
		int meetingId, MM_MeetingType meetType, Context context) = 0;
	
	/*
	*  ���������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  meetingInfo: ������Ϣ��statusCode Ϊ0ʱ��Ч
	*  context�� �����Ĳ���
	*/
	virtual void OnJoinMeeting(int statusCode, JoinMeetingInfo * meetingInfo, Context context) = 0;
	
	/*
	*  ȡ�òλ����б�֪ͨ
	*  participants: �λ����б�
	*  userCount: �λ�����
	*/
	virtual void OnGetUserList(ParticipantInfo * participants, int userCount) = 0;
	
	/*
	*  �����û��������֪ͨ
	*  accountId: �¼����û�����Ѷ��
	*  accountName: �¼����û����ǳ�
	*/
	virtual void OnUserJoinEvent(int accountId, char * accountName, int accoundNameLen) = 0;
	
	/*
	*  �����û��뿪����
	*  accountId: �뿪�û�����Ѷ��
	*  accountName: �뿪�û����ǳ�
	*/
	virtual void OnUserLeaveEvent(int accountId, char * accountName, int accountNameLen) = 0;
	
	/*
	*  ���뷢�Խ��
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnAskForSpeak(int statusCode, Context context) = 0;
	
	/*
	*  ������֪ͨ��ʼ����
	*  speakReason: ��ʼ���Ե�ԭ��0��������������,1��ʾ�������ɴ������,2��ʾ��������������ָ�����Բ���
	*  accountName: ԭʼ�����˵����ƣ����speakReason��1�����ֶα�ʾ�����ߵ����ƣ����speakReason��2�����ֶα�ʾ�����˵�����
	*/
	virtual void OnStartSpeakEvent(int speakReason ,
		char * accountName, int accountNameLen) = 0;
	
	/*
	*  ������֪ͨ���������߲���
	*  speakReason: ��ʼ���Ե�ԭ��0��������������,1��ʾ�������ɴ������,2��ʾ��������������ָ�����Բ���
	*  accountName: ԭʼ�����˵����ƣ����speakReason��1�����ֶα�ʾ�����ߵ����ƣ����speakReason��2�����ֶα�ʾ�����˵�����
	*  newSpeakerAcountName���·������ǳ�
	*  newSpeakerAccountId: �·�������Ѷ��
	*/
	virtual void OnUserStartSpeakEvent(int speakReason, char * accountName,
		int accountNameLen, char * newSpeakerAcountName, int newSpeakerAccountId) = 0;
		
	/*
	*  ����ֹͣ���Խ��
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnAskForStopSpeak(int statusCode, Context context) = 0;
	
	/*
	*  ������ֹ֪ͨͣ����
	*  reason: ֹͣ���Ե�ԭ��0����������ʧЧ���Լ�����ֹͣ���˳�����ȣ���1������ʧЧ�ɴ�����������2������ʧЧ�������˲�������
	*  accountName: ԭʼ�����˵����ƣ����reason��1�����ֶα�ʾ�����ߵ����ƣ����reason��2�����ֶα�ʾ�����˵�����
	*/
	virtual void OnStopSpeakEvent(int reason, char * accountName,
		int accountNameLen, int accountNameID) = 0;
	
	/*
	*  ������֪ͨ����������ֹͣ����
	*  reason: ֹͣ���Ե�ԭ��0����������ʧЧ���Լ�����ֹͣ���˳�����ȣ���1������ʧЧ�ɴ�����������2������ʧЧ�������˲�������
	*  accountName: ԭʼ�����˵����ƣ����reason��1�����ֶα�ʾ�����ߵ����ƣ����reason��2�����ֶα�ʾ�����˵�����
	*  speakerAcountName: ֹͣ�����û����ǳ�
	*  speakerAccountId: ֹͣ�����û�����Ѷ��
	*/
	virtual void OnUserStopSpeakEvent(int reason, char * accountName, int accountNameLen,
		char * speakerAcountName, int speakerAccountNameLen, int speakerAccountId) = 0;
	
	/*
	*  ��������ͷ��Ƶ���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
	*  context�� �����Ĳ���
	*/
	virtual void OnPublishCameraVideo(int statusCode, int resourceId, Context context) = 0;

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
	virtual void OnUserPublishCameraVideoEvent(int resourceId, int syncId,
		int accountId, char * accountName, int accountNameLen,
		char * extraInfo, int extraInfoLen) = 0;
	
	/*
	*  ��������ɼ���Ƶ���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
	*  context�� �����Ĳ���
	*/
	virtual void OnPublishWinCaptureVideo(int statusCode, int resourceId, Context context) = 0;

	/*
	*  �����ɼ�����Ƶ���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
	*  context�� �����Ĳ���
	*/
	virtual void OnPublishDataCardVideo(int statusCode, int resourceId, Context context) = 0;

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
	virtual void OnUserPublishDataVideoEvent(int resourceId, int syncId,
		int accountId, char * accountName, int accountNameLen,
		char * extraInfo, int extraInfoLen) = 0;
	
	
	/*
	*  ������˷���Ƶ���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  resourceId: ��ԴID, ���ڻ����0 - ������ԴID С��0 - ��Ч��ԴID
	*  context�� �����Ĳ���
	*/
	virtual void OnPublishMicAudio(int statusCode, int resourceId, Context context) = 0;

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
	virtual void OnUserPublishMicAudioEvent(int resourceId, int syncId, int accountId,
		char * accountName, int accoundNameLen,
		char * extraInfo, int extraInfoLen) = 0;
	
	/*
	*  ȡ������ͷ��Ƶ�������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnUnPublishCameraVideo(int statusCode, Context context) = 0;

	/*
	*  ȡ������������������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnUnPublishWinCaptureVideo(int statusCode, Context context) = 0;

	/*
	*  ȡ���ɼ�����Ƶ�������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnUnPublishDataCardVideo(int statusCode, Context context) = 0;

	/*
	*  ȡ����Ƶ�������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnUnPublishMicAudio(int statusCode, Context context) = 0;
	
	/*
	*  �����û�ȡ����������ͷ��Ƶ��
	*  resourceId�� ��ԴID
	*  accountId�� ȡ���������û�����Ѷ��
	*  accountName: ȡ���������û����ǳ�
	*/
	virtual void OnUserUnPublishCameraVideoEvent(int resourceId, int accountId,
		char * accountName, int accountNameLen) = 0;

	/*
	*  �����û�ȡ�������ĵ���
	*  resourceId�� ��ԴID
	*  accountId�� ȡ���������û�����Ѷ��
	*  accountName: ȡ���������û����ǳ�
	*/
	virtual void OnUserUnPublishDataCardVideoEvent(int resourceId, int accountId,
		char * accountName, int accountNameLen) = 0;

	/*
	*  �����û�ȡ��������Ƶ��
	*  resourceId�� ��ԴID
	*  accountId�� ȡ���������û�����Ѷ��
	*  accountName: ȡ���������û����ǳ�
	*/
	virtual void OnUserUnPublishMicAudioEvent(int resourceId, int accountId,
		char * accountName, int accountNameLen) = 0;
	
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
	virtual void OnYUVData(int accountId,int resourceId, char * yuvBuffer,
		int yuvBufferSize, int width, int height, int orientation) = 0;

	/*
	 *	�����˸ı䵱ǰ����ģʽ���(�����˲����յ�)
	 *  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnHostChangeMeetingMode(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	�����˸ı䵱ǰ����ģʽ֪ͨ
	 *  meetingStyle : ��ǰ����ģʽ
	 */
	virtual void OnHostChangeMeetingModeEvent(int meetingStyle) = 0;

	/*
	 *	�������߳��û����
	 *  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnHostKickoutUser(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	�������߳��û�֪ͨ
	 *  meetId : �����
	 *  kickedUserId : ���߳��û���Ѷ��
	 *  descLen : ��Ѷ�ų���
	 */
	virtual void OnHostKickoutUserEvent(int meetId,
		char * kickedUserId, int idLen) = 0;

	/*
	 *	�û�����������
	 *  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnRaiseHandReq(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	�յ��û���������֪ͨ���������յ���
	 *  accountId: �����û�ID
	 *  accountName: �����û�����
	 *  nameLen: ���Ƴ���
	 */
	virtual void OnRaiseHandReqEvent(int accountId, char * accountName, int nameLen) = 0;

	/*
	 *	���������������������
	 *  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnAskForMeetingLock(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	������ָ���û����Խ��
	 *  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnHostOrderOneSpeak(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	������ָ���û�ֹͣ���Խ��
	 *  statusCode : ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnHostOrderOneStopSpeak(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	��������״̬�ı�֪ͨ
	 *  statusCode : ����״̬�룬0 - ����  1 - ����
	 *  description : ������Ϣ
	 *  descLen : ������Ϣ����
	 */
	virtual void OnLockStatusChangedEvent(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	SDK�쳣֪ͨ
	 *  exceptionType: �쳣����
	 *  exceptionDesc: �쳣������Ϣ
	 *  descLen: ������Ϣ����
	 *  extraInfo: ��չ��Ϣ
	 *  infoLen: ��չ��Ϣ����
	 */
	virtual void OnMeetingManageExecptionEvent(SDKExceptionType exceptionType,
		char * exceptionDesc, int descLen, char * extraInfo, int infoLen) = 0;

	/*
	 *	�豸״̬�仯֪ͨ
	 *  type : �豸״̬�仯����
	 *  devName �� �豸����
	 *  nameLen �� ���Ƴ���
	 */
	virtual void OnDeviceStatusEvent(DevStatusChangeType type,
		char * devName, int nameLen) = 0;

	/*
	 *	����̽����
	 *  type: ����̽������
	 *  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description: ������Ϣ
	 *  descLen: ������Ϣ����
	 *  
	 */
	virtual void OnNetDiagnosticCheck(NetDiagnosticType type, int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	����ͷ�����������
	 *  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description: ������Ϣ
	 *  descLen: ������Ϣ����
	 *
	 */
	virtual void OnPlayVideoTest(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	������Ƶ�����
	 *  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	 *  description: ������Ϣ
	 *  descLen: ������Ϣ����
	 *
	 */
	virtual void OnPlaySoundTest(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	͸����Ϣ����֪ͨ
	 *  accountId:	����͸����Ϣ����Ѷ��
	 *  data:		͸����Ϣ����
	 *  dataLen:	��Ϣ����
	 */
	virtual void OnTransparentMsgEvent(int accountId, char * data, int dataLen) = 0;

	/*
	 *	������Ӧ����
	 *  statusCode:	������
	 */
	virtual void OnMicSendResponse(int statusCode) = 0;
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
	virtual void OnNetworkStatusLevelNoticeEvent(int netlevel) = 0;

	/**
	* �豸��ʧ�ص� ,
	* android�豸�ײ�ý�����ⲻ֧���豸�б���豸������
	* û���豸�ָ���˵�������Եײ�����id֪ͨ�����豸��ʧ����ֱ��֪ͨUI�� ��
	* ֻ����� �ֻ���M1S��
	* @param  resourceid ��ʧ�豸��Ӧ����Դid
	*
	*/
	virtual void OnDeviceLostNoticeEvent(int accountid, int resourceid ) = 0;

	/*
	 *	�û���Ϣ�޸Ļص�
	 *  statusCode : ״̬�룬0 - �ɹ������� - ʧ��
	 */
	virtual void OnModifyNickName(int statusCode,
		const char * desc, int descLen, Context context) = 0;


	/*
	 *	MeetingSDK�ص�֪ͨ
	 *  statusCode: ״̬�룬���MEETINGMANAGE_SMSDK_CBTYPE
	 *  description: ������Ϣ
	 *  descLen: ������Ϣ����
	 */
	virtual void OnMeetingSDKCallback(MEETINGMANAGE_SMSDK_CBTYPE e, char * description, int descLen) = 0;

		/*
	 *  ��ȡ������ϸ��Ϣ���
	 *  statusCode: ״̬�룬0 - �ɹ������� - ʧ��
	 *  description: ����ֵ������Ϣ
	 *  descLen: ������Ϣ����
	 *  meetInfo: ������ϸ��Ϣ
	 *  context: �����Ĳ���
	 */
	virtual void OnGetMeetingInfo(int statusCode, char * description,
		int descLen,MeetingInfo &meetInfo, Context context) = 0;

	/*
     *  ���Ӳλ���Ա���
     */
    virtual void OnModifyMeetingInviters(int statusCode, char * description,
		int descLen, Context context) = 0;

	/*
     *  ������ָ����/�ر���˷硢������������ͷ ���
     */
	virtual void OnHostOrderOneDoOpration(int statusCode, char * description, 
		int descLen, Context context) = 0;

	/*
     *  �յ�������ָ����/�ر���˷硢������������ͷ �¼�֪ͨ
     */
	virtual void OnHostOrderDoOpratonEvent(int opType) = 0;

	/*
     *  �յ������λ��ߴ�/�ر���˷硢������������ͷ �¼�֪ͨ
     */
	virtual void OnOtherChangeAudioSpeakerStatusEvent(int accountId, int opType) = 0;

	/*
     *  ��ȡ΢�ŷ���ķ������ݽ��
     */
	virtual void OnGetMeetingInvitationSMS(int statusCode, char* description, int descLen, 
		char* invitationSMS, int smsLen, char* yyURL, int urlLen, Context context) = 0;

	virtual void OnSendASpeakerStatus(int statusCode, char * description, 
		int descLen, Context context) = 0;


	/*
	*  ������Ƶ���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnSubscribrVideo(int accountid,int statusCode) = 0;

	/*
	*  ����UI ��Ϣ
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*/
	virtual void OnSendUIMsgRespone(int status)=0;

	/*
	*  �յ�UI��Ϣ
	*  msgid: ��Ϣid
	*  srcUserid: ���ͷ���Ѷ��
	*  msg:��Ϣ
	*/
	virtual void OnRecvUImsgEvent(int msgid, int srcUserid, char* msg, int msgLen) = 0;
};

#endif