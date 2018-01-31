#ifndef _HOSTMANAGECALLBACK_H_
#define _HOSTMANAGECALLBACK_H_

#include "HostManageDefine.h"

class IHostManageCB
{
public:
	
	/*
	*  Start结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*  context： 上下文参数
	*/
	virtual void OnStart(int statusCode, char * description,
		int descLen,  Context context) = 0;
	
	/*
	*  登陆host结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*  context： 上下文参数
	*/
	virtual void OnConnectMeetingVDN(int statusCode, char * description,
		int descLen,  Context context) = 0;

	/*
	*  设置用户信息结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*/
	virtual void OnSetAccountInfo(int StatusCode, char * description, int descLen) = 0;
	
	/*
	*  邀请结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnMeetingInvite(int statusCode, Context context) = 0;
	
	/*
	*  收到邀请
	*  inviterAccountId： 发送邀请人的视讯号
	*  inviterAccountName： 发送邀请人的昵称
	*  meetingId： 被邀请加入的会议ID
	*/
	virtual void OnMeetingInviteEvent(int inviterAccountId,
		char * inviterAccountName, int accountNameLen, int meetingId) = 0;

	/*
	*  收到联系人推荐信息
	*  info 联系人推荐信息
	*/
	virtual void OnContactRecommendEvent(RecommendContactInfo & info) = 0;

	/*
	 *	被迫下线通知
	 *  accountId: 被迫下线用户的视讯号 
	 *  token: 被迫下线用户的token
	 *  tokenLen: token长度
	 */
	virtual void OnForcedOfflineEvent(int accountId, char * token, int tokenLen) = 0;
};

#endif