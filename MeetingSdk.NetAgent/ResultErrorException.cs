using System;

namespace MeetingSdk.NetAgent
{
    public class ResultErrorException : Exception
    {
        public ResultErrorException()
        {
            
        }

        public ResultErrorException(string message)
            : base(message)
        {
            
        }
    }
}
