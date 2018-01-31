using System.Windows;
using System.Windows.Forms.Integration;
using Caliburn.Micro;
using DependencyPropertyHelper = Caliburn.Micro.DependencyPropertyHelper;

namespace MeetingSdk.Wpf
{
    public static class MeetingWindow
    {
        public static readonly DependencyProperty VideoBoxProperty =
            DependencyPropertyHelper.RegisterAttached(
                "VideoBox",
                typeof(object),
                typeof(MeetingWindow),
                null);

        public static readonly DependencyProperty AttachProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(MeetingWindow),
                null,
                OnAttachChanged
            );

        public static readonly DependencyProperty VideoBoxTypeProperty =
            DependencyPropertyHelper.RegisterAttached(
                "VideoBoxType",
                typeof(VideoBoxType),
                typeof(MeetingWindow),
                VideoBoxType.None,
                OnVideoBoxTypeChanged);

        public static void SetVideoBox(DependencyObject d, object value)
        {
            d.SetValue(VideoBoxProperty, value);
        }

        public static object GetVideoBox(DependencyObject d)
        {
            return d.GetValue(VideoBoxProperty);
        }
        
        public static void SetAttach(DependencyObject d, string attach)
        {
            d.SetValue(AttachProperty, attach);
        }
        
        public static string GetAttach(DependencyObject d)
        {
            return d.GetValue(AttachProperty) as string;
        }

        public static void SetVideoBoxType(DependencyObject d, VideoBoxType boxType)
        {
            d.SetValue(VideoBoxTypeProperty, boxType);
        }

        public static VideoBoxType GetVideoBoxType(DependencyObject d)
        {
            object o = d.GetValue(VideoBoxTypeProperty);
            if (o == null)
                return VideoBoxType.None;

            return (VideoBoxType) o;
        }

        static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WindowsFormsHost host = d as WindowsFormsHost;
            var name = e.NewValue as string;
            if (string.IsNullOrEmpty(name) || e.NewValue == e.OldValue || host == null)
            {
                return;
            }

            host.ChildChanged += Host_ChildChanged;
            var windowManager = IoC.Get<IMeetingWindowManager>();
            var videoBox = new VideoBox(name, host);
            SetVideoBox(d, videoBox);
            windowManager.VideoBoxManager?.SetVideoBox(videoBox);
        }

        private static void OnVideoBoxTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || e.NewValue == e.OldValue)
                return;

            var videoBoxType = (VideoBoxType) e.NewValue;
            var videoBox = GetVideoBox(d) as VideoBox;
            if (videoBox != null)
            {
                videoBox.VideoBoxType = videoBoxType;
            }
        }

        private static void Host_ChildChanged(object sender, ChildChangedEventArgs e)
        {
            WindowsFormsHost host = sender as WindowsFormsHost;
            var videoBox = GetVideoBox(host) as VideoBox;
            if (videoBox != null && host?.Child != null)
            {
                videoBox.SetHandle();
                videoBox.SetBinding();
            }
        }
    }
}
