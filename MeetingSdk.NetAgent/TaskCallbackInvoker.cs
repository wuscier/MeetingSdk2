using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace MeetingSdk.NetAgent
{
    public class TaskCallbackInvoker
    {
        const int MaxClearNum = 60;

        private static readonly ConcurrentDictionary<string, IList<ITaskCallback>> Hash;

        private static readonly object LockObj = new object();

        private static readonly System.Threading.Timer Timer;

        static TaskCallbackInvoker()
        {
            Hash = new ConcurrentDictionary<string, IList<ITaskCallback>>();
            Timer = new System.Threading.Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public static void Register(ITaskCallback taskCallback)
        {
            lock (LockObj)
            {
                var cache = Hash.GetOrAdd(taskCallback.Name, (name) => new List<ITaskCallback>());
                cache.Add(taskCallback);

                _clearNum = 0;
                Timer.Change(1000, Timeout.Infinite);
            }
        }

        public static bool RegisterSingle(ITaskCallback taskCallback)
        {
            var cache = Hash.GetOrAdd(taskCallback.Name, (name) => new List<ITaskCallback>());
            if (cache.Count > 0)
                return false;

            lock (LockObj)
            {
                if (cache.Count > 0)
                    return false;

                cache.Add(taskCallback);
                _clearNum = 0;
                Timer.Change(1000, Timeout.Infinite);
                return true;
            }
        }

        public static void Invoke<T>(string name, string uniqueId, T result)
              where T : class, IMeetingResult
        {
            IList<ITaskCallback> cache;
            if (Hash.TryGetValue(name, out cache))
            {
                var items = new List<ITaskCallback>();
                foreach (var cb in cache)
                {
                    if (string.IsNullOrEmpty(uniqueId) ||
                        uniqueId.Equals(cb.UniqueId))
                    {
                        items.Add(cb);
                    }
                }
                foreach (var item in items)
                {
                    item.SetResult(result);
                }
            }
        }

        private static int _clearNum;
        private static int _runClear;

        static void TimerCallback(object state)
        {
            if (Interlocked.CompareExchange(ref _runClear, 1, 0) == 0)
            {
                ClearExpires();
                Timer.Change(_clearNum > MaxClearNum ? Timeout.Infinite : 1000, Timeout.Infinite);
                _runClear = 0;
            }
        }

        static void ClearExpires()
        {
            int cached = 0;
            foreach (var key in Hash.Keys)
            {
                var t = DateTime.Now.GetTimestamp();
                IList<ITaskCallback> cache;
                if (Hash.TryGetValue(key, out cache))
                {
                    var items = new List<ITaskCallback>();
                    foreach (var cb in cache)
                    {
                        if (cb.Task.IsCompleted ||
                            cb.Task.IsCanceled ||
                            cb.Task.IsFaulted)
                        {
                            items.Add(cb);
                        }
                        else if (cb.Timeout > 0 && cb.StartTime + cb.Timeout < t)
                        {
                            cb.SetException(new ResultTimeoutException("超时。"));
                            items.Add(cb);
                        }
                    }
                    foreach (var item in items)
                    {
                        cached++;
                        cache.Remove(item);
                    }
                }
            }
            if (cached == 0)
            {
                _clearNum++;
            }
        }
    }
}
