using System;
using System.Threading;
using System.Threading.Tasks;

namespace MeetingSdk.NetAgent
{
    public interface ITaskCallback
    {
        long StartTime { get; }
        long Timeout { get; }
        string Name { get; }
        string UniqueId { get; }

        void SetResult(object result);
        void SetException(Exception e);

        Task Task { get; }
    }

    public abstract class TaskCallbackBase<TResult> : ITaskCallback
        where TResult : class, IMeetingResult
    {
        public long StartTime { get; }
        public long Timeout { get; }
        public string Name { get; }
        public string UniqueId { get; }

        private int _set = 0;
        private readonly TaskCompletionSource<TResult> _tcs;
        
        public Task Task => _tcs.Task;

        protected TaskCallbackBase(string name, string uniqueId, Action<Task> action)
            : this(name, uniqueId, 0, action)
        {
        }

        protected TaskCallbackBase(string name, string uniqueId, uint timeout, Action<Task> action)
        {
            this.Name = name;
            this.UniqueId = uniqueId;

            StartTime = DateTime.Now.GetTimestamp();
            Timeout = timeout;
            
            _tcs = new TaskCompletionSource<TResult>();
            if (action != null)
            {
                _tcs.Task.ContinueWith(action);
            }
        }

        protected virtual void SetResult(TResult result)
        {
            if (Interlocked.CompareExchange(ref _set, 1, 0) == 0)
            {
                _tcs.SetResult(result);
            }
        }

        public void SetResult(object result)
        {
            TResult temp = result as TResult;
            if (temp == null)
                SetException(new NullReferenceException("转换结果失败。"));

            else
                SetResult(temp);
        }

        public void SetException(Exception e)
        {
            if (Interlocked.CompareExchange(ref _set, 1, 0) == 0)
            {
                _tcs.SetException(e);
            }
        }
    }
}
