namespace MeetingSdk.Wpf
{
    public interface IStreamParameterProvider<in T>
        where T : IStreamParameter
    {
        void Provider(T parameter, string sourceName);
    }
}
