namespace MeetingSdk.NetAgent
{
    public interface IMeetingResult
    {
        int StatusCode { get; }
        string Message { get; }
    }

    public class MeetingResult : IMeetingResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public static MeetingResult<T> Error<T>(string message)
        {
            var result = new MeetingResult<T>
            {
                Result = default(T),
                StatusCode = -9999,
                Message = message
            };
            return result;
        }
    }

    public class MeetingResult<T> : MeetingResult
    {
        public T Result { get; set; }
    }
}
