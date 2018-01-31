using System.Collections.Generic;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public interface IScreen
    {
        Size Size { get; set; }

        IList<IVideoBox> Items { get; }
    }
}
