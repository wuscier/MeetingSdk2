using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MeetingSdk.Wpf;
using Prism.Mvvm;
using Prism.Regions;

namespace MeetingSdkTestWpf.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IMeetingWindowManager _windowManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _windowManager = IoC.Get<IMeetingWindowManager>();
        }

        public async void Loaded(object source)
        {          

            _windowManager.Initialize();
            MeetingSdkEventsRegister.Instance.RegisterSdkEvents();

            var deviceNameAccessor = IoC.Get<IDeviceNameAccessor>();
            var providers = IoC.GetAll<IDeviceNameProvider>();
            foreach (var provider in providers)
            {
                await provider.Provider(deviceNameAccessor);
            }
            

            _regionManager.RequestNavigate("ContentRegion", "MainView");
        }

        public void Main()
        {
            _regionManager.RequestNavigate("ContentRegion", "MainView");
        }

        public void Test()
        {
            _regionManager.RequestNavigate("ContentRegion", "TestView");
        }
    }
}
