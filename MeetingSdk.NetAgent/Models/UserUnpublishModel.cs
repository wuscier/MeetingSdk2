namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 用户取消发布视频、数据视频、MIC消息数据
    /// </summary>
    public class UserUnpublishModel
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        public int ResourceId { get; set; }
        /// <summary>
        /// 视讯号
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 帐户名称
        /// </summary>
        public string AccountName { get; set; }
    }
}
