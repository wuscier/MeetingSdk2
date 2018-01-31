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

#define MEETINGMANAGE_USERID_LEN		64		// ��Ѷ�ų���
#define MEETINGMANAGE_sourceName_len	2048
#define MEETINGMANAGE_DEVICENAME_LEN	256
#define MEETINGMANAGE_USERNAME_LEN		128
#define MEETINGMANAGE_TIME_LEN			64
#define MEETINGMANAGE_EXTRAINFO_LEN		1024
#define MEETINGMANAGE_STREAMINFO_COUNT  20
#define MEETINGMANAGE_TOPIC_LEN			200
#define MEETINGMANAGE_TOKEN_LEN			128

// SDK���󷵻�����
enum SDKErrorCode {
	SDK_ERRCODE_SUCCEED						= 0,		// �ɹ�
	SDK_ERRCODE_SDK_UNINIT					= -1,		// sdkδ����
	SDK_ERRCODE_FAILED						= -1000,	// ʧ�ܣ�ͨ�ã�
	SDK_ERRCODE_INVALID_ARGUMENTS			= -1001,	// ��Ч����
	SDK_ERRCODE_NETWORK_DISCONNECT			= -1002,    // �����쳣
	SDK_ERRCODE_FUNCTION_INVOKE_AGAIN		= -1003,	// �ظ�����
	SDK_ERRCODE_SEND_REQUEST_FAILED			= -1004,	// �������󵽷�����ʧ�ܣ�ͨ�ã�
	SDK_ERRCODE_MEETING_CALLBACK_NULL		= -1005,	// ����ģ��ص�ʵ��Ϊ��
	SDK_ERRCODE_GET_NPSREQUEST_FAILED		= -1006,    // ���ͻ�ȡNPS��������ʧ��
	SDK_ERRCODE_START_ASYNMODEL_FAILED		= -1007,	// �����첽ģ��ʧ��
	SDK_ERRCODE_GETTOKEN_FAILED				= -1008,	// ��ȡtokenʧ��
	SDK_ERRCODE_BINDTOKEN_REQUEST_FAILD		= -1009,    // ���ͼ�Ȩ����ʧ��
	SDK_ERRCODE_RECORDSOUND_FAILED			= -1010,	// ��Ƶ�ɼ�����ʧ��
	SDK_ERRCODE_INMEETING					= -1011,	// �ڻ�����
	SDK_ERRCODE_NOT_INMEETING				= -1012,	// ���ڻ�����
	SDK_ERRCODE_NOT_SPEAKING				= -1013,	// ���Ƿ���״̬
	SDK_ERRCODE_MEETINGSDK_FAILED			= -1014,	// ý����������
	SDK_ERRCODE_GETUSERLIST_NOTINMEETING    = -1015,	// ��ȡ�λ��б����ڻ�����
	SDK_ERRCODE_GETUSERLIST_REQUESTFAILED   = -1016,	// ���ͻ�ȡ�λ��б�����ʧ��
	SDK_ERRCODE_START_MEETCTRLAGENT_FAILED  = -1017,	// �������Agentʧ��
};


