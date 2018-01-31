using System.Collections.Generic;
using System.Windows.Forms.Integration;

namespace MeetingSdk.Wpf.Interfaces
{
    public interface IHost
    {
        IList<WindowsFormsHost> Hosts { get; set; }
    }
}
