using System.Collections.Generic;

namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 发言人信息
    /// </summary>
    public class MeetingSpeakerModel
    {
        public MeetingSpeakerModel()
        {
            this.MeetingUserStreamInfos = new List<MeetingUserStreamModel>();
        }

        /// <summary>
        /// 发言人基本信息
        /// </summary>
        public AccountModel Account { get; set; }
        /// <summary>
        /// 发言人的流集合
        /// </summary>
        public List<MeetingUserStreamModel> MeetingUserStreamInfos { get; set; }
    }
}
