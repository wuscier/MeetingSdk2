#ifndef _MEETINGMANAGECALLBACK_H_
#define _MEETINGMANAGECALLBACK_H_

#include "MeetingManageDefine.h"

class IMeetingManageCB
{
public:
	/*
	*  模块启动回调
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*  descLen： 描述长度
	*  context： 上下文参数
	*/
	virtual void OnStart(int statusCode, char * description,
		int descLen, Context context) = 0;

	/*
	*  登录结果回调
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*  descLen： 描述长度
	*  info:	 登录信息
	*  context： 上下文参数
	*/
	virtual void OnLogin(int statusCode, char * description,
		int descLen, LoginInfo info, Context context) = 0;
	
	/*
	*  绑定Token结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*  context： 上下文参数
	*/
	virtual void OnBindToken(int statusCode, char * description,
		int descLen, Context context) = 0;
	
	/*
	*  检查会议号是否存在结果
	*  statusCode: 状态码，0 - 会议存在  非0 - 会议不存在，错误码
	*  description: 对应状态码的提示
	*  context： 上下文参数
	*/
	virtual void OnCheckMeetExist(int statusCode, char * description,
		int descLen, Context context) = 0;
	
	/*
	*  取得会议列表结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  description: 对应状态码的提示
	*  meetList: 会议列表
	*  meetCount: 会议列表中的会议数
	*  context： 上下文参数
	*/
	virtual void OnGetMeetingList(int statusCode, char * description, int descLen,
		MeetingInfo * meetList, int meetCount, Context context) = 0;

	/*
	 *	重置会议密码结果
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 对应状态码的提示
	 *  descLen: 提示信息长度 
	 */
	virtual void OnResetMeetingPassword(int statusCode, char * description, int descLen) = 0;

	/*
	 *	获取会议密码结果
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 对应状态码的提示
	 *  descLen: 提示信息长度 
	 *  password: 密码
	 *  pwdLen: 密码长度
	 *  hostId: 主持人ID
	 *  hostIdLen: ID长度
	 */
	virtual void OnGetMeetingPassword(int statusCode,
		char * description, int descLen,
		char * password, int pwdLen,
		char * hostId, int hostIdLen) = 0;

	/*
	 *	查询会议是否有密码结果
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 对应状态码的提示
	 *  descLen: 提示信息长度
	 *  hasPassword: 是否有密码 1表示有密码，其他表示无密码
	 *  hostId: 主持人ID
	 *  hostIdLen: ID长度
	 */
	virtual void OnCheckMeetingHasPassword(int statusCode, char * description,
		int descLen, int hasPassword, char * hostId, int hostIdLen) = 0;

	/*
	 *	检查会议密码是否有效结果
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 对应状态码的提示
	 *  descLen: 提示信息长度 
	 */
	virtual void OnCheckMeetingPasswordValid(int statusCode, char * description,
		int descLen) = 0;
	
	/*
	*  创建会议结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  meetingId: 新创建的会议ID，statusCode 为0时有效
	*  meetType : 会议类型
	*  context： 上下文参数
	*/
	virtual void OnCreateMeeting(int statusCode,
		int meetingId, MM_MeetingType meetType, Context context) = 0;
	
	/*
	*  加入会议结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  meetingInfo: 会议信息，statusCode 为0时有效
	*  context： 上下文参数
	*/
	virtual void OnJoinMeeting(int statusCode, JoinMeetingInfo * meetingInfo, Context context) = 0;
	
	/*
	*  取得参会者列表通知
	*  participants: 参会者列表
	*  userCount: 参会人数
	*/
	virtual void OnGetUserList(ParticipantInfo * participants, int userCount) = 0;
	
	/*
	*  其他用户加入会议通知
	*  accountId: 新加入用户的视讯号
	*  accountName: 新加入用户的昵称
	*/
	virtual void OnUserJoinEvent(int accountId, char * accountName, int accoundNameLen) = 0;
	
	/*
	*  其他用户离开会议
	*  accountId: 离开用户的视讯号
	*  accountName: 离开用户的昵称
	*/
	virtual void OnUserLeaveEvent(int accountId, char * accountName, int accountNameLen) = 0;
	
	/*
	*  申请发言结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnAskForSpeak(int statusCode, Context context) = 0;
	
	/*
	*  服务器通知开始发言
	*  speakReason: 开始发言的原因，0发言者正常产生,1表示发言者由传麦产生,2表示发言者由主持人指定发言产生
	*  accountName: 原始操作人的名称，如果speakReason是1，该字段表示传麦者的名称；如果speakReason是2，该字段表示主持人的名称
	*/
	virtual void OnStartSpeakEvent(int speakReason ,
		char * accountName, int accountNameLen) = 0;
	
