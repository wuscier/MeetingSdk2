namespace MeetingSdk.NetAgent.Models
{
    public class RecommendContactModel
    {
        /// <summary>
        /// 推荐人Id
        /// </summary>
        public int SourceId { get; set; }

        public string ContactId { get; set; }

        public string ContactName { get; set; }

        public int Version { get; set; }

        public int Number { get; set; }
    }
}
