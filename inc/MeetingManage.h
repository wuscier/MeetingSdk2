#ifndef _MEETINGMANAGE_H_
#define _MEETINGMANAGE_H_

#include "MeetingManageDefine.h"
#include "MeetingManageCallBack.h"
#include <cstddef>

class MEETING_MANAGE_API IMeetingManage
{
public:

	/*********************** ģ�����ӿ� ************************************/
	/*
	 *  ģ������			���첽�ӿڡ�
	 * ��devmodel :���ն�����
	 *  configPath: �����ļ�·��
	 *  pathLen: ·������
	 *  context�������Ĳ���
	 *  ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int Start(const char * devmodel, char * configPath, int pathLen, Context context) = 0;

	/*
	 *  ����Ӳ������·����android�汾ʹ�ã�  ��ͬ���ӿڡ�
	 *   rkPath : Ӳ������·��
	 *   pathLen: ·������
	 *  ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int SetRkPath(const char* rkPath, int pathLen) = 0;

	/*
	 *	NPS��������ַ����	 ��ͬ���ӿڡ�
	 *  ע���ýӿ�����NPS��ַ����Start����֮ǰ��Ч
	 *  npsUrlList : NPS��ַ��URL����
	 *  urlSize    : NPS���鳤��
	 *  ����ֵ : 0 - �ɹ������� - ʧ��
	 */
	virtual int SetNpsUrl(char ** npsUrlList, int urlSize) = 0;

	/*
	 *	ģ��ֹͣ			��ͬ���ӿڡ�
	 *  ����ֵ��0 - �ɹ������� - ʧ��
	 */
	virtual int Stop() = 0;

	/************************** ��Ȩ��ؽӿ� *********************************/
	/*
	*  ��¼(��SDK������Ȩ�����ô˽ӿڲ���Ҫ�ٵ���BindToken)		���첽�ӿڡ�
	*  nube�� ��ҵ��Աnube��
	*  nubeLen�� nube�ų���
	*  pass�� �û�����
	*  passLen�� �û����볤��
	*  devicetype �豸����
	*  dtlen		�豸���ͳ���
	*  context�� �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int Login(const char* nube, int nubeLen,
		const char* pass, int passLen, const char * devicetype, 
		int dtlen, Context context) = 0;

	/*
	* ��¼��ʹ�ô��Ž��е�¼��	���첽�ӿڡ�
	* imei�� ����
	* imeiLen�� ���ų���
	*/
	virtual int Login(const char * imei, int imeiLen, Context context) = 0;


	/*
	*  ��¼(��SDK������Ȩ�����ô˽ӿڲ���Ҫ�ٵ���BindToken)		���첽�ӿڡ�
	*  nube�� ��ҵ��Աnube��
	*  appkey�� appkey
	*  uid uid
	*  context�� �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int Login(const char* nube,const char* appkey, const char * uid, Context context) = 0;

	//�״ε�½ ���ŵ�¼����

	/*
	*  ��Token�������߼�Ȩ��ɺ󣬵��ô˽ӿڰ�token���û���Ϣ�����ô˽ӿڲ���Ҫ��Login�� 	���첽�ӿڡ�
	*  token: token
	*  tokenLen�� token����
	*  accountId: ��Ѷ��
	*  accountName�� ����
	*  accountNameLen�� ���Ƴ���
	*  context�� �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int BindToken(const char* token, int tokenLen,
		int accountId, const char* accountName,
		int accountNameLen, Context context) = 0;
	
	/*********************** �����ѯ�ӿ� ************************************/
	/*
	*  �жϻ����Ƿ����		���첽�ӿڡ�
	*  meetingId: ����ID
	*  context�� �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int IsMeetingExist(int meetingId, Context context) = 0;
	
	/*
	*  ȡ�ÿ��ԲμӵĻ����б�	���첽�ӿڡ�
	*  context�� �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int GetMeetingList(Context context) = 0;

	/*
	*  ȡ�û�����ϸ��Ϣ			���첽�ӿڡ�
	*  meetId:	�����
	*  context�� �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int GetMeetingInfo(int meetId, Context context) = 0;
	
	/******************* ȡ������Ƶ�豸�б�ӿ� ******************************/
	/*
	*  ȡ����Ƶ�ɼ��豸�б�		��ͬ���ӿڡ�
	*  devInfo : �����߷���洢�ռ䣬�����洢�豸��Ϣ
	*  maxCount  : �����Դ洢���豸���������С��ʵ���豸��������ֻ�����maxNum���豸��Ϣ��devInfo��
	*  ����ֵ��ʵ���豸����
	*/
	virtual int GetVideoDeviceList(MEETINGMANAGE_VideoDeviceInfo *devInfo, int maxCount) = 0;
	
	/*
	*  ȡ����Ƶ�ɼ��豸�б�		��ͬ���ӿڡ�
	*  devicelist: �����߷���洢�ռ䣬�����洢�豸����
	*  listsize  ��devicelist�����Դ洢���豸���������С��ʵ���豸��������ֻ�����listsize���豸��devicelist��
	*  ����ֵ    ��ʵ���豸����
	*/
	virtual int GetAudioCaptureDeviceList(char** devicelist, int listsize) = 0;
	
