namespace MeetingSdk.NetAgent.Models
{
    public class ParticipantModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        /// <summary>
        /// 是否已经举手
        /// </summary>
        public bool IsRaisedHand { get; set; }
        /// <summary>
        /// 是否正在发言
        /// </summary>
        public bool IsSpeaking { get; set; }
        /// <summary>
        /// 是否是主持人
        /// </summary>
        public bool IsHost { get; set; }
        /// <summary>
        /// 扬声器是否打开
        /// </summary>
        public bool IsSpeakerOn { get; set; }
    }
}
