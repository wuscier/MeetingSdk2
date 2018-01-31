namespace MeetingSdk.NetAgent.Models
{
    public class AudioStreamModel
    {
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public int BitsPerSameple { get; set; }
        public int StreamId { get; set; }
        public string AccountId { get; set; }
    }
}
