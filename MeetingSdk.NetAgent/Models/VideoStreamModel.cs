namespace MeetingSdk.NetAgent.Models
{
    public class VideoStreamModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StreamId { get; set; }
        public string AccountId { get; set; }
        public VideoType VideoType { get; set; }
    }
}
