namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 会议信息
    /// </summary>
    public class MeetingModel
    {
        /// <summary>
        /// 会议ID
        /// </summary>
        public int MeetingId { get; set; }
        /// <summary>
        /// 会议类型
        /// </summary>
        public MeetingType MeetingType { get; set; }
        /// <summary>
        /// 会议创建者信息
        /// </summary>
        public AccountModel Account { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 是否有密码
        /// </summary>
        public bool? HavePwd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HostId { get; set; }
        /// <summary>
        /// 会议主题
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 会议状态
        /// </summary>
        public MeetingStatus MeetingStatus { get; set; }
    }
}
