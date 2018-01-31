using System;

namespace MeetingSdk.NetAgent
{
    public class EventTaskCallback<TResult> : TaskCallbackBase<TResult>
        where TResult : class, IMeetingResult
    {
        private readonly Action<TResult> _action;
        public EventTaskCallback(string name, Action<TResult> action)
            : base(name, "", null)
        {
            _action = action;
        }

        protected override void SetResult(TResult result)
        {
            try
            {
                _action.Invoke(result);
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "EventTaskCallback Error.");
            }
        }
    }
}
