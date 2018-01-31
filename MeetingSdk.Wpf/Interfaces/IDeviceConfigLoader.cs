using System.Collections.Generic;

namespace MeetingSdk.Wpf
{
    public interface IDeviceConfigLoader
    {
        IList<DeviceConfigItem> LoadConfig();
        void SaveConfig(IDeviceNameAccessor accessor);
    }
}
