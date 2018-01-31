using System;
using System.Diagnostics;

namespace MeetingSdk.NetAgent
{
    public interface IMeetingLogger
    {
        void LogMessage(string message);
        void LogError(Exception e, string message);
    }

    public class MeetingLogger : IMeetingLogger
    {
        private static IMeetingLogger _logger;
        private MeetingLogger()
        {
            _logger = new InnerLogger();
        }

        public static readonly IMeetingLogger Logger = new MeetingLogger();

        public void LogError(Exception e, string message)
        {
            _logger.LogError(e, message);
        }

        public void LogMessage(string message)
        {
            _logger.LogMessage(message);
        }

        public static void SetLogger(IMeetingLogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        class InnerLogger : IMeetingLogger
        {
            public void LogError(Exception e, string message)
            {
                Trace.WriteLine($"error:{e.Message}, message:{message}");
            }

            public void LogMessage(string message)
            {
                Trace.WriteLine(message);
            }
        }
    }
}
