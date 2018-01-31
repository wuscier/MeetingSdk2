namespace MeetingSdk.NetAgent.Models
{
    public class LiveParameter
    {
        public string Url1 { get; set; }
        public string Url2 { get; set; }
        public string FilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int VideoBitrate { get; set; }
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public int BitsPerSample { get; set; }
        public int AudioBitrate { get; set; }
        public bool IsLive { get; set; }
        public bool IsRecord { get; set; }
    }
}