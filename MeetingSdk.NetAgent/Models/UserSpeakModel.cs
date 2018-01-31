namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 用户发言回调结果
    /// </summary>
    public class UserSpeakModel
    {
        /// <summary>
        /// 发言原因
        /// </summary>
        public SpeakReason SpeakReason { get; set; }
        /// <summary>
        /// 原始操作人的名称
        /// </summary>
        public string RelatedSpeakerName { get; set; }
        /// <summary>
        /// 新的发言人信息
        /// </summary>
        public AccountModel Account { get; set; }
    }
}
