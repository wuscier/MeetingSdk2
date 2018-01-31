#ifndef _MEETINGMANAGE_H_
#define _MEETINGMANAGE_H_

#include "MeetingManageDefine.h"
#include "MeetingManageCallBack.h"
#include <cstddef>

class MEETING_MANAGE_API IMeetingManage
{
public:

	/*********************** 模块管理接口 ************************************/
	/*
	 *  模块启动			【异步接口】
	 * 　devmodel :　终端类型
	 *  configPath: 配置文件路径
	 *  pathLen: 路径长度
	 *  context：上下文参数
	 *  返回值：0 –  成功；其他 – 失败
	 */
	virtual int Start(const char * devmodel, char * configPath, int pathLen, Context context) = 0;

	/*
	 *  设置硬编解码库路径（android版本使用）  【同步接口】
	 *   rkPath : 硬编解码库路径
	 *   pathLen: 路径长度
	 *  返回值：0 –  成功；其他 – 失败
	 */
	virtual int SetRkPath(const char* rkPath, int pathLen) = 0;

	/*
	 *	NPS服务器地址设置	 【同步接口】
	 *  注：该接口设置NPS地址仅在Start调用之前生效
	 *  npsUrlList : NPS地址的URL数组
	 *  urlSize    : NPS数组长度
	 *  返回值 : 0 - 成功；其他 - 失败
	 */
	virtual int SetNpsUrl(char ** npsUrlList, int urlSize) = 0;

	/*
	 *	模块停止			【同步接口】
	 *  返回值：0 - 成功；其他 - 失败
	 */
	virtual int Stop() = 0;

	/************************** 鉴权相关接口 *********************************/
	/*
	*  登录(在SDK中做鉴权，调用此接口不需要再调用BindToken)		【异步接口】
	*  nube： 企业成员nube号
	*  nubeLen： nube号长度
	*  pass： 用户密码
	*  passLen： 用户密码长度
	*  devicetype 设备类型
	*  dtlen		设备类型长度
	*  context： 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int Login(const char* nube, int nubeLen,
		const char* pass, int passLen, const char * devicetype, 
		int dtlen, Context context) = 0;

	/*
	* 登录（使用串号进行登录）	【异步接口】
	* imei： 串号
	* imeiLen： 串号长度
	*/
	virtual int Login(const char * imei, int imeiLen, Context context) = 0;


	/*
	*  登录(在SDK中做鉴权，调用此接口不需要再调用BindToken)		【异步接口】
	*  nube： 企业成员nube号
	*  appkey： appkey
	*  uid uid
	*  context： 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int Login(const char* nube,const char* appkey, const char * uid, Context context) = 0;

	//首次登陆 串号登录问题

	/*
	*  绑定Token（调用者鉴权完成后，调用此接口绑定token和用户信息，调用此接口不需要调Login） 	【异步接口】
	*  token: token
	*  tokenLen： token长度
	*  accountId: 视讯号
	*  accountName： 名称
	*  accountNameLen： 名称长度
	*  context： 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int BindToken(const char* token, int tokenLen,
		int accountId, const char* accountName,
		int accountNameLen, Context context) = 0;
	
	/*********************** 会议查询接口 ************************************/
	/*
	*  判断会议是否存在		【异步接口】
	*  meetingId: 会议ID
	*  context： 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int IsMeetingExist(int meetingId, Context context) = 0;
	
	/*
	*  取得可以参加的会议列表	【异步接口】
	*  context： 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int GetMeetingList(Context context) = 0;

	/*
	*  取得会议详细信息			【异步接口】
	*  meetId:	会议号
	*  context： 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int GetMeetingInfo(int meetId, Context context) = 0;
	
	/******************* 取得音视频设备列表接口 ******************************/
	/*
	*  取得视频采集设备列表		【同步接口】
	*  devInfo : 调用者分配存储空间，用来存储设备信息
	*  maxCount  : 最多可以存储的设备个数，如果小于实际设备个数，则只会填充maxNum个设备信息到devInfo里
	*  返回值：实际设备个数
	*/
	virtual int GetVideoDeviceList(MEETINGMANAGE_VideoDeviceInfo *devInfo, int maxCount) = 0;
	
	/*
	*  取得音频采集设备列表		【同步接口】
	*  devicelist: 调用者分配存储空间，用来存储设备名称
	*  listsize  ：devicelist最多可以存储的设备个数，如果小于实际设备个数，则只会填充listsize个设备到devicelist里
	*  返回值    ：实际设备个数
	*/
	virtual int GetAudioCaptureDeviceList(char** devicelist, int listsize) = 0;
	
