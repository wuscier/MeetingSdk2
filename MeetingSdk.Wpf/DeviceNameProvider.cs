using System.Threading.Tasks;

namespace MeetingSdk.Wpf
{
    public interface IDeviceNameProvider
    {
        Task Provider(IDeviceNameAccessor accessor);
    }

    public class DeviceNameProvider : IDeviceNameProvider
    {
        private readonly IDeviceConfigLoader _deviceConfigLoader;
        public DeviceNameProvider(IDeviceConfigLoader deviceConfigLoader)
        {
            _deviceConfigLoader = deviceConfigLoader;
        }

        public async Task Provider(IDeviceNameAccessor accessor)
        {
            await Async.Create(() => Task.Run(() => LoadConfig(accessor))).TryRun("加载DeviceName配置信息。");
        }

        void LoadConfig(IDeviceNameAccessor accessor)
        {
            var items = _deviceConfigLoader.LoadConfig();
            foreach (var item in items)
            {
                foreach (var deviceName in item.DeviceNames)
                {
                    accessor.SetName(item.TypeName, deviceName.Name, deviceName.Option);
                }
            }
        }
    }
}
