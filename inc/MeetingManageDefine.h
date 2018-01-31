#ifndef _MEETINGMANAGEDEFINE_H_
#define _MEETINGMANAGEDEFINE_H_

#include <iostream>

#ifdef WIN32

	#ifdef MEETINGMANAGE_EXPORTS
		#define MEETING_MANAGE_API  __declspec(dllexport)
	#else
		#define MEETING_MANAGE_API  __declspec(dllimport)
		#pragma comment (lib, "meetingmanage.lib")
	#endif
#else
	#define MEETING_MANAGE_API
#endif

typedef void * Context;

typedef void (*FUN_VIDEO_PREVIEW)(char *pFrame, int width, int height, int frameLen);

#define MEETINGMANAGE_USERID_LEN		64		// 视讯号长度
#define MEETINGMANAGE_sourceName_len	2048
#define MEETINGMANAGE_DEVICENAME_LEN	256
#define MEETINGMANAGE_USERNAME_LEN		128
#define MEETINGMANAGE_TIME_LEN			64
#define MEETINGMANAGE_EXTRAINFO_LEN		1024
#define MEETINGMANAGE_STREAMINFO_COUNT  20
#define MEETINGMANAGE_TOPIC_LEN			200
#define MEETINGMANAGE_TOKEN_LEN			128

// SDK错误返回类型
enum SDKErrorCode {
	SDK_ERRCODE_SUCCEED						= 0,		// 成功
	SDK_ERRCODE_SDK_UNINIT					= -1,		// sdk未启动
	SDK_ERRCODE_FAILED						= -1000,	// 失败（通用）
	SDK_ERRCODE_INVALID_ARGUMENTS			= -1001,	// 无效参数
	SDK_ERRCODE_NETWORK_DISCONNECT			= -1002,    // 网络异常
	SDK_ERRCODE_FUNCTION_INVOKE_AGAIN		= -1003,	// 重复调用
	SDK_ERRCODE_SEND_REQUEST_FAILED			= -1004,	// 发送请求到服务器失败（通用）
	SDK_ERRCODE_MEETING_CALLBACK_NULL		= -1005,	// 会议模块回调实例为空
	SDK_ERRCODE_GET_NPSREQUEST_FAILED		= -1006,    // 发送获取NPS配置请求失败
	SDK_ERRCODE_START_ASYNMODEL_FAILED		= -1007,	// 启动异步模型失败
	SDK_ERRCODE_GETTOKEN_FAILED				= -1008,	// 获取token失败
	SDK_ERRCODE_BINDTOKEN_REQUEST_FAILD		= -1009,    // 发送鉴权请求失败
	SDK_ERRCODE_RECORDSOUND_FAILED			= -1010,	// 音频采集测试失败
	SDK_ERRCODE_INMEETING					= -1011,	// 在会议中
	SDK_ERRCODE_NOT_INMEETING				= -1012,	// 不在会议中
	SDK_ERRCODE_NOT_SPEAKING				= -1013,	// 不是发言状态
	SDK_ERRCODE_MEETINGSDK_FAILED			= -1014,	// 媒体库请求错误
	SDK_ERRCODE_GETUSERLIST_NOTINMEETING    = -1015,	// 获取参会列表，不在会议中
	SDK_ERRCODE_GETUSERLIST_REQUESTFAILED   = -1016,	// 发送获取参会列表请求失败
	SDK_ERRCODE_START_MEETCTRLAGENT_FAILED  = -1017,	// 启动会控Agent失败
};


