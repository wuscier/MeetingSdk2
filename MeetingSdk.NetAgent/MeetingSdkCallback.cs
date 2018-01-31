using MeetingSdk.NetAgent.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MeetingSdk.NetAgent
{
    /// <summary>
    /// 处理C++层回调事件，转换参数到.NET友好封闭，提供默认分发事件处理程序
    /// </summary>
    public class MeetingSdkCallback
    {
        public Action<IntPtr, int, IntPtr> OnStart;
        public Action<IntPtr, int, IntPtr> OnLogin;
        public Action<IntPtr, int, IntPtr> OnBindToken;

        public Action<IntPtr, int, IntPtr> OnCheckMeetingExist;
        public Action<IntPtr, int, IntPtr> OnGetMeetingList;
        public Action<IntPtr, int, IntPtr> OnGetMeetingInfo;

        public Action<IntPtr, int, IntPtr> OnResetMeetingPassword;
        public Action<IntPtr, int, IntPtr> OnGetMeetingPassword;
        public Action<IntPtr, int, IntPtr> OnCheckMeetingHasPassword;

        public Action<IntPtr, int, IntPtr> OnCheckMeetingPasswordValid;

        public Action<IntPtr, int, IntPtr> OnCreateMeeting;
        public Action<IntPtr, int, IntPtr> OnJoinMeeting;

        public Action<IntPtr, int, IntPtr> OnGetUserList;
        public Action<IntPtr, int, IntPtr> OnUserJoinEvent;
        public Action<IntPtr, int, IntPtr> OnUserLeaveEvent;

        public Action<IntPtr, int, IntPtr> OnStartSpeakEvent;
        public Action<IntPtr, int, IntPtr> OnStopSpeakEvent;
        public Action<IntPtr, int, IntPtr> OnUserStartSpeakEvent;
        public Action<IntPtr, int, IntPtr> OnUserStopSpeakEvent;
        public Action<IntPtr, int, IntPtr> OnAskForSpeak;
        public Action<IntPtr, int, IntPtr> OnAskForStopSpeak;

        public Action<IntPtr, int, IntPtr> OnPublishCameraVideo;
        public Action<IntPtr, int, IntPtr> OnPublishWinCaptureVideo;
        public Action<IntPtr, int, IntPtr> OnPublishDataCardVideo;
        public Action<IntPtr, int, IntPtr> OnPublishMicAudio;

        public Action<IntPtr, int, IntPtr> OnUnpublishCameraVideo;
        public Action<IntPtr, int, IntPtr> OnUnpublishWinCaptureVideo;
        public Action<IntPtr, int, IntPtr> OnUnpublishDataCardVideo;
        public Action<IntPtr, int, IntPtr> OnUnpublishMicAudio;

        public Action<IntPtr, int, IntPtr> OnUserPublishCameraVideoEvent;
        public Action<IntPtr, int, IntPtr> OnUserPublishDataVideoEvent;
        public Action<IntPtr, int, IntPtr> OnUserPublishMicAudioEvent;

        public Action<IntPtr, int, IntPtr> OnUserUnpublishCameraVideoEvent;
        public Action<IntPtr, int, IntPtr> OnUserUnpublishDataCardVideoEvent;
        public Action<IntPtr, int, IntPtr> OnUserUnpublishMicAudioEvent;

        public Action<IntPtr, int, IntPtr> OnYuvData;

        public Action<IntPtr, int, IntPtr> OnHostChangeMeetingMode;
        public Action<IntPtr, int, IntPtr> OnHostChangeMeetingModeEvent;

        public Action<IntPtr, int, IntPtr> OnHostKickoutUser;
        public Action<IntPtr, int, IntPtr> OnHostKickoutUserEvent;

        public Action<IntPtr, int, IntPtr> OnRaiseHandRequest;
        public Action<IntPtr, int, IntPtr> OnRaiseHandRequestEvent;

        public Action<IntPtr, int, IntPtr> OnAskForMeetingLock;
        public Action<IntPtr, int, IntPtr> OnHostOrderOneSpeak;
        public Action<IntPtr, int, IntPtr> OnHostOrderOneStopSpeak;
        public Action<IntPtr, int, IntPtr> OnLockStatusChangedEvent;
        public Action<IntPtr, int, IntPtr> OnMeetingManageExceptionEvent;
        public Action<IntPtr, int, IntPtr> OnDeviceStatusEvent;

        public Action<IntPtr, int, IntPtr> OnNetDiagnosticCheck;
        public Action<IntPtr, int, IntPtr> OnPlayVideoTest;
        public Action<IntPtr, int, IntPtr> OnPlaySoundTest;
        public Action<IntPtr, int, IntPtr> OnSendUiMsg;
        public Action<IntPtr, int, IntPtr> OnTransparentMsgEvent;
        public Action<IntPtr, int, IntPtr> OnUiMsgReceivedEvent;
        public Action<IntPtr, int, IntPtr> OnMicSendResponse;
        public Action<IntPtr, int, IntPtr> OnNetworkStatusLevelNoticeEvent;
        public Action<IntPtr, int, IntPtr> OnDeviceLostNoticeEvent;
        public Action<IntPtr, int, IntPtr> OnMeetingSdkCallback;
        public Action<IntPtr, int, IntPtr> OnSendAudioSpeakerStatus;
        public Action<IntPtr, int, IntPtr> OnGetMeetingInvitationSMS;
        public Action<IntPtr, int, IntPtr> OnHostOrderOneDoOpration;
        public Action<IntPtr, int, IntPtr> OnHostOrderDoOpratonEvent;
        public Action<IntPtr, int, IntPtr> OnOtherChangeAudioSpeakerStatusEvent;
        public Action<IntPtr, int, IntPtr> OnModifyNickName;
        public Action<IntPtr, int, IntPtr> OnModifyMeetingInviters;


        public Action<IntPtr, int, IntPtr> OnStartHost;
        public Action<IntPtr, int, IntPtr> OnConnectMeetingVDN;
        public Action<IntPtr, int, IntPtr> OnConnectMeetingVDNAfterMeetingInstStarted;
        public Action<IntPtr, int, IntPtr> OnSetAccountInfo;
        public Action<IntPtr, int, IntPtr> OnMeetingInvite;
        public Action<IntPtr, int, IntPtr> OnMeetingInviteEvent;
        public Action<IntPtr, int, IntPtr> OnContactRecommendEvent;
        public Action<IntPtr, int, IntPtr> OnForcedOfflineEvent;


        private MeetingSdkCallback()
        {   
            OnStart= Start;
            OnLogin =                                               Login;
            OnBindToken=                                            BindToken;
            OnCheckMeetingExist =                                   CheckMeetingExist;
            OnGetMeetingList =                                      GetMeetingList;
            OnGetMeetingInfo = GetMeetingInfo;
            OnResetMeetingPassword=                                 ResetMeetingPassword;
            OnGetMeetingPassword=                                   GetMeetingPassword;
            OnCheckMeetingHasPassword=                              CheckMeetingHasPassword;

            OnCheckMeetingPasswordValid=                            CheckMeetingPasswordValid;

            OnCreateMeeting=                                        CreateMeeting;
            OnJoinMeeting=                                          JoinMeeting;

            OnGetUserList=                                          GetUserList;
            OnUserJoinEvent=                                        UserJoinEvent;
            OnUserLeaveEvent=                                       UserLeaveEvent;

            OnStartSpeakEvent=                                      StartSpeakEvent;
            OnStopSpeakEvent =                                      StopSpeakEvent;
            OnUserStartSpeakEvent =                                 UserStartSpeakEvent;
            OnUserStopSpeakEvent =                                  UserStopSpeakEvent;
            OnAskForSpeak =                                         AskForSpeak;
            OnAskForStopSpeak =                                     AskForStopSpeak;

            OnPublishCameraVideo=                                   PublishCameraVideo;
            OnPublishWinCaptureVideo=                               PublishWinCaptureVideo;
            OnPublishDataCardVideo=                                 PublishDataCardVideo;
            OnPublishMicAudio=                                      PublishMicAudio;

            OnUnpublishCameraVideo=                                 UnpublishCameraVideo;
            OnUnpublishWinCaptureVideo=                             UnpublishWinCaptureVideo;
            OnUnpublishDataCardVideo=                               UnpublishDataCardVideo;
            OnUnpublishMicAudio=                                    UnpublishMicAudio;

            OnUserPublishCameraVideoEvent=                          UserPublishCameraVideoEvent;
            OnUserPublishDataVideoEvent=                            UserPublishDataVideoEvent;
            OnUserPublishMicAudioEvent=                             UserPublishMicAudioEvent;
            OnUserUnpublishCameraVideoEvent=                        UserUnpublishCameraVideoEvent;
            OnUserUnpublishDataCardVideoEvent=                      UserUnpublishDataCardVideoEvent;
            OnUserUnpublishMicAudioEvent=                           UserUnpublishMicAudioEvent;

            OnYuvData=                                              YuvData;

            OnHostChangeMeetingMode=                                HostChangeMeetingMode;
            OnHostChangeMeetingModeEvent=                           HostChangeMeetingModeEvent;

            OnHostKickoutUser=                                      HostKickoutUser;
            OnHostKickoutUserEvent=                                 HostKickoutUserEvent;

            OnRaiseHandRequest=                                     RaiseHandRequest;
            OnRaiseHandRequestEvent=                                RaiseHandRequestEvent;

            OnAskForMeetingLock=                                    AskForMeetingLock;
            OnHostOrderOneSpeak=                                    HostOrderOneSpeak;
            OnHostOrderOneStopSpeak=                                HostOrderOneStopSpeak;
            OnLockStatusChangedEvent=                               LockStatusChangedEvent;
            OnMeetingManageExceptionEvent=                          MeetingManageExceptionEvent;
            OnDeviceStatusEvent=                                    DeviceStatusEvent;

            OnNetDiagnosticCheck=                                   NetDiagnosticCheck;
            OnPlayVideoTest=                                        PlayVideoTest;
            OnPlaySoundTest=                                        PlaySoundTest;
            OnSendUiMsg =                                           SendUiMsg;
            OnTransparentMsgEvent=                                  TransparentMsgEvent;
            OnUiMsgReceivedEvent =                                  UiMsgReceivedEvent;
            OnMicSendResponse =                                      MicSendResponse;
            OnNetworkStatusLevelNoticeEvent=                        NetworkStatusLevelNoticeEvent;
            OnDeviceLostNoticeEvent=                                DeviceLostNoticeEvent;
            OnMeetingSdkCallback=                                   SdkCallback;
            OnSendAudioSpeakerStatus = SendAudioSpeakerStatus;
            OnGetMeetingInvitationSMS = GetMeetingInvitationSMS;
            OnHostOrderOneDoOpration = HostOrderOneDoOpration;
            OnHostOrderDoOpratonEvent = HostOrderDoOpratonEvent;
            OnOtherChangeAudioSpeakerStatusEvent = OtherChangeAudioSpeakerStatusEvent;
            OnModifyNickName = ModifyNickName;
            OnModifyMeetingInviters = ModifyMeetingInviters;



            OnStartHost = StartHost;
            OnConnectMeetingVDN = ConnectMeetingVDN;
            OnConnectMeetingVDNAfterMeetingInstStarted = ConnectMeetingVDNAfterMeetingInstStarted;
            OnSetAccountInfo = SetAccountInfo;
            OnMeetingInvite= MeetingInvite;
            OnMeetingInviteEvent= MeetingInviteEvent;
            OnContactRecommendEvent= ContactRecommendEvent;
            OnForcedOfflineEvent = ForcedOfflineEvent;
        }

        private static readonly MeetingSdkCallback Instance = new MeetingSdkCallback();
        
        internal static void PFuncCallBack(int cmdId, IntPtr pData, int dataLen, IntPtr ctx)
        {
            CallbackType callbackType = (CallbackType)cmdId;

            switch (callbackType)
            {
                case CallbackType.start:
                    Instance.OnStart(pData, dataLen, ctx);
                    break;
                case CallbackType.login:
                    Instance.OnLogin(pData, dataLen, ctx);
                    break;
                case CallbackType.bind_token:
                    Instance.OnBindToken(pData, dataLen, ctx);
                    break;
                case CallbackType.check_meeting_exist:
                    Instance.OnCheckMeetingExist(pData, dataLen, ctx);
                    break;
                case CallbackType.get_meeting_list:
                    Instance.OnGetMeetingList(pData, dataLen, ctx);
                    break;
                case CallbackType.get_meeting_info:
                    Instance.OnGetMeetingInfo(pData, dataLen, ctx);
                    break;
                case CallbackType.reset_meeting_password:
                    Instance.OnResetMeetingPassword(pData, dataLen, ctx);
                    break;
                case CallbackType.get_meeting_password:
                    Instance.OnGetMeetingPassword(pData, dataLen, ctx);
                    break;
                case CallbackType.check_meeting_has_password:
                    Instance.OnCheckMeetingHasPassword(pData, dataLen, ctx);
                    break;
                case CallbackType.check_meeting_password_valid:
                    Instance.OnCheckMeetingPasswordValid(pData, dataLen, ctx);
                    break;
                case CallbackType.create_meeting:
                    Instance.OnCreateMeeting(pData, dataLen, ctx);
                    break;
                case CallbackType.join_meeting:
                    Instance.OnJoinMeeting(pData, dataLen, ctx);
                    break;
                case CallbackType.get_user_list:
                    Instance.OnGetUserList(pData, dataLen, ctx);
                    break;
                case CallbackType.user_join_event:
                    Instance.OnUserJoinEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.user_leave_event:
                    Instance.OnUserLeaveEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.ask_for_speak:
                    Instance.OnAskForSpeak(pData, dataLen, ctx);
                    break;
                case CallbackType.start_speak_event:
                    Instance.OnStartSpeakEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.user_start_speak_event:
                    Instance.OnUserStartSpeakEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.ask_for_stop_speak:
                    Instance.OnAskForStopSpeak(pData, dataLen, ctx);
                    break;
                case CallbackType.stop_speak_event:
                    Instance.OnStopSpeakEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.user_stop_speak_event:
                    Instance.OnUserStopSpeakEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.publish_camera_video:
                    Instance.OnPublishCameraVideo(pData, dataLen, ctx);
                    break;
                case CallbackType.user_publish_camera_video_event:
                    Instance.OnUserPublishCameraVideoEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.publish_win_capture_video:
                    Instance.OnPublishWinCaptureVideo(pData, dataLen, ctx);
                    break;
                case CallbackType.publish_data_card_video:
                    Instance.OnPublishDataCardVideo(pData, dataLen, ctx);
                    break;
                case CallbackType.user_publish_data_video_event:
                    Instance.OnUserPublishDataVideoEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.publish_mic_audio:
                    Instance.OnPublishMicAudio(pData, dataLen, ctx);
                    break;
                case CallbackType.user_publish_mic_audio_event:
                    Instance.OnUserPublishMicAudioEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.unpublish_camera_video:
                    Instance.OnUnpublishCameraVideo(pData, dataLen, ctx);
                    break;
                case CallbackType.unpublish_win_capture_video:
                    Instance.OnUnpublishWinCaptureVideo(pData, dataLen, ctx);
                    break;
                case CallbackType.unpublish_data_card_video:
                    Instance.OnUnpublishDataCardVideo(pData, dataLen, ctx);
                    break;
                case CallbackType.unpublish_mic_audio:
                    Instance.OnUnpublishMicAudio(pData, dataLen, ctx);
                    break;
                case CallbackType.user_unpublish_camera_video_event:
                    Instance.OnUserUnpublishCameraVideoEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.user_unpublish_data_card_video_event:
                    Instance.OnUserUnpublishDataCardVideoEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.user_unpublish_mic_audio_event:
                    Instance.OnUserUnpublishMicAudioEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.yuv_data:
                    Instance.OnYuvData(pData, dataLen, ctx);
                    break;
                case CallbackType.host_change_meeting_mode:
                    Instance.OnHostChangeMeetingMode(pData, dataLen, ctx);
                    break;
                case CallbackType.host_change_meeting_mode_event:
                    Instance.OnHostChangeMeetingModeEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.host_kickout_user:
                    Instance.OnHostKickoutUser(pData, dataLen, ctx);
                    break;
                case CallbackType.host_kickout_user_event:
                    Instance.OnHostKickoutUserEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.raise_hand_request:
                    Instance.OnRaiseHandRequest(pData, dataLen, ctx);
                    break;
                case CallbackType.raise_hand_request_event:
                    Instance.OnRaiseHandRequestEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.ask_for_meeting_lock:
                    Instance.OnAskForMeetingLock(pData, dataLen, ctx);
                    break;
                case CallbackType.host_order_one_speak:
                    Instance.OnHostOrderOneSpeak(pData, dataLen, ctx);
                    break;
                case CallbackType.host_order_one_stop_speak:
                    Instance.OnHostOrderOneStopSpeak(pData, dataLen, ctx);
                    break;
                case CallbackType.lock_status_changed_event:
                    Instance.OnLockStatusChangedEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.meeting_manage_exception_event:
                    Instance.OnMeetingManageExceptionEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.device_status_event:
                    Instance.OnDeviceStatusEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.net_diagnostic_check:
                    Instance.OnNetDiagnosticCheck(pData, dataLen, ctx);
                    break;
                case CallbackType.play_video_test:
                    Instance.OnPlayVideoTest(pData, dataLen, ctx);
                    break;
                case CallbackType.play_sound_test:
                    Instance.OnPlaySoundTest(pData, dataLen, ctx);
                    break;
                case CallbackType.transparent_msg_event:
                    Instance.OnTransparentMsgEvent(pData, dataLen, ctx);
                    break;

                case CallbackType.receive_ui_msg_event:
                    Instance.OnUiMsgReceivedEvent(pData, dataLen, ctx);
                    break;

                case CallbackType.send_ui_msg:
                    Instance.OnSendUiMsg(pData, dataLen, ctx);
                    break;

                case CallbackType.mic_send_response:
                    Instance.OnMicSendResponse(pData, dataLen, ctx);
                    break;
                case CallbackType.network_status_level_notice_event:
                    Instance.OnNetworkStatusLevelNoticeEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.device_lost_notice_event:
                    Instance.OnDeviceLostNoticeEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.meeting_sdk_callback:
                    Instance.OnMeetingSdkCallback(pData, dataLen, ctx);
                    break;
                case CallbackType.send_audio_speaker_status:
                    Instance.OnSendAudioSpeakerStatus(pData, dataLen, ctx);
                    break;
                case CallbackType.get_meeting_invitation_sms:
                    Instance.OnGetMeetingInvitationSMS(pData, dataLen, ctx);
                    break;
                case CallbackType.other_change_audio_speaker_status_event:
                    Instance.OnOtherChangeAudioSpeakerStatusEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.host_order_do_opraton_event:
                    Instance.OnHostOrderDoOpratonEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.modify_nick_name:
                    Instance.OnModifyNickName(pData, dataLen, ctx);
                    break;
                case CallbackType.modify_meeting_inviters:
                    Instance.OnModifyMeetingInviters(pData, dataLen, ctx);
                    break;

                case CallbackType.start_host:
                    Instance.OnStartHost(pData, dataLen, ctx);
                    break;
                case CallbackType.connect_meeting_vdn_after_instance_started:
                    Instance.OnConnectMeetingVDNAfterMeetingInstStarted(pData, dataLen, ctx);
                    break;
                case CallbackType.connect_meeting_vdn:
                    Instance.OnConnectMeetingVDN(pData, dataLen, ctx);
                    break;
                case CallbackType.set_account_info:
                    Instance.OnSetAccountInfo(pData, dataLen, ctx);
                    break;
                case CallbackType.meeting_invite:
                    Instance.OnMeetingInvite(pData, dataLen, ctx);
                    break;
                case CallbackType.meeting_invite_event:
                    Instance.OnMeetingInviteEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.contact_recommend_event:
                    Instance.OnContactRecommendEvent(pData, dataLen, ctx);
                    break;
                case CallbackType.force_offline_event:
                    Instance.OnForcedOfflineEvent(pData, dataLen, ctx);
                    break;


                default:
                    break;
            }
        }

        internal static void PFunVideoPriview(string pFrame, int width, int height, int frameLen)
        {
            
        }

        private void Start(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.start.ToString(), "", result);
        }

        private void Login(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<LoginModel>();
            try
            {
                LoginResult loginResult = Marshal.PtrToStructure<LoginResult>(pData);
                result.Result = new LoginModel()
                {
                    Account = new AccountModel(loginResult.LoginInfo.AccountId, loginResult.LoginInfo.AccountName),
                    Token = loginResult.LoginInfo.Token,
                    TokenStartTime = loginResult.LoginInfo.TokenStartTime,
                    TokenEndTime = loginResult.LoginInfo.TokenEndTime
                };
                result.StatusCode = loginResult.Result.Status;
                result.Message = loginResult.Result.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.login.ToString(), "", result);
        }

        private void BindToken(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult result = new MeetingResult();

            try
            {
                AsyncCallbackResult asyncCallbackResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                result.Message = asyncCallbackResult.Message;
                result.StatusCode = asyncCallbackResult.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.bind_token.ToString(), "", result);
        }

        private void CheckMeetingExist(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            var uniqueId = "";
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
                
                var ctxResult = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = ctxResult.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.check_meeting_exist.ToString(), uniqueId, result);
        }
        
        private void GetMeetingList(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<IList<MeetingModel>> result = new MeetingResult<IList<MeetingModel>>();
            var uniqueId = "";
            try
            {
                var cbResult = Marshal.PtrToStructure<GetMeetingListResult>(pData);
                result.StatusCode = cbResult.Result.Status;
                result.Message = cbResult.Result.Message;

                result.Result = new List<MeetingModel>();

                int meetingInfoSize = Marshal.SizeOf<MeetingInfo>();

                for (int i = 0; i < cbResult.MeetingCount; i++)
                {
                    IntPtr pointer = (IntPtr) (cbResult.MeetingList.ToInt64() + i * meetingInfoSize);
                    MeetingInfo meetingInfo = Marshal.PtrToStructure<MeetingInfo>(pointer);

                    MeetingModel meetingModel = new MeetingModel()
                    {
                        Account = new AccountModel(meetingInfo.CreatorAccountId, meetingInfo.CreatorName),
                        HavePwd = meetingInfo.HavePwd == 1,
                        HostId = meetingInfo.HostId,
                        MeetingId = meetingInfo.MeetingId,
                        MeetingStatus = (MeetingStatus) meetingInfo.MeetingStatus,
                        MeetingType = (MeetingType) meetingInfo.MeetingType,
                        StartTime = meetingInfo.StartTime,
                        Topic = meetingInfo.Topic,
                    };

                    DateTime dt = new DateTime(1970, 1, 1).AddSeconds(double.Parse(meetingModel.StartTime));
                    meetingModel.StartTime = TimeZone.CurrentTimeZone.ToLocalTime(dt).ToString("yyyy-MM-dd HH:mm:ss");

                    result.Result.Add(meetingModel);
                }

                var ctxResult = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = ctxResult.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.get_meeting_list.ToString(), uniqueId, result);
        }

        private void GetMeetingInfo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<MeetingModel>();
            var uniqueId = "";
            try
            {
                var cbResult = Marshal.PtrToStructure<CreateMeetingResult>(pData);
                result.StatusCode = cbResult.Result.Status;
                result.Message = cbResult.Result.Message;
                
                result.Result = new MeetingModel()
                {
                    Account = new AccountModel(cbResult.MeetingInfo.CreatorAccountId, cbResult.MeetingInfo.CreatorName),
                    HostId = cbResult.MeetingInfo.HostId,
                    MeetingId = cbResult.MeetingInfo.MeetingId,
                    MeetingStatus = (MeetingStatus) cbResult.MeetingInfo.MeetingStatus,
                    MeetingType = (MeetingType) cbResult.MeetingInfo.MeetingType,
                    Topic = cbResult.MeetingInfo.Topic
                };

                DateTime dt = new DateTime(1970, 1, 1).AddSeconds(double.Parse(cbResult.MeetingInfo.StartTime));
                result.Result.StartTime = TimeZone.CurrentTimeZone.ToLocalTime(dt).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbResult.MeetingInfo.HavePwd >= 0)
                {
                    result.Result.HavePwd = cbResult.MeetingInfo.HavePwd == 1;
                }

                var ctxResult = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = ctxResult.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.get_meeting_info.ToString(), uniqueId, result);
        }

        private void ResetMeetingPassword(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult result = new MeetingResult();

            AsyncCallbackResult asyncResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

            try
            {
                result.Message = asyncResult.Message;
                result.StatusCode = asyncResult.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.reset_meeting_password.ToString(), "", result);
        }

        private void GetMeetingPassword(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<MeetingPasswordModel> result = new MeetingResult<MeetingPasswordModel>();

            try
            {
                MeetingPasswordResult pwdResult = Marshal.PtrToStructure<MeetingPasswordResult>(pData);

                result.Result = new MeetingPasswordModel()
                {
                    HostId = pwdResult.hostId,
                    Password = pwdResult.password,
                };

                result.Message = pwdResult.m_result.Message;
                result.StatusCode = pwdResult.m_result.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.get_meeting_password.ToString(), "", result);
        }

        private void CheckMeetingHasPassword(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<MeetingHasPwdModel> result = new MeetingResult<MeetingHasPwdModel>();

            try
            {
                MeetingHasPasswordResult pwdResult = Marshal.PtrToStructure<MeetingHasPasswordResult>(pData);

                result.Result = new MeetingHasPwdModel()
                {
                    HostId = pwdResult.hostId,
                    HasPwd = pwdResult.hasPwd == 1,
                };

                result.Message = pwdResult.m_result.Message;
                result.StatusCode = pwdResult.m_result.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.check_meeting_has_password.ToString(), "", result);
        }

        private void CheckMeetingPasswordValid(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.check_meeting_password_valid.ToString(), "", result);
        }

        private void CreateMeeting(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<MeetingModel> result = new MeetingResult<MeetingModel>();

            try
            {
                CreateMeetingResult createMeetingResult = Marshal.PtrToStructure<CreateMeetingResult>(pData);
                result.StatusCode = createMeetingResult.Result.Status;
                result.Message = createMeetingResult.Result.Message;
                result.Result = new MeetingModel()
                {
                    Account = new AccountModel(
                        createMeetingResult.MeetingInfo.CreatorAccountId,
                        createMeetingResult.MeetingInfo.CreatorName
                    ),
                    MeetingId = createMeetingResult.MeetingInfo.MeetingId,
                    MeetingType = (MeetingType) createMeetingResult.MeetingInfo.MeetingType,
                    StartTime = createMeetingResult.MeetingInfo.StartTime,
                };
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.StatusCode = -1000;
            }

            TaskCallbackInvoker.Invoke(CallbackType.create_meeting.ToString(), "", result);
        }

        private void JoinMeeting(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<JoinMeetingModel> result = new MeetingResult<JoinMeetingModel>();

            try
            {
                JoinMeetingResult joinMeetingResult = Marshal.PtrToStructure<JoinMeetingResult>(pData);
                result.StatusCode = joinMeetingResult.StatusCode;

                if (joinMeetingResult.StatusCode == 0)
                {
                    JoinMeetingInfo joinMeetingInfo =
                        Marshal.PtrToStructure<JoinMeetingInfo>(joinMeetingResult.JoinMeetingInfo);

                    result.Result = joinMeetingInfo.ToModel();
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.StatusCode = -1000;
            }

            TaskCallbackInvoker.Invoke(CallbackType.join_meeting.ToString(), "", result);
        }

        private void GetUserList(IntPtr pData, int dataLen, IntPtr ctx)
        {
            
        }

        private void UserJoinEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<AccountModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<AttendeeInfo>(pData);
                result.Result = new AccountModel(cbResult.m_accountId, cbResult.m_accountName);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.StatusCode = -1000;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_join_event.ToString(), "", result);
        }

        private void UserLeaveEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<AccountModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<AttendeeInfo>(pData);
                result.Result = new AccountModel(cbResult.m_accountId, cbResult.m_accountName);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.StatusCode = -1000;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_leave_event.ToString(), "", result);
        }

        private void StartSpeakEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<SpeakModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<SpeakResult>(pData);
                result.Result = new SpeakModel()
                {
                    SpeakerName = cbResult.AccountName,
                    SpeakReason = (SpeakReason)cbResult.SpeakReason,

                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.start_speak_event.ToString(), "", result);
        }
        private void StopSpeakEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<SpeakModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<SpeakResult>(pData);
                result.Result = new SpeakModel()
                {
                    SpeakerName = cbResult.AccountName,
                    SpeakReason = (SpeakReason)cbResult.SpeakReason,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.stop_speak_event.ToString(), "", result);
        }

        private void UserStartSpeakEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserSpeakModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserSpeakResult>(pData);
                result.Result = new UserSpeakModel()
                {
                    SpeakReason = (SpeakReason)cbResult.m_speakeReason,
                    RelatedSpeakerName = cbResult.m_accountName,
                    Account = new AccountModel(cbResult.m_newSpeakerAccountId, cbResult.m_newSpeakerAccountName)
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_start_speak_event.ToString(), "", result);
        }
        
        private void UserStopSpeakEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserSpeakModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserSpeakResult>(pData);
                result.Result = new UserSpeakModel()
                {
                    SpeakReason = (SpeakReason)cbResult.m_speakeReason,
                    RelatedSpeakerName = cbResult.m_accountName,
                    Account = new AccountModel(cbResult.m_newSpeakerAccountId, cbResult.m_newSpeakerAccountName)
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_stop_speak_event.ToString(), "", result);
        }

        private void AskForSpeak(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.ask_for_speak.ToString(), "", result);
        }

        private void AskForStopSpeak(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.ask_for_stop_speak.ToString(), "", result);
        }

        private void PublishCameraVideo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<int> result = new MeetingResult<int>();
            string uniqueId = string.Empty;

            try
            {
                PublishStreamResult publishStreamResult = Marshal.PtrToStructure<PublishStreamResult>(pData);
                result.Result = publishStreamResult.m_streamId;
                result.StatusCode = publishStreamResult.m_statusCode;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.publish_camera_video.ToString(), uniqueId, result);
        }

        private void PublishWinCaptureVideo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<int> result = new MeetingResult<int>();
            string uniqueId = string.Empty;

            try
            {
                PublishStreamResult publishStreamResult = Marshal.PtrToStructure<PublishStreamResult>(pData);
                result.Result = publishStreamResult.m_streamId;
                result.StatusCode = publishStreamResult.m_statusCode;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.publish_win_capture_video.ToString(), uniqueId, result);
        }

        private void PublishDataCardVideo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<int> result = new MeetingResult<int>();
            string uniqueId = string.Empty;

            try
            {
                PublishStreamResult publishStreamResult = Marshal.PtrToStructure<PublishStreamResult>(pData);
                result.Result = publishStreamResult.m_streamId;
                result.StatusCode = publishStreamResult.m_statusCode;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.publish_data_card_video.ToString(), uniqueId, result);

        }

        private void PublishMicAudio(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<int> result = new MeetingResult<int>();
            string uniqueId = string.Empty;
            try
            {
                PublishStreamResult publishStreamResult = Marshal.PtrToStructure<PublishStreamResult>(pData);
                result.Result = publishStreamResult.m_streamId;
                result.StatusCode = publishStreamResult.m_statusCode;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.publish_mic_audio.ToString(), uniqueId, result);
        }

        private void UnpublishCameraVideo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult taskResult = new MeetingResult();
            string uniqueId = string.Empty;
            try
            {
                AsyncCallbackResult result = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                taskResult.Message = result.Message;
                taskResult.StatusCode = result.Status;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                taskResult.Message = e.Message;
                taskResult.StatusCode = -10000;
            }

            TaskCallbackInvoker.Invoke(CallbackType.unpublish_camera_video.ToString(), uniqueId, taskResult);
        }

        private void UnpublishWinCaptureVideo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult taskResult = new MeetingResult();
            string uniqueId = string.Empty;
            try
            {
                AsyncCallbackResult result = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                taskResult.Message = result.Message;
                taskResult.StatusCode = result.Status;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                taskResult.Message = e.Message;
                taskResult.StatusCode = -10000;
            }

            TaskCallbackInvoker.Invoke(CallbackType.unpublish_win_capture_video.ToString(), uniqueId, taskResult);

        }

        private void UnpublishDataCardVideo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult taskResult = new MeetingResult();
            string uniqueId = string.Empty;
            try
            {
                AsyncCallbackResult result = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                taskResult.Message = result.Message;
                taskResult.StatusCode = result.Status;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                taskResult.Message = e.Message;
                taskResult.StatusCode = -10000;
            }

            TaskCallbackInvoker.Invoke(CallbackType.unpublish_data_card_video.ToString(), uniqueId, taskResult);
        }

        private void UnpublishMicAudio(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult taskResult = new MeetingResult();
            string uniqueId = string.Empty;
            try
            {
                AsyncCallbackResult result = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                taskResult.Message = result.Message;
                taskResult.StatusCode = result.Status;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                taskResult.Message = e.Message;
                taskResult.StatusCode = -10000;
            }

            TaskCallbackInvoker.Invoke(CallbackType.unpublish_mic_audio.ToString(), uniqueId, taskResult);
        }

        #region 消息


        private void UserPublishCameraVideoEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserPublishModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserPublishData>(pData);
                
                var model = new UserPublishModel()
                {
                    ResourceId = cbResult.resourceId,
                    SyncId = cbResult.syncId,
                    AccountId = cbResult.accountId,
                    AccountName = cbResult.accountName,
                    ExtraInfo = cbResult.extraInfo
                };
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_publish_camera_video_event.ToString(), "", result);
        }

        private void UserPublishDataVideoEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserPublishModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserPublishData>(pData);

                var model = new UserPublishModel()
                {
                    ResourceId = cbResult.resourceId,
                    SyncId = cbResult.syncId,
                    AccountId = cbResult.accountId,
                    AccountName = cbResult.accountName,
                    ExtraInfo = cbResult.extraInfo
                };
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_publish_data_video_event.ToString(), "", result);
        }

        private void UserPublishMicAudioEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserPublishModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserPublishData>(pData);

                var model = new UserPublishModel()
                {
                    ResourceId = cbResult.resourceId,
                    SyncId = cbResult.syncId,
                    AccountId = cbResult.accountId,
                    AccountName = cbResult.accountName,
                    ExtraInfo = cbResult.extraInfo
                };
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_publish_mic_audio_event.ToString(), "", result);
        }

        private void UserUnpublishCameraVideoEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserUnpublishModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserUnpublishData>(pData);

                var model = new UserUnpublishModel()
                {
                    ResourceId = cbResult.resourceId,
                    AccountId = cbResult.accountId,
                    AccountName = cbResult.accountName,
                };
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_unpublish_camera_video_event.ToString(), "", result);
        }

        private void UserUnpublishDataCardVideoEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserUnpublishModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserUnpublishData>(pData);

                var model = new UserUnpublishModel()
                {
                    ResourceId = cbResult.resourceId,
                    AccountId = cbResult.accountId,
                    AccountName = cbResult.accountName,
                };
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_unpublish_data_card_video_event.ToString(), "", result);
        }

        private void UserUnpublishMicAudioEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UserUnpublishModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UserUnpublishData>(pData);

                var model = new UserUnpublishModel()
                {
                    ResourceId = cbResult.resourceId,
                    AccountId = cbResult.accountId,
                    AccountName = cbResult.accountName,
                };
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.user_unpublish_mic_audio_event.ToString(), "", result);
        }

        #endregion

        private void YuvData(IntPtr pData, int dataLen, IntPtr ctx)
        {
            
        }

        private void HostChangeMeetingMode(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_change_meeting_mode.ToString(), "", result);
        }

        private void HostChangeMeetingModeEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<MeetingMode>();
            try
            {
                var cbResult = Marshal.PtrToStructure<int>(pData);
                result.Result = (MeetingMode) cbResult;
                result.Message = "";
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_change_meeting_mode_event.ToString(), "", result);
        }

        private void HostKickoutUser(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<MeetingMode>();
            try
            {
                var cbResult = Marshal.PtrToStructure<int>(pData);
                result.Result = (MeetingMode)cbResult;
                result.Message = "";
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_kickout_user.ToString(), "", result);
        }

        private void HostKickoutUserEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<KickoutUserModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<KickoutUserData>(pData);
                result.Result = new KickoutUserModel()
                {
                    MeetingId = cbResult.meetingId,
                    KickedUserId = cbResult.kickedUserId
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_kickout_user_event.ToString(), "", result);
        }

        private void RaiseHandRequest(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.raise_hand_request.ToString(), "", result);
        }

        private void RaiseHandRequestEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<AccountModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<AttendeeInfo>(pData);
                result.Result = new AccountModel(cbResult.m_accountId, cbResult.m_accountName);
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.raise_hand_request_event.ToString(), "", result);
        }

        private void AskForMeetingLock(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.ask_for_meeting_lock.ToString(), "", result);
        }

        private void HostOrderOneSpeak(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_order_one_speak.ToString(), "", result);
        }

        private void HostOrderOneStopSpeak(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_order_one_stop_speak.ToString(), "", result);
        }

        private void LockStatusChangedEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.lock_status_changed_event.ToString(), "", result);
        }

        private void MeetingManageExceptionEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<ExceptionModel> result = new MeetingResult<ExceptionModel>();

            try
            {
                ExceptionResult sdkCallbackResult = Marshal.PtrToStructure<ExceptionResult>(pData);
                result.Result = new ExceptionModel()
                {
                    Description = sdkCallbackResult.description,
                    ExceptionType = (ExceptionType)sdkCallbackResult.exceptionType,
                    ExtraInfo = sdkCallbackResult.extraInfo,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.meeting_manage_exception_event.ToString(), "", result);
        }

        private void DeviceStatusEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<DeviceStatusModel> result = new MeetingResult<DeviceStatusModel>();

            try
            {
                DeviceStatusResult sdkCallbackResult = Marshal.PtrToStructure<DeviceStatusResult>(pData);
                result.Result = new DeviceStatusModel()
                {
                    DeviceName = sdkCallbackResult.devName,
                    DeviceStatusType = (DeviceStatusType)sdkCallbackResult.type,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.device_status_event.ToString(), "", result);
        }

        private void NetDiagnosticCheck(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<NetType> result = new MeetingResult<NetType>();

            try
            {
                NetStatusResult netStatusResult = Marshal.PtrToStructure<NetStatusResult>(pData);
                result.Result = (NetType)netStatusResult.netStatusType;
                result.Message = netStatusResult.m_result.Message;
                result.StatusCode = netStatusResult.m_result.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.net_diagnostic_check.ToString(), "", result);

        }

        private void PlayVideoTest(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult result = new MeetingResult();

            try
            {
                AsyncCallbackResult asyncCallbackResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                result.Message = asyncCallbackResult.Message;
                result.StatusCode = asyncCallbackResult.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.play_video_test.ToString(), "", result);
        }

        private void PlaySoundTest(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult result = new MeetingResult();

            try
            {
                AsyncCallbackResult asyncCallbackResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                result.Message = asyncCallbackResult.Message;
                result.StatusCode = asyncCallbackResult.Status;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.play_sound_test.ToString(), "", result);
        }

        private void SendUiMsg(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.send_ui_msg.ToString(), "", result);
        }

        private void TransparentMsgEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<TransparentMsg>();
            try
            {
                var cbResult = Marshal.PtrToStructure<TransparentMsgResult>(pData);
                result.Result = new TransparentMsg()
                {
                    Data = cbResult.data,
                    SenderAccountId = cbResult.senderAccountId,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.transparent_msg_event.ToString(), "", result);
        }

        private void UiMsgReceivedEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<UiTransparentMsg>();
            try
            {
                var cbResult = Marshal.PtrToStructure<UiTransparentMsgResult>(pData);
                result.Result = new UiTransparentMsg()
                {
                    Data = cbResult.data,
                    TargetAccountId = cbResult.toAccountId,
                    MsgId = cbResult.msgId,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.receive_ui_msg_event.ToString(), "", result);
        }

        private void MicSendResponse(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.mic_send_response.ToString(), "", result);
        }

        private void NetworkStatusLevelNoticeEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<int>();
            try
            {
                var cbResult = Marshal.PtrToStructure<NetLevelResult>(pData);
                result.Result = cbResult.NetLevel;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.network_status_level_notice_event.ToString(), "", result);
        }

        private void DeviceLostNoticeEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<ResourceModel> result = new MeetingResult<ResourceModel>();

            try
            {
                ResourceResult sdkCallbackResult = Marshal.PtrToStructure<ResourceResult>(pData);
                result.Result = new ResourceModel()
                {
                    AccountId = sdkCallbackResult.accountId,
                    ResourceId = sdkCallbackResult.resourceId,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.device_lost_notice_event.ToString(), "", result);
        }

        private void SdkCallback(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult<SdkCallbackModel> result = new MeetingResult<SdkCallbackModel>();

            try
            {
                SdkCallbackResult sdkCallbackResult = Marshal.PtrToStructure<SdkCallbackResult>(pData);
                result.Result = new SdkCallbackModel()
                {
                    Description = sdkCallbackResult.description,
                    SdkNoticeType = (SdkNoticeType)sdkCallbackResult.type,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }

            TaskCallbackInvoker.Invoke(CallbackType.meeting_sdk_callback.ToString(), "", result);
        }
        
        private void SendAudioSpeakerStatus(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.send_audio_speaker_status.ToString(), "", result);
        }

        private void GetMeetingInvitationSMS(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<MeetingInvitationSMSModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<MeetingInvitationSMSData>(pData);
                result.StatusCode = cbResult.Result.Status;
                result.Message = cbResult.Result.Message;

                result.Result = new MeetingInvitationSMSModel()
                {
                    InvitationSMS = cbResult.invitationSMS,
                    YyUrl = cbResult.yyURL
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.get_meeting_invitation_sms.ToString(), "", result);
        }

        private void HostOrderOneDoOpration(IntPtr pData, int dataLen, IntPtr ctx)
        {
            MeetingResult taskResult = new MeetingResult();
            var uniqueId = "";
            try
            {
                AsyncCallbackResult result = Marshal.PtrToStructure<AsyncCallbackResult>(pData);

                taskResult.Message = result.Message;
                taskResult.StatusCode = result.Status;

                ContextData cd = Marshal.PtrToStructure<ContextData>(ctx);
                uniqueId = cd.UniqueId;
            }
            catch (Exception e)
            {
                taskResult.Message = e.Message;
                taskResult.StatusCode = -10000;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_order_one_do_opration.ToString(), uniqueId, taskResult);
        }

        private void OtherChangeAudioSpeakerStatusEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<OtherChangeAudioSpeakerStatusModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<OtherChangeAudioSpeakerStatusData>(pData);
                result.Result = new OtherChangeAudioSpeakerStatusModel()
                {
                    AccountId =  cbResult.accountId,
                    OprateType = (HostOprateType)cbResult.opType
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.other_change_audio_speaker_status_event.ToString(), "", result);
        }

        private void HostOrderDoOpratonEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<HostOprateType>();
            try
            {
                var cbResult = Marshal.PtrToStructure<HostOperateTypeResult>(pData);
                result.Result = (HostOprateType)cbResult.operateType;
                result.Message = "";
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.host_order_do_opraton_event.ToString(), "", result);
        }

        private void ModifyNickName(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.modify_nick_name.ToString(), "", result);
        }

        private void ModifyMeetingInviters(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.modify_meeting_inviters.ToString(), "", result);
        }


        private void ForcedOfflineEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<ForcedOfflineModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<ForcedOfflineResult>(pData);
                result.Result = new ForcedOfflineModel()
                {
                    AccountId = cbResult.accountId,
                    Token = cbResult.token,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.force_offline_event.ToString(), "", result);
        }

        private void ContactRecommendEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<RecommendContactModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<RecommendContactInfo>(pData);
                result.Result = new RecommendContactModel()
                {
                    ContactId = cbResult.m_contactListId,
                    ContactName = cbResult.m_contactListName,
                    Number = cbResult.m_contacterNum,
                    SourceId = cbResult.m_sourceId,
                    Version = cbResult.m_contactListVer,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.contact_recommend_event.ToString(), "", result);
        }

        private void MeetingInviteEvent(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult<MeetingInvitationModel>();
            try
            {
                var cbResult = Marshal.PtrToStructure<MeetingInvitationResult>(pData);
                result.Result = new MeetingInvitationModel()
                {
                    MeetingId = cbResult.meetingId,
                    SenderAccountName = cbResult.inviterAccountName,
                    SenderId = cbResult.inviterAccountId,
                };
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.meeting_invite_event.ToString(), "", result);
        }

        private void MeetingInvite(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.meeting_invite.ToString(), "", result);
        }

        private void SetAccountInfo(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.set_account_info.ToString(), "", result);
        }

        private void ConnectMeetingVDN(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.connect_meeting_vdn.ToString(), "", result);
        }

        private void ConnectMeetingVDNAfterMeetingInstStarted(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.connect_meeting_vdn_after_instance_started.ToString(), "", result);
        }

        private void StartHost(IntPtr pData, int dataLen, IntPtr ctx)
        {
            var result = new MeetingResult();
            try
            {
                var cbResult = Marshal.PtrToStructure<AsyncCallbackResult>(pData);
                result.StatusCode = cbResult.Status;
                result.Message = cbResult.Message;
            }
            catch (Exception e)
            {
                result.StatusCode = -10000;
                result.Message = e.Message;
            }
            TaskCallbackInvoker.Invoke(CallbackType.start_host.ToString(), "", result);
        }
    }
}
