namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 发言回调结果
    /// </summary>
    public class SpeakModel
    {
        /// <summary>
        /// 发言原因
        /// </summary>
        public SpeakReason SpeakReason { get; set; }
        /// <summary>
        /// 原始操作人的名称
        /// </summary>
        public string SpeakerName { get; set; }
    }
}
