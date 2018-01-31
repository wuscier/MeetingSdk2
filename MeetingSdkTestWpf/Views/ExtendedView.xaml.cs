﻿using System;
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
using System.Windows.Shapes;

namespace MeetingSdkTestWpf.Views
{
    /// <summary>
    /// ExtendedView.xaml 的交互逻辑
    /// </summary>
    public partial class ExtendedView
    {
        public ExtendedView()
        {
            InitializeComponent();
        }

        public IntPtr DisplayHanlde { get; set; }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayHanlde = pictureBox.Handle;
        }
    }
}
