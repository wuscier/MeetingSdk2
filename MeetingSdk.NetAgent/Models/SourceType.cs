namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 媒体流来源
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown=-1,
        /// <summary>
        /// 设备源
        /// </summary>
        Device,
        /// <summary>
        /// 文件源
        /// </summary>
        File,
        /// <summary>
        /// 网络源
        /// </summary>
        Net,
        /// <summary>
        /// 回调源
        /// </summary>
        Callback
    }
}