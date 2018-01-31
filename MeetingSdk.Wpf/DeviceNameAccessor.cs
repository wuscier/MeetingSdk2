using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MeetingSdk.Wpf
{
    public interface IDeviceNameAccessor : IEnumerable<KeyValuePair<string, DeviceName>>
    {
        bool Contains(string deviceName);
        bool Contains(string typeName, string deviceName);

        bool TryGetSingleName(string typeName, out string deviceName);
        void SetSingleName(string typeName, string deviceName);
        
        bool TryGetName(string typeName, Func<DeviceName, bool> predicate, out IEnumerable<string> deviceName);
        void SetName(string typeName, string deviceName, string option = null);
    }

    public class DeviceName
    {
        public const string Camera = "Camera";
        public const string Microphone = "Microphone";
        public const string Speaker = "Speaker";

        public DeviceName(string name, string option = null)
        {
            this.Name = name ?? "";
            this.Option = option;
        }

        public string Option { get; }

        public string Name { get; }

        public static implicit operator string(DeviceName deviceName)
        {
            return deviceName?.Name;
        }

        public static implicit operator DeviceName(string deviceName)
        {
            return new DeviceName(deviceName);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var deviceName = obj as DeviceName;
            if (deviceName != null)
                return this.Name.Equals(deviceName.Name);

            var str = obj as string;
            if (str != null)
                return string.Equals(str, this.Name);

            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public class Comparer : IEqualityComparer<DeviceName>
        {
            public bool Equals(DeviceName x, DeviceName y)
            {
                return object.Equals(x, y);
            }

            public int GetHashCode(DeviceName obj)
            {
                return obj.GetHashCode();
            }
        }
    }

    public class DeviceNameAccessor : IDeviceNameAccessor
    {
        private readonly Dictionary<string, HashSet<DeviceName>> _names = 
            new Dictionary<string, HashSet<DeviceName>>();

        public DeviceNameAccessor()
        {
        }
        
        public bool Contains(string deviceName)
        {
            var result = false;
            foreach (var name in _names)
            {
                if (name.Value.Contains(deviceName))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Contains(string typeName, string deviceName)
        {
            HashSet<DeviceName> hash;
            if (_names.TryGetValue(typeName, out hash))
            {
                return hash.Contains(deviceName);
            }
            return false;
        }

        public bool TryGetSingleName(string typeName, out string deviceName)
        {
            deviceName = null;
            if (!_names.ContainsKey(typeName))
                return false;

            var hash = _names[typeName];
            deviceName = hash.FirstOrDefault();
            return !string.IsNullOrEmpty(deviceName);
        }

        public void SetSingleName(string typeName, string deviceName)
        {
            if (deviceName == null)
            {
                var hash = EnsureGet(typeName);
                hash.Clear();
            }
            else
            {
                var hash = EnsureGet(typeName);
                if (hash.Count > 0)
                    hash.Clear();
                hash.Add(deviceName);
            }
        }

        public bool TryGetName(string typeName, Func<DeviceName, bool> predicate, out IEnumerable<string> deviceName)
        {
            deviceName = Enumerable.Empty<string>();
            if (!_names.ContainsKey(typeName))
                return false;

            var hash = EnsureGet(typeName);
            deviceName = hash.Where(predicate).Select(m=>m.Name).ToArray();
            return deviceName.Any();
        }

        public void SetName(string typeName, string deviceName, string option = null)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                var hash = EnsureGet(typeName);
                hash.Clear();
            }
            else
            {
                var hash = EnsureGet(typeName);
                var name = hash.RemoveWhere(m => m.Name == deviceName);
                hash.Add(new DeviceName(deviceName, option));
            }
        }

        private HashSet<DeviceName> EnsureGet(string typeName)
        {
            HashSet<DeviceName> hash;
            if (!_names.TryGetValue(typeName, out hash))
            {
                lock (_names)
                {
                    hash = new HashSet<DeviceName>(new DeviceName.Comparer());
                    _names.Add(typeName, hash);
                }
            }
            return hash;
        }

        public IEnumerator<KeyValuePair<string, DeviceName>> GetEnumerator()
        {
            foreach (var name in _names)
            {
                foreach (var deviceName in name.Value)
                {
                    yield return new KeyValuePair<string, DeviceName>(name.Key, deviceName);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
