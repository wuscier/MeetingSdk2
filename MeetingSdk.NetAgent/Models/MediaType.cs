namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 媒体类型
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// 摄像头视频
        /// </summary>
        Camera,
        /// <summary>
        /// 麦克风音频
        /// </summary>
        Microphone,
        /// <summary>
        /// 屏幕采集视频
        /// </summary>
        VideoDoc,
        /// <summary>
        /// 屏幕分享音频
        /// </summary>
        AudioDoc,
        /// <summary>
        /// 采集卡视频
        /// </summary>
        VideoCaptureCard,
        /// <summary>
        /// 采集卡音频
        /// </summary>
        AudioCaptureCard,
        /// <summary>
        /// 流媒体
        /// </summary>
        StreamMedia,
        /// <summary>
        /// 文件
        /// </summary>
        File,
        /// <summary>
        /// 白板
        /// </summary>
        WhiteBoard,
        /// <summary>
        /// 远程控制
        /// </summary>
        RemoteControl,
        MediaTypeMax
    }
}