//SDK异步回调状态码
enum SDKCallbackStatus
{
	SDK_STATUS_SUCCEED						= 0,		//成功
	SDK_STATUS_FAIL							= -1999,	//异步调用失败（通用错误）
	SDK_STATUS_GETCONFIG_FAIL				= -2000,	//获取配置信息失败 OnStart 
	SDK_STATUS_LOGIN_FAIL					= -2001,	//鉴权失败 OnLogin
	SDK_STATUS_LOGIN_NOSERVICE				= -2002,	//企业中心登陆返回,服务未开通 OnLogin
	SDK_STATUS_LOGIN_SERVICEOUT				= -2003,	//企业中心登陆返回,服务逾期 OnLogin
	SDK_STATUS_BINDTOKEN_FAIL				= -2004,	//绑定token失败 OnLogin OnBindToken
	SDK_STATUS_START_AGENT_FAIL				= -2005,	//启动本地代理失败 OnBindToken
	SDK_STATUS_MEET_NOTEXIST				= -2006,	//会议号不存在 OnCheckMeetExist OnResetMeetingPassword OnGetMeetingPassword OnCheckMeetingHasPassword OnCheckMeetingPasswordValid
	SDK_STATUS_TOKEN_TIMEOUT				= -2007,	//Token已过期 OnGetMeetingList OnResetMeetingPassword OnGetMeetingPassword OnCheckMeetingPasswordValid
	SDK_STATUS_NOT_HOST						= -2008,	//接口调用者不是会议主持人 OnResetMeetingPassword OnGetMeetingPassword
	SDK_STATUS_WRONG_PWD					= -2009,	//会议密码错误 OnCheckMeetingPasswordValid
	SDK_STATUS_JOIN_CREATED_MEET_FAIL		= -2010,	//加入新创建的即时会议失败
	SDK_STATUS_JOINMEET_FAIL_INMEET			= -2011,	//加入会议失败，已经在会议中，需要先退会
	SDK_STATUS_JOINMEET_FAIL_GETUSERLIST	= -2012,	//加入会议成功，但取参会人列表失败
	SDK_STATUS_NOTOKEN						= -2013,	//token无效或token过期,会控服务器返回
	SDK_STATUS_JOINMEET_FAIL_MEETEND		= -2014,	//加入会议失败，会议已结束
	SDK_STATUS_JOINMEET_FAIL_SERVER_ERR		= -2015,	//加入会议失败，服务器返回-915
	SDK_STATUS_JOINMEET_FAIL_MEETLOCK		= -2016,	//加入会议失败，会议已加锁
	SDK_STATUS_JOINMEET_FAIL_NORSP			= -2017,	//加入会议失败，请求超时，服务器无应答
	SDK_STATUS_NOTINMEET					= -2018,	//不在会议中，操作失败
};


// 设备状态变化类型
enum DevStatusChangeType{
	DT_DEVUNKNOWN = -1,
	DT_VIDEOLOST,
	DT_VIDEOFOUND,
	DT_DATAVIDEOLOST,
	DT_DATAVIDEOFOUND,
	DT_MICLOST,
	DT_MICFOUND,
	DT_SPEAKLOST,
	DT_SPEAKFOUND
};

// SDK异常类型
enum SDKExceptionType {
	SETYPE_UNKNOWN = -1,		      // 未知
	SETYPE_NETWORK_RECONNECT,	      // 网络重连
	SETYPE_NETWORK_ENDRETRY,		  // 网络重连结束
	SETYPE_NETWORK_RECONNECT_SUCCEED, // 网络重连成功
	SETYPE_TOKEN_TIMEOUT		      // TOKEN过期
};

enum NetDiagnosticType {
	NDT_UNKNOWN = -1,		// 未知
	NDT_NET_CONN,			// 网络连通性
	NDT_MEET_CONN,			// 会议连通性
	NDT_BAND_DETECT			// 网速检测
};

enum MM_MeetingType {
	MT_UNKNOWN = -1,		// 未知
	MT_JUSTMEETING = 1,		// 即时会议
	MT_DATEDMEETING			// 预约会议
};

//帧类型
typedef enum _MEETINGMANAGE_FrameType
{
	MEETINGMANAGE_FRAME_TYPE_YUV,
	MEETINGMANAGE_FRAME_TYPE_H264,
	MEETINGMANAGE_FRAME_TYPE_RTP
}MEETINGMANAGE_FrameType;

