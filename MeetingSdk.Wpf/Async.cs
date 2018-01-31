using System;
using System.Threading.Tasks;

namespace MeetingSdk.Wpf
{
    public static class Async
    {
        public static Func<Task<T>> Create<T>(Func<Task<T>> func)
        {
            return func;
        }

        public static Func<Task> Create(Func<Task> func)
        {
            return func;
        }
    }
}
