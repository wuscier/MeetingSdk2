namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 用户的流信息
    /// </summary>
    public class MeetingUserStreamModel
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public AccountModel Account { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public int ResourceId { get; set; }
        /// <summary>
        /// 同步ID
        /// </summary>
        public int SyncGroupId { get; set; }
        /// <summary>
        /// 媒体类型
        /// </summary>
        public int MediaType { get; set; }
    }
}
