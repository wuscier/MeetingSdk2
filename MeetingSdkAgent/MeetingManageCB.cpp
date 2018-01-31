#include "MeetingManageCB.h"
#include "MeetingSdkClient.h"
//#include <iostream>

// 构造函数
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
*  模块启动回调
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  descLen： 描述长度
*  context： 上下文参数
*/
void MeetingManageCB::OnStart(int statusCode, char * description, int descLen, Context context)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(start, &startResult, 0, 0);
}

/*
*  登录结果回调
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  descLen： 描述长度
*  info:	 登录信息
*  context： 上下文参数
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
*  绑定Token结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  context： 上下文参数
*/
void MeetingManageCB::OnBindToken(int statusCode, char * description, int descLen, Context context)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(bind_token, &result, 0, 0);
}

/*
*  检查会议号是否存在结果
*  statusCode: 状态码，0 - 会议存在  非0 - 会议不存在，错误码
*  description: 对应状态码的提示
*  context： 上下文参数
*/
void MeetingManageCB::OnCheckMeetExist(int statusCode, char * description, int descLen, Context context)
{
		AsyncCallResult result;
		result.m_statusCode = statusCode;
		strcpy_s(result.m_message, sizeof(result.m_message), description);
		cb(check_meeting_exist, &result, 0, context);
}

/*
*  取得会议列表结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  meetList: 会议列表
*  meetCount: 会议列表中的会议数
*  context： 上下文参数
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
*	重置会议密码结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  descLen: 提示信息长度
*/
void MeetingManageCB::OnResetMeetingPassword(int statusCode, char * description, int descLen)
{
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
	cb(reset_meeting_password, &startResult, 0, 0);
}

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
*	查询会议是否有密码结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  descLen: 提示信息长度
*  hasPassword: 是否有密码 1表示有密码，其他表示无密码
*  hostId: 主持人ID
*  hostIdLen: ID长度
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
*	检查会议密码是否有效结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 对应状态码的提示
*  descLen: 提示信息长度
*/
void MeetingManageCB::OnCheckMeetingPasswordValid(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);
	cb(check_meeting_password_valid, &result, 0, 0);
}

/*
*  创建会议结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  meetingId: 新创建的会议ID，statusCode 为0时有效
*  meetType : 会议类型
*  context： 上下文参数
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
*  加入会议结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  meetingInfo: 会议信息，statusCode 为0时有效
*  context： 上下文参数
*/
void MeetingManageCB::OnJoinMeeting(int statusCode, JoinMeetingInfo * meetingInfo, Context context)
{
		JoinMeetingResult joinMeetingResult;
		joinMeetingResult.m_statusCode = statusCode;
		joinMeetingResult.m_joinMeetingInfo = meetingInfo;
		cb(join_meeting, &joinMeetingResult, 0, 0);
}

/*
*  取得参会者列表通知
*  participants: 参会者列表
*  userCount: 参会人数
*/
void MeetingManageCB::OnGetUserList(ParticipantInfo * participants, int userCount)
{

}

/*
*  其他用户加入会议通知
*  accountId: 新加入用户的视讯号
*  accountName: 新加入用户的昵称
*/
void MeetingManageCB::OnUserJoinEvent(int accountId, char * accountName, int accoundNameLen)
{
		AttendeeInfo attendeeInfo;
		attendeeInfo.m_accountId = accountId;
		strcpy_s(attendeeInfo.m_accountName, sizeof(attendeeInfo.m_accountName), accountName);

		cb(user_join_event, &attendeeInfo, 0, 0);
}

/*
*  其他用户离开会议
*  accountId: 离开用户的视讯号
*  accountName: 离开用户的昵称
*/
void MeetingManageCB::OnUserLeaveEvent(int accountId, char * accountName, int accountNameLen)
{
		AttendeeInfo attendeeInfo;
		attendeeInfo.m_accountId = accountId;
		strcpy_s(attendeeInfo.m_accountName, sizeof(attendeeInfo.m_accountName), accountName);

		cb(user_leave_event, &attendeeInfo, 0, 0);
}

/*
*  申请发言结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  context： 上下文参数
*/
void MeetingManageCB::OnAskForSpeak(int statusCode, Context context) 
{
		AsyncCallResult asyncCallResult;
		asyncCallResult.m_statusCode = statusCode;

		cb(ask_for_speak, &asyncCallResult, 0, 0);
}