//SDK�첽�ص�״̬��
enum SDKCallbackStatus
{
	SDK_STATUS_SUCCEED						= 0,		//�ɹ�
	SDK_STATUS_FAIL							= -1999,	//�첽����ʧ�ܣ�ͨ�ô���
	SDK_STATUS_GETCONFIG_FAIL				= -2000,	//��ȡ������Ϣʧ�� OnStart 
	SDK_STATUS_LOGIN_FAIL					= -2001,	//��Ȩʧ�� OnLogin
	SDK_STATUS_LOGIN_NOSERVICE				= -2002,	//��ҵ���ĵ�½����,����δ��ͨ OnLogin
	SDK_STATUS_LOGIN_SERVICEOUT				= -2003,	//��ҵ���ĵ�½����,�������� OnLogin
	SDK_STATUS_BINDTOKEN_FAIL				= -2004,	//��tokenʧ�� OnLogin OnBindToken
	SDK_STATUS_START_AGENT_FAIL				= -2005,	//�������ش���ʧ�� OnBindToken
	SDK_STATUS_MEET_NOTEXIST				= -2006,	//����Ų����� OnCheckMeetExist OnResetMeetingPassword OnGetMeetingPassword OnCheckMeetingHasPassword OnCheckMeetingPasswordValid
	SDK_STATUS_TOKEN_TIMEOUT				= -2007,	//Token�ѹ��� OnGetMeetingList OnResetMeetingPassword OnGetMeetingPassword OnCheckMeetingPasswordValid
	SDK_STATUS_NOT_HOST						= -2008,	//�ӿڵ����߲��ǻ��������� OnResetMeetingPassword OnGetMeetingPassword
	SDK_STATUS_WRONG_PWD					= -2009,	//����������� OnCheckMeetingPasswordValid
	SDK_STATUS_JOIN_CREATED_MEET_FAIL		= -2010,	//�����´����ļ�ʱ����ʧ��
	SDK_STATUS_JOINMEET_FAIL_INMEET			= -2011,	//�������ʧ�ܣ��Ѿ��ڻ����У���Ҫ���˻�
	SDK_STATUS_JOINMEET_FAIL_GETUSERLIST	= -2012,	//�������ɹ�����ȡ�λ����б�ʧ��
	SDK_STATUS_NOTOKEN						= -2013,	//token��Ч��token����,��ط���������
	SDK_STATUS_JOINMEET_FAIL_MEETEND		= -2014,	//�������ʧ�ܣ������ѽ���
	SDK_STATUS_JOINMEET_FAIL_SERVER_ERR		= -2015,	//�������ʧ�ܣ�����������-915
	SDK_STATUS_JOINMEET_FAIL_MEETLOCK		= -2016,	//�������ʧ�ܣ������Ѽ���
	SDK_STATUS_JOINMEET_FAIL_NORSP			= -2017,	//�������ʧ�ܣ�����ʱ����������Ӧ��
	SDK_STATUS_NOTINMEET					= -2018,	//���ڻ����У�����ʧ��
};


// �豸״̬�仯����
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

// SDK�쳣����
enum SDKExceptionType {
	SETYPE_UNKNOWN = -1,		      // δ֪
	SETYPE_NETWORK_RECONNECT,	      // ��������
	SETYPE_NETWORK_ENDRETRY,		  // ������������
	SETYPE_NETWORK_RECONNECT_SUCCEED, // ���������ɹ�
	SETYPE_TOKEN_TIMEOUT		      // TOKEN����
};

enum NetDiagnosticType {
	NDT_UNKNOWN = -1,		// δ֪
	NDT_NET_CONN,			// ������ͨ��
	NDT_MEET_CONN,			// ������ͨ��
	NDT_BAND_DETECT			// ���ټ��
};

enum MM_MeetingType {
	MT_UNKNOWN = -1,		// δ֪
	MT_JUSTMEETING = 1,		// ��ʱ����
	MT_DATEDMEETING			// ԤԼ����
};

//֡����
typedef enum _MEETINGMANAGE_FrameType
{
	MEETINGMANAGE_FRAME_TYPE_YUV,
	MEETINGMANAGE_FRAME_TYPE_H264,
	MEETINGMANAGE_FRAME_TYPE_RTP
}MEETINGMANAGE_FrameType;

//smsdk֪ͨ
typedef enum _MEETINGMANAGE_SMSDK_CBTYPE
{
	SMSDK_TYPE_ERR,						//֪ͨʧ��
	SMSDK_TYPE_NET_LINKNORMAL,			//�����ź��ȶ�
	SMSDK_TYPE_NET_LINKBAD,				//�����źŲ�
	SMSDK_TYPE_NET_LINKERROR,			//�����źż���
	SMSDK_TYPE_VIDEO_CLOSE_ADAPTE,		//����Ӧ�ر���Ƶ
	SMSDK_TYPE_CPU_OTHEROVERLOAD,		//��������ռ��cpu����
	SMSDK_TYPE_CPU_SELFOVERLOD,			//����ռ��cpu����
}MEETINGMANAGE_SMSDK_CBTYPE;


