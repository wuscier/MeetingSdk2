using System;
using Caliburn.Micro;
using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.Wpf
{
    public interface IStreamParameter
    {
        PublishAudioModel GetPublishAudioModel();
        PublishVideoModel GetPublishVideoModel();
        SubscribeAudioModel GetSubscribeAudioModel();
        SubscribeVideoModel GetSubscribeVideoModel();
    }

    public class VideoStreamParameter : IStreamParameter
    {
        public int CapLeft { get; set; }
        public int CapTop { get; set; }
        public int CapRight { get; set; }
        public int CapBottom { get; set; }
        /// <summary>
        /// 帧率
        /// </summary>
        public int CapFps { get; set; }
        /// <summary>
        /// 颜色空间
        /// </summary>
        public VideoColorSpace VideoColorSpace { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public int EncWidth { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int EncHeight { get; set; }
        /// <summary>
        /// 帧率
        /// </summary>
        public int EncFps { get; set; }
        /// <summary>
        /// 码率
        /// </summary>
        public int EncBitrate { get; set; }
        /// <summary>
        /// 编码级别
        /// </summary>
        public VideoCodeLevel VideoCodeLevel { get; set; }
        /// <summary>
        /// 编码器Id
        /// </summary>
        public VideoCodeId VideoCodeId { get; set; }
        /// <summary>
        /// 编码器类型
        /// </summary>
        public VideoCodeType VideoCodeType { get; set; }

        /// <summary>
        /// 显示充满模式
        /// </summary>
        public DisplayFillMode DisplayFillMode { get; set; }

        /// <summary>
        /// fec数据比例，-1 表示不指定
        /// </summary>
        public int FecDataCount { get; set; }

        /// <summary>
        /// fec校验包比例 -1 表示不指定
        /// </summary>
        public int FecCheckCount { get; set; }

        /// <summary>
        /// 数据包发送份数 -1 表示不指定
        /// </summary>
        public int DataSendCount { get; set; }

        /// <summary>
        /// 校验包发送份数 -1 表示不指定
        /// </summary>
        public int CheckSendCount { get; set; }

        /// <summary>
        /// 补发数据包发送份数 -1 表示不指定
        /// </summary>
        public int DataRetransSendCount { get; set; }

        /// <summary>
        /// 补发校验包发送份数 -1 表示不指定
        /// </summary>
        public int CheckRetransSendCount { get; set; }

        /// <summary>
        /// 补发请求次数 -1 表示不指定
        /// </summary>
        public int DataResendCount { get; set; }

        /// <summary>
        /// 接收延迟窗口大小，单位ms（仅下行使用） -1 表示不指定
        /// </summary>
        public int DelayTimeWinsize { get; set; }

        public virtual PublishAudioModel GetPublishAudioModel()
        {
            return null;
        }

        public virtual PublishVideoModel GetPublishVideoModel()
        {
            return null;
        }

        public virtual SubscribeAudioModel GetSubscribeAudioModel()
        {
            return null;
        }

        public virtual SubscribeVideoModel GetSubscribeVideoModel()
        {
            return null;
        }
    }

    public class AudioStreamParameter : IStreamParameter
    {
        /// <summary>
        /// 是否混音标识 ，取值始终为1
        /// </summary>
        public int IsMix { get; set; }


        /// <summary>
        /// 采样率
        /// </summary>
        public int CapSampleRate { get; set; }

        /// <summary>
        /// 声道数
        /// </summary>
        public int CapChannels { get; set; }

        /// <summary>
        /// 采样精度
        /// </summary>
        public int CapBitsPerSample { get; set; }


        /// <summary>
        /// 采样率
        /// </summary>
        public int EncSampleRate { get; set; }

        /// <summary>
        /// 声道数
        /// </summary>
        public int EncChannels { get; set; }

        /// <summary>
        /// 采样精度
        /// </summary>
        public int EncBitsPerSample { get; set; }


        /// <summary>
        /// 码率
        /// </summary>
        public int EncBitrate { get; set; }

        /// <summary>
        /// 音频编码器ID
        /// </summary>
        public AudioCodeId AudioCodeId { get; set; }


        /// <summary>
        /// fec数据比例，-1 表示不指定
        /// </summary>
        public int FecDataCount { get; set; }

        /// <summary>
        /// fec校验包比例 -1 表示不指定
        /// </summary>
        public int FecCheckCount { get; set; }

        /// <summary>
        /// 数据包发送份数 -1 表示不指定
        /// </summary>
        public int DataSendCount { get; set; }

        /// <summary>
        /// 校验包发送份数 -1 表示不指定
        /// </summary>
        public int CheckSendCount { get; set; }

        /// <summary>
        /// 补发数据包发送份数 -1 表示不指定
        /// </summary>
        public int DataRetransSendCount { get; set; }

        /// <summary>
        /// 补发校验包发送份数 -1 表示不指定
        /// </summary>
        public int CheckRetransSendCount { get; set; }

        /// <summary>
        /// 补发请求次数 -1 表示不指定
        /// </summary>
        public int DataResendCount { get; set; }

        /// <summary>
        /// 接收延迟窗口大小，单位ms（仅下行使用） -1 表示不指定
        /// </summary>
        public int DelayTimeWinsize { get; set; }

        public virtual PublishAudioModel GetPublishAudioModel()
        {
            return null;
        }

        public virtual PublishVideoModel GetPublishVideoModel()
        {
            return null;
        }

        public virtual SubscribeAudioModel GetSubscribeAudioModel()
        {
            return null;
        }

        public virtual SubscribeVideoModel GetSubscribeVideoModel()
        {
            return null;
        }
    }

    public class PublishMicStreamParameter : AudioStreamParameter
    {
        public override PublishAudioModel GetPublishAudioModel()
        {
            PublishAudioModel publishAudioModel = new PublishAudioModel()
            {
                AudioSendModel = new AudioSendModel()
                {
                    AudioCapModel = new AudioCapModel()
                    {
                        BitsPerSample = CapBitsPerSample,
                        Channels = CapChannels,
                        SampleRate = CapSampleRate,
                    },
                    AudioEncModel = new AudioEncModel()
                    {
                        AudioCodeId = AudioCodeId,
                        Bitrate = EncBitrate,
                        BitsPerSample = EncBitsPerSample,
                        Channels = EncChannels,
                        SampleRate = EncSampleRate,
                    },
                    ExtraInfo = null,
                    IsMix = IsMix,
                    SourceName = null,
                    SourceType = SourceType.Device,
                },
                AvSyncGroupId = 0,
                MediaType = MediaType.Microphone,
                StreamType = StreamType.AudioSend,
                TransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
            };
            return publishAudioModel;
        }
    }

    public class PublishCameraStreamParameter : VideoStreamParameter
    {
        public override PublishVideoModel GetPublishVideoModel()
        {
            PublishVideoModel publishVideoModel = new PublishVideoModel()
            {
                AvSyncGroupId = 0,
                MediaType = MediaType.Camera,
                StreamType = StreamType.VideoSend,
                VideoSendModel = new VideoSendModel()
                {
                    CaptureModel = new VideoCaptureModel()
                    {
                        VideoColorSpace = VideoColorSpace,
                        Bottom = CapBottom,
                        Fps = EncFps,
                        Left = CapLeft,
                        Right = CapRight,
                        Top = CapTop,
                        CapWinHandle = IntPtr.Zero,
                    },
                    DisplayFillMode = DisplayFillMode,
                    DisplayWindow = IntPtr.Zero,
                    EncodeModel = new VideoEncodeModel()
                    {
                        Bitrate = EncBitrate,
                        Fps = EncFps,
                        Height = EncHeight,
                        VideoCodeId = VideoCodeId,
                        VideoCodeLevel = VideoCodeLevel,
                        VideoCodeType = VideoCodeType,
                        Width = EncWidth,
                    },
                    ExtraInfo = null,
                    SourceName = null,
                    SourceType = SourceType.Device,
                },
                VideoTransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
            };

            return publishVideoModel;
        }
    }

    public class PublishDataCardStreamParameter : VideoStreamParameter
    {
        public override PublishVideoModel GetPublishVideoModel()
        {
            PublishVideoModel publishVideoModel = new PublishVideoModel()
            {
                AvSyncGroupId = 0,
                MediaType = MediaType.VideoDoc,
                StreamType = StreamType.VideoSend,
                VideoSendModel = new VideoSendModel()
                {
                    CaptureModel = new VideoCaptureModel()
                    {
                        VideoColorSpace = VideoColorSpace,
                        Bottom = CapBottom,
                        Fps = EncFps,
                        Left = CapLeft,
                        Right = CapRight,
                        Top = CapTop,
                        CapWinHandle = IntPtr.Zero,
                    },
                    DisplayFillMode = DisplayFillMode,
                    DisplayWindow = IntPtr.Zero,
                    EncodeModel = new VideoEncodeModel()
                    {
                        Bitrate = EncBitrate,
                        Fps = EncFps,
                        Height = EncHeight,
                        VideoCodeId = VideoCodeId,
                        VideoCodeLevel = VideoCodeLevel,
                        VideoCodeType = VideoCodeType,
                        Width = EncWidth,
                    },
                    ExtraInfo = null,
                    SourceName = null,
                    SourceType = SourceType.Device,
                },
                VideoTransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
            };

            return publishVideoModel;
        }

    }

    public class PublishWinCaptureStreamParameter : VideoStreamParameter
    {
        public override PublishVideoModel GetPublishVideoModel()
        {
            PublishVideoModel publishVideoModel = new PublishVideoModel()
            {
                AvSyncGroupId = 0,
                MediaType = MediaType.VideoCaptureCard,
                StreamType = StreamType.VideoSend,
                VideoSendModel = new VideoSendModel()
                {
                    CaptureModel = new VideoCaptureModel()
                    {
                        VideoColorSpace = VideoColorSpace,
                        Bottom = CapBottom,
                        Fps = EncFps,
                        Left = CapLeft,
                        Right = CapRight,
                        Top = CapTop,
                        CapWinHandle = IntPtr.Zero,
                    },
                    DisplayFillMode = DisplayFillMode,
                    DisplayWindow = IntPtr.Zero,
                    EncodeModel = new VideoEncodeModel()
                    {
                        Bitrate = EncBitrate,
                        Fps = EncFps,
                        Height = EncHeight,
                        VideoCodeId = VideoCodeId,
                        VideoCodeLevel = VideoCodeLevel,
                        VideoCodeType = VideoCodeType,
                        Width = EncWidth,
                    },
                    ExtraInfo = null,
                    SourceName = null,
                    SourceType = SourceType.Device,
                },
                VideoTransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
            };

            return publishVideoModel;
        }

    }

    public class SubscribeMicStreamParameter : AudioStreamParameter
    {
        public override SubscribeAudioModel GetSubscribeAudioModel()
        {
            SubscribeAudioModel subscribeAudioModel = new SubscribeAudioModel()
            {
                AvSyncGroupId = Helper.GetCurrentTimeTotalMilliseconds(),
                MediaType = MediaType.Microphone,
                ResourceId = 0,
                StreamType = StreamType.AudioRecv,
                UserId = null,

                TransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
                AudioRecvModel = new AudioRecvModel()
                {
                    IsMix = 0,
                    SourceName = null,
                    SourceType = SourceType.Callback,
                },
            };

            return subscribeAudioModel;
        }
    }

    public class SubscribeCameraStreamParameter : VideoStreamParameter
    {
        public override SubscribeVideoModel GetSubscribeVideoModel()
        {
            SubscribeVideoModel subscribeVideoModel = new SubscribeVideoModel()
            {
                AvSyncGroupId = Helper.GetCurrentTimeTotalMilliseconds(),
                MediaType = MediaType.Camera,
                ResourceId = 0,
                StreamType = StreamType.VideoRecv,
                UserId = string.Empty,
                TransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
                VideoRecvModel = new VideoRecvModel()
                {
                    DisplayFillMode = DisplayFillMode.RawWithBlack,
                    DisplayWindow = IntPtr.Zero,
                },
            };

            return subscribeVideoModel;
        }
    }

    public class SubscribeDataCardStreamParameter : VideoStreamParameter
    {
        public override SubscribeVideoModel GetSubscribeVideoModel()
        {
            SubscribeVideoModel subscribeVideoModel = new SubscribeVideoModel()
            {
                AvSyncGroupId = Helper.GetCurrentTimeTotalMilliseconds(),
                MediaType = MediaType.VideoDoc,
                ResourceId = 0,
                StreamType = StreamType.VideoRecv,
                UserId = string.Empty,
                TransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
                VideoRecvModel = new VideoRecvModel()
                {
                    DisplayFillMode = DisplayFillMode.RawWithBlack,
                    DisplayWindow = IntPtr.Zero,
                },
            };

            return subscribeVideoModel;
        }

    }

    public class SubscribeWinCaptureStreamParameter : VideoStreamParameter
    {
        public override SubscribeVideoModel GetSubscribeVideoModel()
        {
            SubscribeVideoModel subscribeVideoModel = new SubscribeVideoModel()
            {
                AvSyncGroupId = Helper.GetCurrentTimeTotalMilliseconds(),
                MediaType = MediaType.VideoCaptureCard,
                ResourceId = 0,
                StreamType = StreamType.VideoRecv,
                UserId = string.Empty,
                TransModel = new TransModel()
                {
                    CheckRetransSendCount = CheckRetransSendCount,
                    CheckSendCount = CheckSendCount,
                    DataResendCount = DataResendCount,
                    DataRetransSendCount = DataRetransSendCount,
                    DataSendCount = DataSendCount,
                    DelayTimeWinsize = DelayTimeWinsize,
                    FecCheckCount = FecCheckCount,
                    FecDataCount = FecDataCount,
                },
                VideoRecvModel = new VideoRecvModel()
                {
                    DisplayFillMode = DisplayFillMode.RawWithBlack,
                    DisplayWindow = IntPtr.Zero,
                },
            };

            return subscribeVideoModel;
        }

    }

    public class Helper
    {
        public static uint GetCurrentTimeTotalMilliseconds()
        {
            double total = DateTime.Now.Subtract(new DateTime(DateTime.Now.Year - 1, 1, 1)).TotalMilliseconds;

            uint id = (uint)total;
            return id;
        }
    }
}