//smsdk通知
typedef enum _MEETINGMANAGE_SMSDK_CBTYPE
{
	SMSDK_TYPE_ERR,						//通知失败
	SMSDK_TYPE_NET_LINKNORMAL,			//网络信号稳定
	SMSDK_TYPE_NET_LINKBAD,				//网络信号差
	SMSDK_TYPE_NET_LINKERROR,			//网络信号极差
	SMSDK_TYPE_VIDEO_CLOSE_ADAPTE,		//自适应关闭视频
	SMSDK_TYPE_CPU_OTHEROVERLOAD,		//其他进程占用cpu过载
	SMSDK_TYPE_CPU_SELFOVERLOD,			//自身占用cpu过载
}MEETINGMANAGE_SMSDK_CBTYPE;


/**************************** 登录参数定义 *********************************/
typedef struct _LoginInfo
{
	int		m_accountId;								// 登录用户的视讯号
	char	m_accountName[MEETINGMANAGE_USERNAME_LEN];	// 登录用户的名称
	char	m_tokenStartTime[MEETINGMANAGE_TIME_LEN];	// 用户Token的起始时间
	char	m_tokenEndTime[MEETINGMANAGE_TIME_LEN];		// 用户Token的结束时间
	char	m_token[MEETINGMANAGE_TOKEN_LEN];			// token
}LoginInfo;

/**************************** 会议参数定义 *********************************/

//参会人信息
typedef struct _ParticipantInfo
{
	int 			 m_accountId;			//视讯号
	char 			 m_accountName[MEETINGMANAGE_USERNAME_LEN];	//昵称
	bool			 m_bIsRaiseHand;		//是否已经举手
	bool			 m_bIsSpeaking;			//是否正在发言
	int				 m_hostFlag;			//是否是主持人
	bool			 m_aRenderOpened;	//扬声器打开关闭状态
}ParticipantInfo;

//流信息
typedef struct _MeetingUserStreamInfo {
	char 			 m_accountName[MEETINGMANAGE_USERNAME_LEN]; 	//昵称
	char 			 m_deviceName[MEETINGMANAGE_DEVICENAME_LEN];
	int 			 m_accountId;			//视讯号
	int 			 m_resourceId;	
	int				 m_asynGroupId;			//同步ID
	int				 m_mediaType;			//类型说明 枚举
} MeetingUserStreamInfo;

//发言人信息
typedef struct _MeetingSpeakerInfo {
	char 			 m_accountName[MEETINGMANAGE_USERNAME_LEN];
	int 			 m_accountId;
	int				 m_streamCount;
	MeetingUserStreamInfo m_streamInfos[MEETINGMANAGE_STREAMINFO_COUNT];
}MeetingSpeakerInfo;

//加入会议详细信息
/*
 *	m_lockInfo	    [IN] 是否加锁 1 加锁 0 不加锁; 默认为0(不加锁)
 *  m_presenterId   [IN] 主持人账号ID; 默认主持人为创会者
 *  m_userType      [IN] 参会用户类型，1为普通用户 2为主持人用户
 *  m_meetingStyle  [IN] 会议模式，1为自由模式 2为主持人模式; 默认为1(自由模式)
 *  m_presenterName [IN] 主持人名字
 *  m_liveStatus    [IN] 会议类型,1代表直播会议 2代表普通会议; 默认为2(普通会议)
 *  m_speakerCount  [IN] 发言人数
 *  m_Speakers      [IN] 发言者列表
 */
