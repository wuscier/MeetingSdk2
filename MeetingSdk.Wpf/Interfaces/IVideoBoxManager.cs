using MeetingSdk.NetAgent.Models;
using System.Collections.Generic;

namespace MeetingSdk.Wpf
{
    public interface IVideoBoxManager
    {
        IList<VideoBox> Items { get; }
        Dictionary<string, object> Properties { get; }

        void SetProperty(string key, object value);

        void SetVideoBox(VideoBox videoBox);

        /// <summary>
        /// 获取可用窗口
        /// </summary>
        /// <param name="accountId"></param>    
        /// <param name="videoBoxType"></param>
        /// <param name="videoBox"></param>
        /// <returns></returns>
        bool TryGet(AccountModel accountModel, VideoBoxType videoBoxType, MediaType mediaType, out VideoBox videoBox);

        /// <summary>
        /// 释放该用户的所有占用窗口
        /// </summary>
        /// <param name="accountId"></param>
        void Release(int accountId);

        /// <summary>
        /// 释放该用户占用的窗口
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="videoBoxType"></param>
        void Release(int accountId, VideoBoxType videoBoxType);
    }
}
