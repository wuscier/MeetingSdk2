using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public class DefaultModeDisplayer : IModeDisplayer
    {
        public bool Display(IList<IVideoBox> videoBoxs, Size canvasSize)
        {
            return true;
        }
    }


    public class ModeDisplayerStore
    {
        public IModeDisplayer Create()
        {
            return Create(CurrentModeDisplayerType);
        }

        public IModeDisplayer Create(ModeDisplayerType type)
        {
            IModeDisplayer modeDisplayer = IoC.Get<IModeDisplayer>(type.ToString());
            return modeDisplayer;
        }

        private ModeDisplayerType _currentModeDisplayType;
        public ModeDisplayerType CurrentModeDisplayerType
        {
            get { return _currentModeDisplayType; }
            set
            {
                _currentModeDisplayType = value;
            }
        }
    }

    public enum ModeDisplayerType
    {
        [Description("主讲模式")]
        SpeakerMode,
        [Description("课件模式")]
        ShareMode,
        [Description("互动模式")]
        InteractionMode
    }
}
