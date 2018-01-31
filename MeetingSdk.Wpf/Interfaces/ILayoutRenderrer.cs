using System.Collections.Generic;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public interface ILayoutRenderer
    {
        bool Render(IList<IVideoBox> videoBoxs, Size canvasSize, string specialVideoBoxName = null);
    }
}
