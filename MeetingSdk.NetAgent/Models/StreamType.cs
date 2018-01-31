namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 媒体流类型
    /// </summary>
    public enum StreamType
    {
        /// <summary>
        /// 视频发送
        /// </summary>
        VideoSend,
        /// <summary>
        /// 视频接收
        /// </summary>
        VideoRecv,
        /// <summary>
        /// 音频发送
        /// </summary>
        AudioSend,
        /// <summary>
        /// 音频接收
        /// </summary>
        AudioRecv,
        /// <summary>
        /// 直播录制
        /// </summary>
        Live,
        /// <summary>
        /// 命令发送
        /// </summary>
        CmdSend,
        /// <summary>
        /// 命令接受
        /// </summary>
        CmdRecv,
    }
}