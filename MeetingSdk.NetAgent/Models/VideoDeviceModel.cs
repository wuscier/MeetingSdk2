using System.Collections.Generic;

namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 视频设备信息
    /// </summary>
    public class VideoDeviceModel
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 支持的格式列表
        /// </summary>
        public List<VideoFormatModel> VideoFormatModels { get; set; }
    }
}
