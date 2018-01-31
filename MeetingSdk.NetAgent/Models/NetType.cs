namespace MeetingSdk.NetAgent.Models
{
    public enum NetType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// 网络联通性
        /// </summary>
        NetConnection,
        /// <summary>
        /// 会议连通性
        /// </summary>
        MeetingConnection,
        /// <summary>
        /// 网速检测
        /// </summary>
        BandDetect,
    }
}