/*
*  服务器通知开始发言
*  speakReason: 开始发言的原因，0发言者正常产生,1表示发言者由传麦产生,2表示发言者由主持人指定发言产生
*  accountName: 原始操作人的名称，如果speakReason是1，该字段表示传麦者的名称；如果speakReason是2，该字段表示主持人的名称
*/
void MeetingManageCB::OnStartSpeakEvent(int speakReason, char * accountName, int accountNameLen)
{
		SpeakResult startSpeakResult;
		startSpeakResult.m_speakReason = speakReason;
		strcpy_s(startSpeakResult.m_accountName, sizeof(startSpeakResult.m_accountName), accountName);

		cb(start_speak_event, &startSpeakResult, 0, 0);
}

/*
*  服务器通知其他发言者产生
*  speakReason: 开始发言的原因，0发言者正常产生,1表示发言者由传麦产生,2表示发言者由主持人指定发言产生
*  accountName: 原始操作人的名称，如果speakReason是1，该字段表示传麦者的名称；如果speakReason是2，该字段表示主持人的名称
*  newSpeakerAcountName：新发言者昵称
*  newSpeakerAccountId: 新发言者视讯号
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
*  申请停止发言结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  context： 上下文参数
*/
void MeetingManageCB::OnAskForStopSpeak(int statusCode, Context context)
{
		AsyncCallResult asyncCallResult;
		asyncCallResult.m_statusCode = statusCode;

		cb(ask_for_stop_speak, &asyncCallResult, 0, 0);
}

/*
*  服务器通知停止发言
*  reason: 停止发言的原因，0发言者正常失效（自己申请停止，退出会议等）；1发言者失效由传麦动作产生。2发言者失效由主持人操作产生
*  accountName: 原始操作人的名称，如果reason是1，该字段表示传麦者的名称；如果reason是2，该字段表示主持人的名称
*/
void MeetingManageCB::OnStopSpeakEvent(int reason, char * accountName, int accountNameLen, int accountNameID)
{
		SpeakResult stopSpeakResult;
		stopSpeakResult.m_speakReason = reason;
		strcpy_s(stopSpeakResult.m_accountName, sizeof(stopSpeakResult.m_accountName), accountName);

		cb(stop_speak_event, &stopSpeakResult, 0, 0);
}

/*
*  服务器通知其他发言者停止发言
*  reason: 停止发言的原因，0发言者正常失效（自己申请停止，退出会议等）；1发言者失效由传麦动作产生。2发言者失效由主持人操作产生
*  accountName: 原始操作人的名称，如果reason是1，该字段表示传麦者的名称；如果reason是2，该字段表示主持人的名称
*  speakerAcountName: 停止发言用户的昵称
*  speakerAccountId: 停止发言用户的视讯号
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
*  发布摄像头视频结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
*  context： 上下文参数
*/
void MeetingManageCB::OnPublishCameraVideo(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_camera_video, &publishStreamResult, 0, context);
}

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
*  发布桌面采集视频结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
*  context： 上下文参数
*/
void MeetingManageCB::OnPublishWinCaptureVideo(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_win_capture_video, &publishStreamResult, 0, context);
}

/*
*  发布采集卡视频结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
*  context： 上下文参数
*/
void MeetingManageCB::OnPublishDataCardVideo(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_data_card_video, &publishStreamResult, 0, context);
}

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
*  发布麦克风音频结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  resourceId: 资源ID, 大于或等于0 - 可用资源ID 小于0 - 无效资源ID
*  context： 上下文参数
*/
void MeetingManageCB::OnPublishMicAudio(int statusCode, int resourceId, Context context)
{
		PublishStreamResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;
		publishStreamResult.m_streamId = resourceId;

		cb(publish_mic_audio, &publishStreamResult, 0, context);
}

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
*  取消摄像头视频发布结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  context： 上下文参数
*/
void MeetingManageCB::OnUnPublishCameraVideo(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_camera_video, &publishStreamResult, 0, context);
}

/*
*  取消本地桌面分享发布结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  context： 上下文参数
*/
void MeetingManageCB::OnUnPublishWinCaptureVideo(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_win_capture_video, &publishStreamResult, 0, context);
}

/*
*  取消采集卡视频发布结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  context： 上下文参数
*/
void MeetingManageCB::OnUnPublishDataCardVideo(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_data_card_video, &publishStreamResult, 0, context);
}

