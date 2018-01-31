#ifndef _HOST_MANAGE_H_
#define _HOST_MANAGE_H_

#include "HostManageDefine.h"
#include "HostManageCallBack.h"

class HOST_MANAGE_API IHostManage
{
public:
	virtual ~IHostManage() = 0;

	/*
	 *  ģ������			���첽�ӿڡ�
	 *  configPath: �����ļ�·��
	 *  pathLen: ·������
	 *  context�������Ĳ���
	 *  ����ֵ��0 �C  �ɹ������� �C ʧ��
	 */
	virtual int Start(const char * devmodel, char * configPath,
		int pathLen, Context context) = 0;

	/*
	 *	ģ��ֹͣ			��ͬ���ӿڡ�
	 *  ����ֵ��0 - �ɹ������� - ʧ��
	 */
	virtual int Stop() = 0;

	/*
	*  ����host������	���첽�ӿڡ�
	*  accountId: ��Ѷ��
	*  accountName�� �û���
	*  nameLen�����Ƴ���
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int ConnectMeetingVDN(int accountId, char * accountName,
		int nameLen, char * token, int tokenLen, Context context) = 0;

	/*
	 *	�����û���Ϣ		���첽�ӿڡ�
	 *	accountName��	�û��ǳ�
	 *  nameLen��		�ǳƳ���
	 *  ����ֵ�� 0 - �ɹ��� ���� - ʧ��
	 */
	virtual int SetAccountInfo(const char * accountName, int nameLen) = 0;

	/*
	*  ����host�������������̻���ʵ����������ã�	���첽�ӿڡ�
	*  context: �����Ĳ���
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int ConnectMeetingVDNAfterMeetingInstStarted( Context context) = 0;
	/*
	*  �Ͽ���host����	��ͬ���ӿڡ�
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int DisConnectMeetingVDN() = 0;

	/*
	*  ����λ�(ConnectMeetingVDN ���óɹ�֮��ſ��Ե�������ӿ�)		���첽�ӿڡ�
	*  meetingId: ����ID
	*  accountIdList: ����������Ѷ���б�
	*  accountSize: ����������Ѷ�Ÿ���	
	*  ����ֵ��0 �C  �ɹ������� �C ʧ��
	*/
	virtual int MeetingInvite(int meetingId, int * accountIdList,
		int accountSize) = 0;
};

extern "C"
{
	/**
	*���� ����IHostManage����
	*
	*@param cb			 [IN]�ص�����
	*@return ����  IHostManage����
	*/
	HOST_MANAGE_API IHostManage * CreateHostManageObject(IHostManageCB * cb);

	/**
	*���� ����IHostManage���� 
	*
	*@param object	[IN]IHostManage����
	*@return 
	*/
	HOST_MANAGE_API void DestroyHostManageObject(IHostManage * object);
};

#endif