/**************************** ��¼�������� *********************************/
typedef struct _LoginInfo
{
	int		m_accountId;								// ��¼�û�����Ѷ��
	char	m_accountName[MEETINGMANAGE_USERNAME_LEN];	// ��¼�û�������
	char	m_tokenStartTime[MEETINGMANAGE_TIME_LEN];	// �û�Token����ʼʱ��
	char	m_tokenEndTime[MEETINGMANAGE_TIME_LEN];		// �û�Token�Ľ���ʱ��
	char	m_token[MEETINGMANAGE_TOKEN_LEN];			// token
}LoginInfo;

/**************************** ����������� *********************************/

//�λ�����Ϣ
typedef struct _ParticipantInfo
{
	int 			 m_accountId;			//��Ѷ��
	char 			 m_accountName[MEETINGMANAGE_USERNAME_LEN];	//�ǳ�
	bool			 m_bIsRaiseHand;		//�Ƿ��Ѿ�����
	bool			 m_bIsSpeaking;			//�Ƿ����ڷ���
	int				 m_hostFlag;			//�Ƿ���������
	bool			 m_aRenderOpened;	//�������򿪹ر�״̬
}ParticipantInfo;

//����Ϣ
typedef struct _MeetingUserStreamInfo {
	char 			 m_accountName[MEETINGMANAGE_USERNAME_LEN]; 	//�ǳ�
	char 			 m_deviceName[MEETINGMANAGE_DEVICENAME_LEN];
	int 			 m_accountId;			//��Ѷ��
	int 			 m_resourceId;	
	int				 m_asynGroupId;			//ͬ��ID
	int				 m_mediaType;			//����˵�� ö��
} MeetingUserStreamInfo;

//��������Ϣ
typedef struct _MeetingSpeakerInfo {
	char 			 m_accountName[MEETINGMANAGE_USERNAME_LEN];
	int 			 m_accountId;
	int				 m_streamCount;
	MeetingUserStreamInfo m_streamInfos[MEETINGMANAGE_STREAMINFO_COUNT];
}MeetingSpeakerInfo;

//���������ϸ��Ϣ
/*
 *	m_lockInfo	    [IN] �Ƿ���� 1 ���� 0 ������; Ĭ��Ϊ0(������)
 *  m_presenterId   [IN] �������˺�ID; Ĭ��������Ϊ������
 *  m_userType      [IN] �λ��û����ͣ�1Ϊ��ͨ�û� 2Ϊ�������û�
 *  m_meetingStyle  [IN] ����ģʽ��1Ϊ����ģʽ 2Ϊ������ģʽ; Ĭ��Ϊ1(����ģʽ)
 *  m_presenterName [IN] ����������
 *  m_liveStatus    [IN] ��������,1����ֱ������ 2������ͨ����; Ĭ��Ϊ2(��ͨ����)
 *  m_speakerCount  [IN] ��������
 *  m_Speakers      [IN] �������б�
 */
typedef struct _JoinMeetingInfo { 
	int					 m_lockInfo;			//�Ƿ���� 1 ���� 0 ������
	int					 m_presenterId;			//�������˺�ID
	int					 m_userType;			//�λ��û����ͣ�1Ϊ��ͨ�û���2Ϊ�������û���
	int					 m_meetingStyle;		//����ģʽ��1Ϊ����ģʽ��2Ϊ������ģʽ
	char				 m_presenterName[MEETINGMANAGE_USERNAME_LEN];	//����������
	int					 m_liveStatus;			//����int��1����ֱ�����飬2������ͨ����
	int 				 m_speakerCount;		//��������
	MeetingSpeakerInfo * m_Speakers;			//�������б�
} JoinMeetingInfo;

//������Ϣ
typedef struct _MeetingInfo
{
	int				 m_meetingId;
	int				 m_meetingType;
	int				 m_createrAccountId;
	char			 m_createrName[MEETINGMANAGE_USERNAME_LEN];
	char			 m_startTime[MEETINGMANAGE_TIME_LEN];

	int				m_havepwd ; //�ӿڷ�����ȷ��ȡֵ��Ȼ���UI ȷ��ȡֵ�ĺ��� 	//-1:��ʼֵ��0��û���룬1��������
	int				m_HostID;	//20160715���������޸�
	char			m_topic[MEETINGMANAGE_TOPIC_LEN];	//�������� 20160805

	int				m_meetingStatus;			//����״̬: 1 δ��ʼ   2 ������   3 ����
} MeetingInfo;

/**************************** ý��������� *********************************/