/*
*  取消音频发布结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  context： 上下文参数
*/
void MeetingManageCB::OnUnPublishMicAudio(int statusCode, Context context)
{
		AsyncCallResult publishStreamResult;
		publishStreamResult.m_statusCode = statusCode;

		cb(unpublish_mic_audio, &publishStreamResult, 0, context);
}

/*
*  其他用户取消发布摄像头视频流
*  resourceId： 资源ID
*  accountId： 取消发布流用户的视讯号
*  accountName: 取消发布流用户的昵称
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
*  其他用户取消发布文档流
*  resourceId： 资源ID
*  accountId： 取消发布流用户的视讯号
*  accountName: 取消发布流用户的昵称
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
*  其他用户取消发布音频流
*  resourceId： 资源ID
*  accountId： 取消发布流用户的视讯号
*  accountName: 取消发布流用户的昵称
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
*  回调YUV数据
*  accountId： YUV数据所属用户的视讯号
*  resourceId: 资源ID
*  yuvBuffer： YUV数据存储的buffer
*  yuvBufferSize： YUV数据长度
*  width： 视频的宽度
*  height： 视频的高度
*  orientation: 视频方向回调值
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
*	主持人改变当前会议模式结果(主持人才能收到)
*  statusCode : 状态码，0 - 成功  非0 - 失败
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnHostChangeMeetingMode(int statusCode, char * description, int descLen)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(host_change_meeting_mode, &startResult, 0, 0);
}

/*
*	主持人改变当前会议模式通知
*  meetingStyle : 当前会议模式
*/
void MeetingManageCB::OnHostChangeMeetingModeEvent(int meetingStyle)
{

		cb(host_change_meeting_mode_event, &meetingStyle, 0, 0);
}

/*
*	主持人踢出用户结果
*  statusCode : 状态码，0 - 成功  非0 - 失败
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnHostKickoutUser(int statusCode, char * description, int descLen)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(host_kickout_user, &startResult, 0, 0);
}

/*
*	主持人踢出用户通知
*  meetId : 会议号
*  kickedUserId : 被踢出用户视讯号
*  descLen : 视讯号长度
*/
void MeetingManageCB::OnHostKickoutUserEvent(int meetId, char * kickedUserId, int idLen)
{
		KickoutUserData data;
		data.meetingId = meetId;
		strcpy_s(data.kickedUserId, MEETINGMANAGE_USERID_LEN, kickedUserId);

		cb(host_kickout_user_event, &data, 0, 0);
}

/*
*	用户举手请求结果
*  statusCode : 状态码，0 - 成功  非0 - 失败
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnRaiseHandReq(int statusCode, char * description, int descLen)
{
		AsyncCallResult startResult;
		startResult.m_statusCode = statusCode;
		strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
		cb(raise_hand_request, &startResult, 0, 0);
}

/*
*	收到用户举手请求通知（主持人收到）
*  accountId: 举手用户ID
*  accountName: 举手用户名称
*  nameLen: 名称长度
*/
void MeetingManageCB::OnRaiseHandReqEvent(int accountId, char * accountName, int nameLen)
{
	AttendeeInfo info;
	info.m_accountId = accountId;
	strcpy_s(info.m_accountName, MEETINGMANAGE_USERNAME_LEN, accountName);

	cb(raise_hand_request_event, &info, 0, 0);
}

/*
*	会议加锁解锁申请请求结果
*  statusCode : 状态码，0 - 成功  非0 - 失败
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnAskForMeetingLock(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(ask_for_meeting_lock, &result, 0, 0);
}

/*
*	主持人指定用户发言结果
*  statusCode : 状态码，0 - 成功  非0 - 失败
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnHostOrderOneSpeak(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(host_order_one_speak, &result, 0, 0);
}

/*
*	主持人指定用户停止发言结果
*  statusCode : 状态码，0 - 成功  非0 - 失败
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnHostOrderOneStopSpeak(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(host_order_one_stop_speak, &result, 0, 0);
}

/*
*	会议锁定状态改变通知
*  statusCode : 锁定状态码，0 - 解锁  1 - 加锁
*  description : 描述信息
*  descLen : 描述信息长度
*/
void MeetingManageCB::OnLockStatusChangedEvent(int statusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);

	cb(lock_status_changed_event, &result, 0, 0);
}