typedef struct _JoinMeetingInfo { 
	int					 m_lockInfo;			//是否加锁 1 加锁 0 不加锁
	int					 m_presenterId;			//主持人账号ID
	int					 m_userType;			//参会用户类型，1为普通用户；2为主持人用户。
	int					 m_meetingStyle;		//会议模式，1为自由模式；2为主持人模式
	char				 m_presenterName[MEETINGMANAGE_USERNAME_LEN];	//主持人名字
	int					 m_liveStatus;			//类型int；1代表直播会议，2代表普通会议
	int 				 m_speakerCount;		//发言人数
	MeetingSpeakerInfo * m_Speakers;			//发言者列表
} JoinMeetingInfo;

//会议信息
typedef struct _MeetingInfo
{
	int				 m_meetingId;
	int				 m_meetingType;
	int				 m_createrAccountId;
	char			 m_createrName[MEETINGMANAGE_USERNAME_LEN];
	char			 m_startTime[MEETINGMANAGE_TIME_LEN];

	int				m_havepwd ; //接口服务器确定取值，然后跟UI 确定取值的含义 	//-1:初始值，0：没密码，1：有密码
	int				m_HostID;	//20160715密码需求修改
	char			m_topic[MEETINGMANAGE_TOPIC_LEN];	//会议主题 20160805

	int				m_meetingStatus;			//会议状态: 1 未开始   2 进行中   3 结束
} MeetingInfo;

/**************************** 媒体参数定义 *********************************/

//媒体流类型定义
typedef enum MEETINGMANAGE_StreamType
{
	MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND,     //视频发送链路
	MEETINGMANAGE_STREAM_TYPE_VIDEO_RECV,     //视频接收链路
	MEETINGMANAGE_STREAM_TYPE_AUDIO_SEND,     //音频发送链路
	MEETINGMANAGE_STREAM_TYPE_AUDIO_RECV,     //音频接收链路
	MEETINGMANAGE_STREAM_TYPE_LIVE,           //直播录制链路
	MEETINGMANAGE_STREAM_TYPE_CMD_SEND,       //命令发送链路
	MEETINGMANAGE_STREAM_TYPE_CMD_RECV        //命令接收链路
} MEETINGMANAGE_StreamType;

//媒体流来源类型定义
typedef enum _MEETINGMANAGESourceType {
	MEETINGMANAGE_SOURCE_TYPE_UNKNOWN = -1,
	MEETINGMANAGE_SOURCE_TYPE_DEVICE,         ///<设备源 设备
	MEETINGMANAGE_SOURCE_TYPE_FILE,           ///<文件源
	MEETINGMANAGE_SOURCE_TYPE_NET,            ///<网络源
	MEETINGMANAGE_SOURCE_TYPE_CALLBACK        ///<回调源 远程
} MEETINGMANAGESourceType;

typedef enum _MEETINGMANAGE_VideoColorSpace
{
	MEETINGMANAGE_COLORSPACE_I420,
	MEETINGMANAGE_COLORSPACE_YV12,
	MEETINGMANAGE_COLORSPACE_NV12,
	MEETINGMANAGE_COLORSPACE_YUY2,
	MEETINGMANAGE_COLORSPACE_YUYV,
	MEETINGMANAGE_COLORSPACE_RGB,
	MEETINGMANAGE_COLORSPACE_MJPG
}MEETINGMANAGE_VideoColorSpace;

#define COLORSPACE_NAME_I420 "I420"
#define COLORSPACE_NAME_YV12 "YV12"
#define COLORSPACE_NAME_NV12 "NV12"
#define COLORSPACE_NAME_YUY2 "YUY2"
#define COLORSPACE_NAME_YUYV "YUYV"
#define COLORSPACE_NAME_RGB  "RGB"
#define COLORSPACE_NAME_MJPG "MJPG"

typedef struct _MEETINGMANAGE_VideoSize
{
	int width;
	int height;
}MEETINGMANAGE_VideoSize;

typedef struct _MEETINGMANAGE_VideoFormat
{
	int colorspace;             //VideoColorSpace
	char colorspaceName[32];
	MEETINGMANAGE_VideoSize size[32];
	int fps[32];
	int sizeCount;
	int fpsCount;
}MEETINGMANAGE_VideoFormat;

