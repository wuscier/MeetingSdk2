#ifndef _HOSTMANAGECALLBACK_H_
#define _HOSTMANAGECALLBACK_H_

#include "HostManageDefine.h"

class IHostManageCB
{
public:
	
	/*
	*  Start���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*  context�� �����Ĳ���
	*/
	virtual void OnStart(int statusCode, char * description,
		int descLen,  Context context) = 0;
	
	/*
	*  ��½host���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*  context�� �����Ĳ���
	*/
	virtual void OnConnectMeetingVDN(int statusCode, char * description,
		int descLen,  Context context) = 0;

	/*
	*  �����û���Ϣ���
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  description: ��Ӧ״̬�����ʾ
	*/
	virtual void OnSetAccountInfo(int StatusCode, char * description, int descLen) = 0;
	
	/*
	*  ������
	*  statusCode: ״̬�룬0 - �ɹ�  ��0 - ʧ��
	*  context�� �����Ĳ���
	*/
	virtual void OnMeetingInvite(int statusCode, Context context) = 0;
	
	/*
	*  �յ�����
	*  inviterAccountId�� ���������˵���Ѷ��
	*  inviterAccountName�� ���������˵��ǳ�
	*  meetingId�� ���������Ļ���ID
	*/
	virtual void OnMeetingInviteEvent(int inviterAccountId,
		char * inviterAccountName, int accountNameLen, int meetingId) = 0;

	/*
	*  �յ���ϵ���Ƽ���Ϣ
	*  info ��ϵ���Ƽ���Ϣ
	*/
	virtual void OnContactRecommendEvent(RecommendContactInfo & info) = 0;

	/*
	 *	��������֪ͨ
	 *  accountId: ���������û�����Ѷ�� 
	 *  token: ���������û���token
	 *  tokenLen: token����
	 */
	virtual void OnForcedOfflineEvent(int accountId, char * token, int tokenLen) = 0;
};

#endif