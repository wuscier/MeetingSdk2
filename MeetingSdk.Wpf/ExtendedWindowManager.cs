using Caliburn.Micro;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Binding = System.Windows.Data.Binding;
using MeetingSdk.Wpf.Interfaces;
using Label = System.Windows.Forms.Label;
using System.Drawing;
using MeetingSdk.NetAgent;

namespace MeetingSdk.Wpf
{
    public delegate void RemoveWindowDelegate(string windowName);

    public class ExtendedWindowManager : IExtendedWindowManager, IScreen, ILayoutWindow, IDisposeWindow
    {
        private readonly IMeetingWindowManager windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeetingSdkAgent _meetingSdkAgent;

        private RemoveWindowDelegate _disposeWindowDelegate;
        private readonly static object _syncRoot = new object();


        private readonly Dictionary<string, VideoBox> _items = new Dictionary<string, VideoBox>();

        public ExtendedWindowManager(IMeetingWindowManager meetingWindowManager, IEventAggregator eventAggregator, IMeetingSdkAgent meetingSdkAgent)
        {
            windowManager = meetingWindowManager;
            _eventAggregator = eventAggregator;
            _meetingSdkAgent = meetingSdkAgent;

            _eventAggregator.GetEvent<VideoBoxAddedEvent>().Subscribe(AccountResourceAddedEventHandler);
            _eventAggregator.GetEvent<VideoBoxRemovedEvent>().Subscribe(AccountResourceRemovedEventHandler);

            LayoutRendererStore = new LayoutRendererStore()
            {
                CurrentLayoutRenderType = LayoutRenderType.AverageLayout,
            };

            Properties = new Dictionary<string, object>();
        }