typedef struct _MEETINGMANAGE_VideoDeviceInfo
{
	char name[MEETINGMANAGE_DEVICENAME_LEN];
	MEETINGMANAGE_VideoFormat format[16];
	int formatCount;
}MEETINGMANAGE_VideoDeviceInfo;

typedef struct _MEETINGMANAGEVideoCapParam
{
	void    *capWinHandle;
	int     left;
	int     top;
	int     right;
	int     bottom;
	int 	fps;									///<帧率
	MEETINGMANAGE_VideoColorSpace colorSpace;		//颜色空间
}MEETINGMANAGEVideoCapParam;

typedef enum _MEETINGMANAGEVideoCodecID
{
	MEETINGMANAGE_VIDEO_CODEC_ID_H264,				///<H264编解码
	MEETINGMANAGE_VIDEO_CODEC_ID_VP8				///<VP8编解码
}MEETINGMANAGEVideoCodecID;

typedef enum __MEETINGMANAGEVideoCodecLevel
{
	MEETINGMANAGE_CODEC_LEVEL_LOW,
	MEETINGMANAGE_CODEC_LEVEL_MEDIUM,
	MEETINGMANAGE_CODEC_LEVEL_HIGH
}MEETINGMANAGEVideoCodecLevel;

typedef enum _MEETINGMANAGEVideoCodecType
{
	MEETINGMANAGE_CODEC_TYPE_SOFT,      ///<软编
	MEETINGMANAGE_CODEC_TYPE_HARD,      ///<硬编

}MEETINGMANAGEVideoCodecType;

typedef struct _MEETINGMANAGEVideoEncParam
{
	int 				width;						///<宽
	int 				height;						///<高
	int 				fps;						///<帧率
	int 				bitrate;					///<码率(码率单位Kbps)
	MEETINGMANAGEVideoCodecLevel 	level;          ///<编码级别
	MEETINGMANAGEVideoCodecID 		codecID;        ///<编码器ID
	MEETINGMANAGEVideoCodecType      codecType;     ///<编码器类型
} MEETINGMANAGEVideoEncParam;

//显示充满模式定义
typedef enum _MEETINGMANAGE_DisplayFillMode
{
	//保持原始比例充满模式，留黑边
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_WITHBLACK,
	//保持原始比例充满模式，不留黑边,居中
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_NOBLACK_MIDDLE,
	//保持原始比例充满模式，不留黑边,单边裁剪-左上
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_NOBLACK_LEFT,
	//保持原始比例充满模式，不留黑边,单边裁剪-右下
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_NOBLACK_RIGHT,
	//拉伸充满模式
	MEETINGMANAGE_FULL_FILL_MODE_STRECH
} MEETINGMANAGE_DisplayFillMode;

//传输参数
typedef struct _MEETINGMANAGE_transParam
{
	int             fecDataCount;            //fec数据比例，-1 表示不指定
	int             fecCheckCount;           //fec校验包比例 -1 表示不指定
	int             dataSendCount;	         //数据包发送份数 -1 表示不指定
	int             checkSendCount;	         //校验包发送份数 -1 表示不指定
	int             dataRetransSendCount;	 //补发数据包发送份数 -1 表示不指定
	int             checkRetransSendCount;	 //补发校验包发送份数 -1 表示不指定
	int             dataResendCount;		 //补发请求次数 -1 表示不指定
	int				delayTimeWinsize;		 //接收延迟窗口大小，单位ms（仅下行使用） -1 表示不指定
} MEETINGMANAGE_transParam;


