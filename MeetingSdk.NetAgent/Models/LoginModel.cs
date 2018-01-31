namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 登录后的回调结果
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public AccountModel Account { get; set; }
        /// <summary>
        /// 用户Token的起始时间
        /// </summary>
        public string TokenStartTime { get; set; }
        /// <summary>
        /// 用户Token的结束时间
        /// </summary>
        public string TokenEndTime { get; set; }
        /// <summary>
        /// 用户Token
        /// </summary>
        public string Token { get; set; }
    }
}
