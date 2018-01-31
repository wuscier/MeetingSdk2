#ifndef _HOSTMANAGEDEFINE_H_
#define _HOSTMANAGEDEFINE_H_

#ifdef WIN32

#ifdef HOSTMANAGE_EXPORTS
#define HOST_MANAGE_API  __declspec(dllexport)
#else
#define HOST_MANAGE_API  __declspec(dllimport)
//#pragma comment (lib, "hostmanage.lib")
#endif
#else
#define HOST_MANAGE_API
#endif

typedef void * Context;

#define CONTACTLIST_ID_LEN 256
#define CONTACTLIST_NAME_LEN 256

// HOST���󷵻�����
enum HostErrorCode {
	HOST_ERRCODE_SUCCEED						= 0,	// �ɹ�
	HOST_ERRCODE_FAILED						= -1000,	// ʧ�ܣ�ͨ�ã�
	HOST_ERRCODE_INVALID_ARGUMENTS			= -1001,	// ��Ч����
	HOST_ERRCODE_NETWORK_DISCONNECT			= -1002,    // �����쳣
	HOST_ERRCODE_OTHER_TASK_DOING			= -1003,	// �������������
	HOST_ERRCODE_SEND_REQUEST_FAILED		= -1004,	// �������󵽷�����ʧ�ܣ�ͨ�ã�
	HOST_ERRCODE_GET_NPSREQUEST_FAILED		= -1006,    // ���ͻ�ȡNPS��������ʧ��
	HOST_ERRCODE_HOSTAGENT_START_FAILED     = -1007,	// ����HostAgentʧ��
	HOST_ERRCODE_HOSTAGENT_REGISTER_FAILED  = -1008,	// ע��HostAgentʧ��
};

typedef struct _RecommendContactInfo
{
	int m_sourceId;									//�Ƽ���
	char m_contactListId[CONTACTLIST_ID_LEN];		//ͨѶ¼id
	char m_contactListName[CONTACTLIST_NAME_LEN];	//ͨѶ¼����
	int			m_contactListVer;					//ͨѶ¼�汾
	int			m_contacterNum;						//��ϵ�˸��� 
}RecommendContactInfo;

#endif