	/*
	*  服务器通知其他发言者产生
	*  speakReason: 开始发言的原因，0发言者正常产生,1表示发言者由传麦产生,2表示发言者由主持人指定发言产生
	*  accountName: 原始操作人的名称，如果speakReason是1，该字段表示传麦者的名称；如果speakReason是2，该字段表示主持人的名称
	*  newSpeakerAcountName：新发言者昵称
	*  newSpeakerAccountId: 新发言者视讯号
	*/
	virtual void OnUserStartSpeakEvent(int speakReason, char * accountName,
		int accountNameLen, char * newSpeakerAcountName, int newSpeakerAccountId) = 0;
		
	/*
	*  申请停止发言结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnAskForStopSpeak(int statusCode, Context context) = 0;
	
	/*
	*  服务器通知停止发言
	*  reason: 停止发言的原因，0发言者正常失效（自己申请停止，退出会议等）；1发言者失效由传麦动作产生。2发言者失效由主持人操作产生
	*  accountName: 原始操作人的名称，如果reason是1，该字段表示传麦者的名称；如果reason是2，该字段表示主持人的名称
	*/
	virtual void OnStopSpeakEvent(int reason, char * accountName,
		int accountNameLen, int accountNameID) = 0;
	
	/*
	*  服务器通知其他发言者停止发言
	*  reason: 停止发言的原因，0发言者正常失效（自己申请停止，退出会议等）；1发言者失效由传麦动作产生。2发言者失效由主持人操作产生
	*  accountName: 原始操作人的名称，如果reason是1，该字段表示传麦者的名称；如果reason是2，该字段表示主持人的名称
	*  speakerAcountName: 停止发言用户的昵称
	*  speakerAccountId: 停止发言用户的视讯号
	*/
	virtual void OnUserStopSpeakEvent(int reason, char * accountName, int accountNameLen,
		char * speakerAcountName, int speakerAccountNameLen, int speakerAccountId) = 0;
	
	/*
	*  发布摄像头视频结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
	*  context： 上下文参数
	*/
	virtual void OnPublishCameraVideo(int statusCode, int resourceId, Context context) = 0;

	/*
	*  其他用户发布摄像头视频
	*  resourceId： 资源ID
	*  syncId： 音视频同步ID
	*  accountId： 发布摄像头用户的视讯号
	*  accountName: 发布摄像头用户的昵称
	*  accountNameLen: 昵称长度
	*  extraInfo: 发布流扩展信息
	*  extraInfoLen: 扩展信息长度
	*/
	virtual void OnUserPublishCameraVideoEvent(int resourceId, int syncId,
		int accountId, char * accountName, int accountNameLen,
		char * extraInfo, int extraInfoLen) = 0;
	
	/*
	*  发布桌面采集视频结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
	*  context： 上下文参数
	*/
	virtual void OnPublishWinCaptureVideo(int statusCode, int resourceId, Context context) = 0;

	/*
	*  发布采集卡视频结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
	*  context： 上下文参数
	*/
	virtual void OnPublishDataCardVideo(int statusCode, int resourceId, Context context) = 0;

	/*
	*  其他用户发布文档视频（采集卡 或 桌面采集） 文档视频的媒体类型是一个，同时只能有一种是打开状态
	*  resourceId： 资源ID
	*  syncId： 音视频同步ID
	*  accountId： 发布文档流用户的视讯号
	*  accountName: 发布文档流用户的昵称
	*  accountNameLen: 昵称长度
	*  extraInfo: 发布流扩展信息
	*  extraInfoLen: 扩展信息长度
	*/
	virtual void OnUserPublishDataVideoEvent(int resourceId, int syncId,
		int accountId, char * accountName, int accountNameLen,
		char * extraInfo, int extraInfoLen) = 0;
	
	
	/*
	*  发布麦克风音频结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
	*  context： 上下文参数
	*/
	virtual void OnPublishMicAudio(int statusCode, int resourceId, Context context) = 0;

	/*
	*  其他用户发布麦克风音频
	*  resourceId： 资源ID
	*  syncId： 音视频同步ID
	*  accountId： 发布麦克风音频用户的视讯号
	*  accountName: 发布麦克风音频用户的昵称
	*  accountNameLen: 昵称长度
	*  extraInfo: 发布流扩展信息
	*  extraInfoLen: 扩展信息长度
	*/
	virtual void OnUserPublishMicAudioEvent(int resourceId, int syncId, int accountId,
		char * accountName, int accoundNameLen,
		char * extraInfo, int extraInfoLen) = 0;
	