//ý�������Ͷ���
typedef enum MEETINGMANAGE_StreamType
{
	MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND,     //��Ƶ������·
	MEETINGMANAGE_STREAM_TYPE_VIDEO_RECV,     //��Ƶ������·
	MEETINGMANAGE_STREAM_TYPE_AUDIO_SEND,     //��Ƶ������·
	MEETINGMANAGE_STREAM_TYPE_AUDIO_RECV,     //��Ƶ������·
	MEETINGMANAGE_STREAM_TYPE_LIVE,           //ֱ��¼����·
	MEETINGMANAGE_STREAM_TYPE_CMD_SEND,       //�������·
	MEETINGMANAGE_STREAM_TYPE_CMD_RECV        //���������·
} MEETINGMANAGE_StreamType;

//ý������Դ���Ͷ���
typedef enum _MEETINGMANAGESourceType {
	MEETINGMANAGE_SOURCE_TYPE_UNKNOWN = -1,
	MEETINGMANAGE_SOURCE_TYPE_DEVICE,         ///<�豸Դ �豸
	MEETINGMANAGE_SOURCE_TYPE_FILE,           ///<�ļ�Դ
	MEETINGMANAGE_SOURCE_TYPE_NET,            ///<����Դ
	MEETINGMANAGE_SOURCE_TYPE_CALLBACK        ///<�ص�Դ Զ��
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
	int 	fps;									///<֡��
	MEETINGMANAGE_VideoColorSpace colorSpace;		//��ɫ�ռ�
}MEETINGMANAGEVideoCapParam;

typedef enum _MEETINGMANAGEVideoCodecID
{
	MEETINGMANAGE_VIDEO_CODEC_ID_H264,				///<H264�����
	MEETINGMANAGE_VIDEO_CODEC_ID_VP8				///<VP8�����
}MEETINGMANAGEVideoCodecID;

typedef enum __MEETINGMANAGEVideoCodecLevel
{
	MEETINGMANAGE_CODEC_LEVEL_LOW,
	MEETINGMANAGE_CODEC_LEVEL_MEDIUM,
	MEETINGMANAGE_CODEC_LEVEL_HIGH
}MEETINGMANAGEVideoCodecLevel;

typedef enum _MEETINGMANAGEVideoCodecType
{
	MEETINGMANAGE_CODEC_TYPE_SOFT,      ///<���
	MEETINGMANAGE_CODEC_TYPE_HARD,      ///<Ӳ��

}MEETINGMANAGEVideoCodecType;

typedef struct _MEETINGMANAGEVideoEncParam
{
	int 				width;						///<��
	int 				height;						///<��
	int 				fps;						///<֡��
	int 				bitrate;					///<����(���ʵ�λKbps)
	MEETINGMANAGEVideoCodecLevel 	level;          ///<���뼶��
	MEETINGMANAGEVideoCodecID 		codecID;        ///<������ID
	MEETINGMANAGEVideoCodecType      codecType;     ///<����������
} MEETINGMANAGEVideoEncParam;

//��ʾ����ģʽ����
typedef enum _MEETINGMANAGE_DisplayFillMode
{
	//����ԭʼ��������ģʽ�����ڱ�
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_WITHBLACK,
	//����ԭʼ��������ģʽ�������ڱ�,����
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_NOBLACK_MIDDLE,
	//����ԭʼ��������ģʽ�������ڱ�,���߲ü�-����
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_NOBLACK_LEFT,
	//����ԭʼ��������ģʽ�������ڱ�,���߲ü�-����
	MEETINGMANAGE_FULL_FILL_MODE_RAWRATIO_NOBLACK_RIGHT,
	//�������ģʽ
	MEETINGMANAGE_FULL_FILL_MODE_STRECH
} MEETINGMANAGE_DisplayFillMode;

