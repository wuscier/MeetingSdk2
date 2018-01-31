using Caliburn.Micro;

namespace MeetingSdk.Wpf
{
    public static class StreamParameterProviders
    {
        public static IStreamParameterProvider<T> GetProvider<T>()
            where T:IStreamParameter
        {
            return IoC.Get<IStreamParameterProvider<T>>();
        }

        public static void ProviderParameter<T>(T parameter, string sourceName)
            where T : IStreamParameter
        {
            var provider = GetProvider<T>();
            provider.Provider(parameter,sourceName);
        }

        public static T GetParameter<T>(string sourceName)
            where T : IStreamParameter, new()
        {
            T parameter = new T();
            ProviderParameter(parameter,sourceName);
            return parameter;
        }
    }
}
