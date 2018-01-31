using System.Collections.Generic;

namespace MeetingSdk.Wpf
{
    public class DeviceConfigItem
    {
        public DeviceConfigItem()
        {
            DeviceNames = new List<DeviceName>();
        }

        public string TypeName { get; set; }
        public IList<DeviceName> DeviceNames { get; set; }
    }
}
