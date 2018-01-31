using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.Wpf
{
    public interface IMeetingWindowManager
    {
        void AddLayoutWindow(ILayoutWindow layoutWindow);
        void RemoveLayoutWindow(string windowName);

        IEnumerable<ILayoutWindow> LayoutWindows { get; }

        LayoutRendererStore LayoutRendererStore { get; set; }
        ModeDisplayerStore ModeDisplayerStore { get; set; }

        bool LayoutChange(string windowName, LayoutRenderType layoutRenderType);
        bool ModeChange(ModeDisplayerType modeDisplayerType);


        void Initialize();
        Task Join(int meetingId, bool creating, bool autoPublish = true, bool autoSubscribe = true);
        Task Leave();

        /// <summary>
        /// 本机登录
        /// </summary>
        Participant Participant { get; }

        int HostId { get; set; }

        IEnumerable<Participant> Participants { get; }

        IVideoBoxManager VideoBoxManager { get; }

        Task<MeetingResult<int>> Publish(MediaType mediaType, string deviceName);
        Task<MeetingResult> Unpublish(MediaType mediaType, int resourceId);
        MeetingResult Subscribe(int accountId, int resourceId, MediaType mediaType);
        MeetingResult Unsubscribe(int accountId, int resourceId, MediaType mediaType);
    }
}
