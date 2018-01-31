using System.Collections.Generic;

namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 视频格式信息
    /// </summary>
    public class VideoFormatModel
    {
        /// <summary>
        /// 颜色空间
        /// </summary>
        public int Colorsapce { get; set; }
        /// <summary>
        /// 颜色空间名称
        /// </summary>
        public string ColorspaceName { get; set; }
        /// <summary>
        /// 视频的尺寸
        /// </summary>
        public List<SizeModel> SizeModels { get; set; }
        /// <summary>
        /// 每秒传输帧数
        /// </summary>
        public List<int> Fps { get; set; }
    }
}