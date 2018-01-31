using System;

namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 订阅视频流参数
    /// </summary>
    public class SubscribeVideoModel
    {
        /// <summary>
        /// 视讯号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 媒体流类型, 取值MEETINGMANAGE_STREAM_TYPE_VIDEO_RECV
        /// </summary>
        public StreamType StreamType { get; set; }

        /// <summary>
        /// 视频流接收参数
        /// </summary>
        public VideoRecvModel VideoRecvModel { get; set; }

        /// <summary>
        /// 资源标识
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// 媒体类型,取值MEETINGMANAGE_VIDEO_CAMRA以及MEETINGMANAGE_VIDEO_DOC
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

    public class VideoRecvModel
    {
        /// <summary>
        /// 视频预览窗口句柄
        /// </summary>
        public IntPtr DisplayWindow { get; set; }
        /// <summary>
        /// 显示充满模式,预定义显示模式，用户可以自己指定需要的显示比例
        /// </summary>
        public DisplayFillMode DisplayFillMode { get; set; }
    }
}