	/*
	*  ȡ����Ƶ��Ⱦ�豸�б�		��ͬ���ӿڡ�
	*  devicelist: �����߷���洢�ռ䣬�����洢�豸����
	*  listsize  ��devicelist�����Դ洢���豸���������С��ʵ���豸��������ֻ�����listsize���豸��devicelist��
	*  ����ֵ    ��ʵ���豸����
	*/
	virtual int GetAudioRenderDeviceList(char** devicelist, int listsize) = 0;

	/*
	 *	��ʼ����ͷ���			���첽�ӿڡ� (����)
	 *  colorsps:	   ��ɫ�ռ�
	 *  fps:		   ֡��
	 *  width:		   ���
	 *  height:		   �߶�
	 *  previewWindow: Ԥ�����ھ��
	 *  videoCapName:  ����ͷ�豸��
	 *  ����ֵ��
	 */
	virtual int AsynPlayVideoTest(int colorsps, int fps,
		int width, int height, void * previewWindow, char videoCapName[256]) = 0; 


		/*
	 *	��ʼ����ͷ���			��ͬ�����ӿڡ�
	 *  colorsps:	   ��ɫ�ռ�
	 *  fps:		   ֡��
	 *  width:		   ���
	 *  height:		   �߶�
	 *  previewWindow: Ԥ�����ھ��
	 *  videoCapName:  ����ͷ�豸��
	 *  ����ֵ��0 �ɹ� ��0ʧ��
	 */
	virtual int PlayVideoTest(int colorsps, int fps,
		int width, int height, void * previewWindow, char videoCapName[256]) = 0; 

	/*
	 *	ֹͣ����ͷ���			��ͬ���ӿڡ�
	 */
	virtual void StopVideoTest() = 0;