	/*
	*  取得音频渲染设备列表		【同步接口】
	*  devicelist: 调用者分配存储空间，用来存储设备名称
	*  listsize  ：devicelist最多可以存储的设备个数，如果小于实际设备个数，则只会填充listsize个设备到devicelist里
	*  返回值    ：实际设备个数
	*/
	virtual int GetAudioRenderDeviceList(char** devicelist, int listsize) = 0;

	/*
	 *	开始摄像头检测			【异步接口】 (废弃)
	 *  colorsps:	   颜色空间
	 *  fps:		   帧率
	 *  width:		   宽度
	 *  height:		   高度
	 *  previewWindow: 预览窗口句柄
	 *  videoCapName:  摄像头设备名
	 *  返回值：
	 */
	virtual int AsynPlayVideoTest(int colorsps, int fps,
		int width, int height, void * previewWindow, char videoCapName[256]) = 0; 


		/*
	 *	开始摄像头检测			【同步步接口】
	 *  colorsps:	   颜色空间
	 *  fps:		   帧率
	 *  width:		   宽度
	 *  height:		   高度
	 *  previewWindow: 预览窗口句柄
	 *  videoCapName:  摄像头设备名
	 *  返回值：0 成功 非0失败
	 */
	virtual int PlayVideoTest(int colorsps, int fps,
		int width, int height, void * previewWindow, char videoCapName[256]) = 0; 

	/*
	 *	停止摄像头检测			【同步接口】
	 */
	virtual void StopVideoTest() = 0;

