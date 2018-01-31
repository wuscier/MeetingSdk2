namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 音频订阅参数
    /// </summary>
    public class SubscribeAudioModel
    {
        /// <summary>
        /// 视讯号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 媒体流类型
        /// </summary>
        public StreamType StreamType { get; set; }
        /// <summary>
        /// 音频接收参数
        /// </summary>
        public AudioRecvModel AudioRecvModel { get; set; }
        /// <summary>
        /// 资源标识
        /// </summary>
        public int ResourceId { get; set; }
        /// <summary>
        /// 媒体类型
        /// </summary>
        public MediaType MediaType { get; set; }
        /// <summary>
        /// 音视频同步标识
        /// </summary>
        public uint AvSyncGroupId { get; set; }
        /// <summary>
        /// 传输参数
        /// </summary>
        public TransModel TransModel { get; set; }
    }

    /// <summary>
    /// 音频接收参数
    /// </summary>
    public class AudioRecvModel
    {
        /// <summary>
        /// 流媒体来源
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// 流媒体来源名称
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 是否混音标识, 默认设置为true
        /// </summary>
        public int IsMix { get; set; }
    }
}