	/*
	 *	��ʼ����ͷ��⣨YUV���ݻص���ʽ��		���첽�ӿڡ�
	 *  colorsps:	    ��ɫ�ռ�
	 *  fps:		    ֡��
	 *  width:		    ���
	 *  height:		    �߶�
	 *  videoCapName:	����ͷ�豸��
	 *  fun:			YUV���ݻص�����
	 *  ����ֵ��
	 */
	virtual int AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
		int height, char videoCapName[256], FUN_VIDEO_PREVIEW fun) = 0;

	/*
	 *	ֹͣ����ͷ��⣨YUV���ݻص���ʽ��		��ͬ���ӿڡ�
	 */
	virtual int StopVideoTestYUVCB() = 0;

	/*
	 *	��ʼ������Ƶ���		 ���첽�ӿڡ�
	 *  wavFile: �����Ų����ļ�·��  ·��ΪNULL���������� ·����ΪNULL����MIC����¼��
	 *  renderName���豸��
	 *  ����ֵ�� 
	 */
	virtual int AsynPlaySoundTest(char wavFile[256], char renderName[256]) = 0; 

	/*
	 *	ֹͣ������Ƶ���		 ��ͬ���ӿڡ�
	 */
	virtual void StopPlaySoundTest() = 0;
	
	/*
	 *	��ʼ¼�����			 ��ͬ���ӿڡ�
	 *
	 *  micName�� ��˷��豸��
	 *  wavFile: ��¼�ƵĲ����ļ�
	 *  ����ֵ��
	 */
	virtual int RecordSoundTest(char micName[256], char wavFile[256]) = 0; 

	/*
	 *	ֹͣ¼�����			 ��ͬ���ӿڡ�
	 */
	virtual void StopRecordSoundTest() = 0;

	/*	
	*  ����̽��				 ���첽�ӿڡ�
	*  ��ʼ����̽��ϵ��̽�⡡��һ���У���̽�ⲽ�裬ÿһ����ɳɹ��������Զ���ʼ�����̽�⣮
	*  ̽������Ϊ��������ͨ�ԣ�������ͨ�ԣ����ٴ���̽��
	*
	*  ����ֵ��0:ͬ����ʼ���óɹ�������������ʼʧ�ܣ�����δ�ɹ���ʼ��
	*		
	*/
	virtual int AsynStartNetDiagnosticSerialCheck() = 0;

	/*	
	*   ֹͣ�������̽��		 ��ͬ���ӿڡ�
	*   ������̽����뵽������̽�⣢�׶Σ��˳�ҳ���ʱ����Ҫ�������������
	*/
	virtual int StopNetBandDetect() = 0;

	/*
	*  ��ȡ����̽����			 ��ͬ���ӿڡ�
	*  ������̽������ΪNDT_BAND_DETECT�Ļص��У�ͬ�����ã���ȡ̽����
	*  upwidth: ���д���
	*  downwidth: ���д���
	*  ����ֵ�������ɹ������� ʧ��
	*/
	virtual int GetNetBandDetectResult(int & upwidth, int & downwidth) = 0;

	/****************************** ����������ؽӿ� ***********************************/

	/*
	 *	���û�������   (������������)         ���첽�ӿڡ�
	 *  meetingId �� �����
	 *  encode �� ����
	 *  ����ֵ �� 0 - �ɹ������� - ʧ��
	 */
	virtual int ResetMeetingPassword(int meetingid, const char* encode = NULL) = 0;
	
	/*
	 *	��ȡ��������   �������������ˣ�         ���첽�ӿڡ�
	 *  meetingId : �����
	 *  ����ֵ �� 0 - �ɹ������� - ʧ��
	 */
	virtual int GetMeetingPassword(int meetingid) = 0;

	/*
	 *	�������Ƿ��������     ���첽�ӿڡ�
	 *  meetingId : �����
	 *  ����ֵ �� 0 - �ɹ������� - ʧ��
	 */
	virtual int CheckMeetingHasPassword(int meetingid) = 0;

	/*
	 *	�����������Ƿ���Ч (��������������û�������鶼��ҪУ������)    ���첽�ӿڡ�
	 *  meetingId : �����
	 *  ����ֵ �� 0 - �ɹ������� - ʧ��
	 */
	virtual int CheckMeetingPasswordValid(int meetingid,
		const char* encryptcode = NULL) = 0;
	
	/********************************* ������ؽӿ� *******************************/

	/*
	*  ��������		���첽�ӿڡ�
	*  appType: Ӧ������
	*  typeLen: ���ͳ���
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int CreateMeeting(char * appType, int typeLen, Context context) = 0;

	/*
	 *	�������������	 ���첽�ӿڡ�
	 *  ���ӿ�ֻ�ṩ���롢��������Ϣ����λ��б��ܣ����ṩ��ʱ����������
	 *  �û�Ҫʵ�ּ�ʱ��������Ҫ����host ʵ��������������ӿ�ʵʱ���롣
	 *  
	 *	appType			: Ӧ������
	 *  typeLen			: ���ͳ���
	 *  inviteeList		: ���������б�
	 *  inviteeCount	: ����������Ŀ
	 *  context			: �����Ĳ���
	 *  ����ֵ : 0 - �ɹ������� - ʧ��
	 *  
	 */
	virtual int CreateAndInviteMeeting(char * appType, int typeLen,
		int * inviteeList, int inviteeCount, Context context) = 0;

	/*
	* ����ԤԼ���飬UI����Ҫ��callback��ͬ�����ú���GetMeetingId �Ի�ȡ���δ��������Ļ���š�
	*  appType: Ӧ������
	*  typeLen: ���ͳ���
	* ����˵����	year,
	*			month
	*			day
	*           hour
	*           minute
	*           second
	*           endtime	�������ʱ�� ��ʽyyyy-mm-dd hh:mm:ss ��2016-08-02 09:08:30
	*			topic   ��������
	*			encryptcode ,����Ϊ�ա�
	* ����ֵ:  0 - �ɹ������� - ʧ��
	*/
	virtual int CreateDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour, 
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, const char * encryptcode = NULL) = 0;
	/*
	* ����������ԤԼ���飬UI����Ҫ��callback��ͬ�����ú���GetMeetingId �Ի�ȡ���δ��������Ļ���š�
	*  appType: Ӧ������
	*  typeLen: ���ͳ���
	* ����˵����	
	*			appType 
	*			year,
	*			month
	*			day
	*           hour
	*           minute
	*           second
	*           endtime	�������ʱ�� ��ʽyyyy-mm-dd hh:mm:ss ��2016-08-02 09:08:30
	*			topic   ��������
	*			inviteeList		: ���������б�
	*			inviteeCount	: ����������Ŀ
	*			encryptcode ,����Ϊ�ա�
	* ����ֵ:  0 - �ɹ������� - ʧ��
	*/
	virtual int CreateAndInviteDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour, 
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, int * inviteeList, int inviteeCount,const char * encryptcode = NULL) = 0;
	

	/*
	*  ���Ӳλ���Ա ������й�������������������Ƶ�˺ż�����顣��ԭ������������λ���Ա���Ͷ��ţ��Ѿ��λ����Ա�Ͳ����Ͷ�����
	* ����˵����	
	*			meetId			�����
	*			appType			Ӧ������
	*			smsType			ָ�����ŷ�������Ϊ��ʱ���黹��ԤԼ���� 1����ʱ���� 2 ԤԼ����
	*			accountList		��Ҫ��λ�����Ѷ���б�
	*			accountNum		��Ҫ��λ�����Ѷ�Ÿ���
	*			context			�����Ĳ���
	*/
	virtual int ModifyMeetingInviters(int meetId,const char * appType,int smsType,int *accountList,int accountNum, Context context) = 0;

	/*
	*  ������� 	���첽�ӿڡ�
	*  meetingId: ����ID
	*  autoSpeak: �Ƿ�����Զ�����, true - �����Զ�����  false - ���᲻�Զ�����
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int JoinMeeting(int meetingId, bool autoSpeak, Context context) = 0;

	/*
	*  ��ȡ���������Ϣ 	��ͬ���ӿڡ�
	*  meetingId: ����ID
	*  joinMeetingInfo: ���������Ϣ
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*  ע���÷�������OnCreateMeeting��OnJoinMeeting�ص������е����ܷ�����Ч�ļ��������Ϣ
	*/
	virtual int GetJoinMeetingInfo(int meetingId, JoinMeetingInfo * joinMeetingInfo) = 0;
	
	/*
	*  �����µ�ͬ��id  	��ͬ���ӿڡ�
	*  ����ֵ���²�����ͬ��id
	*/
	virtual int GenericSyncId() = 0;

	/*
	*  ��ȡ����ģʽ��Ϣ    ��ͬ���ӿڡ�
	*  �ڼ������ɹ���UI �������� GetParticipants֮����á�ͬ������
	*  myRole :	2 ����������, 1 ������ͨ�λ���.
	*  curMode : 1Ϊ����ģʽ��2Ϊ������ģʽ��
	*  hostId : �����˵�id
	*  idLen : ������id����
	*  hostNick : �������ǳ�
	*  nickLen : �������ǳƳ���
	*  liveStatus : ֱ��״̬ , 1����ֱ�����飬2������ͨ���飬0������֪
	*/
	virtual void GetMeetingModeInfo(int & myRole, int & curMode,
		char * hostId, int idLen, char * hostNick, 
		int nickLen, int & liveStatus) = 0;
	 
	/*
	*  ��ȡ��ǰ����ģʽ     ��ͬ���ӿڡ�
	*  curMode : 1 Ϊ����ģʽ��2 Ϊ������ģʽ��
	*/
	virtual void GetCurMeetingMode(int & curMode) = 0; 

 	/*
	*  �����˸ı䵱ǰ����ģʽ  ���첽�ӿڡ�
	*  toMode : 1Ϊ����ģʽ��2Ϊ������ģʽ
	*  ����ֵ : 0 - �ɹ������� - ʧ��
	*/
	virtual int HostChangeMeetingMode(int toMode) = 0;

	/*
	 *	�������߳��û�       ���첽�ӿڡ�
	 *  accountId : ���߳��û���Ѷ��
	 *  ����ֵ �� 0 - �ɹ������� - ʧ��
	 */
	virtual int HostKickoutUser(int accountId) = 0;

	/*
	 *	�û���������       ���첽�ӿڡ�
	 *  ����ֵ �� 0 - �ɹ������� - ʧ��
	 */
	virtual int RaiseHandReq() = 0;

	/*
	*  �������������������   ���첽�ӿڡ�
	*  bToLock �� true ������ false ����
	*  ����ֵ : 0 - �ɹ������� - ʧ��
	*/	
	virtual int AskForMeetingLock(bool bToLock) = 0;

	/*
	*  ��ȡ��������״̬     ��ͬ���ӿڡ�
	*  ����ֵ : true ������false ����
	*		
	*/	
	virtual bool GetMeetingLockStatus() = 0;

	/*
	*  ������ָ���û�����		���첽�ӿڡ�
	*  toAccountId : ��ָ���ķ����˵�id
	*  toLen : ��ָ���ķ����˵�id����
	*  kickAccountId : ���ڷ����˵���Ѷ�ţ�����ռ�û���Ѷ�ţ����մ���ʾ�����Ƿ��дﵽ����������ж�ָ�������Ƿ�ɹ�
	*  kickLen : ���ڷ�����id����
	*  ����ֵ �� 0 - �ɹ������� - ʧ��
	*/
	
	virtual int HostOrderOneSpeak(char * toAccountId, int toLen, 
		char * kickAccountId, int kickLen) = 0;

	/*
	*  ������ָ���û�ֹͣ����    ���첽�ӿڡ�
	*  toAccountId : ��ָ���ķ����˵�id
	*  toLen : ��ָ���ķ����˵�id����
	*  ����ֵ �� 0 - �ɹ������� - ʧ��
	*/
	virtual int HostOrderOneStopSpeak(char * toAccountId, int toLen) = 0;
	
	/*
	*  ��ȡָ���û�����������Ϣ		��ͬ���ӿڡ�
	*  accountId: ��������Ѷ��
	*  streamsInfo: �洢����Ϣ������, �ڴ�ռ��ɵ����߷���
	*  maxCount: �����߷���洢�ռ�ĸ���,maxCount С��ʵ��������ʱ��ֻ����ǰmaxCount��
	*  ����ֵ��ʵ������Ϣ����
	*/
	virtual int GetUserPublishStreamInfo(int accountId,
		MeetingUserStreamInfo * streamsInfo, int maxCount) = 0;
	
	/*
	*  ��ȡ��ǰ�û����ĵ���������Ϣ		��ͬ���ӿڡ� 
	*  streamsInfo: �洢����Ϣ������, �ڴ�ռ��ɵ����߷���
	*  maxCount: �����߷���洢�ռ�ĸ���,maxCount С��ʵ��������ʱ��ֻ����ǰmaxCount��
	*  ����ֵ��ʵ������Ϣ����
	*/ 
	virtual int GetCurrentSubscribleStreamInfo(
		MeetingUserStreamInfo * streamsInfo, int maxCount) = 0;
	
	// /*
	// *  ȡ�õ�ǰ�ڻ����еĲλ�����	��ͬ���ӿڡ�
	// *  ����ֵ���λ�����
	// */
	// virtual int GetParticipantsCount() = 0;
	
	/*
	*  ȡ�òλ����б�	��ͬ���ӿڡ�
	*  participants: �洢�λ�����Ϣ�����飬�ڴ�ռ��ɵ����߷���
	*  maxCount: ���Դ洢�Ĳλ�����Ϣ���������������߷��������������maxCount С��ʵ������ʱ��ֻ����ǰmaxCount���λ�����Ϣ
	*  ����ֵ��>= 0 - ʵ�ʵĲλ�����; <0 - ʧ��
	*/
	virtual int GetParticipants(ParticipantInfo* participants,int maxCount) = 0;

	/*
	*  ȡ�òλ����б�	��ͬ���ӿڡ�
	*  participants: �洢�λ�����Ϣ�����飬�ڴ�ռ��ɵ����߷���
	*  pageNum: ��Ҫ��ȡ�λ������ڵ�ҳ��
	*  countPerPage: ÿҳ�Ĳλ���Ա����
	*  ����ֵ��>= 0 - ʵ�ʵĲλ�����; <0 - ʧ��
	*/
	virtual int GetParticipants(ParticipantInfo * participants,
		int pageNum, int countPerPage) = 0;
	
	/*
	*  �˳���ǰ���� 	��ͬ���ӿڡ�
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int LeaveMeeting() = 0; 
	
	/*
	*  ���뷢��		���첽�ӿڡ�
	*  speakerId: ��������ɷ��ԣ�����������ǿն��������������洢�����˵���Ѷ��
	*  speakerIdLen : ��Ѷ�ų���
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int AskForSpeak(char * speakerId, int speakerIdLen, Context context) = 0;
	
	/*
	*  ����ֹͣ����		���첽�ӿڡ�
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int AskForStopSpeak(Context context) = 0;

	/*
	 *	��ȡ�������б�			[ͬ���ӿ�]
	 *  speakerInfoList: �洢��������Ϣ�����飬�ڴ�ռ��ɵ����߷���
	 *  maxCount: ���������Ϣ��ȡ��
	 *  ����ֵ: >= 0 - ʵ�ʵĲλ�����; <0 - ʧ��
	 */
	virtual int GetSpeakerList(MeetingSpeakerInfo * speakerInfos, int maxCount) = 0;

	/*
	 *	��ȡָ����������Ϣ		[ͬ���ӿ�]
	 *  speakerInfo: ��������Ϣ
	 *
	 *  ����ֵ: 0 - �ɹ��� ��0 - ʧ��
	 */
	virtual int GetSpeakerInfo(int accountId, MeetingSpeakerInfo * speakerInfo) = 0;

	/*
	 *	��ȡ����QOS����		[ͬ���ӿ�]
	 *	outdata:�ⲿ������ڴ�
	 *  outlen: �ⲿ������ڴ泤�ȣ��ɹ���ʱ�����ڲ����ص����ݳ��ȡ�
	 *  ����ֵ��= 0 ���ɹ���-1�����Ȳ�����outlen Ϊ�ڴ��ĳ��ȡ�������ֵʧ�ܡ�
	 */
	virtual  int  GetMeetingQos( char *outdata ,int & outlen ) = 0;

	
	/*
	*  ��������ͷ��Ƶ	���첽�ӿڡ�
	*  param: ����ͷ��Ƶ��������
	*  isNeedCallBackMedia: �Ƿ���Ҫ�ص�YUV���� true:��Ҫ�ص�  false������Ҫ�ص�
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int PublishCameraVideo(MEETINGMANAGE_PublishCameraParam *param,
		bool isNeedCallBackMedia, Context context) = 0;
	
	/*
	*  �����ɼ�����Ƶ	���첽�ӿڡ�
	*  param: �ɼ�����Ƶ��������
	*  isNeedCallBackMedia: �Ƿ���Ҫ�ص�YUV���� true:��Ҫ�ص�  false������Ҫ�ص�
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam *param,
		bool isNeedCallBackMedia, Context context) = 0;
	
	/*
	*  ��������������Ƶ��	���첽�ӿڡ�
	*  param: ����ɼ���Ƶ��������
	*  isNeedCallBackMedia: �Ƿ���Ҫ�ص�YUV���� true:��Ҫ�ص�  false������Ҫ�ص�
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam *param,
		bool isNeedCallBackMedia, Context context) = 0;
	
	/*
	*  ������Ƶ��   ���첽�ӿڡ�
	*  param: ��Ƶ��������
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int PublishMicAudio(MEETINGMANAGE_publishMicParam *param,
		Context context) = 0;
	
	/*
	*  ȡ����������ͷ��Ƶ�� ���첽�ӿڡ�
	*  resourceId: ��ԴID
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int UnpublishCameraVideo(int resourceId, Context context) = 0;

	/*
	*  ȡ�������ɼ�����Ƶ�� ���첽�ӿڡ�
	*  resourceId: ��ԴID
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int UnpublishDataCardVideo(int resourceId, Context context) = 0;

	/*
	*  ȡ����������������Ƶ�� ���첽�ӿڡ�
	*  resourceId: ��ԴID
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int UnpublishWinCaptureVideo(int resourceId, Context context) = 0;

	/*
	*  ȡ��������Ƶ�� ���첽�ӿڡ�
	*  resourceId: ��ԴID
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int UnpublishMicAudio(int resourceId, Context context) = 0;
	
	/*
	*  ������Ƶ  ��ͬ���ӿڡ�
	*  param: ��Ƶ���Ĳ���
	*  isNeedCallBackMedia: �Ƿ���Ҫ�ص�YUV���� true:��Ҫ�ص�  false������Ҫ�ص�
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int	SubscribeVideo(MEETINGMANAGE_subscribeVideoParam *param,
		bool isNeedCallBackMedia) = 0;

	/*
	*  ������Ƶ  ���첽�ӿڡ�
	*  param: ��Ƶ���Ĳ���
	*  isNeedCallBackMedia: �Ƿ���Ҫ�ص�YUV���� true:��Ҫ�ص�  false������Ҫ�ص�
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int	AsynSubscribeVideo(MEETINGMANAGE_subscribeVideoParam *param,
		bool isNeedCallBackMedia) = 0;
	
	/*
	*  ������Ƶ  ��ͬ���ӿڡ�
	*  param: ��Ƶ���Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int SubscribeAudio(MEETINGMANAGE_subscribeAudioParam *param) = 0;
	
	/*
	*  ȡ������  ��ͬ���ӿڡ�
	*  accountId: ��������Ѷ��
	*  resourceId: ��ԴID
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int	Unsubscribe(int accountId, int resourceId) = 0;
	
	/*
	*  ����ý����  ��ͬ���ӿڡ�
	*  resourceId: ��ԴID
	*  frameType�� ý���ʽ
	*  frameData�� ý������
	*  dataLen��   ���ݳ���
	*	orientation:ͼ����Ŀǰandroid����Ҫ����windows�²���Ҫ��������Ϣ
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int PushMediaFrameData(int resourceId,
		MEETINGMANAGE_FrameType frameType, char * frameData, int dataLen,int orientation = 0 ) = 0;

	/*
	*  ��ʼ��Ⱦ������Ƶ  ��ͬ���ӿڡ�
	*  resourceId: ��ԴID
	*  displayWindow: ��ʾ���ھ��
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int StartLocalVideoRender(int resourceId, void* displayWindow,int aspx=0,int aspy=0) = 0;

	/*
	*  ֹͣ��Ⱦ������Ƶ  ��ͬ���ӿڡ�
	*  resourceId: ��ԴID
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int StopLocalVideoRender(int resourceId) = 0;
	
	/*
	*  ��ʼ��ȾԶ����Ƶ  ��ͬ���ӿڡ�
	*  accountId: ��������Ѷ��
	*  resourceId: ��ԴID
	*  displayWindow: ��ʾ����
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/

	virtual int StartRemoteVideoRender(int accountId,
		int resourceId, void* displayWindow ,int aspx=0,int aspy=0) = 0;

	/*
	*  ֹͣ��ȾԶ����Ƶ  ��ͬ���ӿڡ�
	*  accountId: ��������Ѷ��
	*  resourceId: ��ԴID
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int StopRemoteVideoRender(int accountId, int resourceId) = 0;

	/*
	 *	��ʼYUV���ݵĻص�֪ͨ
	 *  accountId: ��������Ѷ��
	 *  resourceId: ��ԴID
	 *  ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int StartYUVDataCallBack(int accountId, int resourceId) = 0;

	/*
	 *	ֹͣYUV���ݵĻص�֪ͨ
	 *  accountId: ��������Ѷ��
	 *  resourceId: ��ԴID
	 *  ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int StopYUVDataCallBack(int accountId, int resourceId) = 0;

	/*
	 *	����͸����Ϣ
	 *  destAccount: Ŀ����Ѷ��
	 *  data��͸������
	 *  dataLen�� ���ݳ���
	 *  ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int SendUiTransparentMsg(int destAccount, char * data, int dataLen) = 0;


	/*
	 *  ͸��UI��Ϣ ��Ⱥ����
	 *    msgId              ��ϢID
	 *    dstUserAccount     Ŀ���û���Ѷ�� 0Ⱥ��
	 *    msgData            ��Ϣ����
	 ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int AsynSendUIMsg(int msgId, int dstUserAccount,const char *msgData) = 0;


	 
	/*
	*  ��ȡ�������б�	��ͬ���ӿڡ�
	*  participants: �洢�ɴ�������Ϣ�����飬�ڴ�ռ��ɵ����߷���
	*  maxCount: ���Դ洢�Ĵ�������Ϣ���������������߷��������������maxCount С��ʵ������ʱ��ֻ����ǰmaxCount���λ�����Ϣ
	*  ����ֵ��>= 0 - ʵ�ʵĲλ�����; <0 - ʧ��
	*/
	virtual int GetMicSendList(ParticipantInfo* participants,int maxCount) = 0;

	/*
	*  ���������� ��ͬ���ӿڡ�
	*/
	virtual int AsynMicSendReq(int beSpeakedUserId) = 0;


	/*
	*  ������Ⱦ���� ��ͬ���ӿڡ�
	*  accountId: ��������Ѷ��
	*  resourceId: ��ԴID
	*  displayWindow ��Ⱦ���ھ��
	*  aspx ��Ƶ��߱ȷ���
	*  aspy ��Ƶ��߱ȷ�ĸ
	* ע�� ���������
	*	 1. aspx��aspy��Ϊ0����֤��Ƶ��߱ȵ�������������ڣ��п��ܻ��кڱߡ�(Ĭ��)
	*    2. aspx��aspy��Ϊ-1����Ƶ�������ڣ�����֤��Ƶ��߱ء�
	*	 3. aspx��aspy������0��ǿ��ʹ�����õĿ�߱ȡ�
	 *	 4. ����Է���Ƶ��·��������ӿڻ�ʧ��
	* ����ֵ �ɹ�����0��ʧ�ܷ��ش�����
	*/
	virtual int AddDisplayWindow(int accountId, int resourceId,void *displayWindow,int aspx = 0, int aspy =0)=0;


	/*
	 *  �Ƴ���Ⱦ���� ��ͬ���ӿڡ�
	 *  accountId: ��������Ѷ��
	 *  resourceId: ��ԴID
	 *  displayWindow ��Ⱦ���ھ��
	 *  aspx ��Ƶ��߱ȷ���
	 *   aspy ��Ƶ��߱ȷ�ĸ
	 * ����ֵ �ɹ�����0��ʧ�ܷ��ش�����
	 * ע�� ���������
	 *	 1. aspx��aspy��Ϊ0����֤��Ƶ��߱ȵ�������������ڣ��п��ܻ��кڱߡ�(Ĭ��)
	 *   2. aspx��aspy��Ϊ-1����Ƶ�������ڣ�����֤��Ƶ��߱ء�
	 *	 3. aspx��aspy������0��ǿ��ʹ�����õĿ�߱ȡ�
	 *	 4. ����Է���Ƶ��·��������ӿڻ�ʧ��
	 * ˵�����ù���֧�ֳ�ֱ�������������а汾
	 */
	virtual int RemoveDisplayWindow(int accountId, int resourceId,void *displayWindow,int aspx = 0, int aspy =0)=0;


	/*
	*  ����ֱ��ý���� ��ͬ���ӿڡ�
	* ������param (���ʵ�λ��kbps)
	* ����ֵ��>0 �ɹ� streamidý������ʶ  ��<0 ʧ�� ������
	*/
	virtual int PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam * param) =0;


	/*
	*  ֹͣ����ֱ��ý���� ��ͬ���ӿڡ�
	* ���� streamID ý������ʶ
	* ����ֵ��>0 �ɹ� streamidý������ʶ  ��<0 ʧ�� ������
	*/
	virtual int UnpublishLiveStream(int streamID) =0;

	/*
	 *  ��ʼֱ��������ͬ���ӿڡ�
	 * ����: streamID ý������ʶ
	 *	     url ������ַ
	 * ����ֵ:�ɹ�����0��ʧ�ܷ�������ֵ
	 */
	virtual int StartLiveRecord(int streamID, char *url) =0;

	/*
	*  ֱֹͣ������ ��ͬ���ӿڡ�
	* ���� streamID ý������ʶ
	* ����ֵ:�ɹ�����0��ʧ�ܷ�������ֵ
	*/
	virtual int StopLiveRecord(int streamID) = 0;


	/*
	* ������Ƶ��·��Ϣ ��ͬ���ӿڡ�
	* ������ streamID ý������ʶ
	*		  streamInfo ��Ƶ��·��Ϣָ��
	*		  streamnum ��Ƶ��·����������޶�20��
	* ����ֵ���ɹ�����0��ʧ�� ���ش�����
	*/
	virtual int UpdateLiveStreamVideoInfo(int streamID, MEETINGMANAGE_VideoStreamInfo *streamInfo, int streamnum) =0;

	/*
	 * ������Ƶ��·��Ϣ ��ͬ���ӿڡ�
	 * ������ streamID ý������ʶ
	 *		  streamInfo ��Ƶ��·��Ϣָ��
	 *		  streamnum ��Ƶ��·����������޶�20��
	 * ����ֵ���ɹ�����0��ʧ�� ���ش�����
	 */
	virtual int UpdateLiveStreamAudioInfo(int streamID, MEETINGMANAGE_AudioStreamInfo *streamInfo, int streamnum)=0;

	/*
	*  ��ʼmp4�ļ�¼�� ��ͬ���ӿڡ�
	* ����: streamID ý������ʶ
	*		filepath ·����ָ���ļ���
	* ����ֵ:�ɹ�����0��ʧ�ܷ�������ֵ
	*/
	virtual int StartMp4Record(int streamID, char *filepath) = 0;

	/*
	*  ֹͣmp4�ļ�¼�� ��ͬ���ӿڡ�
	* ����: streamID ý������ʶ
	* ����ֵ:�ɹ�����0��ʧ�ܷ�������ֵ
	*/
	virtual int StopMp4Record(int streamID) = 0;


	/***************** ����Ӧ�ӿ� ******************/
	/*
	 *  ������Ƶ��ʾģʽ ��ͬ���ӿڡ�
	 *   videoDisplayMode    ��Ƶ������ʾģʽ
	 *                1     ȫ����ģʽ
	 *                2     ȫ����ģʽ
	 *                3     ����Ӧģʽ
	 *   ����ֵ       EC_OK �ɹ�    EC_FAIL ʧ��
	 */
	virtual int SetVideoDisplayMode(int videoDisplayMode) = 0;


	/*
	 *  ���ô��ڻ�����ʾ������ ��ͬ���ӿڡ�
	 *  accountId: ��������Ѷ��
	 *  resourceId: ��ԴID           
	 *   clarityLevel       �����ȼ���
	 *                1     �������ȣ����棩
	 *                2     �������ȣ��л��棩
	 *                3     �������ȣ�С���棩
	 *				  4     ���ش���
	 */
	virtual int SetVideoClarity(int accountId, int resourceId,int clarityLevel) = 0;

	/*
	 *  �����Ƿ���������Ӧ(�λ�ǰ����) ��ͬ���ӿڡ�
	 *   isEnabled          �Ƿ�����  0��������  ��0������
	 *   ����ֵ       EC_OK �ɹ�    EC_FAIL ʧ��
	 */
	virtual int  SetAutoAdjustEnableStatus(int isEnabled)= 0;


	/*
	 *  �����Ƿ�������˫��(�λ�ǰ����)��ͬ���ӿڡ�
	 *   isEnabled          �Ƿ�����  0��������  ��0������
	 *   ����ֵ       EC_OK �ɹ�    EC_FAIL ʧ��
	 */
	virtual int  SetPublishDoubleVideoStreamStatus(int isEnabled)= 0;


	/*
	 *  ��������ģʽ��Ƶ��������� ��ͬ���ӿڡ�
	 *   frameWidth    С��������
	 *   frameHeight   С������߶�
	 *   bitrate       С���������ʣ���λkbps��
	 *   frameRate     С������֡��
	 */
	virtual int SetLowVideoStreamCodecParam(int frameWidth,int frameHeight,int bitrate,int frameRate)= 0;
	 
	/* ����cpu������ ��ͬ���ӿڡ�
	*
	*   processCpu    ����cpu������
	*   totalCpu      ����cpu������
	*/
	virtual int SetCurCpuInfo(int processCpu,int totalCpu)=0;

	/*
	 *  ������Ƶ�������ն˻��������������� (�λ�ǰ����) ��ͬ���ӿڡ�
	 * ������ AudioMaxBufferNum ��Ƶ���ն˻�������󻺳���
	 *		  AudioStartVadBufferNum ��Ƶ���ն˻�������ʼ������ѹ����ֵ
	 *		  AudioStopVadBufferNum ��Ƶ���ն˻�����ֹͣ������ѹ����ֵ
	 * ����ֵ �ɹ�����0��ʧ�ܷ��ش�����
	 */
	virtual int SetAudioMixRecvBufferNum(int AudioMaxBufferNum, int AudioStartVadBufferNum, int AudioStopVadBufferNum)=0;



	/****************** �û����� ******************/

	/*
	 *	�޸��û���Ϣ		���첽�ӿڡ�
	 *	accountName�� �û��ǳ�
	 *  nameLen��     �ǳƳ���
	 *  ����ֵ�� 0 - �ɹ��� ���� - ʧ��
	 */
	virtual int ModifyNickName(const char * accountName, int nameLen, Context context) = 0;


	/***** ������ָ����/�ر���˷硢������������ͷ��ؽӿ� *****/
	/*
	*  ������ָ�������û�ִ��ĳ����� ���첽�ӿڡ�
	*  @toUserId	�� �����Ʒ���Ѷ��
	*  @oprateType	�� �������� 
	*						1    ������ͷ��Ƶ
							2    �ر�����ͷ��Ƶ
						    3    ����˷���Ƶ
							4    �ر���˷���Ƶ
						    5    ����Ļ������Ƶ
							6    �ر���Ļ������Ƶ
						    7    ��������
							8    �ر�������
	*  ͬ������ֵ�� 0 �ɹ�   ����ʧ��
	*/
	virtual int HostOrderOneDoOpration(int toUserId, int oprateType, Context context) = 0;

	/*
	 *  ��ȡ�λ᷽��Ƶ���ֱ���	��ͬ���ӿڡ�
	 *  @accountId	:	��������Ѷ��
	 *  @resourceID	:	��Ƶ����ԴID
	 *  @videoWidth :	��Ƶ�ֱ���-��ȣ����������
	 *  @videoHeight:	��Ƶ�ֱ���-�߶ȣ����������
	 *  ͬ������ֵ�� 0 �ɹ�   ����ʧ��
	 */
	virtual int GetSpeakerVideoStreamParam(int accountId, int resourceID, int &videoWidth, int &videoHeight) = 0; 

	/*
	 *  ��ȡ���ŷ���ķ�������	���첽�ӿڡ�
	 *  @meetId			: �����
	 *  @inviterPhoneId	: ������������Ѷ��
	 *  @inviterName	: �����������ǳ�
	 *  @meetingType	: �������� ��ʱ����:1 ԤԼ���飺2
	 *  @app			: ��Ʒ���� ������: JIHY, ���Ӽ����飺KESHI_JIHY�����ƻ��HVS
	 *  @urlType		: url ���� 1��http  2��https
	 *  @context		: �����Ĳ���
	 *  ͬ������ֵ�� 0 �ɹ�   ����ʧ��
	 */
	 virtual int GetMeetingInvitationSMS(int meetId, int inviterPhoneId, const char* inviterName, int inviterNameLen,
		int meetingType, const char* app, int appLen, int urlType, Context context) = 0;

	 /*
	  *  ֪ͨ�����λ᷽����������״̬	���첽�ӿڡ�
	  *  @isOpen		: �������򿪹ر�״̬ 0���ر�  1����
	  *  @context		: �����Ĳ���
	  *  ͬ������ֵ�� 0 �ɹ�   ����ʧ��
	  */
	 virtual int SendAudioSpeakerStatus(int isOpen, Context context) = 0;


	 /***** �������� *****/
	 /* �����Ƿ�ʹ�ô�������� (Startǰ����) ��ͬ���ӿڡ�
	*
	*   isProxy     0����ʹ��  1��ʹ��
		ͬ������ֵ�� 0 �ɹ�   ����ʧ��
	*/
	virtual int SetUseProxy(int isProxy)=0;

	/*
	*  ������Ƶ���������� �������ô˽ӿ�Ĭ��Ӳ�����룩
	* ����: isHard ���������͡���0��ʾӲ�����룻0��ʾ������� 
	* ����ֵ �ɹ�����0��ʧ�ܷ��ش�����
	*/
	virtual int SetDecoderType(int isHard) = 0;


	virtual ~IMeetingManage() = 0;
};

extern "C"
{
	/**
	*���� ����MeetingManage����
	*
	*@param cb			 [IN]�ص�����
	*@return ����  MeetingManage����
	*/
	MEETING_MANAGE_API IMeetingManage * CreateMeetingManageObject(IMeetingManageCB * cb);

	/**
	*���� ����MeetingManage���� 
	*
	*@param object	[IN]MeetingManage����
	*@return 
	*/
	MEETING_MANAGE_API void DestroyMeetingManageObject(IMeetingManage * object);



};

#endif
