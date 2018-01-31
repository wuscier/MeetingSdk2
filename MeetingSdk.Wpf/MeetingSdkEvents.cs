using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;
using Prism.Events;

namespace MeetingSdk.Wpf
{
    public class UserUnpublishMicAudioEvent : PubSubEvent<UserUnpublishModel>
    {

    }
    public class UserUnpublishDataCardVideoEvent : PubSubEvent<UserUnpublishModel>
    {

    }
    public class UserUnpublishCameraVideoEvent : PubSubEvent<UserUnpublishModel>
    {

    }
    public class UserPublishMicAudioEvent : PubSubEvent<UserPublishModel>
    {

    }
    public class UserPublishDataVideoEvent : PubSubEvent<UserPublishModel>
    {

    }
    public class UserPublishCameraVideoEvent : PubSubEvent<UserPublishModel>
    {

    }

    public class StartSpeakEvent : PubSubEvent<SpeakModel>
    {

    }

    public class StopSpeakEvent : PubSubEvent<SpeakModel>
    {

    }

    public class UserStartSpeakEvent : PubSubEvent<UserSpeakModel>
    {

    }

    public class UserStopSpeakEvent : PubSubEvent<UserSpeakModel>
    {

    }

    public class UserJoinEvent : PubSubEvent<AccountModel>
    {

    }

    public class UserLeaveEvent : PubSubEvent<AccountModel>
    {

    }

    public class UserLoginEvent : PubSubEvent<UserInfo>
    {

    }

    public class UserLogoutEvent : PubSubEvent<UserInfo>
    {

    }

    public class NetStatusNoticeEvent : PubSubEvent<int>
    {

    }

    public class NetCheckedEvent : PubSubEvent<NetType>
    {

    }
    public class UserRaiseHandRequestEvent : PubSubEvent<AccountModel>
    {

    }

    public class TransparentMsgReceivedEvent : PubSubEvent<TransparentMsg>
    {

    }

    public class UiTransparentMsgReceivedEvent : PubSubEvent<UiTransparentMsg>
    {

    }

    public class HostOperationReceivedEvent : PubSubEvent<HostOprateType>
    {

    }

    public class HostKickoutUserEvent : PubSubEvent<KickoutUserModel>
    {

    }



    public class DeviceLostNoticeEvent : PubSubEvent<ResourceModel>
    {

    }
    public class DeviceStatusChangedEvent : PubSubEvent<DeviceStatusModel>
    {

    }
    public class MeetingManageExceptionEvent : PubSubEvent<ExceptionModel>
    {

    }
    public class SdkCallbackEvent : PubSubEvent<SdkCallbackModel>
    {

    }
    public class LockStatusChangedEvent : PubSubEvent<MeetingResult>
    {

    }

    public class ExtendedViewsClosedEvent : PubSubEvent
    {

    }

    public class ExtendedViewsShowedEvent : PubSubEvent
    {

    }

    public class ModeDisplayerTypeChangedEvent : PubSubEvent<ModeDisplayerType>
    {

    }

    public class RefreshCanvasEvent:PubSubEvent
    {

    }

    public class LayoutChangedEvent : PubSubEvent<LayoutRenderType>
    {

    }

    public class MeetingInvitationEvent : PubSubEvent<MeetingInvitationModel>
    {

    }

    /// <summary>
    /// 此事件用于 同步 主屏和其他屏的视频资源（画面数量）。
    /// </summary>
    public class VideoBoxAddedEvent : PubSubEvent<VideoBox>
    {

    }

    public class VideoBoxRemovedEvent : PubSubEvent<VideoBox>
    {

    }

    public class ContactRecommendEvent : PubSubEvent<RecommendContactModel>
    {

    }
    public class ForcedOfflineEvent : PubSubEvent<ForcedOfflineModel>
    {

    }

}
