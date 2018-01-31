using Caliburn.Micro;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.Wpf
{
    public class StreamResource<TParameter> : PropertyChangedBase
        where TParameter : class, IStreamParameter
    {
        public int ResourceId { get; set; }

        public MediaType MediaType { get; set; }

        public TParameter StreamParameter { get; set; }

        public int SyncId { get; set; }

        private bool _isUsed;

        public bool IsUsed
        {
            get => _isUsed;
            set
            {
                _isUsed = value;
                this.NotifyOfPropertyChange(() => this.IsUsed);
            }
        }
    }
}
