using MeetingSdkTestWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetingSdkTestWpf.Views
{
    /// <summary>
    /// TestView.xaml 的交互逻辑
    /// </summary>
    public partial class TestView : UserControl
    {
        public TestView()
        {
            InitializeComponent();

        }

        private void TestView_Loaded(object sender, RoutedEventArgs e)
        {
            TestViewModel testViewModel = DataContext as TestViewModel;

            if (testViewModel != null)
            {
                testViewModel.PreviewWindowHandle = previewVideo.Handle;
            }
        }
    }
}