	/*
	 *	开始摄像头检测（YUV数据回调方式）		【异步接口】
	 *  colorsps:	    颜色空间
	 *  fps:		    帧率
	 *  width:		    宽度
	 *  height:		    高度
	 *  videoCapName:	摄像头设备名
	 *  fun:			YUV数据回调方法
	 *  返回值：
	 */
	virtual int AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
		int height, char videoCapName[256], FUN_VIDEO_PREVIEW fun) = 0;

	/*
	 *	停止摄像头检测（YUV数据回调方式）		【同步接口】
	 */
	virtual int StopVideoTestYUVCB() = 0;

	/*
	 *	开始播放音频检测		 【异步接口】
	 *  wavFile: 待播放波形文件路径  路径为NULL则检测扬声器 路径不为NULL则检测MIC声音录制
	 *  renderName：设备名
	 *  返回值： 
	 */
	virtual int AsynPlaySoundTest(char wavFile[256], char renderName[256]) = 0; 

	/*
	 *	停止播放音频检测		 【同步接口】
	 */
	virtual void StopPlaySoundTest() = 0;
	
	/*
	 *	开始录音检测			 【同步接口】
	 *
	 *  micName： 麦克风设备名
	 *  wavFile: 待录制的波形文件
	 *  返回值：
	 */
	virtual int RecordSoundTest(char micName[256], char wavFile[256]) = 0; 

	/*
	 *	停止录音检测			 【同步接口】
	 */
	virtual void StopRecordSoundTest() = 0;

	/*	
	*  网络探测				 【异步接口】
	*  开始网络探测系列探测　，一共有３个探测步骤，每一步完成成功，都会自动开始下面的探测．
	*  探测依次为：网络连通性，会议连通性，网速带宽探测
	*
	*  返回值：0:同步开始调用成功．，其他，开始失败，测试未成功开始．
	*		
	*/
	virtual int AsynStartNetDiagnosticSerialCheck() = 0;

	/*	
	*   停止网络带宽探测		 【同步接口】
	*   在网络探测进入到＂带宽探测＂阶段，退出页面的时候，需要调用这个函数．
	*/
	virtual int StopNetBandDetect() = 0;

	/*
	*  获取带宽探测结果			 【同步接口】
	*  在网络探测类型为NDT_BAND_DETECT的回调中，同步调用，获取探测结果
	*  upwidth: 上行带宽
	*  downwidth: 下行带宽
	*  返回值：０　成功，其他 失败
	*/
	virtual int GetNetBandDetectResult(int & upwidth, int & downwidth) = 0;

	/****************************** 会议密码相关接口 ***********************************/

	/*
	 *	重置会议密码   (必须是主持人)         【异步接口】
	 *  meetingId ： 会议号
	 *  encode ： 密码
	 *  返回值 ： 0 - 成功；其他 - 失败
	 */
	virtual int ResetMeetingPassword(int meetingid, const char* encode = NULL) = 0;
	
	/*
	 *	获取会议密码   （必须是主持人）         【异步接口】
	 *  meetingId : 会议号
	 *  返回值 ： 0 - 成功；其他 - 失败
	 */
	virtual int GetMeetingPassword(int meetingid) = 0;

	/*
	 *	检查会议是否存在密码     【异步接口】
	 *  meetingId : 会议号
	 *  返回值 ： 0 - 成功；其他 - 失败
	 */
	virtual int CheckMeetingHasPassword(int meetingid) = 0;

	/*
	 *	检查会议密码是否有效 (除主持人以外的用户进入会议都需要校验密码)    【异步接口】
	 *  meetingId : 会议号
	 *  返回值 ： 0 - 成功；其他 - 失败
	 */
	virtual int CheckMeetingPasswordValid(int meetingid,
		const char* encryptcode = NULL) = 0;
	
	/********************************* 会议相关接口 *******************************/

	/*
	*  创建会议		【异步接口】
	*  appType: 应用类型
	*  typeLen: 类型长度
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int CreateMeeting(char * appType, int typeLen, Context context) = 0;

	/*
	 *	创建并邀请会议	 【异步接口】
	 *  本接口只提供邀请、将邀请信息存入参会列表功能，不提供即时邀请能力。
	 *  用户要实现即时能力，需要启动host 实例，并调用邀请接口实时邀请。
	 *  
	 *	appType			: 应用类型
	 *  typeLen			: 类型长度
	 *  inviteeList		: 被邀请人列表
	 *  inviteeCount	: 被邀请人数目
	 *  context			: 上下文参数
	 *  返回值 : 0 - 成功；其他 - 失败
	 *  
	 */
	virtual int CreateAndInviteMeeting(char * appType, int typeLen,
		int * inviteeList, int inviteeCount, Context context) = 0;

	/*
	* 创建预约会议，UI层需要在callback中同步调用函数GetMeetingId 以获取本次创建产生的会议号。
	*  appType: 应用类型
	*  typeLen: 类型长度
	* 参数说明：	year,
	*			month
	*			day
	*           hour
	*           minute
	*           second
	*           endtime	会议结束时间 格式yyyy-mm-dd hh:mm:ss 如2016-08-02 09:08:30
	*			topic   会议主题
	*			encryptcode ,可以为空。
	* 返回值:  0 - 成功，其他 - 失败
	*/
	virtual int CreateDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour, 
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, const char * encryptcode = NULL) = 0;
	/*
	* 创建并邀请预约会议，UI层需要在callback中同步调用函数GetMeetingId 以获取本次创建产生的会议号。
	*  appType: 应用类型
	*  typeLen: 类型长度
	* 参数说明：	
	*			appType 
	*			year,
	*			month
	*			day
	*           hour
	*           minute
	*           second
	*           endtime	会议结束时间 格式yyyy-mm-dd hh:mm:ss 如2016-08-02 09:08:30
	*			topic   会议主题
	*			inviteeList		: 被邀请人列表
	*			inviteeCount	: 被邀请人数目
	*			encryptcode ,可以为空。
	* 返回值:  0 - 成功，其他 - 失败
	*/
	virtual int CreateAndInviteDatedMeeting(char * appType, int typeLen, unsigned int year,
		unsigned int month, unsigned int day, unsigned int hour, 
		unsigned int minute, unsigned int second, const char * endtime,
		const char * topic, int * inviteeList, int inviteeCount,const char * encryptcode = NULL) = 0;
	

	/*
	*  增加参会人员 会议进行过程中允许邀请其它视频账号加入会议。向原来会议的新增参会人员发送短信，已经参会的人员就不发送短信了
	* 参数说明：	
	*			meetId			会议号
	*			appType			应用类型
	*			smsType			指定短信发送需求为即时会议还是预约会议 1：即时会议 2 预约会议
	*			accountList		被要请参会人视讯号列表
	*			accountNum		被要请参会人视讯号个数
	*			context			上下文参数
	*/
	virtual int ModifyMeetingInviters(int meetId,const char * appType,int smsType,int *accountList,int accountNum, Context context) = 0;

	/*
	*  加入会议 	【异步接口】
	*  meetingId: 会议ID
	*  autoSpeak: 是否进会自动发言, true - 进会自动发言  false - 进会不自动发言
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int JoinMeeting(int meetingId, bool autoSpeak, Context context) = 0;

	/*
	*  获取加入会议信息 	【同步接口】
	*  meetingId: 会议ID
	*  joinMeetingInfo: 加入会议信息
	*  返回值：0 –  成功；其他 – 失败
	*  注：该方法仅在OnCreateMeeting和OnJoinMeeting回调方法中调用能返回有效的加入会议信息
	*/
	virtual int GetJoinMeetingInfo(int meetingId, JoinMeetingInfo * joinMeetingInfo) = 0;
	
	/*
	*  产生新的同步id  	【同步接口】
	*  返回值：新产生的同步id
	*/
	virtual int GenericSyncId() = 0;

	/*
	*  获取会议模式信息    【同步接口】
	*  在加入会议成功后，UI 主动调用 GetParticipants之后调用。同步调用
	*  myRole :	2 我是主持人, 1 我是普通参会者.
	*  curMode : 1为自由模式；2为主持人模式。
	*  hostId : 主持人的id
	*  idLen : 主持人id长度
	*  hostNick : 主持人昵称
	*  nickLen : 主持人昵称长度
	*  liveStatus : 直播状态 , 1代表直播会议，2代表普通会议，0代表不感知
	*/
	virtual void GetMeetingModeInfo(int & myRole, int & curMode,
		char * hostId, int idLen, char * hostNick, 
		int nickLen, int & liveStatus) = 0;
	 
	/*
	*  获取当前会议模式     【同步接口】
	*  curMode : 1 为自由模式；2 为主持人模式。
	*/
	virtual void GetCurMeetingMode(int & curMode) = 0; 

 	/*
	*  主持人改变当前会议模式  【异步接口】
	*  toMode : 1为自由模式；2为主持人模式
	*  返回值 : 0 - 成功；其他 - 失败
	*/
	virtual int HostChangeMeetingMode(int toMode) = 0;

	/*
	 *	主持人踢出用户       【异步接口】
	 *  accountId : 被踢出用户视讯号
	 *  返回值 ： 0 - 成功；其他 - 失败
	 */
	virtual int HostKickoutUser(int accountId) = 0;

	/*
	 *	用户举手请求       【异步接口】
	 *  返回值 ： 0 - 成功；其他 - 失败
	 */
	virtual int RaiseHandReq() = 0;

	/*
	*  会议加锁解锁申请请求   【异步接口】
	*  bToLock ： true 加锁， false 解锁
	*  返回值 : 0 - 成功；其他 - 失败
	*/	
	virtual int AskForMeetingLock(bool bToLock) = 0;

	/*
	*  获取会议锁定状态     【同步接口】
	*  返回值 : true 加锁；false 解锁
	*		
	*/	
	virtual bool GetMeetingLockStatus() = 0;

	/*
	*  主持人指定用户发言		【异步接口】
	*  toAccountId : 被指定的发言人的id
	*  toLen : 被指定的发言人的id长度
	*  kickAccountId : 正在发言人的视讯号（被抢占用户视讯号）传空串表示根据是否有达到最大发言人数判断指定发言是否成功
	*  kickLen : 正在发言人id长度
	*  返回值 ： 0 - 成功；其他 - 失败
	*/
	
	virtual int HostOrderOneSpeak(char * toAccountId, int toLen, 
		char * kickAccountId, int kickLen) = 0;

	/*
	*  主持人指定用户停止发言    【异步接口】
	*  toAccountId : 被指定的发言人的id
	*  toLen : 被指定的发言人的id长度
	*  返回值 ： 0 - 成功；其他 - 失败
	*/
	virtual int HostOrderOneStopSpeak(char * toAccountId, int toLen) = 0;
	
	/*
	*  获取指定用户发布流的信息		【同步接口】
	*  accountId: 发言者视讯号
	*  streamsInfo: 存储流信息的数组, 内存空间由调用者分配
	*  maxCount: 调用者分配存储空间的个数,maxCount 小于实际流总数时，只拷贝前maxCount个
	*  返回值：实际流信息个数
	*/
	virtual int GetUserPublishStreamInfo(int accountId,
		MeetingUserStreamInfo * streamsInfo, int maxCount) = 0;
	
	/*
	*  获取当前用户订阅的所有流信息		【同步接口】 
	*  streamsInfo: 存储流信息的数组, 内存空间由调用者分配
	*  maxCount: 调用者分配存储空间的个数,maxCount 小于实际流总数时，只拷贝前maxCount个
	*  返回值：实际流信息个数
	*/ 
	virtual int GetCurrentSubscribleStreamInfo(
		MeetingUserStreamInfo * streamsInfo, int maxCount) = 0;
	
	// /*
	// *  取得当前在会议中的参会人数	【同步接口】
	// *  返回值：参会人数
	// */
	// virtual int GetParticipantsCount() = 0;
	
	/*
	*  取得参会人列表	【同步接口】
	*  participants: 存储参会人信息的数组，内存空间由调用者分配
	*  maxCount: 可以存储的参会人信息的最大个数，调用者分配数组的容量，maxCount 小于实际人数时，只拷贝前maxCount个参会人信息
	*  返回值：>= 0 - 实际的参会人数; <0 - 失败
	*/
	virtual int GetParticipants(ParticipantInfo* participants,int maxCount) = 0;

	/*
	*  取得参会人列表	【同步接口】
	*  participants: 存储参会人信息的数组，内存空间由调用者分配
	*  pageNum: 需要获取参会人所在的页码
	*  countPerPage: 每页的参会人员数量
	*  返回值：>= 0 - 实际的参会人数; <0 - 失败
	*/
	virtual int GetParticipants(ParticipantInfo * participants,
		int pageNum, int countPerPage) = 0;
	
	/*
	*  退出当前会议 	【同步接口】
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int LeaveMeeting() = 0; 
	
	/*
	*  申请发言		【异步接口】
	*  speakerId: 如果是自由发言，则这个对象是空对象，如果是抢麦，则存储被抢人的视讯号
	*  speakerIdLen : 视讯号长度
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int AskForSpeak(char * speakerId, int speakerIdLen, Context context) = 0;
	
	/*
	*  申请停止发言		【异步接口】
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int AskForStopSpeak(Context context) = 0;

	/*
	 *	获取发言者列表			[同步接口]
	 *  speakerInfoList: 存储发言者信息的数组，内存空间由调用者分配
	 *  maxCount: 最大发言者信息获取数
	 *  返回值: >= 0 - 实际的参会人数; <0 - 失败
	 */
	virtual int GetSpeakerList(MeetingSpeakerInfo * speakerInfos, int maxCount) = 0;

	/*
	 *	获取指定发言者信息		[同步接口]
	 *  speakerInfo: 发言者信息
	 *
	 *  返回值: 0 - 成功， 非0 - 失败
	 */
	virtual int GetSpeakerInfo(int accountId, MeetingSpeakerInfo * speakerInfo) = 0;

	/*
	 *	获取会议QOS数据		[同步接口]
	 *	outdata:外部分配的内存
	 *  outlen: 外部分配的内存长度，成功的时候是内部返回的数据长度。
	 *  返回值：= 0 ：成功，-1：长度不够，outlen 为期待的长度。，其他值失败。
	 */
	virtual  int  GetMeetingQos( char *outdata ,int & outlen ) = 0;

	
	/*
	*  发布摄像头视频	【异步接口】
	*  param: 摄像头视频发布参数
	*  isNeedCallBackMedia: 是否需要回调YUV数据 true:需要回调  false：不需要回调
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int PublishCameraVideo(MEETINGMANAGE_PublishCameraParam *param,
		bool isNeedCallBackMedia, Context context) = 0;
	
	/*
	*  发布采集卡视频	【异步接口】
	*  param: 采集卡视频发布参数
	*  isNeedCallBackMedia: 是否需要回调YUV数据 true:需要回调  false：不需要回调
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam *param,
		bool isNeedCallBackMedia, Context context) = 0;
	
	/*
	*  发布本地桌面视频流	【异步接口】
	*  param: 桌面采集视频发布参数
	*  isNeedCallBackMedia: 是否需要回调YUV数据 true:需要回调  false：不需要回调
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam *param,
		bool isNeedCallBackMedia, Context context) = 0;
	
	/*
	*  发布音频流   【异步接口】
	*  param: 音频发布参数
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int PublishMicAudio(MEETINGMANAGE_publishMicParam *param,
		Context context) = 0;
	
	/*
	*  取消发布摄像头视频流 【异步接口】
	*  resourceId: 资源ID
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int UnpublishCameraVideo(int resourceId, Context context) = 0;

	/*
	*  取消发布采集卡视频流 【异步接口】
	*  resourceId: 资源ID
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int UnpublishDataCardVideo(int resourceId, Context context) = 0;

	/*
	*  取消发布本地桌面视频流 【异步接口】
	*  resourceId: 资源ID
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int UnpublishWinCaptureVideo(int resourceId, Context context) = 0;

	/*
	*  取消发布音频流 【异步接口】
	*  resourceId: 资源ID
	*  context: 上下文参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int UnpublishMicAudio(int resourceId, Context context) = 0;
	
	/*
	*  订阅视频  【同步接口】
	*  param: 视频订阅参数
	*  isNeedCallBackMedia: 是否需要回调YUV数据 true:需要回调  false：不需要回调
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int	SubscribeVideo(MEETINGMANAGE_subscribeVideoParam *param,
		bool isNeedCallBackMedia) = 0;

	/*
	*  订阅视频  【异步接口】
	*  param: 视频订阅参数
	*  isNeedCallBackMedia: 是否需要回调YUV数据 true:需要回调  false：不需要回调
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int	AsynSubscribeVideo(MEETINGMANAGE_subscribeVideoParam *param,
		bool isNeedCallBackMedia) = 0;
	
	/*
	*  订阅音频  【同步接口】
	*  param: 音频订阅参数
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int SubscribeAudio(MEETINGMANAGE_subscribeAudioParam *param) = 0;
	
	/*
	*  取消订阅  【同步接口】
	*  accountId: 发言者视讯号
	*  resourceId: 资源ID
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int	Unsubscribe(int accountId, int resourceId) = 0;
	
	/*
	*  发送媒体流  【同步接口】
	*  resourceId: 资源ID
	*  frameType： 媒体格式
	*  frameData： 媒体数据
	*  dataLen：   数据长度
	*	orientation:图像方向，目前android下需要传，windows下不需要传具体信息
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int PushMediaFrameData(int resourceId,
		MEETINGMANAGE_FrameType frameType, char * frameData, int dataLen,int orientation = 0 ) = 0;

	/*
	*  开始渲染本地视频  【同步接口】
	*  resourceId: 资源ID
	*  displayWindow: 显示窗口句柄
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int StartLocalVideoRender(int resourceId, void* displayWindow,int aspx=0,int aspy=0) = 0;

	/*
	*  停止渲染本地视频  【同步接口】
	*  resourceId: 资源ID
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int StopLocalVideoRender(int resourceId) = 0;
	
	/*
	*  开始渲染远端视频  【同步接口】
	*  accountId: 发言者视讯号
	*  resourceId: 资源ID
	*  displayWindow: 显示窗口
	*  返回值：0 –  成功；其他 – 失败
	*/

	virtual int StartRemoteVideoRender(int accountId,
		int resourceId, void* displayWindow ,int aspx=0,int aspy=0) = 0;

	/*
	*  停止渲染远端视频  【同步接口】
	*  accountId: 发言者视讯号
	*  resourceId: 资源ID
	*  返回值：0 –  成功；其他 – 失败
	*/
	virtual int StopRemoteVideoRender(int accountId, int resourceId) = 0;

	/*
	 *	开始YUV数据的回调通知
	 *  accountId: 发言者视讯号
	 *  resourceId: 资源ID
	 *  返回值：0 –  成功；其他 – 失败
	 */
	virtual int StartYUVDataCallBack(int accountId, int resourceId) = 0;

	/*
	 *	停止YUV数据的回调通知
	 *  accountId: 发言者视讯号
	 *  resourceId: 资源ID
	 *  返回值：0 –  成功；其他 – 失败
	 */
	virtual int StopYUVDataCallBack(int accountId, int resourceId) = 0;

	/*
	 *	发送透传信息
	 *  destAccount: 目标视讯号
	 *  data：透传数据
	 *  dataLen： 数据长度
	 *  返回值：0 –  成功；其他 – 失败
	 */
	virtual int SendUiTransparentMsg(int destAccount, char * data, int dataLen) = 0;


	/*
	 *  透传UI消息 （群发）
	 *    msgId              消息ID
	 *    dstUserAccount     目标用户视讯号 0群发
	 *    msgData            消息内容
	 返回值：0 –  成功；其他 – 失败
	 */
	virtual int AsynSendUIMsg(int msgId, int dstUserAccount,const char *msgData) = 0;


	 
	/*
	*  获取传麦人列表	【同步接口】
	*  participants: 存储可传麦人信息的数组，内存空间由调用者分配
	*  maxCount: 可以存储的传麦人信息的最大个数，调用者分配数组的容量，maxCount 小于实际人数时，只拷贝前maxCount个参会人信息
	*  返回值：>= 0 - 实际的参会人数; <0 - 失败
	*/
	virtual int GetMicSendList(ParticipantInfo* participants,int maxCount) = 0;

	/*
	*  发起传麦申请 【同步接口】
	*/
	virtual int AsynMicSendReq(int beSpeakedUserId) = 0;


	/*
	*  增加渲染窗口 【同步接口】
	*  accountId: 发言者视讯号
	*  resourceId: 资源ID
	*  displayWindow 渲染窗口句柄
	*  aspx 视频宽高比分子
	*  aspy 视频宽高比分母
	* 注： 三种情况：
	*	 1. aspx和aspy都为0，保证视频宽高比的情况下填满窗口，有可能会有黑边。(默认)
	*    2. aspx和aspy都为-1，视频填满窗口，不保证视频宽高必。
	*	 3. aspx和aspy都大于0，强制使用设置的宽高比。
	 *	 4. 如果对非视频链路调用这个接口会失败
	* 返回值 成功返回0；失败返回错误码
	*/
	virtual int AddDisplayWindow(int accountId, int resourceId,void *displayWindow,int aspx = 0, int aspy =0)=0;


	/*
	 *  移除渲染窗口 【同步接口】
	 *  accountId: 发言者视讯号
	 *  resourceId: 资源ID
	 *  displayWindow 渲染窗口句柄
	 *  aspx 视频宽高比分子
	 *   aspy 视频宽高比分母
	 * 返回值 成功返回0；失败返回错误码
	 * 注： 三种情况：
	 *	 1. aspx和aspy都为0，保证视频宽高比的情况下填满窗口，有可能会有黑边。(默认)
	 *   2. aspx和aspy都为-1，视频填满窗口，不保证视频宽高必。
	 *	 3. aspx和aspy都大于0，强制使用设置的宽高比。
	 *	 4. 如果对非视频链路调用这个接口会失败
	 * 说明：该功能支持除直播推流以外所有版本
	 */
	virtual int RemoveDisplayWindow(int accountId, int resourceId,void *displayWindow,int aspx = 0, int aspy =0)=0;


	/*
	*  发布直播媒体流 【同步接口】
	* 参数：param (码率单位是kbps)
	* 返回值：>0 成功 streamid媒体流标识  ，<0 失败 错误码
	*/
	virtual int PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam * param) =0;


	/*
	*  停止发布直播媒体流 【同步接口】
	* 参数 streamID 媒体流标识
	* 返回值：>0 成功 streamid媒体流标识  ，<0 失败 错误码
	*/
	virtual int UnpublishLiveStream(int streamID) =0;

	/*
	 *  开始直播推流【同步接口】
	 * 参数: streamID 媒体流标识
	 *	     url 推流地址
	 * 返回值:成功返回0，失败返回其他值
	 */
	virtual int StartLiveRecord(int streamID, char *url) =0;

	/*
	*  停止直播推流 【同步接口】
	* 参数 streamID 媒体流标识
	* 返回值:成功返回0，失败返回其他值
	*/
	virtual int StopLiveRecord(int streamID) = 0;


	/*
	* 更新视频链路信息 【同步接口】
	* 参数： streamID 媒体流标识
	*		  streamInfo 视频链路信息指针
	*		  streamnum 视频链路个数（最大限度20）
	* 返回值：成功返回0；失败 返回错误码
	*/
	virtual int UpdateLiveStreamVideoInfo(int streamID, MEETINGMANAGE_VideoStreamInfo *streamInfo, int streamnum) =0;

	/*
	 * 更新音频链路信息 【同步接口】
	 * 参数： streamID 媒体流标识
	 *		  streamInfo 音频链路信息指针
	 *		  streamnum 音频链路个数（最大限度20）
	 * 返回值：成功返回0；失败 返回错误码
	 */
	virtual int UpdateLiveStreamAudioInfo(int streamID, MEETINGMANAGE_AudioStreamInfo *streamInfo, int streamnum)=0;

	/*
	*  开始mp4文件录制 【同步接口】
	* 参数: streamID 媒体流标识
	*		filepath 路径需指定文件名
	* 返回值:成功返回0，失败返回其他值
	*/
	virtual int StartMp4Record(int streamID, char *filepath) = 0;

	/*
	*  停止mp4文件录制 【同步接口】
	* 参数: streamID 媒体流标识
	* 返回值:成功返回0，失败返回其他值
	*/
	virtual int StopMp4Record(int streamID) = 0;


	/***************** 自适应接口 ******************/
	/*
	 *  设置视频显示模式 【同步接口】
	 *   videoDisplayMode    视频画面显示模式
	 *                1     全高清模式
	 *                2     全流畅模式
	 *                3     自适应模式
	 *   返回值       EC_OK 成功    EC_FAIL 失败
	 */
	virtual int SetVideoDisplayMode(int videoDisplayMode) = 0;


	/*
	 *  设置窗口画面显示清晰度 【同步接口】
	 *  accountId: 发言者视讯号
	 *  resourceId: 资源ID           
	 *   clarityLevel       清晰度级别
	 *                1     高清晰度（大画面）
	 *                2     中清晰度（中画面）
	 *                3     低清晰度（小画面）
	 *				  4     隐藏窗口
	 */
	virtual int SetVideoClarity(int accountId, int resourceId,int clarityLevel) = 0;

	/*
	 *  设置是否启用自适应(参会前调用) 【同步接口】
	 *   isEnabled          是否启用  0：不启用  非0：启用
	 *   返回值       EC_OK 成功    EC_FAIL 失败
	 */
	virtual int  SetAutoAdjustEnableStatus(int isEnabled)= 0;


	/*
	 *  设置是否人像推双流(参会前调用)【同步接口】
	 *   isEnabled          是否启用  0：不启用  非0：启用
	 *   返回值       EC_OK 成功    EC_FAIL 失败
	 */
	virtual int  SetPublishDoubleVideoStreamStatus(int isEnabled)= 0;


	/*
	 *  设置流畅模式视频流编码参数 【同步接口】
	 *   frameWidth    小流编码宽度
	 *   frameHeight   小流编码高度
	 *   bitrate       小流编码码率（单位kbps）
	 *   frameRate     小流编码帧率
	 */
	virtual int SetLowVideoStreamCodecParam(int frameWidth,int frameHeight,int bitrate,int frameRate)= 0;
	 
	/* 设置cpu利用率 【同步接口】
	*
	*   processCpu    进程cpu利用率
	*   totalCpu      总体cpu利用率
	*/
	virtual int SetCurCpuInfo(int processCpu,int totalCpu)=0;

	/*
	 *  设置音频混音接收端混音器缓冲区参数 (参会前调用) 【同步接口】
	 * 参数： AudioMaxBufferNum 音频接收端混音器最大缓冲区
	 *		  AudioStartVadBufferNum 音频接收端混音器开始挤静音压缩阈值
	 *		  AudioStopVadBufferNum 音频接收端混音器停止挤静音压缩阈值
	 * 返回值 成功返回0；失败返回错误码
	 */
	virtual int SetAudioMixRecvBufferNum(int AudioMaxBufferNum, int AudioStartVadBufferNum, int AudioStopVadBufferNum)=0;



	/****************** 用户设置 ******************/

	/*
	 *	修改用户信息		【异步接口】
	 *	accountName： 用户昵称
	 *  nameLen：     昵称长度
	 *  返回值： 0 - 成功； 其他 - 失败
	 */
	virtual int ModifyNickName(const char * accountName, int nameLen, Context context) = 0;


	/***** 主持人指定打开/关闭麦克风、扬声器、摄像头相关接口 *****/
	/*
	*  主持人指定其他用户执行某项操作 【异步接口】
	*  @toUserId	： 被控制方视讯号
	*  @oprateType	： 操作类型 
	*						1    打开摄像头视频
							2    关闭摄像头视频
						    3    打开麦克风音频
							4    关闭麦克风音频
						    5    打开屏幕分享视频
							6    关闭屏幕分享视频
						    7    打开扬声器
							8    关闭扬声器
	*  同步返回值： 0 成功   其他失败
	*/
	virtual int HostOrderOneDoOpration(int toUserId, int oprateType, Context context) = 0;

	/*
	 *  获取参会方视频流分辨率	【同步接口】
	 *  @accountId	:	发言者视讯号
	 *  @resourceID	:	视频流资源ID
	 *  @videoWidth :	视频分辨率-宽度（输出参数）
	 *  @videoHeight:	视频分辨率-高度（输出参数）
	 *  同步返回值： 0 成功   其他失败
	 */
	virtual int GetSpeakerVideoStreamParam(int accountId, int resourceID, int &videoWidth, int &videoHeight) = 0; 

	/*
	 *  获取短信分享的分享内容	【异步接口】
	 *  @meetId			: 会议号
	 *  @inviterPhoneId	: 会议邀请者视讯号
	 *  @inviterName	: 会议邀请者昵称
	 *  @meetingType	: 会议类型 即时会议:1 预约会议：2
	 *  @app			: 产品类型 极会议: JIHY, 可视极会议：KESHI_JIHY，红云会诊：HVS
	 *  @urlType		: url 种类 1：http  2：https
	 *  @context		: 上下文参数
	 *  同步返回值： 0 成功   其他失败
	 */
	 virtual int GetMeetingInvitationSMS(int meetId, int inviterPhoneId, const char* inviterName, int inviterNameLen,
		int meetingType, const char* app, int appLen, int urlType, Context context) = 0;

	 /*
	  *  通知其他参会方本地扬声器状态	【异步接口】
	  *  @isOpen		: 扬声器打开关闭状态 0：关闭  1：打开
	  *  @context		: 上下文参数
	  *  同步返回值： 0 成功   其他失败
	  */
	 virtual int SendAudioSpeakerStatus(int isOpen, Context context) = 0;


	 /***** 代理设置 *****/
	 /* 设置是否使用代理服务器 (Start前调用) 【同步接口】
	*
	*   isProxy     0：不使用  1：使用
		同步返回值： 0 成功   其他失败
	*/
	virtual int SetUseProxy(int isProxy)=0;

	/*
	*  设置视频解码器类型 （不调用此接口默认硬件解码）
	* 参数: isHard 解码器类型。非0表示硬件解码；0表示软件解码 
	* 返回值 成功返回0；失败返回错误码
	*/
	virtual int SetDecoderType(int isHard) = 0;


	virtual ~IMeetingManage() = 0;
};

extern "C"
{
	/**
	*描述 创建MeetingManage对象
	*
	*@param cb			 [IN]回调对象
	*@return 返回  MeetingManage对象
	*/
	MEETING_MANAGE_API IMeetingManage * CreateMeetingManageObject(IMeetingManageCB * cb);

	/**
	*描述 销毁MeetingManage对象 
	*
	*@param object	[IN]MeetingManage对象
	*@return 
	*/
	MEETING_MANAGE_API void DestroyMeetingManageObject(IMeetingManage * object);



};

#endif
