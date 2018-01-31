using System;
using System.Runtime.InteropServices;

namespace MeetingSdk.NetAgent
{

    #region MeetingManage
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ContextData
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)] public string JsonData;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string UniqueId;
    }

    class Len
    {
        public const int UserId = 64;
        public const int SourceName = 2048;
        public const int DeviceName = 256;
        public const int UserName = 128;
        public const int Time = 64;
        public const int ExtraInfo = 1024;
        public const int StreamInfoCount = 20;
        public const int Topic = 200;
        public const int Token = 128;
        public const int ContactId = 256;
        public const int ContactName = 256;
    }

    // SDK错误返回类型
    enum SDKErrorCode
    {
        SDK_ERRCODE_SUCCEED = 0,        // 成功
        SDK_ERRCODE_SDK_UNINIT = -1,        // sdk未启动
        SDK_ERRCODE_FAILED = -1000, // 失败（通用）
        SDK_ERRCODE_INVALID_ARGUMENTS = -1001,  // 无效参数
        SDK_ERRCODE_NETWORK_DISCONNECT = -1002,    // 网络异常
        SDK_ERRCODE_FUNCTION_INVOKE_AGAIN = -1003,  // 重复调用
        SDK_ERRCODE_SEND_REQUEST_FAILED = -1004,    // 发送请求到服务器失败（通用）
        SDK_ERRCODE_MEETING_CALLBACK_NULL = -1005,  // 会议模块回调实例为空
        SDK_ERRCODE_GET_NPSREQUEST_FAILED = -1006,    // 发送获取NPS配置请求失败
        SDK_ERRCODE_START_ASYNMODEL_FAILED = -1007, // 启动异步模型失败
        SDK_ERRCODE_GETTOKEN_FAILED = -1008,    // 获取token失败
        SDK_ERRCODE_BINDTOKEN_REQUEST_FAILD = -1009,    // 发送鉴权请求失败
        SDK_ERRCODE_RECORDSOUND_FAILED = -1010, // 音频采集测试失败
        SDK_ERRCODE_INMEETING = -1011,  // 在会议中
        SDK_ERRCODE_NOT_INMEETING = -1012,  // 不在会议中
        SDK_ERRCODE_NOT_SPEAKING = -1013,   // 不是发言状态
        SDK_ERRCODE_MEETINGSDK_FAILED = -1014,  // 媒体库请求错误
        SDK_ERRCODE_GETUSERLIST_NOTINMEETING = -1015,   // 获取参会列表，不在会议中
        SDK_ERRCODE_GETUSERLIST_REQUESTFAILED = -1016,  // 发送获取参会列表请求失败
        SDK_ERRCODE_START_MEETCTRLAGENT_FAILED = -1017, // 启动会控Agent失败
    };

    //SDK异步回调状态码
    enum SDKCallbackStatus
    {
        SDK_STATUS_SUCCEED = 0,     //成功
        SDK_STATUS_FAIL = -1999,    //异步调用失败（通用错误）
        SDK_STATUS_GETCONFIG_FAIL = -2000,  //获取配置信息失败 OnStart 
        SDK_STATUS_LOGIN_FAIL = -2001,  //鉴权失败 OnLogin
        SDK_STATUS_LOGIN_NOSERVICE = -2002, //企业中心登陆返回,服务未开通 OnLogin
        SDK_STATUS_LOGIN_SERVICEOUT = -2003,    //企业中心登陆返回,服务逾期 OnLogin
        SDK_STATUS_BINDTOKEN_FAIL = -2004,  //绑定token失败 OnLogin OnBindToken
        SDK_STATUS_START_AGENT_FAIL = -2005,    //启动本地代理失败 OnBindToken
        SDK_STATUS_MEET_NOTEXIST = -2006,   //会议号不存在 OnCheckMeetExist OnResetMeetingPassword OnGetMeetingPassword OnCheckMeetingHasPassword OnCheckMeetingPasswordValid
        SDK_STATUS_TOKEN_TIMEOUT = -2007,   //Token已过期 OnGetMeetingList OnResetMeetingPassword OnGetMeetingPassword OnCheckMeetingPasswordValid
        SDK_STATUS_NOT_HOST = -2008,    //接口调用者不是会议主持人 OnResetMeetingPassword OnGetMeetingPassword
        SDK_STATUS_WRONG_PWD = -2009,   //会议密码错误 OnCheckMeetingPasswordValid
        SDK_STATUS_JOIN_CREATED_MEET_FAIL = -2010,  //加入新创建的即时会议失败
        SDK_STATUS_JOINMEET_FAIL_INMEET = -2011,    //加入会议失败，已经在会议中，需要先退会
        SDK_STATUS_JOINMEET_FAIL_GETUSERLIST = -2012,   //加入会议成功，但取参会人列表失败
        SDK_STATUS_NOTOKEN = -2013, //token无效或token过期,会控服务器返回
        SDK_STATUS_JOINMEET_FAIL_MEETEND = -2014,   //加入会议失败，会议已结束
        SDK_STATUS_JOINMEET_FAIL_SERVER_ERR = -2015,    //加入会议失败，服务器返回-915
        SDK_STATUS_JOINMEET_FAIL_MEETLOCK = -2016,  //加入会议失败，会议已加锁
        SDK_STATUS_JOINMEET_FAIL_NORSP = -2017, //加入会议失败，请求超时，服务器无应答
        SDK_STATUS_NOTINMEET = -2018,   //不在会议中，操作失败
    };


    // 设备状态变化类型
    enum DevStatusChangeType
    {
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
    enum SDKExceptionType
    {
        SETYPE_UNKNOWN = -1,              // 未知
        SETYPE_NETWORK_RECONNECT,         // 网络重连
        SETYPE_NETWORK_ENDRETRY,          // 网络重连结束
        SETYPE_NETWORK_RECONNECT_SUCCEED, // 网络重连成功
        SETYPE_TOKEN_TIMEOUT              // TOKEN过期
    };

    enum NetDiagnosticType
    {
        NDT_UNKNOWN = -1,       // 未知
        NDT_NET_CONN,           // 网络连通性
        NDT_MEET_CONN,          // 会议连通性
        NDT_BAND_DETECT         // 网速检测
    };

    enum MM_MeetingType
    {
        MT_UNKNOWN = -1,        // 未知
        MT_JUSTMEETING = 1,     // 即时会议
        MT_DATEDMEETING         // 预约会议
    };

    //帧类型
    enum MEETINGMANAGE_FrameType
    {
        MEETINGMANAGE_FRAME_TYPE_YUV,
        MEETINGMANAGE_FRAME_TYPE_H264,
        MEETINGMANAGE_FRAME_TYPE_RTP
    }
    

    //smsdk通知
    enum MEETINGMANAGE_SMSDK_CBTYPE
    {
        SMSDK_TYPE_ERR, //通知失败
        SMSDK_TYPE_NET_LINKNORMAL, //网络信号稳定
        SMSDK_TYPE_NET_LINKBAD, //网络信号差
        SMSDK_TYPE_NET_LINKERROR, //网络信号极差
        SMSDK_TYPE_VIDEO_CLOSE_ADAPTE, //自适应关闭视频
        SMSDK_TYPE_CPU_OTHEROVERLOAD, //其他进程占用cpu过载
        SMSDK_TYPE_CPU_SELFOVERLOD, //自身占用cpu过载
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct LoginInfo
    {
        public int AccountId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string AccountName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.Time)] public string TokenStartTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.Time)] public string TokenEndTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.Token)] public string Token;
    }



    //参会人信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ParticipantInfo
    {
        public int AccountId; //视讯号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string AccountName; //昵称
        public byte IsRaiseHand; //是否已经举手
        public byte IsSpeaking; //是否正在发言
        public short Dummy;
        public int HostFlag; //是否是主持人
        public byte RenderOpened; //扬声器打开关闭状态
    }


    //流信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingUserStreamInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string AccountName; //昵称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.DeviceName)] public string DeviceName;
        public int AccountId; //视讯号
        public int ResourceId;
        public int SynGroupId; //同步ID
        public int MediaType; //类型说明 枚举
    }



    //发言人信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingSpeakerInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)]
        public string AccountName;
        public int AccountId;
        public int StreamCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Len.StreamInfoCount)]
        public MeetingUserStreamInfo[] StreamInfos;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct JoinMeetingInfo
    {
        public int LockInfo; //是否加锁 1 加锁 0 不加锁
        public int PresenterId; //主持人账号ID
        public int UserType; //参会用户类型，1为普通用户；2为主持人用户。
        public int MeetingStyle; //会议模式，1为自由模式；2为主持人模式
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string PresenterName; //主持人名字
        public int LiveStatus; //类型int；1代表直播会议，2代表普通会议
        public int SpeakerCount; //发言人数
        public IntPtr SpeakersPtr; //发言者列表
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingInfo
    {
        public int MeetingId;
        public int MeetingType;
        public int CreatorAccountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string CreatorName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.Time)] public string StartTime;
        public int HavePwd;
        public int HostId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.Topic)] public string Topic;
        public int MeetingStatus;
    }


    //媒体流类型定义
    internal enum MEETINGMANAGE_StreamType
    {
        MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND, //视频发送链路
        MEETINGMANAGE_STREAM_TYPE_VIDEO_RECV, //视频接收链路
        MEETINGMANAGE_STREAM_TYPE_AUDIO_SEND, //音频发送链路
        MEETINGMANAGE_STREAM_TYPE_AUDIO_RECV, //音频接收链路
        MEETINGMANAGE_STREAM_TYPE_LIVE, //直播录制链路
        MEETINGMANAGE_STREAM_TYPE_CMD_SEND, //命令发送链路
        MEETINGMANAGE_STREAM_TYPE_CMD_RECV //命令接收链路
    }

    //媒体流来源类型定义
    internal enum MEETINGMANAGESourceType
    {
        MEETINGMANAGE_SOURCE_TYPE_UNKNOWN = -1,
        MEETINGMANAGE_SOURCE_TYPE_DEVICE,

        //设备源设备
        MEETINGMANAGE_SOURCE_TYPE_FILE,

        //文件源
        MEETINGMANAGE_SOURCE_TYPE_NET,

        //网络源
        MEETINGMANAGE_SOURCE_TYPE_CALLBACK //回调源远程
    }

    internal enum MEETINGMANAGE_VideoColorSpace
    {
        MEETINGMANAGE_COLORSPACE_I420,
        MEETINGMANAGE_COLORSPACE_YV12,
        MEETINGMANAGE_COLORSPACE_NV12,
        MEETINGMANAGE_COLORSPACE_YUY2,
        MEETINGMANAGE_COLORSPACE_YUYV,
        MEETINGMANAGE_COLORSPACE_RGB,
        MEETINGMANAGE_COLORSPACE_MJPG
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct VideoSize
    {
        public int Width;
        public int Height;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct VideoFormat
    {
        public int Colorspace; //VideoColorSpace
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string ColorspaceName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public VideoSize[] Sizes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public int[] Fps;
        public int SizeCount;
        public int FpsCount;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct VideoDeviceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.DeviceName)] public string Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public VideoFormat[] Formats;
        public int FormatCount;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGEVideoCapParam
    {
        public IntPtr capWinHandle;
        public int left;
        public int top;
        public int right;
        public int bottom;
        public int fps; //帧率
        public MEETINGMANAGE_VideoColorSpace colorSpace; //颜色空间
    }

    internal enum MEETINGMANAGEVideoCodecID
    {
        MEETINGMANAGE_VIDEO_CODEC_ID_H264, //H264编解码
        MEETINGMANAGE_VIDEO_CODEC_ID_VP8 //VP8编解码
    }

    internal enum MEETINGMANAGEVideoCodecLevel
    {
        MEETINGMANAGE_CODEC_LEVEL_LOW,
        MEETINGMANAGE_CODEC_LEVEL_MEDIUM,
        MEETINGMANAGE_CODEC_LEVEL_HIGH
    }

    internal enum MEETINGMANAGEVideoCodecType
    {
        MEETINGMANAGE_VIDEO_CODEC_TYPE_SOFT, //软编
        MEETINGMANAGE_VIDEO_CODEC_TYPE_HARD //硬编
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGEVideoEncParam
    {
        public int width; //宽
        public int height; //高
        public int fps; //帧率
        public int bitrate; //码率(码率单位Kbps)
        public MEETINGMANAGEVideoCodecLevel level; //编码级别
        public MEETINGMANAGEVideoCodecID codecID; //编码器ID
        public MEETINGMANAGEVideoCodecType codecType; //编码器类型
    }


    //显示充满模式定义
    internal enum MEETINGMANAGE_DisplayFillMode
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
    }

    //传输参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_transParam
    {
        public int fecDataCount; //fec数据比例，-1 表示不指定
        public int fecCheckCount; //fec校验包比例 -1 表示不指定
        public int dataSendCount; //数据包发送份数 -1 表示不指定
        public int checkSendCount; //校验包发送份数 -1 表示不指定
        public int dataRetransSendCount; //补发数据包发送份数 -1 表示不指定
        public int checkRetransSendCount; //补发校验包发送份数 -1 表示不指定
        public int dataResendCount; //补发请求次数 -1 表示不指定
        public int delayTimeWinsize; //接收延迟窗口大小，单位ms（仅下行使用） -1 表示不指定
    }

    //媒体类型
    internal enum MEETINGMANAGE_MediaType
    {
        MEETINGMANAGE_VIDEO_CAMRA, //摄像头视频
        MEETINGMANAGE_AUDIO_MICPHONE, //麦克风音频
        MEETINGMANAGE_VIDEO_DOC, //屏幕采集视频(采集桌面,采集进程)
        MEETINGMANAGE_AUDIO_DOC, //屏幕分享音频
        MEETINGMANAGE_VIDEO_CAPTURECARD, //采集卡视频
        MEETINGMANAGE_AUDIO_CAPTURECARD, //采集卡音频
        MEETINGMANAGE_STREAMMEDIA, //流媒体
        MEETINGMANAGE_FILE, //文件
        MEETINGMANAGE_WHITEBOARD, //白板
        MEETINGMANAGE_REMOTECTRL, //远程控制
        MEETINGMANAGE_MEIDATYPE_MAX
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGEAudioCapParam
    {
        public int samplerate;                      ///<采样率
        public int channels;                        ///<声道数
        public int bitspersample;                   ///<采样精度
    }


    internal enum MEETINGMANAGEAudioCodecID
    {
        MEETINGMANAGE_AUDIO_CODEC_UNKNOWN = -1,
        MEETINGMANAGE_AUDIO_CODEC_ALAW,          ///<711 alaw
        MEETINGMANAGE_AUDIO_CODEC_ULAW,          ///<711 ulaw
        MEETINGMANAGE_AUDIO_CODEC_SPEEX,         ///<speex
        MEETINGMANAGE_AUDIO_CODEC_OPUS,           ///<opus
        MEETINGMANAGE_AUDIO_CODEC_AAC
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGEAudioEncParam
    {
        public int samplerate;              ///<采样率
        public int channels;                ///<声道数
        public int bitspersample;           ///<采样精度
        public int bitrate;                 ///<码率(码率单位Kbps)
        public MEETINGMANAGEAudioCodecID codecID;     ///<编码器ID
    }



    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_VideoSendStreamParam
    {
        //发布摄像头媒体流、本地桌面媒体流时，取值为MEETINGMANAGE_SOURCE_TYPE_DEVICE
        //发布远端桌面媒体流时，取值为MEETINGMANAGE_SOURCE_TYPE_CALLBACK
        public MEETINGMANAGESourceType sourceType;

        //发布摄像头媒体流时，取值摄像头具体名称；
        //发布本地桌面媒体流时，取值“DesktopCapture”；
        //发布远端桌面媒体流时，可以不设值；
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.SourceName)] public string sourceName;

        // 发布流的额外信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ExtraInfo)] public string extraInfo;

        public VideoSendParam vsParam;
    }


    //发布摄像头视频流参数定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_PublishCameraParam
    {
        public int avSynGroupID; //音视频同步ID
        public MEETINGMANAGE_StreamType sType; //媒体流类型, 发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
        public MEETINGMANAGE_VideoSendStreamParam sParam; //摄像头采集编码相关参数
        public MEETINGMANAGE_transParam transParam; //媒体流传输相关参数
        public MEETINGMANAGE_MediaType mediaType; //媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_CAMRA
    }

    // 发布本地桌面视频流参数定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_WinCaptureVideoParam
    {
        public int avSynGroupID;                            //音视频同步ID
        public MEETINGMANAGE_StreamType sType;              //媒体流类型, 发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
        public MEETINGMANAGE_VideoSendStreamParam sParam;   //摄像头采集编码相关参数
        public MEETINGMANAGE_transParam transParam;         //媒体流传输相关参数
        public MEETINGMANAGE_MediaType mediaType;       //媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_DOC
    }


    //发布远端桌面视频流参数定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_RemoteWinCaptureVideoParam
    {
        public int avSynGroupID; //音视频同步ID
        public MEETINGMANAGE_StreamType sType; //媒体流类型, 发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
        public MEETINGMANAGE_VideoSendStreamParam sParam; //摄像头采集编码相关参数
        public MEETINGMANAGE_transParam transParam; //媒体流传输相关参数
        public MEETINGMANAGE_MediaType mediaType; //媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_DOC ??TODO，如何取值?
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_LiveParam
    {
        ///<直播流地址，最多支持MAX_LIVE_NUM路直播
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string Url1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string Url2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string filepath;

        ///<录制文件路径
        public int width;

        ///<直播画面宽度
        public int height;

        ///<直播画面高度
        public int vBitrate;

        ///<直播视频码率(单位Kbps)
        public int samplerate;

        ///<采样率
        public int channels;

        ///<声道数
        public int bitspersample;

        ///<采样精度
        public int aBitrate;

        ///<直播音频码率(单位Kbps)
        public int isLive;

        ///<是否直播
        public int isRecord; ///<是否录制
    }


    // 发布直播流参数定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_PubLiveStreamParam
    {
        public MEETINGMANAGE_StreamType sType;              //媒体流类型, 发布直播流时,取值MEETINGMANAGE_STREAM_TYPE_LIVE
        public MEETINGMANAGE_LiveParam sParam;               //直播相关参数
        public MEETINGMANAGE_MediaType mediaType;       //媒体类型,直播流时，取值MEETINGSDK_STREAMMEDIA
    }

    internal enum MEETINGMANAGE_MixVideoType
    {
        MEETINGMANAGE_IS_VIDEO_TYPE = 0,
        MEETINGMANAGE_IS_DATA_TYPE
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_VideoStreamInfo
    {
        public int xPos;              ///<视频在画布上的x坐标
        public int yPos;              ///<视频在画布上的y坐标
        public int width;             ///<视频在画布上的宽
        public int height;            ///<视频在画布上的高
        public int streamID;          ///<媒体流标识
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)]
        public string userid; //视讯号
        public MEETINGMANAGE_MixVideoType videoType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_AudioStreamInfo
    {
        public int samplerate;         ///<采样率
        public int channels;           ///<声道数
        public int bitspersample;      ///<采样精度
        public int streamID;           ///<媒体流标识
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)]
        public string userid; //视讯号
    }


    //audio发送流参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_AudioSendStreamParam
    {
        public MEETINGMANAGESourceType sourceType; //MEETINGMANAGE_SOURCE_TYPE_DEVICE

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.SourceName)] public string sourceName;
        //音频设备名称， 如果设备是采集卡，是否可以传采集卡的名称;TODO???

        // 发布流的额外信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ExtraInfo)] public string extraInfo;

        public PublishAudioParam asParam;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct PublishAudioParam
    {
        public IntPtr capParam; //音频采集参数
        public IntPtr encParam; //音频编码参数
        public int isMix; //是否混音标识 ，取值始终为1
    } //音频发送参数结构体


    //发布媒体流参数定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_publishMicParam
    {
        public int avSynGroupID;
        public MEETINGMANAGE_StreamType sType;              //媒体流类型,默认取值为MEETINGMANAGE_STREAM_TYPE_AUDIO_SEND
        public MEETINGMANAGE_AudioSendStreamParam sParam;      //麦克风采集编码相关参数
        public MEETINGMANAGE_transParam transParam;             //媒体流传输相关参数
        public MEETINGMANAGE_MediaType mediaType;               //媒体类型,发布音频流时，取值MEETINGMANAGE_AUDIO_MICPHONE, 如果是外置声卡，如何选择？？
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_VideoRecvStreamParam
    {
        public VideoRecvParam vrParam;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct VideoRecvParam
    {
        public IntPtr displayWindow;    // 视频预览窗口句柄
        public MEETINGMANAGE_DisplayFillMode fillMode; //显示充满模式,预定义显示模式，用户可以自己指定需要的显示比例
    }  //视频接收参数结构体






    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct VideoSendParam
    {
        //发布远端桌面媒体流时，不需要设置采集以及编码参数
        public IntPtr capParam; //视频采集参数
        public IntPtr encParam; //视频编码参数

        public IntPtr displayWindow; // 视频预览窗口句柄
        public MEETINGMANAGE_DisplayFillMode fillMode; //显示充满模式,预定义显示模式，用户可以自己指定需要的显示比例
    } //视频发送参数结构体

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_subscribeVideoParam
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)]
        public string userid; //视讯号
        public MEETINGMANAGE_StreamType sType;                         //媒体流类型, 取值MEETINGMANAGE_STREAM_TYPE_VIDEO_RECV
        public MEETINGMANAGE_VideoRecvStreamParam sParam;              //媒体流参数
        public int resourceID;                         //资源标识
        public MEETINGMANAGE_MediaType mediaType;                      //媒体类型,取值MEETINGMANAGE_VIDEO_CAMRA以及MEETINGMANAGE_VIDEO_DOC
        public uint AVSynGroupID;                     //音视频同步标识
        public MEETINGMANAGE_transParam transParam;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_AudioRecvStreamParam
    {
        public MEETINGMANAGESourceType sourceType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.SourceName)] public string sourceName;
        public AudioRecvParam arParam;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct AudioRecvParam
    {
        public int isMix;   //是否混音标识, 默认设置为1
    }//音频接收参数结构体


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MEETINGMANAGE_subscribeAudioParam
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)] public string userid; //视讯号
        public MEETINGMANAGE_StreamType sType; //媒体流类型
        public MEETINGMANAGE_AudioRecvStreamParam sParam; //媒体流参数
        public int resourceID; //资源标识
        public MEETINGMANAGE_MediaType mediaType; //媒体类型
        public uint AVSynGroupID; //音视频同步标识
        public MEETINGMANAGE_transParam transParam;
    }


    #endregion


    #region HostManage

    // HOST错误返回类型
    enum HostErrorCode
    {
        HOST_ERRCODE_SUCCEED = 0,   // 成功
        HOST_ERRCODE_FAILED = -1000,    // 失败（通用）
        HOST_ERRCODE_INVALID_ARGUMENTS = -1001, // 无效参数
        HOST_ERRCODE_NETWORK_DISCONNECT = -1002,    // 网络异常
        HOST_ERRCODE_OTHER_TASK_DOING = -1003,  // 其他任务进行中
        HOST_ERRCODE_SEND_REQUEST_FAILED = -1004,   // 发送请求到服务器失败（通用）
        HOST_ERRCODE_GET_NPSREQUEST_FAILED = -1006,    // 发送获取NPS配置请求失败
        HOST_ERRCODE_HOSTAGENT_START_FAILED = -1007,    // 启动HostAgent失败
        HOST_ERRCODE_HOSTAGENT_REGISTER_FAILED = -1008, // 注册HostAgent失败
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct RecommendContactInfo
    {
        public int m_sourceId;                                 //推荐人
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ContactId)]
        public string m_contactListId;       //通讯录id
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ContactName)]
        public string m_contactListName;   //通讯录名称
        public int m_contactListVer;                   //通讯录版本
        public int m_contacterNum;                     //联系人个数 
    }
    #endregion



    #region MeetingStruct

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void PFuncCallBack(int cmdId, IntPtr pData, int dataLen, IntPtr ctx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_VIDEO_PREVIEW(string pFrame, int width, int height, int frameLen);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct AsyncCallbackResult
    {
        public int Status;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string Message;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IntArrayStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] IntArray;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct StringStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string String;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct LongStringStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4096)] public string String;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct AttendeeInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string m_accountName;
        public int m_accountId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct LoginResult
    {
        public AsyncCallbackResult Result;

        public LoginInfo LoginInfo;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct CreateMeetingResult
    {
        public AsyncCallbackResult Result;
        public MeetingInfo MeetingInfo;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct JoinMeetingResult
    {
        public int StatusCode;
        public IntPtr JoinMeetingInfo;
    }

    internal struct GetMeetingListResult
    {
        public AsyncCallbackResult Result;
        public IntPtr MeetingList;
        public int MeetingCount;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct SpeakResult
    {
        public int SpeakReason;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string AccountName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct UserSpeakResult
    {
        public int m_speakeReason;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string m_accountName;
        public int m_newSpeakerAccountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string m_newSpeakerAccountName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct PublishStreamResult
    {
        public int m_statusCode;
        public int m_streamId;
    }

    /// <summary>
    /// 消息类用Data后缀
    /// OnUserPublishCameraVideoEvent
    /// OnUserPublishDataVideoEvent
    /// OnUserPublishMicAudioEvent
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct UserPublishData
    {
        public int accountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string accountName;
        public int syncId;
        public int resourceId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ExtraInfo)] public string extraInfo;
    }

    /// <summary>
    /// 消息类用Data后缀
    /// OnUserUnPublishCameraVideoEvent
    /// OnUserUnPublishDataCardVideoEvent
    /// OnUserUnPublishMicAudioEvent
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct UserUnpublishData
    {
        public int resourceId;
        public int accountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserName)] public string accountName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct SpeakerVideoStreamParamData
    {
        public int width;
        public int height;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct BandWidthData   
    {
        public int upWidth;
        public int downWidth;
    }



    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct YUVData
    {
        public int accountId;
        public int resourceId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.SourceName)]
        public string yuvBuffer;
        public int width;
        public int height;
        public int orientation;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct KickoutUserData
    {
        public int meetingId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)]
        public string kickedUserId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingModeInfo
    {
        
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingInvitationSMSData
    {
        public AsyncCallbackResult Result;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.SourceName)]
        public string invitationSMS;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ExtraInfo)]
        public string yyURL;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct OtherChangeAudioSpeakerStatusData
    {
        public int accountId;
        public int opType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct NetStatusResult
    {
        public NetDiagnosticType netStatusType;
        public AsyncCallbackResult m_result;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct NetLevelResult
    {
        public int NetLevel;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingPasswordResult
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)] public string password;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)] public string hostId;
        public AsyncCallbackResult m_result;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingHasPasswordResult
    {
        public int hasPwd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.UserId)] public string hostId;
        public AsyncCallbackResult m_result;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct TransparentMsgResult
    {
        public int senderAccountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string data;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct UiTransparentMsgResult
    {
        public int msgId;
        public int toAccountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string data;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct HostOperateTypeResult
    {
        public int operateType;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct VideoStreamInfoArray
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public MEETINGMANAGE_VideoStreamInfo[] VideoStreamInfos;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct AudioStreamInfoArray
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public MEETINGMANAGE_AudioStreamInfo[] AudioStreamInfos;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ExceptionResult
    {
        public SDKExceptionType exceptionType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string description;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.ExtraInfo)]
        public string extraInfo;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct SdkCallbackResult
    {
        public MEETINGMANAGE_SMSDK_CBTYPE type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string description;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct DeviceStatusResult
    {
        public DevStatusChangeType type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.DeviceName)]
        public string devName;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ResourceResult
    {
        public int accountId;
        public int resourceId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MeetingInvitationResult
    {
        public int inviterAccountId;
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = Len.UserName)]
        public string inviterAccountName;
        public int meetingId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ForcedOfflineResult
    {
        public int accountId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Len.Token)]
        public string token;
    }


    internal enum CallbackType
    {
        start,
        login,
        bind_token,
        check_meeting_exist,
        get_meeting_list,
        get_meeting_info,
        reset_meeting_password,
        get_meeting_password,
        check_meeting_has_password,
        check_meeting_password_valid,
        create_meeting,
        join_meeting,
        get_user_list,
        user_join_event,
        user_leave_event,
        ask_for_speak,
        start_speak_event,
        user_start_speak_event,
        ask_for_stop_speak,
        stop_speak_event,
        user_stop_speak_event,
        send_ui_msg,

        publish_camera_video,
        publish_win_capture_video,
        publish_data_card_video,
        publish_mic_audio,

        unpublish_camera_video,
        unpublish_win_capture_video,
        unpublish_data_card_video,
        unpublish_mic_audio,

        user_publish_camera_video_event,
        user_publish_data_video_event,
        user_publish_mic_audio_event,

        user_unpublish_camera_video_event,
        user_unpublish_data_card_video_event,
        user_unpublish_mic_audio_event,

        yuv_data,
        host_change_meeting_mode,
        host_change_meeting_mode_event,
        host_kickout_user,
        host_kickout_user_event,
        raise_hand_request,
        raise_hand_request_event,
        ask_for_meeting_lock,
        host_order_one_speak,
        host_order_one_stop_speak,
        lock_status_changed_event,
        meeting_manage_exception_event,
        device_status_event,
        net_diagnostic_check,
        play_video_test,
        play_sound_test,
        transparent_msg_event,
        receive_ui_msg_event,
        mic_send_response,
        network_status_level_notice_event,
        device_lost_notice_event,
        meeting_sdk_callback,
        send_audio_speaker_status,
        get_meeting_invitation_sms,
        host_order_one_do_opration,
        host_order_do_opraton_event,
        other_change_audio_speaker_status_event,
        modify_nick_name,
        modify_meeting_inviters,

        start_host,
        connect_meeting_vdn,
        connect_meeting_vdn_after_instance_started,
        set_account_info,
        meeting_invite,
        meeting_invite_event,
        contact_recommend_event,
        force_offline_event,


    };

    #endregion
}
