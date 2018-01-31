using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using MeetingSdk.NetAgent;
using MeetingSdk.Wpf;
using MeetingSdkTestWpf.Views;
using Prism.Autofac;
using Prism.Modularity;
using Serilog;

namespace MeetingSdkTestWpf
{
    class Bootstrapper : AutofacBootstrapper
    {
        public Bootstrapper()
        {
            InitializeCaliburn();
            InitializeSerilog();
        }

        protected override DependencyObject CreateShell()
        {
            var shellView = Container.Resolve<ShellView>();
            return shellView;
        }
        
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
        protected override IContainer CreateContainer(ContainerBuilder containerBuilder)
        {
            var container = base.CreateContainer(containerBuilder);

            IoC.GetInstance = this.GetInstance;
            IoC.GetAllInstances = this.GetAllInstances;
            IoC.BuildUp = this.BuildUp;
            return container;
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterType<UserInfo>().SingleInstance();

            builder.RegisterInstance(DefaultMeetingSdkAgent.Instance).As<IMeetingSdkAgent>().SingleInstance();
            builder.RegisterType<MeetingWindowManager>().As<IMeetingWindowManager>().SingleInstance();
            builder.RegisterType<DeviceConfigLoader>().As<IDeviceConfigLoader>().SingleInstance();
            builder.RegisterType<DeviceNameAccessor>().As<IDeviceNameAccessor>().SingleInstance();
            builder.RegisterType<DeviceNameProvider>().As<IDeviceNameProvider>();
            builder.RegisterType<VideoBoxManager>().As<IVideoBoxManager>();

            // 注册布局输出
            builder.RegisterType<DefaultLayoutRenderrer>().Named<ILayoutRenderer>("AverageLayout");
            //builder.RegisterType<AverageLayoutRenderer>().Named<ILayoutRenderrer>("AverageLayout");
            //builder.RegisterType<BigSmallsLayoutRenderer>().Named<ILayoutRenderrer>("CloseupLayout");
            //builder.RegisterType<CloseupLayoutRenderer>().Named<ILayoutRenderrer>("BigSmallsLayout");


            builder.RegisterType<PublishMicStreamParameterProvider>().As<IStreamParameterProvider<PublishMicStreamParameter>>().SingleInstance();
            builder.RegisterType<PublishCameraStreamParameterProvider>().As<IStreamParameterProvider<PublishCameraStreamParameter>>().SingleInstance();
            builder.RegisterType<PublishDataCardStreamParameterProvider>().As<IStreamParameterProvider<PublishDataCardStreamParameter>>().SingleInstance();
            builder.RegisterType<PublishWinCaptureStreamParameterProvider>().As<IStreamParameterProvider<PublishWinCaptureStreamParameter>>().SingleInstance();
            builder.RegisterType<SubscribeMicStreamParameterProvider>().As<IStreamParameterProvider<SubscribeMicStreamParameter>>().SingleInstance();
            builder.RegisterType<SubscribeCameraStreamParameterProvider>().As<IStreamParameterProvider<SubscribeCameraStreamParameter>>().SingleInstance();
            builder.RegisterType<SubscribeDataCardStreamParameterProvider>().As<IStreamParameterProvider<SubscribeDataCardStreamParameter>>().SingleInstance();
            builder.RegisterType<SubscribeWinCaptureStreamParameterProvider>().As<IStreamParameterProvider<SubscribeWinCaptureStreamParameter>>().SingleInstance();

            builder.RegisterTypeForNavigation<MainView>();
            builder.RegisterTypeForNavigation<TestView>();

            builder.RegisterType<TestView>().AsSelf().SingleInstance();
            builder.RegisterType<MainView>().AsSelf().SingleInstance();
        }

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (DirectoryModuleCatalog)ModuleCatalog;
            catalog.Initialize();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var path = Path.GetDirectoryName(_assembly.Location) + "\\modules";
            Directory.CreateDirectory(path);
            return new DirectoryModuleCatalog() { ModulePath = path };
        }
        
        private void InitializeSerilog()
        {
            var path = Path.GetDirectoryName(ThisAssembly.Location);
            var logPath = Path.Combine(path, "logs");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile(logPath + "/log-{Date}.txt")
                .CreateLogger();
            MeetingLogger.SetLogger(new WpfLogger());
        }

        #region Caliburn

        protected object GetInstance(System.Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                object instance;
                if (this.Container.TryResolve((System.Type)service, out instance))
                    return instance;
            }
            else
            {
                object instance;
                if (this.Container.TryResolveNamed(key, (System.Type)service, out instance))
                    return instance;
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", (object)(key ?? service.Name)));
        }

        protected IEnumerable<object> GetAllInstances(System.Type service)
        {
            return this.Container.Resolve((System.Type)typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected void BuildUp(object instance)
        {
            this.Container.InjectProperties<object>(instance);
        }

        private void InitializeCaliburn()
        {
            PlatformProvider.Current = new XamlPlatformProvider();

            var baseExtractTypes = AssemblySourceCache.ExtractTypes;
            AssemblySourceCache.ExtractTypes = assembly =>
            {
                var baseTypes = baseExtractTypes(assembly);
                var elementTypes = assembly.GetExportedTypes()
                    .Where(t => typeof(UIElement).IsAssignableFrom(t));

                return baseTypes.Union(elementTypes);
            };

            AssemblySourceCache.Install();
            AssemblySource.Instance.AddRange(SelectAssemblies());
        }

        IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { ThisAssembly };
        }

        #endregion

        private Assembly _assembly;
        public Assembly ThisAssembly => _assembly ?? (_assembly = Assembly.GetEntryAssembly());
    }
}
