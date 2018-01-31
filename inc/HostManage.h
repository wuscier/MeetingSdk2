#ifndef _HOST_MANAGE_H_
#define _HOST_MANAGE_H_

#include "HostManageDefine.h"
#include "HostManageCallBack.h"

class HOST_MANAGE_API IHostManage
{
public:
	virtual ~IHostManage() = 0;

	/*
	 *  模块启动			【异步接口】
	 *  configPath: 配置文件路径
	 *  pathLen: 路径长度
	 *  context：上下文参数
	 *  返回值：0 –  成功；其他 – 失败
	 */
	virtual int Start(const char * devmodel, char * configPath,
		int pathLen, Context context) = 0;

	/*
	 *	模块停止			【同步接口】
	 *  返回值：0 - 成功；其他 - 失败
	 */
	virtual int Stop() = 0;

	/*
	*  连接host服务器	【异步接口】
	*  accountId: 视讯号
	*  accountName： 用户名
	*  nameLen：名称长度
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int ConnectMeetingVDN(int accountId, char * accountName,
		int nameLen, char * token, int tokenLen, Context context) = 0;

	/*
	 *	设置用户信息		【异步接口】
	 *	accountName：	用户昵称
	 *  nameLen：		昵称长度
	 *  返回值： 0 - 成功； 其他 - 失败
	 */
	virtual int SetAccountInfo(const char * accountName, int nameLen) = 0;

	/*
	*  连接host服务器（本进程会议实例启动后调用）	【异步接口】
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int ConnectMeetingVDNAfterMeetingInstStarted( Context context) = 0;
	/*
	*  断开与host连接	【同步接口】
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int DisConnectMeetingVDN() = 0;

	/*
	*  邀请参会(ConnectMeetingVDN 调用成功之后才可以调用这个接口)		【异步接口】
	*  meetingId: 会议ID
	*  accountIdList: 被邀请者视讯号列表
	*  accountSize: 被邀请者视讯号个数	
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int MeetingInvite(int meetingId, int * accountIdList,
		int accountSize) = 0;
};

extern "C"
{
	/**
	*描述 创建IHostManage对象
	*
	*@param cb			 [IN]回调对象
	*@return 返回  IHostManage对象
	*/
	HOST_MANAGE_API IHostManage * CreateHostManageObject(IHostManageCB * cb);

	/**
	*描述 销毁IHostManage对象 
	*
	*@param object	[IN]IHostManage对象
	*@return 
	*/
	HOST_MANAGE_API void DestroyHostManageObject(IHostManage * object);
};

#endif