using System;

namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 视频流发布参数
    /// </summary>
    public class PublishVideoModel
    {
        /// <summary>
        /// 音视频同步ID
        /// </summary>
        public int AvSyncGroupId { get; set; }

        /// <summary>
        /// 媒体流类型,发布视频流时,取值MEETINGMANAGE_STREAM_TYPE_VIDEO_SEND
        /// </summary>
        public StreamType StreamType { get; set; }

        /// <summary>
        /// 采集编码参数
        /// </summary>
        public VideoSendModel VideoSendModel { get; set; }

        /// <summary>
        /// 传输参数
        /// </summary>
        public TransModel VideoTransModel { get; set; }

        /// <summary>
        /// 媒体类型,发布视频流时，取值MEETINGMANAGE_VIDEO_CAMRA
        /// </summary>
        public MediaType MediaType { get; set; }
    }

    public class VideoSendModel
    {
        /// <summary>
        /// 媒体流来源
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// 媒体源名称
        /// 发布摄像头媒体流时，取值摄像头具体名称；
        /// 发布本地桌面媒体流时，取值“DesktopCapture”；
        /// 发布远端桌面媒体流时，可以不设值；
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        public string ExtraInfo { get; set; }

        /// <summary>
        /// 视频采集参数
        /// </summary>
        public VideoCaptureModel CaptureModel { get; set; }

        /// <summary>
        /// 视频编码参数
        /// </summary>
        public VideoEncodeModel EncodeModel { get; set; }

        /// <summary>
        /// 视频窗口显示句柄
        /// </summary>
        public IntPtr DisplayWindow { get; set; }

        /// <summary>
        /// 显示充满模式
        /// </summary>
        public DisplayFillMode DisplayFillMode { get; set; }
    }

    /// <summary>
    /// 显示充满模式
    /// </summary>
    public enum DisplayFillMode
    {
        /// <summary>
        /// 保持原始比例充满模式，留黑边
        /// </summary>
        RawWithBlack,
        /// <summary>
        /// 保持原始比例充满模式，不留黑边,居中
        /// </summary>
        RawNoBlackMiddle,
        /// <summary>
        /// 保持原始比例充满模式，不留黑边,单边裁剪-左上
        /// </summary>
        RawNoBlackLeft,
        /// <summary>
        /// 保持原始比例充满模式，不留黑边,单边裁剪-右下
        /// </summary>
        RawNoBlackRight,
        /// <summary>
        /// 拉伸充满模式
        /// </summary>
        Strech,
    }

    /// <summary>
    /// 视频编码参数
    /// </summary>
    public class VideoEncodeModel
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 帧率
        /// </summary>
        public int Fps { get; set; }
        /// <summary>
        /// 码率
        /// </summary>
        public int Bitrate { get; set; }
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
    }

    /// <summary>
    /// 采集参数
    /// </summary>
    public class VideoCaptureModel
    {
        /// <summary>
        /// 采集句柄
        /// </summary>
        public IntPtr CapWinHandle { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        /// <summary>
        /// 帧率
        /// </summary>
        public int Fps { get; set; }
        /// <summary>
        /// 颜色空间
        /// </summary>
        public VideoColorSpace VideoColorSpace { get; set; }

    }
}