//�������
typedef struct _MEETINGMANAGE_transParam
{
	int             fecDataCount;            //fec���ݱ�����-1 ��ʾ��ָ��
	int             fecCheckCount;           //fecУ������� -1 ��ʾ��ָ��
	int             dataSendCount;	         //���ݰ����ͷ��� -1 ��ʾ��ָ��
	int             checkSendCount;	         //У������ͷ��� -1 ��ʾ��ָ��
	int             dataRetransSendCount;	 //�������ݰ����ͷ��� -1 ��ʾ��ָ��
	int             checkRetransSendCount;	 //����У������ͷ��� -1 ��ʾ��ָ��
	int             dataResendCount;		 //����������� -1 ��ʾ��ָ��
	int				delayTimeWinsize;		 //�����ӳٴ��ڴ�С����λms��������ʹ�ã� -1 ��ʾ��ָ��
} MEETINGMANAGE_transParam;


//ý������
typedef enum _MEETINGMANAGE_MediaType
{
	MEETINGMANAGE_VIDEO_CAMRA,				 //����ͷ��Ƶ
	MEETINGMANAGE_AUDIO_MICPHONE,			 //��˷���Ƶ
	MEETINGMANAGE_VIDEO_DOC,				 //��Ļ�ɼ���Ƶ(�ɼ�����,�ɼ�����)
	MEETINGMANAGE_AUDIO_DOC,				 //��Ļ������Ƶ
	MEETINGMANAGE_VIDEO_CAPTURECARD,		 //�ɼ�����Ƶ
	MEETINGMANAGE_AUDIO_CAPTURECARD,		 //�ɼ�����Ƶ
	MEETINGMANAGE_STREAMMEDIA,				 //��ý��
	MEETINGMANAGE_FILE,					     //�ļ�
	MEETINGMANAGE_WHITEBOARD,				 //�װ�
	MEETINGMANAGE_REMOTECTRL,				 //Զ�̿���
	MEETINGMANAGE_MEIDATYPE_MAX
} MEETINGMANAGE_MediaType;

typedef struct _MEETINGMANAGEAudioCapParam
{
	int 	samplerate;						 ///<������
	int 	channels;						 ///<������
	int 	bitspersample;					 ///<��������
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
	int 			samplerate;				 ///<������
	int 			channels;				 ///<������
	int 			bitspersample;			 ///<��������
	int 			bitrate;				 ///<����(���ʵ�λKbps)
	MEETINGMANAGEAudioCodecID 	codecID;     ///<������ID
}MEETINGMANAGEAudioEncParam;


typedef struct _MEETINGMANAGE_VideoSendStreamParam {
	//��������ͷý��������������ý����ʱ��ȡֵΪMEETINGMANAGE_SOURCE_TYPE_DEVICE
	//����Զ������ý����ʱ��ȡֵΪMEETINGMANAGE_SOURCE_TYPE_CALLBACK
	MEETINGMANAGESourceType  sourceType;

	//��������ͷý����ʱ��ȡֵ����ͷ�������ƣ�
	//������������ý����ʱ��ȡֵ��DesktopCapture����
	//����Զ������ý����ʱ�����Բ���ֵ��
	char sourceName[MEETINGMANAGE_sourceName_len];

	// �������Ķ�����Ϣ
	char extraInfo[MEETINGMANAGE_EXTRAINFO_LEN];

	struct {
		//����Զ������ý����ʱ������Ҫ���òɼ��Լ��������
		MEETINGMANAGEVideoCapParam		* capParam;	//��Ƶ�ɼ�����
		MEETINGMANAGEVideoEncParam		* encParam;	//��Ƶ�������

		void					* displayWindow;	// ��ƵԤ�����ھ��
		MEETINGMANAGE_DisplayFillMode   fillMode;	//��ʾ����ģʽ,Ԥ������ʾģʽ���û������Լ�ָ����Ҫ����ʾ����
	} vsParam;  //��Ƶ���Ͳ����ṹ��

} MEETINGMANAGE_VideoSendStreamParam;

//��������ͷ��Ƶ����������
typedef struct _MEETINGMANAGE_PublishCameraParam
{
	int avSynGroupID;							 //����Ƶͬ��ID
	MEETINGMANAGE_StreamType sType;		         //ý��������, ������Ƶ��ʱ,ȡֵMEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
	MEETINGMANAGE_VideoSendStreamParam	 sParam; //����ͷ�ɼ�������ز���
	MEETINGMANAGE_transParam    transParam;		 //ý����������ز���
	MEETINGMANAGE_MediaType	 mediaType;     	 //ý������,������Ƶ��ʱ��ȡֵMEETINGMANAGE_VIDEO_CAMRA
} MEETINGMANAGE_PublishCameraParam;