//媒体类型
typedef enum _MEETINGMANAGE_MediaType
{
	MEETINGMANAGE_VIDEO_CAMRA,				 //摄像头视频
	MEETINGMANAGE_AUDIO_MICPHONE,			 //麦克风音频
	MEETINGMANAGE_VIDEO_DOC,				 //屏幕采集视频(采集桌面,采集进程)
	MEETINGMANAGE_AUDIO_DOC,				 //屏幕分享音频
	MEETINGMANAGE_VIDEO_CAPTURECARD,		 //采集卡视频
	MEETINGMANAGE_AUDIO_CAPTURECARD,		 //采集卡音频
	MEETINGMANAGE_STREAMMEDIA,				 //流媒体
	MEETINGMANAGE_FILE,					     //文件
	MEETINGMANAGE_WHITEBOARD,				 //白板
	MEETINGMANAGE_REMOTECTRL,				 //远程控制
	MEETINGMANAGE_MEIDATYPE_MAX
} MEETINGMANAGE_MediaType;

typedef struct _MEETINGMANAGEAudioCapParam
{
	int 	samplerate;						 ///<采样率
	int 	channels;						 ///<声道数
	int 	bitspersample;					 ///<采样精度
}MEETINGMANAGEAudioCapParam;

typedef enum _MEETINGMANAGEAudioCodecID
{
	MEETINGMANAGE_AUDIO_CODEC_UNKNOWN = -1,
	MEETINGMANAGE_AUDIO_CODEC_ALAW,          ///<711 alaw
	MEETINGMANAGE_AUDIO_CODEC_ULAW,          ///<711 ulaw
	MEETINGMANAGE_AUDIO_CODEC_SPEEX,         ///<speex
	MEETINGMANAGE_AUDIO_CODEC_OPUS,           ///<opus
	MEETINGMANAGE_AUDIO_CODEC_AAC

}MEETINGMANAGEAudioCodecID;

typedef struct _MEETINGMANAGEAudioEncParam
{
	int 			samplerate;				 ///<采样率
	int 			channels;				 ///<声道数
	int 			bitspersample;			 ///<采样精度
	int 			bitrate;				 ///<码率(码率单位Kbps)
	MEETINGMANAGEAudioCodecID 	codecID;     ///<编码器ID
}MEETINGMANAGEAudioEncParam;


typedef struct _MEETINGMANAGE_VideoSendStreamParam {
	//发布摄像头媒体流、本地桌面媒体流时，取值为MEETINGMANAGE_SOURCE_TYPE_DEVICE
	//发布远端桌面媒体流时，取值为MEETINGMANAGE_SOURCE_TYPE_CALLBACK
	MEETINGMANAGESourceType  sourceType;

	//发布摄像头媒体流时，取值摄像头具体名称；
	//发布本地桌面媒体流时，取值“DesktopCapture”；
	//发布远端桌面媒体流时，可以不设值；
	char sourceName[MEETINGMANAGE_sourceName_len];

	// 发布流的额外信息
	char extraInfo[MEETINGMANAGE_EXTRAINFO_LEN];

	struct {
		//发布远端桌面媒体流时，不需要设置采集以及编码参数
		MEETINGMANAGEVideoCapParam		* capParam;	//视频采集参数
		MEETINGMANAGEVideoEncParam		* encParam;	//视频编码参数

		void					* displayWindow;	// 视频预览窗口句柄
		MEETINGMANAGE_DisplayFillMode   fillMode;	//显示充满模式,预定义显示模式，用户可以自己指定需要的显示比例
	} vsParam;  //视频发送参数结构体

} MEETINGMANAGE_VideoSendStreamParam;

//发布摄像头视频流参数定义
typedef struct _MEETINGMANAGE_PublishCameraParam
{
	int avSynGroupID;							 //音视频同步ID
	MEETINGMANAGE_StreamType sType;		         //媒体流类型, 发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
	MEETINGMANAGE_VideoSendStreamParam	 sParam; //摄像头采集编码相关参数
	MEETINGMANAGE_transParam    transParam;		 //媒体流传输相关参数
	MEETINGMANAGE_MediaType	 mediaType;     	 //媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_CAMRA
} MEETINGMANAGE_PublishCameraParam;


