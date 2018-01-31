using System;

namespace MeetingSdk.NetAgent.Models
{
    public class DatedMeetingModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Topic { get; set; }
        public string Password { get; set; }
    }
}
