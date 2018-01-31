using MeetingSdk.NetAgent.Models;
using System.Windows;

namespace MeetingSdk.Wpf.Interfaces
{
    public interface IGetLiveVideoCoordinate
    {
        VideoStreamModel[] GetVideoStreamModels(Size canvasSize);
    }
}
