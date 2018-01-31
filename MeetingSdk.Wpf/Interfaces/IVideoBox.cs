namespace MeetingSdk.Wpf
{
    public interface IVideoBox
    {
        string Name { get; }
        double PosX { get; set; }
        double PosY { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        AccountResource AccountResource { get; }
    }
}
