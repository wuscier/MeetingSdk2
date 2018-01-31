using Caliburn.Micro;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public class DefaultLayoutRenderrer : ILayoutRenderer
    {
        //public bool CanRender(IVideoBoxManager videoBoxManager, out string cannotMsg)
        //{
        //    cannotMsg = string.Empty;
        //    return true;
        //}

        //public void CalculateViewBoxPositions(IVideoBoxManager videoBoxManager, double width, double height, string key)
        //{
        //    var w = width / 3;
        //    var h = height / 2;

        //    int rows = 0, cols = 0;

        //    List<VideoStreamModel> videoStreamModels = new List<VideoStreamModel>();
        //    foreach (var videoBox in videoBoxManager.Where(v => v.Visible))
        //    {
        //        VideoStreamModel videoStreamModel = new VideoStreamModel()
        //        {
        //            AccountId = videoBox.AccountResource.AccountModel.AccountId.ToString(),
        //            StreamId = videoBox.AccountResource.ResourceId,
        //            Width = (int)Math.Round(w),
        //            Height = (int)Math.Round(h),
        //            X = (int)Math.Round(cols * w),
        //            Y = (int)Math.Round(rows * h)
        //        };

        //        videoStreamModels.Add(videoStreamModel);

        //        cols++;
        //        if (cols > 2)
        //        {
        //            cols = 0;
        //            rows++;
        //        }

        //        if (rows > 1)
        //        {
        //            break;
        //        }
        //    }

        //    videoBoxManager.SetProperty(key, videoStreamModels);
        //}

        public bool Render(IList<IVideoBox> videoBoxs, Size canvasSize, string specialVideoBoxName)
        {
            // 计算2行3列
            var w = canvasSize.Width / 3;
            var h = canvasSize.Height / 2;

            int rows = 0;
            int cols = 0;
            foreach (var videoBox in videoBoxs)
            {
                videoBox.Width = w;
                videoBox.Height = h;
                videoBox.PosX = cols * w;
                videoBox.PosY = rows * h;

                cols++;
                if (cols > 2)
                {
                    cols = 0;
                    rows++;
                }

                if (rows > 1)
                {
                    break;
                }
            }
            return true;
        }

        //bool TryGetContainer(IVideoBoxManager videoBoxManager, out Canvas canvas)
        //{
        //    canvas = null;
        //    if (videoBoxManager == null)
        //        return false;

        //    if (!videoBoxManager.Any())
        //        return false;

        //    canvas = videoBoxManager.First().Host?.Parent as Canvas;
        //    return canvas != null;
        //}
    }

    public class LayoutRendererStore
    {
        public ILayoutRenderer Create()
        {
            return Create(CurrentLayoutRenderType);
        }

        public ILayoutRenderer Create(LayoutRenderType type)
        {
            ILayoutRenderer layoutRenderrer = IoC.Get<ILayoutRenderer>(type.ToString());
            return layoutRenderrer;
        }

        private LayoutRenderType _currentLayoutRenderType = LayoutRenderType.AverageLayout;
        public LayoutRenderType CurrentLayoutRenderType
        {
            get
            {
                return _currentLayoutRenderType;
            }
            set
            {
                _currentLayoutRenderType = value;
            }
        }
    }

    public enum LayoutRenderType
    {
        [Description("自动布局")]
        AutoLayout,
        [Description("平均排列")]
        AverageLayout,
        [Description("特写模式")]
        CloseupLayout,
        [Description("一大多小")]
        BigSmallsLayout
    }
}
