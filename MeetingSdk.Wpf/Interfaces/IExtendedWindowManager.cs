using System.Collections.Generic;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public interface IExtendedWindowManager
    {
        Window ExtendedView { get; }

        LayoutRendererStore LayoutRendererStore { get; }

        IList<VideoBox> Items { get; }

        Dictionary<string, object> Properties { get; }
        void SetProperty(string key, object value);

        void ShowExtendedView(Window extendedView);
        void CloseExtendedView();

        
    }
}