	/*
	*  取消摄像头视频发布结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnUnPublishCameraVideo(int statusCode, Context context) = 0;

	/*
	*  取消本地桌面分享发布结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnUnPublishWinCaptureVideo(int statusCode, Context context) = 0;

	/*
	*  取消采集卡视频发布结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnUnPublishDataCardVideo(int statusCode, Context context) = 0;

	/*
	*  取消音频发布结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnUnPublishMicAudio(int statusCode, Context context) = 0;
	
	/*
	*  其他用户取消发布摄像头视频流
	*  resourceId： 资源ID
	*  accountId： 取消发布流用户的视讯号
	*  accountName: 取消发布流用户的昵称
	*/
	virtual void OnUserUnPublishCameraVideoEvent(int resourceId, int accountId,
		char * accountName, int accountNameLen) = 0;

	/*
	*  其他用户取消发布文档流
	*  resourceId： 资源ID
	*  accountId： 取消发布流用户的视讯号
	*  accountName: 取消发布流用户的昵称
	*/
	virtual void OnUserUnPublishDataCardVideoEvent(int resourceId, int accountId,
		char * accountName, int accountNameLen) = 0;

	/*
	*  其他用户取消发布音频流
	*  resourceId： 资源ID
	*  accountId： 取消发布流用户的视讯号
	*  accountName: 取消发布流用户的昵称
	*/
	virtual void OnUserUnPublishMicAudioEvent(int resourceId, int accountId,
		char * accountName, int accountNameLen) = 0;
	
	/*
	*  回调YUV数据
	*  accountId： YUV数据所属用户的视讯号
	*  resourceId: 资源ID
	*  yuvBuffer： YUV数据存储的buffer
	*  yuvBufferSize： YUV数据长度
	*  width： 视频的宽度
	*  height： 视频的高度
	*  orientation: 视频方向回调值 
	*/
	virtual void OnYUVData(int accountId,int resourceId, char * yuvBuffer,
		int yuvBufferSize, int width, int height, int orientation) = 0;

	/*
	 *	主持人改变当前会议模式结果(主持人才能收到)
	 *  statusCode : 状态码，0 - 成功  非0 - 失败
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnHostChangeMeetingMode(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	主持人改变当前会议模式通知
	 *  meetingStyle : 当前会议模式
	 */
	virtual void OnHostChangeMeetingModeEvent(int meetingStyle) = 0;

	/*
	 *	主持人踢出用户结果
	 *  statusCode : 状态码，0 - 成功  非0 - 失败
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnHostKickoutUser(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	主持人踢出用户通知
	 *  meetId : 会议号
	 *  kickedUserId : 被踢出用户视讯号
	 *  descLen : 视讯号长度
	 */
	virtual void OnHostKickoutUserEvent(int meetId,
		char * kickedUserId, int idLen) = 0;

	/*
	 *	用户举手请求结果
	 *  statusCode : 状态码，0 - 成功  非0 - 失败
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnRaiseHandReq(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	收到用户举手请求通知（主持人收到）
	 *  accountId: 举手用户ID
	 *  accountName: 举手用户名称
	 *  nameLen: 名称长度
	 */
	virtual void OnRaiseHandReqEvent(int accountId, char * accountName, int nameLen) = 0;

