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

// HOST错误返回类型
enum HostErrorCode {
	HOST_ERRCODE_SUCCEED						= 0,	// 成功
	HOST_ERRCODE_FAILED						= -1000,	// 失败（通用）
	HOST_ERRCODE_INVALID_ARGUMENTS			= -1001,	// 无效参数
	HOST_ERRCODE_NETWORK_DISCONNECT			= -1002,    // 网络异常
	HOST_ERRCODE_OTHER_TASK_DOING			= -1003,	// 其他任务进行中
	HOST_ERRCODE_SEND_REQUEST_FAILED		= -1004,	// 发送请求到服务器失败（通用）
	HOST_ERRCODE_GET_NPSREQUEST_FAILED		= -1006,    // 发送获取NPS配置请求失败
	HOST_ERRCODE_HOSTAGENT_START_FAILED     = -1007,	// 启动HostAgent失败
	HOST_ERRCODE_HOSTAGENT_REGISTER_FAILED  = -1008,	// 注册HostAgent失败
};

typedef struct _RecommendContactInfo
{
	int m_sourceId;									//推荐人
	char m_contactListId[CONTACTLIST_ID_LEN];		//通讯录id
	char m_contactListName[CONTACTLIST_NAME_LEN];	//通讯录名称
	int			m_contactListVer;					//通讯录版本
	int			m_contacterNum;						//联系人个数 
}RecommendContactInfo;

#endif