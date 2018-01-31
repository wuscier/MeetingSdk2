namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 发布音频媒体流参数
    /// </summary>
    public class PublishAudioModel
    {
        /// <summary>
        /// 音视频同步Id
        /// </summary>
        public int AvSyncGroupId { get; set; }
        /// <summary>
        /// 媒体流类型,默认取值为MEETINGMANAGE_STREAM_TYPE_AUDIO_SEND
        /// </summary>
        public StreamType StreamType { get; set; }
        /// <summary>
        /// 麦克风采集编码相关参数
        /// </summary>
        public AudioSendModel AudioSendModel { get; set; }
        /// <summary>
        /// 媒体流传输相关参数
        /// </summary>
        public TransModel TransModel { get; set; }
        /// <summary>
        /// 媒体类型,发布音频流时，取值MEETINGMANAGE_AUDIO_MICPHONE, 如果是外置声卡，如何选择？？
        /// </summary>
        public MediaType MediaType { get; set; }
    }

    /// <summary>
    /// 音频发送参数
    /// </summary>
    public class AudioSendModel
    {
        /// <summary>
        /// MEETINGMANAGE_SOURCE_TYPE_DEVICE
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// 音频设备名称， 如果设备是采集卡，是否可以传采集卡的名称;TODO???
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 发布流的额外信息
        /// </summary>
        public string ExtraInfo { get; set; }

        /// <summary>
        /// 音频采集参数
        /// </summary>
        public AudioCapModel AudioCapModel { get; set; }

        /// <summary>
        /// 音频编码参数
        /// </summary>
        public AudioEncModel AudioEncModel { get; set; }

        /// <summary>
        /// 是否混音标识 ，取值始终为1
        /// </summary>
        public int IsMix { get; set; }
    }

    /// <summary>
    /// 音频编码参数
    /// </summary>
    public class AudioEncModel
    {
        /// <summary>
        /// 采样率
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// 声道数
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// 采样精度
        /// </summary>
        public int BitsPerSample { get; set; }


        /// <summary>
        /// 码率
        /// </summary>
        public int Bitrate { get; set; }

        /// <summary>
        /// 音频编码器ID
        /// </summary>
        public AudioCodeId AudioCodeId { get; set; }

    }

    /// <summary>
    /// 音频采集参数
    /// </summary>
    public class AudioCapModel
    {
        /// <summary>
        /// 采样率
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// 声道数
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// 采样精度
        /// </summary>
        public int BitsPerSample { get; set; }
    }
}