        private void AccountResourceAddedEventHandler(VideoBox videoBox)
        {
            lock (_syncRoot)
            {
                if (ExtendedView == null)
                {
                    return;
                }

                ExtendedView.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    if (ExtendedView == null)
                    {
                        return;
                    }

                    var tobeAddedItem = _items[videoBox.Name];

                    if (tobeAddedItem != null && tobeAddedItem.AccountResource != null)
                    {
                        tobeAddedItem = videoBox.Copy();

                        SetDataBindings(tobeAddedItem);

                        _items[videoBox.Name] = tobeAddedItem;

                        MeetingResult meetingResult = _meetingSdkAgent.AddDisplayWindow(tobeAddedItem.AccountResource.AccountModel.AccountId, tobeAddedItem.AccountResource.ResourceId, tobeAddedItem.Handle, 0, 0);
                        System.Console.WriteLine($"AddDisplayWindow：statusCode={meetingResult.StatusCode}, msg={meetingResult.Message}, accountId={tobeAddedItem.AccountResource.AccountModel.AccountId}");
                    }

                    LayoutChange(LayoutRendererStore.CurrentLayoutRenderType);

                    _eventAggregator.GetEvent<ExtendedViewsShowedEvent>().Publish();
                }));
            }
            // 同步画面资源信息。
        }

        private void AccountResourceRemovedEventHandler(VideoBox videoBox)
        {
            lock (_syncRoot)
            {
                if (ExtendedView == null)
                {
                    return;
                }

                ExtendedView.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    if (ExtendedView == null)
                    {
                        return;
                    }

                    var tobeRemovedItem = _items[videoBox.Name];

                    if (tobeRemovedItem != null && tobeRemovedItem.AccountResource != null)
                    {
                        MeetingResult meetingResult = _meetingSdkAgent.RemoveDisplayWindow(tobeRemovedItem.AccountResource.AccountModel.AccountId, tobeRemovedItem.AccountResource.ResourceId, tobeRemovedItem.Handle, 0, 0);
                        System.Console.WriteLine($"RemoveDisplayWindow：statusCode={meetingResult.StatusCode}, msg={meetingResult.Message}, accountId={tobeRemovedItem.AccountResource.AccountModel.AccountId}");
                        tobeRemovedItem.AccountResource = null;
                        _items[videoBox.Name] = tobeRemovedItem;
                    }

                    LayoutChange(LayoutRendererStore.CurrentLayoutRenderType);

                    _eventAggregator.GetEvent<ExtendedViewsShowedEvent>().Publish();
                }));

            }
            // 同步画面资源信息。
        }


        public Window ExtendedView { get; private set; }

        public LayoutRendererStore LayoutRendererStore { get; set; }

        IList<VideoBox> IExtendedWindowManager.Items => _items.Values.ToList();

        public Dictionary<string, object> Properties { get; private set; }
        public void SetProperty(string key, object value)
        {
            if (Properties.Keys.Contains(key))
            {
                Properties[key] = value;
            }
            else
            {
                Properties.Add(key, value);
            }
        }

        public void ShowExtendedView(Window extendedView)
        {
            _disposeWindowDelegate = windowManager.RemoveLayoutWindow;
            windowManager.AddLayoutWindow(this);

            lock (_syncRoot)
            {
                ExtendedView = extendedView;
                Size = new System.Windows.Size()
                {
                    Width = extendedView.Width,
                    Height = extendedView.Height,
                };

                Properties.Clear();
                _items.Clear();

                foreach (var item in windowManager.VideoBoxManager.Items)
                {
                    VideoBox copiedVideoBox = item.Copy();

                    SetDataBindings(copiedVideoBox);

                    _items.Add(copiedVideoBox.Name, copiedVideoBox);

                    if (copiedVideoBox.AccountResource != null && copiedVideoBox.Visible)
                    {
                        MeetingResult meetingResult = _meetingSdkAgent.AddDisplayWindow(copiedVideoBox.AccountResource.AccountModel.AccountId, copiedVideoBox.AccountResource.ResourceId, copiedVideoBox.Handle, 0, 0);
                        System.Console.WriteLine($"ShowExtendedView/AddDisplayWindow：statusCode={meetingResult.StatusCode}, msg={meetingResult.Message}, accountId={copiedVideoBox.AccountResource.AccountModel.AccountId}");
                    }
                }
            }

            LayoutChange(LayoutRendererStore.CurrentLayoutRenderType);
            _eventAggregator.GetEvent<ExtendedViewsShowedEvent>().Publish();
        }

        private void SetDataBindings(VideoBox videoBox)
        {
            var host = ExtendedView as IHost;
            if (host != null)
            {
                var winHost = host.Hosts.FirstOrDefault(h => h.Name == videoBox.Name);
                if (winHost != null)
                {
                    var picBox = winHost.Child as PictureBox;

                    if (picBox != null)
                    {
                        picBox.Tag = videoBox.AccountResource;

                        if (picBox.Controls.Count == 0)
                        {
                            picBox.Paint += (sender, args) =>
                            {
                                PictureBox pictureBox = sender as PictureBox;

                                if (pictureBox != null)
                                {
                                    AccountResource accountResource = pictureBox.Tag as AccountResource;
                                    if (accountResource != null)
                                    {
                                        Label lbl = pictureBox.Controls[0] as Label;

                                        lbl.AutoSize = true;
                                        lbl.Visible = true;

                                        if (accountResource.AccountModel.AccountId == windowManager.HostId && accountResource.MediaType == NetAgent.Models.MediaType.VideoDoc)
                                        {
                                            lbl.Text = accountResource.AccountModel.AccountName + "（课件）";
                                        }
                                        else
                                        {
                                            lbl.Text = accountResource.AccountModel.AccountName;
                                        }

                                        lbl.Visible = !string.IsNullOrEmpty(accountResource.AccountModel.AccountName);
                                        lbl.Location = new System.Drawing.Point((pictureBox.Width - lbl.Width) / 2,
                                            pictureBox.Height - lbl.Height - 30);
                                        System.Console.WriteLine($"pictureBox.Tag：{accountResource.AccountModel.AccountId}, {accountResource.AccountModel.AccountName}");
                                        System.Console.WriteLine($"lbl.Text：{lbl.Text}");
                                        // System.Console.WriteLine($"videoBox：{videoBox.Name}, {videoBox.AccountResource.AccountModel.AccountId}, {videoBox.AccountResource.AccountModel.AccountName}");

                                    }
                                }
                            };

                            Label label = new Label()
                            {
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = System.Drawing.Color.White,
                                ForeColor = System.Drawing.Color.Black,
                                Visible = false
                            };

                            picBox.Controls.Add(label);
                        }


                        videoBox.Handle = picBox.Handle;
                    }

                    Binding wBinding = new Binding("Width") { Source = videoBox };
                    winHost.SetBinding(FrameworkElement.WidthProperty, wBinding);

                    Binding hBinding = new Binding("Height") { Source = videoBox };
                    winHost.SetBinding(FrameworkElement.HeightProperty, hBinding);

                    Binding xBinding = new Binding("PosX") { Source = videoBox };
                    winHost.SetBinding(Canvas.LeftProperty, xBinding);

                    Binding yBinding = new Binding("PosY") { Source = videoBox };
                    winHost.SetBinding(Canvas.TopProperty, yBinding);

                    Binding vBinding = new Binding("Visible")
                    {
                        Source = videoBox,
                        Converter = new BooleanToVisibilityConverter(),
                    };

                    winHost.SetBinding(UIElement.VisibilityProperty, vBinding);
                }
            }
        }

        public void CloseExtendedView()
        {
            lock (_syncRoot)
            {
                if (ExtendedView != null && ExtendedView.IsLoaded)
                {
                    Dispose();

                    _eventAggregator.GetEvent<ExtendedViewsClosedEvent>().Publish();

                    Properties.Clear();
                    ExtendedView.Close();
                    ExtendedView = null;
                }
            }
        }


        public System.Windows.Size Size { get; set; }
        IList<IVideoBox> IScreen.Items => _items.Values.ToList<IVideoBox>();

        public string WindowName => "ExtendedWindow";

        public object Current { get; private set; }

        public bool LayoutChange(LayoutRenderType layoutRenderType)
        {
            return LayoutRendererStore.Create(layoutRenderType).Render(GetVisibleVideoBoxs(), Size, GetSpecialVideoBoxName(layoutRenderType));
        }

        private IList<IVideoBox> GetVisibleVideoBoxs()
        {
            return _items.Values.Where(v => v.Visible).OrderBy(v => v.Sequence).ToList<IVideoBox>();
        }

        private string GetSpecialVideoBoxName(LayoutRenderType layoutRenderType)
        {
            string specialVideoBoxName = Properties.GetValueOrDefault(layoutRenderType.ToString()) as string;
            return specialVideoBoxName;
        }

        public void Dispose()
        {
            _disposeWindowDelegate?.Invoke(this.WindowName);
        }
    }
}
