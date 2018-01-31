using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Caliburn.Micro;
using Binding = System.Windows.Data.Binding;
using Control = System.Windows.Forms.Control;
using Prism.Events;
using Label = System.Windows.Forms.Label;
using System.Drawing;

namespace MeetingSdk.Wpf
{
    public class VideoBox : PropertyChangedBase, IVideoBox
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeetingWindowManager _meetingWindowManager;

        public VideoBox(string name, WindowsFormsHost host)
        {
            this.Name = name;
            this.Host = host;
            Handle = IntPtr.Zero;
            _eventAggregator = IoC.Get<IEventAggregator>();
            _meetingWindowManager = IoC.Get<IMeetingWindowManager>();
        }

        public WindowsFormsHost Host { get; }

        public Label Label { get; set; }


        /// <summary>
        /// 视频窗口Id
        /// </summary>
        public string Name { get; }
        public IntPtr Handle { get; set; }
        public VideoBoxType VideoBoxType { get; set; }

        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                this.NotifyOfPropertyChange(() => this.Width);
            }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                this.NotifyOfPropertyChange(() => this.Height);
            }
        }

        private double _poxX;
        public double PosX
        {
            get => _poxX;
            set
            {
                _poxX = value;
                this.NotifyOfPropertyChange(() => this.PosX);
            }
        }

        private double _poxY;
        public double PosY
        {
            get => _poxY;
            set
            {
                _poxY = value;
                this.NotifyOfPropertyChange(() => this.PosY);
            }
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                this.NotifyOfPropertyChange(() => this.Visible);
            }
        }

        public int Sequence { get; set; }

        public void SetHandle()
        {
            PictureBox pictureBox = Host.Child as PictureBox;
            if (pictureBox == null)
            {
                var panel = Host.Child as System.Windows.Forms.Panel;
                if (panel != null)
                {
                    foreach (Control control in panel.Controls)
                    {
                        pictureBox = control as PictureBox;
                    }
                }
            }
            if (pictureBox != null)
            {
                Label = new Label()
                {
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Visible = false
                };
                pictureBox.Controls.Add(Label);
                pictureBox.Paint += (sender, args) =>
                 {
                     Label.AutoSize = true;
                     if (AccountResource != null)
                     {
                         if (AccountResource.AccountModel.AccountId == _meetingWindowManager.HostId && AccountResource.MediaType == NetAgent.Models.MediaType.VideoDoc)
                         {
                             Label.Text = AccountResource.AccountModel.AccountName + "（课件）";
                         }
                         else
                         {
                             Label.Text = AccountResource.AccountModel.AccountName;
                         }
                         Label.Visible = !string.IsNullOrEmpty(AccountResource.AccountModel.AccountName);
                         Label.Location = new System.Drawing.Point((pictureBox.Width - Label.Width) / 2,
                             pictureBox.Height - Label.Height - 30);
                     }

                 };
                this.Handle = pictureBox.Handle;
            }
        }

        public void SetBinding()
        {
            var wBinding = new Binding("Width") { Source = this };
            Host.SetBinding(FrameworkElement.WidthProperty, wBinding);

            var hBinding = new Binding("Height") { Source = this };
            Host.SetBinding(FrameworkElement.HeightProperty, hBinding);

            var xBinding = new Binding("PosX") { Source = this };
            Host.SetBinding(Canvas.LeftProperty, xBinding);

            var yBinding = new Binding("PosY") { Source = this };
            Host.SetBinding(Canvas.TopProperty, yBinding);

            var vBinding = new Binding("Visible")
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };
            Host.SetBinding(UIElement.VisibilityProperty, vBinding);
        }

        private AccountResource _accountResource;

        public AccountResource AccountResource
        {
            get => _accountResource;
            set
            {
                _accountResource = value;
                Visible = _accountResource != null;
            }
        }
    }
}