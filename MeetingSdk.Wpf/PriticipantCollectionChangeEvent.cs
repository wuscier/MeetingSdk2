using System.Collections.Generic;
using Prism.Events;

namespace MeetingSdk.Wpf
{
    public class ParticipantCollectionChangeEvent : PubSubEvent<IEnumerable<Participant>>
    {
    }
}