// 发布本地桌面视频流参数定义
typedef struct _MEETINGMANAGE_WinCaptureVideoParam
{
	int avSynGroupID;							 //音视频同步ID
	MEETINGMANAGE_StreamType sType;		         //媒体流类型, 发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
	MEETINGMANAGE_VideoSendStreamParam sParam;   //摄像头采集编码相关参数
	MEETINGMANAGE_transParam    transParam;		 //媒体流传输相关参数
	MEETINGMANAGE_MediaType	 mediaType;     	 //媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_DOC
} MEETINGMANAGE_WinCaptureVideoParam;

//发布远端桌面视频流参数定义
typedef struct _MEETINGMANAGE_RemoteWinCaptureVideoParam {
	int avSynGroupID;							 //音视频同步ID
	MEETINGMANAGE_StreamType sType;		         //媒体流类型, 发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
	MEETINGMANAGE_VideoSendStreamParam sParam;   //摄像头采集编码相关参数
	MEETINGMANAGE_transParam    transParam;		 //媒体流传输相关参数
	MEETINGMANAGE_MediaType	 mediaType;     	 //媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_DOC ??TODO，如何取值?
} MEETINGMANAGE_RemoteWinCaptureVideoParam;


typedef struct _MEETINGMANAGE_LiveParam
{
	char    url[2][256];					 ///<直播流地址，最多支持MAX_LIVE_NUM路直播
	char    filepath[256];                   ///<录制文件路径
	int     width;                           ///<直播画面宽度
	int     height;                          ///<直播画面高度
	int     vBitrate;                        ///<直播视频码率(单位Kbps)
	int     samplerate;                      ///<采样率
	int     channels;                        ///<声道数
	int     bitspersample;                   ///<采样精度
	int     aBitrate;                        ///<直播音频码率(单位Kbps)
	int     isLive;                          ///<是否直播
	int     isRecord;                        ///<是否录制
}MEETINGMANAGE_LiveParam;


// 发布直播流参数定义
typedef struct _MEETINGMANAGE_PubLiveStreamParam
{
	MEETINGMANAGE_StreamType sType;		         //媒体流类型, 发布直播流时,取值MEETINGMANAGE_STREAM_TYPE_LIVE
	MEETINGMANAGE_LiveParam sParam;				  //直播相关参数
	MEETINGMANAGE_MediaType	 mediaType;     	 //媒体类型,直播流时，取值MEETINGSDK_STREAMMEDIA
} MEETINGMANAGE_PubLiveStreamParam;


typedef enum _MEETINGMANAGE_MixVideoType
{
	MEETINGMANAGE_IS_VIDEO_TYPE = 0,
	MEETINGMANAGE_IS_DATA_TYPE
}MEETINGMANAGE_MixVideoType;

typedef struct _MEETINGMANAGE_VideoStreamInfo
{
	int 	xPos;              ///<视频在画布上的x坐标
	int 	yPos;              ///<视频在画布上的y坐标
	int 	width;             ///<视频在画布上的宽
	int 	height;            ///<视频在画布上的高
	int 	resourceid;          ///<资源id
	char    userid[MEETINGMANAGE_USERID_LEN]; //视讯号
	MEETINGMANAGE_MixVideoType videoType;		   ///<视频类型，是人像还是数据分享
}MEETINGMANAGE_VideoStreamInfo;

typedef struct _MEETINGMANAGE_AudioStreamInfo
{
	int 	samplerate;         ///<采样率
	int 	channels;           ///<声道数
	int 	bitspersample;      ///<采样精度
	int 	resourceid;           ///<资源id
	char    userid[MEETINGMANAGE_USERID_LEN]; //视讯号
}MEETINGMANAGE_AudioStreamInfo;

