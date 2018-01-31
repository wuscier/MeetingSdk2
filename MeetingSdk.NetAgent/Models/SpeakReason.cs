namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 开始发言的原因
    /// </summary>
    public enum SpeakReason
    {
        /// <summary>
        /// 正常产生
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 由传麦产生
        /// </summary>
        ReceiveMic,
        /// <summary>
        /// 由主持人指定发言
        /// </summary>
        AssignedByHost,
    }
}