// ��������������Ƶ����������
typedef struct _MEETINGMANAGE_WinCaptureVideoParam
{
	int avSynGroupID;							 //����Ƶͬ��ID
	MEETINGMANAGE_StreamType sType;		         //ý��������, ������Ƶ��ʱ,ȡֵMEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
	MEETINGMANAGE_VideoSendStreamParam sParam;   //����ͷ�ɼ�������ز���
	MEETINGMANAGE_transParam    transParam;		 //ý����������ز���
	MEETINGMANAGE_MediaType	 mediaType;     	 //ý������,������Ƶ��ʱ��ȡֵMEETINGMANAGE_VIDEO_DOC
} MEETINGMANAGE_WinCaptureVideoParam;

//����Զ��������Ƶ����������
typedef struct _MEETINGMANAGE_RemoteWinCaptureVideoParam {
	int avSynGroupID;							 //����Ƶͬ��ID
	MEETINGMANAGE_StreamType sType;		         //ý��������, ������Ƶ��ʱ,ȡֵMEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
	MEETINGMANAGE_VideoSendStreamParam sParam;   //����ͷ�ɼ�������ز���
	MEETINGMANAGE_transParam    transParam;		 //ý����������ز���
	MEETINGMANAGE_MediaType	 mediaType;     	 //ý������,������Ƶ��ʱ��ȡֵMEETINGMANAGE_VIDEO_DOC ??TODO�����ȡֵ?
} MEETINGMANAGE_RemoteWinCaptureVideoParam;


typedef struct _MEETINGMANAGE_LiveParam
{
	char    url[2][256];					 ///<ֱ������ַ�����֧��MAX_LIVE_NUM·ֱ��
	char    filepath[256];                   ///<¼���ļ�·��
	int     width;                           ///<ֱ��������
	int     height;                          ///<ֱ������߶�
	int     vBitrate;                        ///<ֱ����Ƶ����(��λKbps)
	int     samplerate;                      ///<������
	int     channels;                        ///<������
	int     bitspersample;                   ///<��������
	int     aBitrate;                        ///<ֱ����Ƶ����(��λKbps)
	int     isLive;                          ///<�Ƿ�ֱ��
	int     isRecord;                        ///<�Ƿ�¼��
}MEETINGMANAGE_LiveParam;


// ����ֱ������������
typedef struct _MEETINGMANAGE_PubLiveStreamParam
{
	MEETINGMANAGE_StreamType sType;		         //ý��������, ����ֱ����ʱ,ȡֵMEETINGMANAGE_STREAM_TYPE_LIVE
	MEETINGMANAGE_LiveParam sParam;				  //ֱ����ز���
	MEETINGMANAGE_MediaType	 mediaType;     	 //ý������,ֱ����ʱ��ȡֵMEETINGSDK_STREAMMEDIA
} MEETINGMANAGE_PubLiveStreamParam;


typedef enum _MEETINGMANAGE_MixVideoType
{
	MEETINGMANAGE_IS_VIDEO_TYPE = 0,
	MEETINGMANAGE_IS_DATA_TYPE
}MEETINGMANAGE_MixVideoType;

typedef struct _MEETINGMANAGE_VideoStreamInfo
{
	int 	xPos;              ///<��Ƶ�ڻ����ϵ�x����
	int 	yPos;              ///<��Ƶ�ڻ����ϵ�y����
	int 	width;             ///<��Ƶ�ڻ����ϵĿ�
	int 	height;            ///<��Ƶ�ڻ����ϵĸ�
	int 	resourceid;          ///<��Դid
	char    userid[MEETINGMANAGE_USERID_LEN]; //��Ѷ��
	MEETINGMANAGE_MixVideoType videoType;		   ///<��Ƶ���ͣ������������ݷ���
}MEETINGMANAGE_VideoStreamInfo;

typedef struct _MEETINGMANAGE_AudioStreamInfo
{
	int 	samplerate;         ///<������
	int 	channels;           ///<������
	int 	bitspersample;      ///<��������
	int 	resourceid;           ///<��Դid
	char    userid[MEETINGMANAGE_USERID_LEN]; //��Ѷ��
}MEETINGMANAGE_AudioStreamInfo;

