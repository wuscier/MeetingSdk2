using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingSdk.NetAgent;
using Serilog;

namespace MeetingSdkTestWpf
{
    public class WpfLogger : IMeetingLogger
    {
        public void LogMessage(string message)
        {
            Log.Logger.Information(message);
        }

        public void LogError(Exception e, string message)
        {
            Log.Logger.Error(e, message);
        }
    }
}
