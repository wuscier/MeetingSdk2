namespace MeetingSdk.Wpf
{
    public interface ILayoutWindow
    {
        string WindowName { get; }

        bool LayoutChange(LayoutRenderType layoutRenderType);
    }
}