//audio����������
typedef struct _MEETINGMANAGE_AudioSendStreamParam {
	MEETINGMANAGESourceType  sourceType;                  //MEETINGMANAGE_SOURCE_TYPE_DEVICE
	char        sourceName[MEETINGMANAGE_sourceName_len]; //��Ƶ�豸���ƣ� ����豸�ǲɼ������Ƿ���Դ��ɼ���������;TODO???

	// �������Ķ�����Ϣ
	char extraInfo[MEETINGMANAGE_EXTRAINFO_LEN];

	struct {
		MEETINGMANAGEAudioCapParam  *capParam;     //��Ƶ�ɼ�����
		MEETINGMANAGEAudioEncParam  *encParam;	   //��Ƶ�������
		int             isMix;				       //�Ƿ������ʶ ��ȡֵʼ��Ϊ1
	}asParam;  //��Ƶ���Ͳ����ṹ��
} MEETINGMANAGE_AudioSendStreamParam;


//����ý������������
////////////////
typedef struct _MEETINGMANAGE_publishMicParam {
		int avSynGroupID;
		MEETINGMANAGE_StreamType   sType;				 //ý��������,Ĭ��ȡֵΪMEETINGMANAGE_STREAM_TYPE_AUDIO_SEND
		MEETINGMANAGE_AudioSendStreamParam	sParam;      //��˷�ɼ�������ز���
		MEETINGMANAGE_transParam   transParam;			 //ý����������ز���
		MEETINGMANAGE_MediaType	mediaType;     			 //ý������,������Ƶ��ʱ��ȡֵMEETINGMANAGE_AUDIO_MICPHONE, ������������������ѡ�񣿣�
} MEETINGMANAGE_publishMicParam;


typedef struct _MEETINGMANAGE_VideoRecvStreamParam {
	struct {
		void					* displayWindow;	// ��ƵԤ�����ھ��
		MEETINGMANAGE_DisplayFillMode   fillMode;	//��ʾ����ģʽ,Ԥ������ʾģʽ���û������Լ�ָ����Ҫ����ʾ����
	} vrParam;  //��Ƶ���ղ����ṹ��
} MEETINGMANAGE_VideoRecvStreamParam;

typedef struct _MEETINGMANAGE_subscribeVideoParam
{
	char 		   			  userid[MEETINGMANAGE_USERID_LEN]; //��Ѷ��
	MEETINGMANAGE_StreamType     sType;							//ý��������, ȡֵMEETINGMANAGE_STREAM_TYPE_VIDEO_RECV
	MEETINGMANAGE_VideoRecvStreamParam    sParam;				//ý��������
	int 					  resourceID; 	  					//��Դ��ʶ
	MEETINGMANAGE_MediaType   mediaType;					    //ý������,ȡֵMEETINGMANAGE_VIDEO_CAMRA�Լ�MEETINGMANAGE_VIDEO_DOC
	unsigned int 			  AVSynGroupID;                     //����Ƶͬ����ʶ
	MEETINGMANAGE_transParam     transParam;
} MEETINGMANAGE_subscribeVideoParam;


typedef struct _MEETINGMANAGE_AudioRecvStreamParam
{
	MEETINGMANAGESourceType  sourceType;

	char sourceName[MEETINGMANAGE_sourceName_len];

	struct
	{
		int    isMix;   //�Ƿ������ʶ, Ĭ������Ϊ1
	} arParam;  //��Ƶ���ղ����ṹ��
} MEETINGMANAGE_AudioRecvStreamParam;

	typedef struct _MEETINGMANAGE_subscribeAudioParam
{
	char 		   						  userid[MEETINGMANAGE_USERID_LEN]; //��Ѷ��
	MEETINGMANAGE_StreamType				  sType;						//ý��������
	MEETINGMANAGE_AudioRecvStreamParam    sParam;							//ý��������
	int 								  resourceID; 	  					//��Դ��ʶ
	MEETINGMANAGE_MediaType 				  mediaType;						//ý������
	unsigned int 						  AVSynGroupID;                     //����Ƶͬ����ʶ
	MEETINGMANAGE_transParam			  transParam;
}MEETINGMANAGE_subscribeAudioParam;

#endif