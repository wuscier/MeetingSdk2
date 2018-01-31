namespace MeetingSdk.NetAgent.Models
{
    public class TransModel
    {
        /// <summary>
        /// fec数据比例，-1 表示不指定
        /// </summary>
        public int FecDataCount { get; set; }

        /// <summary>
        /// fec校验包比例 -1 表示不指定
        /// </summary>
        public int FecCheckCount { get; set; }

        /// <summary>
        /// 数据包发送份数 -1 表示不指定
        /// </summary>
        public int DataSendCount { get; set; }

        /// <summary>
        /// 校验包发送份数 -1 表示不指定
        /// </summary>
        public int CheckSendCount { get; set; }

        /// <summary>
        /// 补发数据包发送份数 -1 表示不指定
        /// </summary>
        public int DataRetransSendCount { get; set; }

        /// <summary>
        /// 补发校验包发送份数 -1 表示不指定
        /// </summary>
        public int CheckRetransSendCount { get; set; }

        /// <summary>
        /// 补发请求次数 -1 表示不指定
        /// </summary>
        public int DataResendCount { get; set; }

        /// <summary>
        /// 接收延迟窗口大小，单位ms（仅下行使用） -1 表示不指定
        /// </summary>
        public int DelayTimeWinsize { get; set; }
    }

}
