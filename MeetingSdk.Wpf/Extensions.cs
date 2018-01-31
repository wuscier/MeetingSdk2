using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.Wpf
{
    public static class Extensions
    {
        public static async Task<T> TryRun<T>(this Func<Task<T>> func, string subject)
        {
            T result = default(T);
            try
            {
                result = await func();
            }
            catch (AggregateException e)
            {
                MeetingLogger.Logger.LogError(e.InnerException, subject);
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, subject);
            }
            return result;
        }

        public static async Task TryRun(this Func<Task> func, string subject)
        {
            try
            {
                await func();
            }
            catch (AggregateException e)
            {
                MeetingLogger.Logger.LogError(e.InnerException, subject);
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, subject);
            }
        }

        public static int RemoveWhere<T>(this BindableCollection<T> collection, Func<T, bool> predicate)
            where T : class
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            var num = 0;
            try
            {
                Monitor.Enter(collection);
                var items = collection.Where(predicate).ToList();
                foreach (var item in items)
                {
                    if (collection.Remove(item))
                    {
                        num++;
                    }
                }
            }
            finally
            {
                Monitor.Exit(collection);
            }
            return num;
        }

        public static bool TryConvertVideoBoxType(this MediaType mediaType, out VideoBoxType videoBoxType)
        {
            var result = false;
            videoBoxType = VideoBoxType.None;
            switch (mediaType)
            {
                case MediaType.Camera:
                    videoBoxType = VideoBoxType.Camera;
                    result = true;
                    break;
                case MediaType.Microphone:
                    break;
                case MediaType.VideoDoc:
                    videoBoxType = VideoBoxType.DataCard;
                    result = true;
                    break;
                case MediaType.AudioDoc:
                    break;
                case MediaType.VideoCaptureCard:
                    videoBoxType = VideoBoxType.WinCapture;
                    result = true;
                    break;
                case MediaType.AudioCaptureCard:
                    break;
                case MediaType.StreamMedia:
                    break;
                case MediaType.File:
                    break;
                case MediaType.WhiteBoard:
                    break;
                case MediaType.RemoteControl:
                    break;
                case MediaType.MediaTypeMax:
                    break;
            }
            return result;
        }

        public static VideoBox Copy(this VideoBox videoBox)
        {
            VideoBox copiedVideoBox = new VideoBox(videoBox.Name, videoBox.Host);

            copiedVideoBox.AccountResource = videoBox.AccountResource;
            copiedVideoBox.VideoBoxType = videoBox.VideoBoxType;
            copiedVideoBox.Visible = videoBox.Visible;

            copiedVideoBox.PosX = videoBox.PosX;
            copiedVideoBox.PosY = videoBox.PosY;
            copiedVideoBox.Height = videoBox.Height;
            copiedVideoBox.Width = videoBox.Width;

            copiedVideoBox.Sequence = videoBox.Sequence;

            return copiedVideoBox;
        }
    }
}
