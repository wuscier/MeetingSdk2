using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MeetingSdk.Wpf;
using Prism.Events;

namespace MeetingSdkTestWpf.Views
{
    /// <summary>
    /// ShellView.xaml 的交互逻辑
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        private readonly Timer _timer;
        public ShellView()
        {
            InitializeComponent();
            _timer = new Timer(LayoutChanged, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void ShellView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _timer.Change(1000, Timeout.Infinite);
        }

        void LayoutChanged(object state)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            //eventAggregator.GetEvent<LayoutChangeEvent>().Publish();
        }
    }
}