//audio发送流参数
typedef struct _MEETINGMANAGE_AudioSendStreamParam {
	MEETINGMANAGESourceType  sourceType;                  //MEETINGMANAGE_SOURCE_TYPE_DEVICE
	char        sourceName[MEETINGMANAGE_sourceName_len]; //音频设备名称， 如果设备是采集卡，是否可以传采集卡的名称;TODO???

	// 发布流的额外信息
	char extraInfo[MEETINGMANAGE_EXTRAINFO_LEN];

	struct {
		MEETINGMANAGEAudioCapParam  *capParam;     //音频采集参数
		MEETINGMANAGEAudioEncParam  *encParam;	   //音频编码参数
		int             isMix;				       //是否混音标识 ，取值始终为1
	}asParam;  //音频发送参数结构体
} MEETINGMANAGE_AudioSendStreamParam;


//发布媒体流参数定义
////////////////
typedef struct _MEETINGMANAGE_publishMicParam {
		int avSynGroupID;
		MEETINGMANAGE_StreamType   sType;				 //媒体流类型,默认取值为MEETINGMANAGE_STREAM_TYPE_AUDIO_SEND
		MEETINGMANAGE_AudioSendStreamParam	sParam;      //麦克风采集编码相关参数
		MEETINGMANAGE_transParam   transParam;			 //媒体流传输相关参数
		MEETINGMANAGE_MediaType	mediaType;     			 //媒体类型,发布音频流时，取值MEETINGMANAGE_AUDIO_MICPHONE, 如果是外置声卡，如何选择？？
} MEETINGMANAGE_publishMicParam;


typedef struct _MEETINGMANAGE_VideoRecvStreamParam {
	struct {
		void					* displayWindow;	// 视频预览窗口句柄
		MEETINGMANAGE_DisplayFillMode   fillMode;	//显示充满模式,预定义显示模式，用户可以自己指定需要的显示比例
	} vrParam;  //视频接收参数结构体
} MEETINGMANAGE_VideoRecvStreamParam;

typedef struct _MEETINGMANAGE_subscribeVideoParam
{
	char 		   			  userid[MEETINGMANAGE_USERID_LEN]; //视讯号
	MEETINGMANAGE_StreamType     sType;							//媒体流类型, 取值MEETINGMANAGE_STREAM_TYPE_VIDEO_RECV
	MEETINGMANAGE_VideoRecvStreamParam    sParam;				//媒体流参数
	int 					  resourceID; 	  					//资源标识
	MEETINGMANAGE_MediaType   mediaType;					    //媒体类型,取值MEETINGMANAGE_VIDEO_CAMRA以及MEETINGMANAGE_VIDEO_DOC
	unsigned int 			  AVSynGroupID;                     //音视频同步标识
	MEETINGMANAGE_transParam     transParam;
} MEETINGMANAGE_subscribeVideoParam;


typedef struct _MEETINGMANAGE_AudioRecvStreamParam
{
	MEETINGMANAGESourceType  sourceType;

	char sourceName[MEETINGMANAGE_sourceName_len];

	struct
	{
		int    isMix;   //是否混音标识, 默认设置为1
	} arParam;  //音频接收参数结构体
} MEETINGMANAGE_AudioRecvStreamParam;

	typedef struct _MEETINGMANAGE_subscribeAudioParam
{
	char 		   						  userid[MEETINGMANAGE_USERID_LEN]; //视讯号
	MEETINGMANAGE_StreamType				  sType;						//媒体流类型
	MEETINGMANAGE_AudioRecvStreamParam    sParam;							//媒体流参数
	int 								  resourceID; 	  					//资源标识
	MEETINGMANAGE_MediaType 				  mediaType;						//媒体类型
	unsigned int 						  AVSynGroupID;                     //音视频同步标识
	MEETINGMANAGE_transParam			  transParam;
}MEETINGMANAGE_subscribeAudioParam;

#endif