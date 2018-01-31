using System;
using System.Threading.Tasks;

namespace MeetingSdk.NetAgent
{
    public class TaskCallback<TResult> : TaskCallbackBase<TResult>
        where TResult : class, IMeetingResult
    {
        public TaskCallback(string name, string uniqueId, Action<Task> action = null) 
            : base(name, uniqueId, action)
        {
        }

        public TaskCallback(string name, string uniqueId, uint timeout = 0, Action<Task> action = null) 
            : base(name, uniqueId, timeout, action)
        {
        }
    }
}