	/*
	 *	会议加锁解锁申请请求结果
	 *  statusCode : 状态码，0 - 成功  非0 - 失败
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnAskForMeetingLock(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	主持人指定用户发言结果
	 *  statusCode : 状态码，0 - 成功  非0 - 失败
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnHostOrderOneSpeak(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	主持人指定用户停止发言结果
	 *  statusCode : 状态码，0 - 成功  非0 - 失败
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnHostOrderOneStopSpeak(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	会议锁定状态改变通知
	 *  statusCode : 锁定状态码，0 - 解锁  1 - 加锁
	 *  description : 描述信息
	 *  descLen : 描述信息长度
	 */
	virtual void OnLockStatusChangedEvent(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	SDK异常通知
	 *  exceptionType: 异常类型
	 *  exceptionDesc: 异常描述信息
	 *  descLen: 描述信息长度
	 *  extraInfo: 扩展信息
	 *  infoLen: 扩展信息长度
	 */
	virtual void OnMeetingManageExecptionEvent(SDKExceptionType exceptionType,
		char * exceptionDesc, int descLen, char * extraInfo, int infoLen) = 0;

	/*
	 *	设备状态变化通知
	 *  type : 设备状态变化类型
	 *  devName ： 设备名称
	 *  nameLen ： 名称长度
	 */
	virtual void OnDeviceStatusEvent(DevStatusChangeType type,
		char * devName, int nameLen) = 0;

	/*
	 *	网络探测结果
	 *  type: 网络探测类型
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 描述信息
	 *  descLen: 描述信息长度
	 *  
	 */
	virtual void OnNetDiagnosticCheck(NetDiagnosticType type, int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	摄像头测试启动结果
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 描述信息
	 *  descLen: 描述信息长度
	 *
	 */
	virtual void OnPlayVideoTest(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	播放音频检测结果
	 *  statusCode: 状态码，0 - 成功  非0 - 失败
	 *  description: 描述信息
	 *  descLen: 描述信息长度
	 *
	 */
	virtual void OnPlaySoundTest(int statusCode,
		char * description, int descLen) = 0;

	/*
	 *	透传消息接收通知
	 *  accountId:	发送透传消息的视讯号
	 *  data:		透传消息内容
	 *  dataLen:	消息长度
	 */
	virtual void OnTransparentMsgEvent(int accountId, char * data, int dataLen) = 0;

	/*
	 *	传麦响应命令
	 *  statusCode:	错误码
	 */
	virtual void OnMicSendResponse(int statusCode) = 0;
	/**
	* 网络状况等级
	*
	* @see lossrateLevel_NULL = 0;		//没有发言人 
	* @see lossrateLevel_one = 1;		//等级1
	* @see lossrateLevel_two = 2;		//等级2
	* @see lossrateLevel_three = 3;		//等级3
	* @see lossrateLevel_four =4;		//等级4
	*
	*/
	virtual void OnNetworkStatusLevelNoticeEvent(int netlevel) = 0;

	/**
	* 设备丢失回调 ,
	* android设备底层媒体管理库不支持设备列表等设备管理功能
	* 没有设备恢复等说法。所以底层以流id通知上来设备丢失，则直接通知UI了 。
	* 只针对于 手机、M1S。
	* @param  resourceid 丢失设备对应的资源id
	*
	*/
	virtual void OnDeviceLostNoticeEvent(int accountid, int resourceid ) = 0;

	/*
	 *	用户信息修改回调
	 *  statusCode : 状态码，0 - 成功，其他 - 失败
	 */
	virtual void OnModifyNickName(int statusCode,
		const char * desc, int descLen, Context context) = 0;


	/*
	 *	MeetingSDK回调通知
	 *  statusCode: 状态码，详见MEETINGMANAGE_SMSDK_CBTYPE
	 *  description: 描述信息
	 *  descLen: 描述信息长度
	 */
	virtual void OnMeetingSDKCallback(MEETINGMANAGE_SMSDK_CBTYPE e, char * description, int descLen) = 0;

		/*
	 *  获取会议详细信息会调
	 *  statusCode: 状态码，0 - 成功，其他 - 失败
	 *  description: 返回值描述信息
	 *  descLen: 描述信息长度
	 *  meetInfo: 会议详细信息
	 *  context: 上下文参数
	 */
	virtual void OnGetMeetingInfo(int statusCode, char * description,
		int descLen,MeetingInfo &meetInfo, Context context) = 0;

	/*
     *  增加参会人员会调
     */
    virtual void OnModifyMeetingInviters(int statusCode, char * description,
		int descLen, Context context) = 0;

	/*
     *  主持人指定打开/关闭麦克风、扬声器、摄像头 结果
     */
	virtual void OnHostOrderOneDoOpration(int statusCode, char * description, 
		int descLen, Context context) = 0;

	/*
     *  收到主持人指定打开/关闭麦克风、扬声器、摄像头 事件通知
     */
	virtual void OnHostOrderDoOpratonEvent(int opType) = 0;

	/*
     *  收到其他参会者打开/关闭麦克风、扬声器、摄像头 事件通知
     */
	virtual void OnOtherChangeAudioSpeakerStatusEvent(int accountId, int opType) = 0;

	/*
     *  获取微信分享的分享内容结果
     */
	virtual void OnGetMeetingInvitationSMS(int statusCode, char* description, int descLen, 
		char* invitationSMS, int smsLen, char* yyURL, int urlLen, Context context) = 0;

	virtual void OnSendASpeakerStatus(int statusCode, char * description, 
		int descLen, Context context) = 0;


	/*
	*  订阅视频结果
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*  context： 上下文参数
	*/
	virtual void OnSubscribrVideo(int accountid,int statusCode) = 0;

	/*
	*  发送UI 消息
	*  statusCode: 状态码，0 - 成功  非0 - 失败
	*/
	virtual void OnSendUIMsgRespone(int status)=0;

	/*
	*  收到UI消息
	*  msgid: 消息id
	*  srcUserid: 发送方视讯号
	*  msg:消息
	*/
	virtual void OnRecvUImsgEvent(int msgid, int srcUserid, char* msg, int msgLen) = 0;
};

#endif