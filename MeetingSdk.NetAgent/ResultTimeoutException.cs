using System;

namespace MeetingSdk.NetAgent
{
    public class ResultTimeoutException : Exception
    {
        public ResultTimeoutException()
        {
            
        }

        public ResultTimeoutException(string message)
            :base(message)
        {
            
        }
    }
}
