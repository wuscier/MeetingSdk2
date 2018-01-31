using System.Collections.Generic;

namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 加入会议信息
    /// </summary>
    public class JoinMeetingModel
    {
        public JoinMeetingModel()
        {
            MeetingSpeakerModels = new List<MeetingSpeakerModel>();
        }

        /// <summary>
        /// 是否加锁，C++回调转换时，1代表true，0代表false
        /// </summary>
        public bool HasLock { get; set; }
        /// <summary>
        /// 主持人信息
        /// </summary>
        public AccountModel Account { get; set; }
        /// <summary>
        /// 用户类型，普通用户还是主持人
        /// </summary>
        public AttendeeType AttendeeType { get; set; }
        /// <summary>
        /// 会议模式
        /// </summary>
        public MeetingMode MeetingMode { get; set; }
        /// <summary>
        /// 是否是直播会议
        /// </summary>
        public LiveStatus LiveStatus { get; set; }
        /// <summary>
        /// 发言者列表
        /// </summary>
        public List<MeetingSpeakerModel> MeetingSpeakerModels { get; set; }
    }
}
