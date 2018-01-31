using System.Collections.Generic;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public interface IModeDisplayer
    {
        //bool CanDisplay(IVideoBoxManager videoBoxManager,out string cannotDisplayMessage);
        bool Display(IList<IVideoBox> videoBoxs, Size canvasSize);
        //void CalculateViewBoxPositions(IVideoBoxManager videoBoxManager, double width, double height, string key);
    }
}