/*
*	SDK异常通知
*  exceptionType: 异常类型
*  exceptionDesc: 异常描述信息
*  descLen: 描述信息长度
*  extraInfo: 扩展信息
*  infoLen: 扩展信息长度
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
*	设备状态变化通知
*  type : 设备状态变化类型
*  devName ： 设备名称
*  nameLen ： 名称长度
*/
void MeetingManageCB::OnDeviceStatusEvent(DevStatusChangeType type, char * devName, int nameLen)
{
	DeviceStatusResult deviceStatusResult;

	deviceStatusResult.type = type;
	strcpy_s(deviceStatusResult.devName, sizeof(deviceStatusResult.devName), devName);

	cb(device_status_event, &deviceStatusResult, 0, 0);
}

/*
*	网络探测结果
*  type: 网络探测类型
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 描述信息
*  descLen: 描述信息长度
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
*	摄像头测试启动结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 描述信息
*  descLen: 描述信息长度
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
*	播放音频检测结果
*  statusCode: 状态码，0 - 成功  非0 - 失败
*  description: 描述信息
*  descLen: 描述信息长度
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
*	透传消息接收通知
*  accountId:	发送透传消息的视讯号
*  data:		透传消息内容
*  dataLen:	消息长度
*/
void MeetingManageCB::OnTransparentMsgEvent(int accountId, char * data, int dataLen)
{
	TransparentMsg msg;
	msg.senderAccountId = accountId;
	strcpy_s(msg.data, sizeof(msg.data), data);

	cb(transparent_msg_event, &msg, 0, 0);
}

/*
*	传麦响应命令
*  statusCode:	错误码
*/
void MeetingManageCB::OnMicSendResponse(int statusCode)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), "");
	cb(mic_send_response, &result, 0, 0);
}

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
void MeetingManageCB::OnNetworkStatusLevelNoticeEvent(int netlevel)
{
	NetLevelResult result;
	result.NetLevel = netlevel;

	cb(network_status_level_notice_event, &result, 0, 0);
}

/**
* 设备丢失回调 ,
* android设备底层媒体管理库不支持设备列表等设备管理功能
* 没有设备恢复等说法。所以底层以流id通知上来设备丢失，则直接通知UI了 。
* 只针对于 手机、M1S。
* @param  resourceid 丢失设备对应的资源id
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
*	MeetingSDK回调通知
*  statusCode: 状态码，详见MEETINGMANAGE_SMSDK_CBTYPE
*  description: 描述信息
*  descLen: 描述信息长度
*/
void MeetingManageCB::OnMeetingSDKCallback(MEETINGMANAGE_SMSDK_CBTYPE e, char * description, int descLen)
{
	SdkCallbackResult sdkCallbackResult;
	sdkCallbackResult.type = e;
	strcpy_s(sdkCallbackResult.description, sizeof(sdkCallbackResult.description), description);

	cb(meeting_sdk_callback, &sdkCallbackResult, 0, 0);
}

/*
*	用户信息修改回调
*  statusCode : 状态码，0 - 成功，其他 - 失败
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
*  获取会议详细信息会调
*  statusCode: 状态码，0 - 成功，其他 - 失败
*  description: 返回值描述信息
*  descLen: 描述信息长度
*  meetInfo: 会议详细信息
*  context: 上下文参数
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
*  增加参会人员会调
*/
void MeetingManageCB::OnModifyMeetingInviters(int statusCode, char * description,
	int descLen, Context context) {
	AsyncCallResult startResult;
	startResult.m_statusCode = statusCode;
	strcpy_s(startResult.m_message, sizeof(startResult.m_message), description);
	cb(modify_meeting_inviters, &startResult, 0, 0);
}

/*
*  主持人指定打开/关闭麦克风、扬声器、摄像头 结果
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
*  收到主持人指定打开/关闭麦克风、扬声器、摄像头 事件通知
*/
void MeetingManageCB::OnHostOrderDoOpratonEvent(int opType)
{
	HostOrderDoOpratonResult opTypeResult;
	opTypeResult.OperationType = opType;
	cb(host_order_do_opraton_event, &opTypeResult, 0, 0);
}

/*
*  收到其他参会者打开/关闭麦克风、扬声器、摄像头 事件通知
*/
void MeetingManageCB::OnOtherChangeAudioSpeakerStatusEvent(int accountId, int opType)
{
	OtherChangeAudioSpeakerStatusData data;
	data.accountId = accountId;
	data.opType = opType;
	cb(other_change_audio_speaker_status_event, &data, 0, 0);
}

/*
*  获取微信分享的分享内容结果
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
