using Caliburn.Micro;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.Wpf
{
    public class Participant : PropertyChangedBase
    {
        public Participant(AccountModel account)
        {
            this.Account = account;
            Resources = new BindableCollection<StreamResource<IStreamParameter>>();
        }

        public AccountModel Account { get; }

        public BindableCollection<StreamResource<IStreamParameter>> Resources { get; }

        private bool _isSpeaking;
        public bool IsSpeaking
        {
            get => _isSpeaking;
            set
            {
                _isSpeaking = value;
                this.NotifyOfPropertyChange(() => _isSpeaking);
                
            }
        }
    }
}
