namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 用户发布视频、数据视频、MIC消息数据
    /// </summary>
    public class UserPublishModel
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        public int ResourceId { get; set; }
        /// <summary>
        /// 音视频同步Id
        /// </summary>
        public int SyncId { get; set; }
        /// <summary>
        /// 视讯号
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 帐户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExtraInfo { get; set; }
    }
}
