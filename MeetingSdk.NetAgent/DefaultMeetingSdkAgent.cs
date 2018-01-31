using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.NetAgent
{
    public class DefaultMeetingSdkAgent : IMeetingSdkAgent
    {
        private static readonly PFuncCallBack Cb = MeetingSdkCallback.PFuncCallBack;
        private static readonly FUN_VIDEO_PREVIEW VideoPreviewCb = MeetingSdkCallback.PFunVideoPriview;
        private const int DefaultTimeout = 30000;

        private DefaultMeetingSdkAgent()
        {
        }

        public bool IsStarted { get; private set; }

        #region Events

        public Action<MeetingResult<UserPublishModel>> OnUserPublishCameraVideoEvent { get; set; }
        public Action<MeetingResult<UserPublishModel>> OnUserPublishDataVideoEvent { get; set; }
        public Action<MeetingResult<UserPublishModel>> OnUserPublishMicAudioEvent { get; set; }

        public Action<MeetingResult<UserUnpublishModel>> OnUserUnpublishCameraVideoEvent { get; set; }
        public Action<MeetingResult<UserUnpublishModel>> OnUserUnpublishDataCardVideoEvent { get; set; }
        public Action<MeetingResult<UserUnpublishModel>> OnUserUnpublishMicAudioEvent { get; set; }

        #region 用户进会与离开事件

        public Action<MeetingResult<AccountModel>> OnUserJoinEvent { get; set; }
        public Action<MeetingResult<AccountModel>> OnUserLeaveEvent { get; set; }

        #endregion

        #region 通知开始与停止发言

        public Action<MeetingResult<SpeakModel>> OnStartSpeakEvent { get; set; }
        public Action<MeetingResult<SpeakModel>> OnStopSpeakEvent { get; set; }

        #endregion

        #region 其它用户开始与停止发言

        public Action<MeetingResult<UserSpeakModel>> OnUserStartSpeakEvent { get; set; }
        public Action<MeetingResult<UserSpeakModel>> OnUserStopSpeakEvent { get; set; }

        #endregion

        /// <summary>
        /// 主持人修改会议模式
        /// </summary>
        public Action<MeetingResult<MeetingMode>> OnHostChangeMeetingModeEvent { get; set; }
        /// <summary>
        /// 主持人踢出用户
        /// </summary>
        public Action<MeetingResult<KickoutUserModel>> OnHostKickoutUserEvent { get; set; }
        /// <summary>
        /// 收到用户举手请求通知（主持人收到）
        /// </summary>
        public Action<MeetingResult<AccountModel>> OnRaiseHandRequestEvent { get; set; }

        /// <summary>
        /// 收到主持人指定打开/关闭麦克风、扬声器、摄像头 事件通知
        /// </summary>
        public Action<MeetingResult<HostOprateType>> OnHostOrderDoOpratonEvent { get; set; }
        /// <summary>
        /// 收到其他参会者打开/关闭麦克风、扬声器、摄像头 事件通知
        /// </summary>
        public Action<MeetingResult<OtherChangeAudioSpeakerStatusModel>> OnOtherChangeAudioSpeakerStatusEvent { get; set; }

        public Action<MeetingResult<int>> OnNetworkStatusLevelNoticeEvent { get; set; }

        public Action<MeetingResult<NetType>> OnNetworkCheckedEvent { get; set; }
        public Action<MeetingResult<TransparentMsg>> OnTransparentMsgReceivedEvent { get; set; }
        public Action<MeetingResult<UiTransparentMsg>> OnUiTransparentMsgReceivedEvent { get; set; }
        public Action<MeetingResult> OnLockStatusChangedEvent { get; set; }
        public Action<MeetingResult<ExceptionModel>> OnMeetingManageExceptionEvent { get; set; }
        public Action<MeetingResult<DeviceStatusModel>> OnDeviceStatusChangedEvent { get; set; }
        public Action<MeetingResult<ResourceModel>> OnDeviceLostNoticeEvent { get; set; }
        public Action<MeetingResult<SdkCallbackModel>> OnSdkCallbackEvent { get; set; }

        public Action<MeetingResult<MeetingInvitationModel>> OnMeetingInviteEvent { get; set; }
        public Action<MeetingResult<RecommendContactModel>> OnContactRecommendEvent { get; set; }
        public Action<MeetingResult<ForcedOfflineModel>> OnForcedOfflineEvent { get; set; }



        #endregion

        private static object _lockObj;
        private static bool _initialized;

        private static DefaultMeetingSdkAgent _instance;
        public static DefaultMeetingSdkAgent Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = LazyInitializer.EnsureInitialized(ref _instance, ref _initialized, ref _lockObj, Initialize);
                }
                return _instance;
            }
        }


        static DefaultMeetingSdkAgent Initialize()
        {
            var obj = new DefaultMeetingSdkAgent();
            try
            {
                MeetingSdkAgent.SetCallback(Cb);

                ITaskCallback callback = null;

                callback = new EventTaskCallback<MeetingResult<MeetingInvitationModel>>(
                CallbackType.meeting_invite_event.ToString(),
                r => Instance.OnMeetingInviteEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<RecommendContactModel>>(
                CallbackType.contact_recommend_event.ToString(),
                r => Instance.OnContactRecommendEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<ForcedOfflineModel>>(
                CallbackType.force_offline_event.ToString(),
                r => Instance.OnForcedOfflineEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);



                // 用户发布摄像头事件
                callback = new EventTaskCallback<MeetingResult<UserPublishModel>>(
                    CallbackType.user_publish_camera_video_event.ToString(),
                    r => Instance.OnUserPublishCameraVideoEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                // 用户发布数据摄像头事件
                callback = new EventTaskCallback<MeetingResult<UserPublishModel>>(
                    CallbackType.user_publish_data_video_event.ToString(),
                    r => Instance.OnUserPublishDataVideoEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                // 用户发布MIC事件
                callback = new EventTaskCallback<MeetingResult<UserPublishModel>>(
                    CallbackType.user_publish_mic_audio_event.ToString(),
                    r => Instance.OnUserPublishMicAudioEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                // 用户取消发布摄像头事件
                callback = new EventTaskCallback<MeetingResult<UserUnpublishModel>>(
                    CallbackType.user_unpublish_camera_video_event.ToString(),
                    r => Instance.OnUserUnpublishCameraVideoEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                // 用户取消发布数据摄像头事件
                callback = new EventTaskCallback<MeetingResult<UserUnpublishModel>>(
                    CallbackType.user_unpublish_data_card_video_event.ToString(),
                    r => Instance.OnUserUnpublishDataCardVideoEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                // 用户取消发布MIC事件
                callback = new EventTaskCallback<MeetingResult<UserUnpublishModel>>(
                    CallbackType.user_unpublish_mic_audio_event.ToString(),
                    r => Instance.OnUserUnpublishMicAudioEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                // 用户进会事件
                callback = new EventTaskCallback<MeetingResult<AccountModel>>(
                    CallbackType.user_join_event.ToString(),
                    r => Instance.OnUserJoinEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                // 用户离会事件
                callback = new EventTaskCallback<MeetingResult<AccountModel>>(
                    CallbackType.user_leave_event.ToString(),
                    r => Instance.OnUserLeaveEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);



                // 通知开始发言事件
                callback = new EventTaskCallback<MeetingResult<SpeakModel>>(
                    CallbackType.start_speak_event.ToString(),
                    r => Instance.OnStartSpeakEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                callback = new EventTaskCallback<MeetingResult<SpeakModel>>(
                    CallbackType.stop_speak_event.ToString(),
                    r => Instance.OnStopSpeakEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<UserSpeakModel>>(
                    CallbackType.user_start_speak_event.ToString(),
                    r => Instance.OnUserStartSpeakEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                callback = new EventTaskCallback<MeetingResult<UserSpeakModel>>(
                    CallbackType.user_stop_speak_event.ToString(),
                    r => Instance.OnUserStopSpeakEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<MeetingMode>>(
                    CallbackType.host_change_meeting_mode_event.ToString(),
                    r => Instance.OnHostChangeMeetingModeEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                callback = new EventTaskCallback<MeetingResult<KickoutUserModel>>(
                    CallbackType.host_kickout_user_event.ToString(),
                    r => Instance.OnHostKickoutUserEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                callback = new EventTaskCallback<MeetingResult<AccountModel>>(
                    CallbackType.raise_hand_request_event.ToString(),
                    r => Instance.OnRaiseHandRequestEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<HostOprateType>>(
                    CallbackType.host_order_do_opraton_event.ToString(),
                    r => Instance.OnHostOrderDoOpratonEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
                callback = new EventTaskCallback<MeetingResult<OtherChangeAudioSpeakerStatusModel>>(
                    CallbackType.other_change_audio_speaker_status_event.ToString(),
                    r => Instance.OnOtherChangeAudioSpeakerStatusEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<int>>(CallbackType.network_status_level_notice_event.ToString(),
                    r => Instance.OnNetworkStatusLevelNoticeEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<NetType>>(CallbackType.net_diagnostic_check.ToString(),
                    r => Instance.OnNetworkCheckedEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<TransparentMsg>>(CallbackType.transparent_msg_event.ToString(),
                    r => Instance.OnTransparentMsgReceivedEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<UiTransparentMsg>>(CallbackType.receive_ui_msg_event.ToString(),
                    r => Instance.OnUiTransparentMsgReceivedEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult>(CallbackType.lock_status_changed_event.ToString(),
                    r => Instance.OnLockStatusChangedEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<ExceptionModel>>(CallbackType.meeting_manage_exception_event.ToString(),
                    r => Instance.OnMeetingManageExceptionEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);


                callback = new EventTaskCallback<MeetingResult<DeviceStatusModel>>(CallbackType.device_status_event.ToString(),
                    r => Instance.OnDeviceStatusChangedEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<ResourceModel>>(CallbackType.device_lost_notice_event.ToString(),
                    r => Instance.OnDeviceLostNoticeEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);

                callback = new EventTaskCallback<MeetingResult<SdkCallbackModel>>(CallbackType.meeting_sdk_callback.ToString(),
                    r => Instance.OnSdkCallbackEvent?.Invoke(r));
                TaskCallbackInvoker.Register(callback);
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "MeetingSdk初始化错误。");
            }
            return (_instance = obj);
        }

        #region -- MeetingManage Method --

        #region 设置，启动及鉴权接口

        public MeetingResult SetNpsUrl(params string[] npsUrlList)
        {

            var result = new MeetingResult();
            IntPtr ptr = IntPtr.Zero;
            try
            {
                if (npsUrlList.Length == 0)
                    throw new Exception("内容是空的。");

                var list = new List<StringStruct>();
                foreach (var item in npsUrlList)
                {
                    list.Add(new StringStruct { String = item });
                }

                var s1 = Marshal.SizeOf<StringStruct>();
                var s2 = s1 * list.Count;
                ptr = Marshal.AllocHGlobal(s2);

                for (int i = 0; i < list.Count; i++)
                {
                    Marshal.StructureToPtr(list[i], ptr + (i * s1), true);
                }

                int temp = MeetingSdkAgent.SetNpsUrl(ptr, list.Count);
                result.StatusCode = temp;
            }
            catch (Exception e)
            {
                result.StatusCode = -1;
                result.Message = e.Message;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return result;
        }

        public MeetingResult SetRkPath(string rkPath)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetRkPath(rkPath, rkPath.BytesLength());
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetRkPath调用出错。");
                throw;
            }
            return result;
        }

        public Task<MeetingResult> Start(string devModel, string configPath)
        {
            var cmdName = CallbackType.start.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);
            var result = (Task<MeetingResult>)callback.Task;
            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                //var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //path = Path.Combine(path, "sdk");

                int temp = MeetingSdkAgent.Start(devModel, configPath, configPath.BytesLength(), IntPtr.Zero);

                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                else
                {
                    var taskResult = result.Result;
                    if (taskResult.StatusCode == 0)
                    {
                        this.IsStarted = true;
                    }
                }

                return result;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在启动中..." });
            }
        }

        public Task<MeetingResult> Stop()
        {
            var result = new MeetingResult();
            try
            {
                int temp = MeetingSdkAgent.Stop();
                if (temp != 0)
                {
                    throw new Exception("");
                }

                this.IsStarted = false;
            }
            catch (Exception e)
            {
                result.StatusCode = -1;
                result.Message = e.Message;
            }
            return Task.FromResult(result);
        }

        public Task<MeetingResult<LoginModel>> LoginViaImei(string imei)
        {
            string cmdName = CallbackType.login.ToString();
            var callback = new TaskCallback<MeetingResult<LoginModel>>(cmdName, "",
                10000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.LoginViaImei(imei, imei.Length, IntPtr.Zero);
                return callback.Task as Task<MeetingResult<LoginModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<LoginModel>() { StatusCode = -1, Message = "正在登录中..." });
            }
        }

        public Task<MeetingResult<LoginModel>> LoginThirdParty(string nube, string appkey, string uid)
        {
            string cmdName = CallbackType.login.ToString();
            var callback = new TaskCallback<MeetingResult<LoginModel>>(cmdName, "",
                10000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.LoginThirdParty(nube, appkey, uid, IntPtr.Zero);
                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("登录失败。"));
                }
                return callback.Task as Task<MeetingResult<LoginModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<LoginModel>() { StatusCode = -1, Message = "正在登录中..." });
            }
        }

        public Task<MeetingResult> BindToken(string token, AccountModel account)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token不能为空", nameof(token));
            }

            if (account == null || account.AccountId == 0)
            {
                throw new ArgumentNullException("视讯号不能为空", nameof(account));
            }

            string cmdName = CallbackType.bind_token.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);


            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.BindToken(token, token.Length, account.AccountId, account.AccountName,
                    string.IsNullOrEmpty(account.AccountName) ? 0 : account.AccountName.Length, IntPtr.Zero);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在绑定令牌中..." });
            }
        }

        #endregion

        #region 获取会议相关信息，音视频设备信息接口

        /// <summary>
        /// 检测会议是否存在
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        public Task<MeetingResult> IsMeetingExist(int meetingId)
        {
            var cmdName = CallbackType.check_meeting_exist.ToString();
            var uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            var callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, 30000, FreePtr(ctxPtr));
            TaskCallbackInvoker.Register(callback);
            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);

                var cd = new ContextData
                {
                    UniqueId = uniqueId
                };
                Marshal.StructureToPtr(cd, ctxPtr, true);
                var temp = MeetingSdkAgent.IsMeetingExist(meetingId, ctxPtr);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
            }
            catch (Exception e)
            {
                callback.SetException(e);
            }
            return (Task<MeetingResult>)callback.Task;
        }

        public Task<MeetingResult<IList<MeetingModel>>> GetMeetingList()
        {
            var cmdName = CallbackType.get_meeting_list.ToString();
            var uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            var callback = new TaskCallback<MeetingResult<IList<MeetingModel>>>(cmdName, uniqueId, DefaultTimeout, FreePtr(ctxPtr));
            try
            {
                TaskCallbackInvoker.Register(callback);
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);
                var cd = new ContextData { UniqueId = uniqueId };
                Marshal.StructureToPtr(cd, ctxPtr, true);

                var temp = MeetingSdkAgent.GetMeetingList(ctxPtr);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
            }
            catch (Exception e)
            {
                callback.SetException(e);
            }
            return (Task<MeetingResult<IList<MeetingModel>>>)callback.Task;
        }

        public Task<MeetingResult<MeetingModel>> GetMeetingInfo(int meetingId)
        {
            var cmdName = CallbackType.get_meeting_info.ToString();
            var uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            var callback = new TaskCallback<MeetingResult<MeetingModel>>(cmdName, uniqueId, 30000, FreePtr(ctxPtr));
            TaskCallbackInvoker.Register(callback);

            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);
                var cd = new ContextData { UniqueId = uniqueId };
                Marshal.StructureToPtr(cd, ctxPtr, true);

                var temp = MeetingSdkAgent.GetMeetingInfo(meetingId, ctxPtr);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
            }
            catch (Exception e)
            {
                callback.SetException(e);
            }
            return (Task<MeetingResult<MeetingModel>>)callback.Task;
        }

        public MeetingResult<JoinMeetingModel> GetJoinMeetingInfo(int meetingId)
        {
            var result = new MeetingResult<JoinMeetingModel>();
            IntPtr ptr = IntPtr.Zero;
            try
            {
                var size = Marshal.SizeOf<JoinMeetingInfo>();
                ptr = Marshal.AllocHGlobal(size);

                var temp = MeetingSdkAgent.GetJoinMeetingInfo(meetingId, ptr);
                if (temp != 0)
                    throw new Exception("调用失败。");

                JoinMeetingInfo info = Marshal.PtrToStructure<JoinMeetingInfo>(ptr);
                var model = info.ToModel();
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "GetJoinMeetingInfo调用错误。");
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取发言者列表
        /// </summary>
        /// <returns></returns>
        public async Task<MeetingResult<IList<MeetingSpeakerModel>>> GetSpeakerList()
        {
            var result = new MeetingResult<IList<MeetingSpeakerModel>>();
            IntPtr ptr = IntPtr.Zero;
            try
            {
                var num = 10;
                var size = Marshal.SizeOf<MeetingSpeakerInfo>();
                var sizeCount = size * num;

                ptr = Marshal.AllocHGlobal(sizeCount);

                var realSize = await Task.Run(() => MeetingSdkAgent.GetSpeakerList(ptr, 10));
                if (realSize < 0)
                    throw new Exception("调用失败。");

                var models = new List<MeetingSpeakerModel>();
                for (int i = 0; i < realSize; i++)
                {
                    MeetingSpeakerInfo info = Marshal.PtrToStructure<MeetingSpeakerInfo>(ptr + (i * size));

                    var model = new MeetingSpeakerModel()
                    {
                        Account = new AccountModel(info.AccountId, info.AccountName)
                    };

                    for (int j = 0; j < info.StreamCount; j++)
                    {
                        model.MeetingUserStreamInfos.Add(info.StreamInfos[j].ToModel());
                    }
                    models.Add(model);
                }
                result.Result = models;
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "GetSpeakerList调用错误。");
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return await Task.FromResult(result);
        }

        public async Task<MeetingResult<MeetingSpeakerModel>> GetSpeakerInfo(int accountId)
        {
            var result = new MeetingResult<MeetingSpeakerModel>();
            IntPtr ptr = IntPtr.Zero;
            try
            {
                var size = Marshal.SizeOf<MeetingSpeakerInfo>();
                ptr = Marshal.AllocHGlobal(size);

                var temp = await Task.Run(() => MeetingSdkAgent.GetSpeakerInfo(accountId, ptr));
                if (temp != 0)
                    throw new Exception("调用失败。");

                MeetingSpeakerInfo info = Marshal.PtrToStructure<MeetingSpeakerInfo>(ptr);
                var model = new MeetingSpeakerModel()
                {
                    Account = new AccountModel(info.AccountId, info.AccountName)
                };
                for (int i = 0; i < info.StreamCount; i++)
                {
                    model.MeetingUserStreamInfos.Add(info.StreamInfos[i].ToModel());
                }
                result.Result = model;
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "GetSpeakerList调用错误。");
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return await Task.FromResult(result);
        }

        public async Task<MeetingResult<MeetingMode>> GetCurMeetingMode()
        {
            var result = new MeetingResult<MeetingMode>();
            try
            {
                var curMode = await Task.Run(() => MeetingSdkAgent.GetCurMeetingMode());
                result.Result = (MeetingMode)curMode;
                result.StatusCode = 0;
                result.Message = "";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
            }
            return await Task.FromResult(result);
        }

        public async Task<MeetingResult<bool>> GetMeetingLockStatus()
        {
            var result = new MeetingResult<bool>();
            try
            {
                var temp = await Task.Run(() => MeetingSdkAgent.GetMeetingLockStatus());
                result.Result = temp;
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "GetMeetingLockStatus调用错误。");
            }
            return await Task.FromResult(result);
        }

        public Task<MeetingResult<MeetingPasswordModel>> GetMeetingPassword(int meetingId)
        {
            string cmdName = CallbackType.get_meeting_password.ToString();

            TaskCallback<MeetingResult<MeetingPasswordModel>> callback = new TaskCallback<MeetingResult<MeetingPasswordModel>>(cmdName, "", DefaultTimeout);


            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.GetMeetingPassword(meetingId);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult<MeetingPasswordModel>>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingPasswordModel>() { StatusCode = -1, Message = "正在获取会议密码中..." });
            }
        }

        public async Task<MeetingResult<IList<MeetingUserStreamModel>>> GetUserPublishStreamInfo(int accountId)
        {
            MeetingResult<IList<MeetingUserStreamModel>> result = new MeetingResult<IList<MeetingUserStreamModel>>();

            int maxCount = 10;
            int streamInfoSize = Marshal.SizeOf<MeetingUserStreamInfo>();
            IntPtr streamsPtr = IntPtr.Zero;

            try
            {
                streamsPtr = Marshal.AllocHGlobal(maxCount * streamInfoSize);
                int realCount =
                    await Task.Run(() => MeetingSdkAgent.GetUserPublishStreamInfo(accountId, streamsPtr, maxCount));

                IList<MeetingUserStreamModel> streamModels = new List<MeetingUserStreamModel>();

                for (int i = 0; i < realCount; i++)
                {
                    IntPtr pointer = (IntPtr)(streamsPtr.ToInt64() + i * streamInfoSize);
                    MeetingUserStreamInfo streamInfo = Marshal.PtrToStructure<MeetingUserStreamInfo>(pointer);

                    MeetingUserStreamModel streamModel = streamInfo.ToModel();

                    streamModels.Add(streamModel);
                }

                result.Result = streamModels;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.StatusCode = -10000;

                MeetingLogger.Logger.LogError(e, "获取指定用户发布流的信息失败。");
            }
            finally
            {
                FreePtr(streamsPtr);
            }

            return result;
        }

        public async Task<MeetingResult<IList<MeetingUserStreamModel>>> GetCurrentSubscribleStreamInfo()
        {
            MeetingResult<IList<MeetingUserStreamModel>> result = new MeetingResult<IList<MeetingUserStreamModel>>();

            int maxCount = 15;
            int streamInfoSize = Marshal.SizeOf<MeetingUserStreamInfo>();
            IntPtr streamsPtr = IntPtr.Zero;

            try
            {
                streamsPtr = Marshal.AllocHGlobal(maxCount * streamInfoSize);
                int realCount =
                    await Task.Run(() => MeetingSdkAgent.GetCurrentSubscribleStreamInfo(streamsPtr, maxCount));

                IList<MeetingUserStreamModel> streamModels = new List<MeetingUserStreamModel>();

                for (int i = 0; i < realCount; i++)
                {
                    IntPtr pointer = (IntPtr)(streamsPtr.ToInt64() + i * streamInfoSize);
                    MeetingUserStreamInfo streamInfo = Marshal.PtrToStructure<MeetingUserStreamInfo>(pointer);

                    MeetingUserStreamModel streamModel = streamInfo.ToModel();

                    streamModels.Add(streamModel);
                }

                result.Result = streamModels;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.StatusCode = -10000;

                MeetingLogger.Logger.LogError(e, "获取当前用户订阅的所有流信息失败。");
            }
            finally
            {
                FreePtr(streamsPtr);
            }

            return result;
        }

        public async Task<MeetingResult<SpeakerVideoStreamParamModel>> GetSpeakerVideoStreamParam(int accountId, int resourceId)
        {
            var result = new MeetingResult<SpeakerVideoStreamParamModel>();
            var ptr = IntPtr.Zero;
            try
            {
                var size = Marshal.SizeOf<SpeakerVideoStreamParamData>();
                ptr = Marshal.AllocHGlobal(size);

                var temp = await Task.Run(() => MeetingSdkAgent.GetSpeakerVideoStreamParam(accountId, resourceId, ptr));

                var data = Marshal.PtrToStructure<SpeakerVideoStreamParamData>(ptr);
                result.Result = new SpeakerVideoStreamParamModel()
                {
                    Width = data.width,
                    Height = data.height
                };

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "GetSpeakerVideoStreamParam调用错误。");
                throw;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return await Task.FromResult(result);
        }


        public MeetingResult<IList<ParticipantModel>> GetParticipants()
        {
            MeetingResult<IList<ParticipantModel>> participants = new MeetingResult<IList<ParticipantModel>>();
            IntPtr participantPtr = IntPtr.Zero;
            try
            {
                int maxParticipant = 10;
                int realParticipant = 0;

                int participantSize = Marshal.SizeOf(typeof(ParticipantInfo));

                int maxBytes = maxParticipant * participantSize;
                participantPtr = Marshal.AllocHGlobal(maxBytes);

                realParticipant = MeetingSdkAgent.GetParticipants(participantPtr, maxParticipant);

                List<ParticipantModel> models = new List<ParticipantModel>();
                for (int i = 0; i < realParticipant; i++)
                {
                    IntPtr pointer = (IntPtr)(participantPtr.ToInt64() + i * participantSize);
                    ParticipantInfo participantInfo = Marshal.PtrToStructure<ParticipantInfo>(pointer);

                    ParticipantModel model = new ParticipantModel()
                    {
                        AccountId = participantInfo.AccountId,
                        AccountName = participantInfo.AccountName,
                        IsHost = participantInfo.HostFlag == 1,
                        IsRaisedHand = participantInfo.IsRaiseHand.ToBool(),
                        IsSpeakerOn = participantInfo.IsSpeaking.ToBool(),
                        IsSpeaking = participantInfo.IsSpeaking.ToBool()
                    };

                    models.Add(model);
                }
                participants.Result = models;
            }
            catch (Exception e)
            {
                participants.Message = e.Message;
                participants.StatusCode = -10000;
            }
            finally
            {
                if (participantPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(participantPtr);
                }
            }
            return participants;
        }

        public MeetingResult<IList<ParticipantModel>> GetParticipantsByPage(int pageNum, int countPerPage)
        {
            MeetingResult<IList<ParticipantModel>> participants = new MeetingResult<IList<ParticipantModel>>();

            IntPtr participantPtr = IntPtr.Zero;

            try
            {
                int realParticipant = 0;

                int participantSize = Marshal.SizeOf(typeof(ParticipantInfo));

                int maxBytes = countPerPage * participantSize;
                participantPtr = Marshal.AllocHGlobal(maxBytes);

                realParticipant =
                     MeetingSdkAgent.GetParticipantsByPage(participantPtr, pageNum, countPerPage);

                List<ParticipantModel> models = new List<ParticipantModel>();

                for (int i = 0; i < realParticipant; i++)
                {
                    IntPtr pointer = (IntPtr)(participantPtr.ToInt64() + i * participantSize);
                    ParticipantInfo participantInfo = Marshal.PtrToStructure<ParticipantInfo>(pointer);

                    ParticipantModel model = new ParticipantModel()
                    {
                        AccountId = participantInfo.AccountId,
                        AccountName = participantInfo.AccountName,
                        IsHost = participantInfo.HostFlag == 1,
                        IsRaisedHand = participantInfo.IsRaiseHand.ToBool(),
                        IsSpeakerOn = participantInfo.IsSpeaking.ToBool(),
                        IsSpeaking = participantInfo.IsSpeaking.ToBool()
                    };

                    models.Add(model);
                }

                participants.Result = models;
            }
            catch (Exception e)
            {
                participants.Message = e.Message;
                participants.StatusCode = -10000;
            }
            finally
            {
                FreePtr(participantPtr);
            }

            return participants;
        }

        public Task<MeetingResult<MeetingInvitationSMSModel>> GetMeetingInvitationSMS(int meetingId, int inviterPhoneId,
    string inviterName, MeetingType meetingType, string app, InviterUrlType urlType)
        {
            if (string.IsNullOrEmpty(inviterName))
                throw new ArgumentException("需提供inviterName", nameof(inviterName));

            if (string.IsNullOrEmpty(app))
                throw new ArgumentException("需提供app", nameof(app));

            var cmdName = CallbackType.get_meeting_invitation_sms.ToString();
            var callback = new TaskCallback<MeetingResult<MeetingInvitationSMSModel>>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.GetMeetingInvitationSMS(meetingId, inviterPhoneId, inviterName,
    inviterName.BytesLength(), (int)meetingType, app, app.BytesLength(), (int)urlType, IntPtr.Zero);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult<MeetingInvitationSMSModel>>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingInvitationSMSModel>() { StatusCode = -1, Message = "正在获取邀请短信中..." });
            }
        }

        public MeetingResult<string> GetMeetingQos()
        {
            var result = new MeetingResult<string>();
            IntPtr charPtr = IntPtr.Zero;
            charPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LongStringStruct>());

            try
            {
                var temp = MeetingSdkAgent.GetMeetingQos(charPtr);
                if (temp != 0)
                    throw new Exception("调用失败。");
                LongStringStruct longStringStruct = Marshal.PtrToStructure<LongStringStruct>(charPtr);
                result.Result = longStringStruct.String;
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "GetMeetingQos调用错误。");
            }
            finally
            {
                if (charPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(charPtr);
                }
            }

            return result;
        }

        public async Task<MeetingResult<IList<ParticipantModel>>> GetMicSendList()
        {
            MeetingResult<IList<ParticipantModel>> participants = new MeetingResult<IList<ParticipantModel>>();

            IntPtr participantPtr = IntPtr.Zero;

            try
            {
                int maxParticipant = 10, realParticipant = 0;

                int participantSize = Marshal.SizeOf(typeof(ParticipantInfo));

                int maxBytes = maxParticipant * participantSize;
                participantPtr = Marshal.AllocHGlobal(maxBytes);

                realParticipant = await Task.Run(() => MeetingSdkAgent.GetMicSendList(participantPtr, maxParticipant));

                List<ParticipantModel> models = new List<ParticipantModel>();

                for (int i = 0; i < realParticipant; i++)
                {
                    IntPtr pointer = (IntPtr)(participantPtr.ToInt64() + i * participantSize);
                    ParticipantInfo participantInfo = Marshal.PtrToStructure<ParticipantInfo>(pointer);

                    ParticipantModel model = new ParticipantModel()
                    {
                        AccountId = participantInfo.AccountId,
                        AccountName = participantInfo.AccountName,
                        IsHost = participantInfo.HostFlag == 1,
                        IsRaisedHand = participantInfo.IsRaiseHand.ToBool(),
                        IsSpeakerOn = participantInfo.IsSpeaking.ToBool(),
                        IsSpeaking = participantInfo.IsSpeaking.ToBool()
                    };

                    models.Add(model);
                }

                participants.Result = models;
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "GetMicSendList调用异常。");
                throw;
            }
            finally
            {
                if (participantPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(participantPtr);
                }
            }

            return participants;
        }

        public MeetingResult<IList<VideoDeviceModel>> GetVideoDevices()
        {
            MeetingResult<IList<VideoDeviceModel>> result = new MeetingResult<IList<VideoDeviceModel>>();

            IntPtr devicePtr = IntPtr.Zero;
            try
            {
                int maxDeviceCount = 10, realDeviceCount = 0;
                int deviceByte = Marshal.SizeOf(typeof(VideoDeviceInfo));

                int maxDeviceBytes = deviceByte * maxDeviceCount;
                devicePtr = Marshal.AllocHGlobal(maxDeviceBytes);

                realDeviceCount = MeetingSdkAgent.GetVideoDeviceList(devicePtr, maxDeviceCount);

                if (realDeviceCount >= 1)
                {
                    List<VideoDeviceModel> videoDevices = new List<VideoDeviceModel>();

                    for (int i = 0; i < realDeviceCount; i++)
                    {
                        IntPtr pointer = (IntPtr)(devicePtr.ToInt64() + i * deviceByte);
                        VideoDeviceInfo videoDeviceInfo = Marshal.PtrToStructure<VideoDeviceInfo>(pointer);



                        if (!string.IsNullOrEmpty(videoDeviceInfo.Name) && videoDeviceInfo.Name.ToLower() == "virtual cam")
                        {
                            continue;
                        }

                        //string log = $"name：{videoDeviceInfo.Name}, ";

                        VideoDeviceModel videoDevice = new VideoDeviceModel();
                        videoDevice.DeviceName = videoDeviceInfo.Name;
                        videoDevice.VideoFormatModels = new List<VideoFormatModel>();

                        for (int j = 0; j < videoDeviceInfo.FormatCount; j++)
                        {
                            VideoFormat videoFormat = videoDeviceInfo.Formats[j];

                            VideoFormatModel vfm = new VideoFormatModel();
                            vfm.ColorspaceName = videoFormat.ColorspaceName;
                            vfm.Colorsapce = videoFormat.Colorspace;

                            if (videoFormat.FpsCount > 0)
                            {
                                vfm.Fps = new List<int>();

                                for (int k = 0; k < videoFormat.FpsCount; k++)
                                {
                                    vfm.Fps.Add(videoFormat.Fps[k]);
                                }
                            }

                            if (videoFormat.SizeCount > 0)
                            {
                                vfm.SizeModels = new List<SizeModel>();

                                for (int k = 0; k < videoFormat.SizeCount; k++)
                                {
                                    //log += $"size：{videoFormat.Sizes[k].Width}*{videoFormat.Sizes[k].Height}\r\n";

                                    SizeModel sizeModel = new SizeModel();
                                    sizeModel.Width = videoFormat.Sizes[k].Width;
                                    sizeModel.Height = videoFormat.Sizes[k].Height;

                                    vfm.SizeModels.Add(sizeModel);
                                }
                            }

                            videoDevice.VideoFormatModels.Add(vfm);
                        }

                        videoDevices.Add(videoDevice);
                        //MeetingLogger.Logger.LogMessage(log);
                    }

                    result.Result = videoDevices;
                }
            }
            catch (Exception exception)
            {
                MeetingLogger.Logger.LogError(exception, "获取摄像头列表失败。");
            }
            finally
            {
                if (devicePtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(devicePtr);
                }
            }

            return result;
        }

        public MeetingResult<IList<string>> GetMicrophones()
        {
            MeetingResult<IList<string>> result = new MeetingResult<IList<string>>();

            IntPtr inputPtr = IntPtr.Zero;
            try
            {
                int maxInputCount = 10, realInputCount = 0;

                int inputByte = Marshal.SizeOf(typeof(StringStruct));
                int maxInputBytes = inputByte * maxInputCount;

                inputPtr = Marshal.AllocHGlobal(maxInputBytes);

                realInputCount = MeetingSdkAgent.GetAudioCaptureDeviceList(inputPtr, maxInputCount);

                List<string> microphones = new List<string>();

                for (int i = 0; i < realInputCount; i++)
                {
                    IntPtr pointer = (IntPtr)(inputPtr.ToInt64() + i * inputByte);

                    StringStruct stringStruct = Marshal.PtrToStructure<StringStruct>(pointer);

                    microphones.Add(stringStruct.String);
                }
                result.Result = microphones;
            }
            catch (Exception exception)
            {
                MeetingLogger.Logger.LogError(exception, "获取麦克风列表失败。");
            }
            finally
            {
                if (inputPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(inputPtr);
                }
            }

            return result;
        }

        public MeetingResult<IList<string>> GetLoudSpeakers()
        {
            MeetingResult<IList<string>> result = new MeetingResult<IList<string>>();

            IntPtr outputPtr = IntPtr.Zero;

            try
            {
                int maxOutputCount = 10, realOutputCount = 0;

                int outputByte = Marshal.SizeOf(typeof(StringStruct));
                int maxOutputBytes = outputByte * maxOutputCount;

                outputPtr = Marshal.AllocHGlobal(maxOutputBytes);

                realOutputCount =
                      MeetingSdkAgent.GetAudioRenderDeviceList(outputPtr, maxOutputCount);

                List<string> loudSpeakers = new List<string>();

                for (int i = 0; i < realOutputCount; i++)
                {
                    IntPtr pointer = (IntPtr)(outputPtr.ToInt64() + i * outputByte);

                    StringStruct stringStruct = Marshal.PtrToStructure<StringStruct>(pointer);

                    loudSpeakers.Add(stringStruct.String);
                }

                result.Result = loudSpeakers;
            }
            catch (Exception exception)
            {
                MeetingLogger.Logger.LogError(exception, "获取扬声器列表失败。");
            }
            finally
            {
                if (outputPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(outputPtr);
                }
            }
            return result;
        }

        public MeetingResult<string> GetSerialNo()
        {
            IntPtr ptr = IntPtr.Zero;
            var imeiResult = new MeetingResult<string>();
            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf<StringStruct>());

                int result = MeetingSdkAgent.GetSerialNo(ptr);

                if (result == 0)
                {
                    var stringStruct = Marshal.PtrToStructure<StringStruct>(ptr);
                    imeiResult.Result = stringStruct.String;
                }
            }
            catch (Exception e)
            {
                imeiResult.StatusCode = -1;
                imeiResult.Message = e.Message;

                MeetingLogger.Logger.LogError(e, e.Message);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return imeiResult;
        }

        #endregion

        #region 音视频，网络测试接口

        public Task<MeetingResult> PlayVideoTest(int colorsps, int fps, int width, int height, IntPtr previewWindow,
            string videoCapName)
        {
            string cmdName = CallbackType.play_video_test.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.AsynPlayVideoTest(colorsps, fps, width, height, previewWindow, videoCapName);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在播放视频测试中..." });
            }
        }

        public async Task<MeetingResult> StopVideoTest()
        {
            MeetingResult taskResult = new MeetingResult();

            try
            {
                await Task.Run(() => MeetingSdkAgent.StopVideoTest());
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }

            return taskResult;
        }

        public async Task<MeetingResult> PlayVideoTestYUVCB(int colorsps, int fps, int width, int height, string videoCapName)
        {
            MeetingResult taskResult = new MeetingResult();

            try
            {
                await Task.Run(
                    () => MeetingSdkAgent.AsynPlayVideoTestYUVCB(colorsps, fps, width, height, videoCapName, VideoPreviewCb));
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }

            return taskResult;
        }

        public async Task<MeetingResult> StopVideoTestYUVCB()
        {
            MeetingResult taskResult = new MeetingResult();

            try
            {
                await Task.Run(() => MeetingSdkAgent.StopVideoTestYUVCB());
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }
            return taskResult;
        }

        public Task<MeetingResult> AsynPlaySoundTest(string wavFile, string renderName)
        {
            string cmdName = CallbackType.play_video_test.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.AsynPlaySoundTest(wavFile, renderName);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;

            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在播放音频测试中..." });
            }
        }

        public MeetingResult StopPlaySoundTest()
        {
            MeetingResult taskResult = new MeetingResult();
            try
            {
                MeetingSdkAgent.StopPlaySoundTest();
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }
            return taskResult;
        }


        public MeetingResult<int> RecordSoundTest(string micName, string wavFile)
        {

            MeetingResult<int> taskResult = new MeetingResult<int>();
            try
            {
                int result = MeetingSdkAgent.RecordSoundTest(micName, wavFile);
                taskResult.StatusCode = result;
                taskResult.Message = result == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }
            return taskResult;
        }

        public MeetingResult StopRecordSoundTest()
        {
            MeetingResult<int> taskResult = new MeetingResult<int>();

            try
            {
                MeetingSdkAgent.StopRecordSoundTest();
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }

            return taskResult;
        }

        public MeetingResult AsynStartNetDiagnosticSerialCheck()
        {
            MeetingResult taskResult = new MeetingResult();

            int result = MeetingSdkAgent.AsynStartNetDiagnosticSerialCheck();

            taskResult.StatusCode = result;
            taskResult.Message = result == 0 ? "成功" : "失败";

            return taskResult;
        }

        public MeetingResult<int> StopNetBandDetect()
        {
            MeetingResult<int> taskResult = new MeetingResult<int>();

            try
            {
                int result = MeetingSdkAgent.StopNetBandDetect();

                taskResult.StatusCode = result;
                taskResult.Message = result == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }
            return taskResult;
        }

        public async Task<MeetingResult<BandWidthModel>> GetNetBandDetectResult()
        {
            MeetingResult<BandWidthModel> taskResult = new MeetingResult<BandWidthModel>();

            IntPtr bandWidthDataPtr = IntPtr.Zero;
            try
            {
                int bandWidthDataSize = Marshal.SizeOf<BandWidthData>();

                bandWidthDataPtr = Marshal.AllocHGlobal(bandWidthDataSize);

                int result = await Task.Run(() => MeetingSdkAgent.GetNetBandDetectResult(bandWidthDataPtr));


                BandWidthData bandWidthData = Marshal.PtrToStructure<BandWidthData>(bandWidthDataPtr);

                BandWidthModel bandWidthModel = new BandWidthModel();
                bandWidthModel.DownWidth = bandWidthData.downWidth;
                bandWidthModel.UpWidth = bandWidthData.upWidth;

                taskResult.Result = bandWidthModel;

                taskResult.StatusCode = result;
                taskResult.Message = result == 0 ? "成功" : "失败";

            }
            catch (Exception e)
            {
                taskResult.StatusCode = -10000;
                taskResult.Message = e.Message;
            }
            finally
            {
                FreePtr(bandWidthDataPtr);
            }

            return taskResult;
        }

        #endregion

        #region 会议相关接口

        public Task<MeetingResult> ResetMeetingPassword(int meetingId, string password)
        {
            string cmdName = CallbackType.reset_meeting_password.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.ResetMeetingPassword(meetingId, password);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在重置会议密码中..." });
            }
        }

        public Task<MeetingResult<MeetingHasPwdModel>> CheckMeetingHasPassword(int meetingId)
        {
            string cmdName = CallbackType.check_meeting_has_password.ToString();

            TaskCallback<MeetingResult<MeetingHasPwdModel>> callback = new TaskCallback<MeetingResult<MeetingHasPwdModel>>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.CheckMeetingHasPassword(meetingId);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult<MeetingHasPwdModel>>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingHasPwdModel>() { StatusCode = -1, Message = "正在检查会议是否有密码中..." });
            }
        }

        public Task<MeetingResult> CheckMeetingPasswordValid(int meetingId, string encryptedPwd)
        {
            string cmdName = CallbackType.check_meeting_password_valid.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.CheckMeetingPasswordValid(meetingId, encryptedPwd);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在检查会议密码是否有效中..." });
            }
        }

        public Task<MeetingResult<MeetingModel>> CreateMeeting(string appType)
        {
            string cmdName = CallbackType.create_meeting.ToString();
            TaskCallback<MeetingResult<MeetingModel>> callback =
                new TaskCallback<MeetingResult<MeetingModel>>(cmdName, "", 10000);


            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.CreateMeeting(appType, appType.BytesLength(), IntPtr.Zero);
                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("创建会议失败。"));
                }
                return callback.Task as Task<MeetingResult<MeetingModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingModel>() { StatusCode = -1, Message = "正在创建会议中..." });
            }
        }

        public Task<MeetingResult<MeetingModel>> CreateAndInviteMeeting(string appType, int[] inviteeList)
        {
            if (inviteeList == null)
            {
                throw new ArgumentNullException("参数inviteeList不能为空");
            }

            string cmdName = CallbackType.create_meeting.ToString();
            TaskCallback<MeetingResult<MeetingModel>> callback =
                new TaskCallback<MeetingResult<MeetingModel>>(cmdName, "", 10000);

            IntPtr invitees = IntPtr.Zero;

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                try
                {
                    int maxCount = inviteeList.Length > 10 ? 10 : inviteeList.Length;
                    invitees = Marshal.AllocHGlobal(Marshal.SizeOf<IntArrayStruct>());
                    IntArrayStruct intArrayStruct;
                    intArrayStruct.IntArray = new int[10];

                    for (int i = 0; i < maxCount; i++)
                    {
                        intArrayStruct.IntArray[i] = inviteeList[i];
                    }

                    Marshal.StructureToPtr(intArrayStruct, invitees, true);

                    int result = MeetingSdkAgent.CreateAndInviteMeeting(appType, appType.BytesLength(), invitees,
                        inviteeList.Length, IntPtr.Zero);

                    if (result != 0)
                    {
                        callback.SetException(new ResultErrorException("创建会议失败。"));
                    }
                }
                catch (Exception e)
                {
                    callback.SetException(new Exception(e.Message));
                }
                finally
                {
                    if (invitees != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(invitees);
                    }
                }
                return callback.Task as Task<MeetingResult<MeetingModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingModel>() { StatusCode = -1, Message = "正在创建会议中..." });
            }
        }

        public Task<MeetingResult<MeetingModel>> CreateDatedMeeting(string appType, DatedMeetingModel datedMeetingModel)
        {
            string cmdName = CallbackType.create_meeting.ToString();
            TaskCallback<MeetingResult<MeetingModel>> callback =
                new TaskCallback<MeetingResult<MeetingModel>>(cmdName, "", 10000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.CreateDatedMeeting(appType, appType.BytesLength(),
                    (uint)datedMeetingModel.StartTime.Year,
                    (uint)datedMeetingModel.StartTime.Month,
                    (uint)datedMeetingModel.StartTime.Day,
                    (uint)datedMeetingModel.StartTime.Hour,
                    (uint)datedMeetingModel.StartTime.Minute,
                    (uint)datedMeetingModel.StartTime.Second,
                    datedMeetingModel.EndTime.ToString(),
                    datedMeetingModel.Topic,
                    datedMeetingModel.Password);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("创建会议失败。"));
                }
                return callback.Task as Task<MeetingResult<MeetingModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingModel>() { StatusCode = -1, Message = "正在创建会议中..." });
            }
        }

        public Task<MeetingResult<MeetingModel>> CreateAndInviteDatedMeeting(string appType, DatedMeetingModel datedMeetingModel, int[] inviteeList)
        {
            if (inviteeList == null)
            {
                throw new ArgumentNullException("参数inviteeList不能为空");
            }

            string cmdName = CallbackType.create_meeting.ToString();
            TaskCallback<MeetingResult<MeetingModel>> callback =
                new TaskCallback<MeetingResult<MeetingModel>>(cmdName, "", 10000);

            IntPtr invitees = IntPtr.Zero;

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                try
                {
                    int maxCount = inviteeList.Length > 10 ? 10 : inviteeList.Length;
                    invitees = Marshal.AllocHGlobal(Marshal.SizeOf<IntArrayStruct>());
                    IntArrayStruct intArrayStruct;
                    intArrayStruct.IntArray = new int[10];

                    for (int i = 0; i < maxCount; i++)
                    {
                        intArrayStruct.IntArray[i] = inviteeList[i];
                    }

                    Marshal.StructureToPtr(intArrayStruct, invitees, true);

                    int result = MeetingSdkAgent.CreateAndInviteDatedMeeting(appType, appType.BytesLength(),
                        (uint)datedMeetingModel.StartTime.Year,
                        (uint)datedMeetingModel.StartTime.Month,
                        (uint)datedMeetingModel.StartTime.Day,
                        (uint)datedMeetingModel.StartTime.Hour,
                        (uint)datedMeetingModel.StartTime.Minute,
                        (uint)datedMeetingModel.StartTime.Second,
                        datedMeetingModel.EndTime.ToString(),
                        datedMeetingModel.Topic,
                        invitees,
                        inviteeList.Length,
                        datedMeetingModel.Password);

                    if (result != 0)
                    {
                        callback.SetException(new ResultErrorException("创建会议失败。"));
                    }
                }
                catch (Exception e)
                {
                    callback.SetException(new Exception(e.Message));
                }
                finally
                {
                    if (invitees != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(invitees);
                    }
                }

                return callback.Task as Task<MeetingResult<MeetingModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<MeetingModel>() { StatusCode = -1, Message = "正在创建会议中..." });
            }
        }

        public Task<MeetingResult> ModifyMeetingInviters(int meetingId, string appType, int smsType, int[] inviters)
        {
            if (inviters == null)
            {
                throw new ArgumentNullException("参数inviters不能为空");
            }

            string cmdName = CallbackType.modify_meeting_inviters.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            IntPtr invitees = IntPtr.Zero;

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                try
                {
                    int maxCount = inviters.Length > 10 ? 10 : inviters.Length;
                    invitees = Marshal.AllocHGlobal(Marshal.SizeOf<IntArrayStruct>());
                    IntArrayStruct intArrayStruct;
                    intArrayStruct.IntArray = new int[10];

                    for (int i = 0; i < maxCount; i++)
                    {
                        intArrayStruct.IntArray[i] = inviters[i];
                    }

                    Marshal.StructureToPtr(intArrayStruct, invitees, true);

                    int result = MeetingSdkAgent.ModifyMeetingInviters(meetingId, appType, smsType, invitees,
                        inviters.Length, IntPtr.Zero);

                    if (result != 0)
                    {
                        callback.SetException(new ResultErrorException("调用出错。"));
                    }
                }
                catch (Exception e)
                {
                    callback.SetException(new Exception(e.Message));
                }
                finally
                {
                    if (invitees != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(invitees);
                    }
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在修改会议邀请者中..." });
            }
        }

        public Task<MeetingResult<JoinMeetingModel>> JoinMeeting(int meetingId, bool autoSpeak)
        {
            string cmdName = CallbackType.join_meeting.ToString();

            TaskCallback<MeetingResult<JoinMeetingModel>> callback =
                new TaskCallback<MeetingResult<JoinMeetingModel>>(cmdName, "", 30000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.JoinMeeting(meetingId, autoSpeak, IntPtr.Zero);
                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("加入会议失败。"));
                }

                return callback.Task as Task<MeetingResult<JoinMeetingModel>>;
            }
            else
            {
                return Task.FromResult(new MeetingResult<JoinMeetingModel>() { StatusCode = -1, Message = "正在加入会议中..." });
            }
        }

        public Task<MeetingResult> LeaveMeeting()
        {
            return Task.Run(() =>
            {
                int result = MeetingSdkAgent.LeaveMeeting();

                MeetingResult taskResult = new MeetingResult()
                {
                    StatusCode = result,
                    Message = result == 0 ? "退会成功！" : "退会失败！",
                };

                return taskResult;
            });
        }

        #endregion

        #region 音视频发布和订阅

        public async Task<MeetingResult<int>> GenericSyncId()
        {
            var result = new MeetingResult<int>();
            try
            {
                var temp = await Task.Run(() => MeetingSdkAgent.GenericSyncId());
                if (temp < 0)
                    throw new Exception("调用失败。");

                result.Result = temp;
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "GenericSyncId调用错误。");
            }
            return await Task.FromResult(result);
        }
        public Task<MeetingResult<int>> PublishCameraVideo(PublishVideoModel publishVideoModel)
        {
            string cmdName = CallbackType.publish_camera_video.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult<int>> callback = new TaskCallback<MeetingResult<int>>(cmdName, uniqueId, DefaultTimeout, FreePtr(ctxPtr));
            TaskCallbackInvoker.Register(callback);
            MEETINGMANAGE_PublishCameraParam publishCameraParam = publishVideoModel.ToStruct();
            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);

                ContextData contextData = new ContextData()
                {
                    UniqueId = uniqueId,
                };

                Marshal.StructureToPtr(contextData, ctxPtr, true);
                int result = MeetingSdkAgent.PublishCameraVideo(publishCameraParam, false, ctxPtr);
                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("发布摄像头视频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "发布摄像头视频流失败。");
            }
            finally
            {
                if (publishCameraParam.sParam.vsParam.capParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishCameraParam.sParam.vsParam.capParam);
                }

                if (publishCameraParam.sParam.vsParam.encParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishCameraParam.sParam.vsParam.encParam);
                }
            }


            return (Task<MeetingResult<int>>)callback.Task;
        }
        public Task<MeetingResult<int>> PublishDataCardVideo(PublishVideoModel publishVideoModel)
        {
            string cmdName = CallbackType.publish_data_card_video.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult<int>> callback = new TaskCallback<MeetingResult<int>>(cmdName, uniqueId,
                10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            MEETINGMANAGE_PublishCameraParam publishDataCardParam = publishVideoModel.ToStruct();

            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);

                ContextData contextData = new ContextData()
                {
                    UniqueId = uniqueId,
                };

                Marshal.StructureToPtr(contextData, ctxPtr, true);

                int result = MeetingSdkAgent.PublishDataCardVideo(publishDataCardParam, false, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("发布采集卡视频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "发布采集卡视频流失败。");
            }
            finally
            {
                if (publishDataCardParam.sParam.vsParam.capParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishDataCardParam.sParam.vsParam.capParam);
                }

                if (publishDataCardParam.sParam.vsParam.encParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishDataCardParam.sParam.vsParam.encParam);
                }
            }


            return callback.Task as Task<MeetingResult<int>>;
        }
        public Task<MeetingResult<int>> PublishWinCaptureVideo(PublishVideoModel publishVideoModel)
        {
            string cmdName = CallbackType.publish_win_capture_video.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult<int>> callback = new TaskCallback<MeetingResult<int>>(cmdName, uniqueId,
                10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            MEETINGMANAGE_WinCaptureVideoParam publishWinCaptureParam = publishVideoModel.ToWinCaptureStruct();

            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);

                ContextData contextData = new ContextData()
                {
                    UniqueId = uniqueId,
                };

                Marshal.StructureToPtr(contextData, ctxPtr, true);

                int result = MeetingSdkAgent.PublishWinCaptureVideo(publishWinCaptureParam, false, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("发布本地桌面视频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "发布本地桌面视频流失败。");
            }
            finally
            {
                if (publishWinCaptureParam.sParam.vsParam.capParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishWinCaptureParam.sParam.vsParam.capParam);
                }

                if (publishWinCaptureParam.sParam.vsParam.encParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishWinCaptureParam.sParam.vsParam.encParam);
                }
            }


            return callback.Task as Task<MeetingResult<int>>;
        }
        public Task<MeetingResult<int>> PublishMicAudio(PublishAudioModel publishAudioModel)
        {
            string cmdName = CallbackType.publish_mic_audio.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult<int>> callback = new TaskCallback<MeetingResult<int>>(cmdName, uniqueId, 10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            MEETINGMANAGE_publishMicParam publishMicParam = publishAudioModel.ToStruct();

            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);

                ContextData contextData = new ContextData()
                {
                    UniqueId = uniqueId,
                };

                Marshal.StructureToPtr(contextData, ctxPtr, true);

                int result = MeetingSdkAgent.PublishMicAudio(publishMicParam, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("发布麦克风音频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "发布麦克风音频流失败。");
            }
            finally
            {
                if (publishMicParam.sParam.asParam.capParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishMicParam.sParam.asParam.capParam);
                }
                if (publishMicParam.sParam.asParam.encParam != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publishMicParam.sParam.asParam.encParam);
                }
            }


            return callback.Task as Task<MeetingResult<int>>;
        }

        public Task<MeetingResult> UnpublishCameraVideo(int resoureId)
        {
            string cmdName = CallbackType.unpublish_camera_video.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, 10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            try
            {
                ContextData cd = new ContextData()
                {
                    UniqueId = uniqueId,
                };
                ctxPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ContextData>());

                Marshal.StructureToPtr(cd, ctxPtr, true);
                int result = MeetingSdkAgent.UnpublishCameraVideo(resoureId, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("取消发布摄像头视频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "取消发布摄像头视频流失败。");
            }

            return callback.Task as Task<MeetingResult>;
        }
        public Task<MeetingResult> UnpublishDataCardVideo(int resoureId)
        {
            string cmdName = CallbackType.unpublish_data_card_video.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, 10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            try
            {
                ContextData cd = new ContextData()
                {
                    UniqueId = uniqueId,
                };
                ctxPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ContextData>());

                Marshal.StructureToPtr(cd, ctxPtr, true);
                int result = MeetingSdkAgent.UnpublishDataCardVideo(resoureId, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("取消发布采集卡视频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "取消发布采集卡视频流失败。");
            }

            return callback.Task as Task<MeetingResult>;
        }
        public Task<MeetingResult> UnpublishWinCaptureVideo(int resoureId)
        {
            string cmdName = CallbackType.unpublish_win_capture_video.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, 10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            try
            {
                ContextData cd = new ContextData()
                {
                    UniqueId = uniqueId,
                };
                ctxPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ContextData>());

                Marshal.StructureToPtr(cd, ctxPtr, true);
                int result = MeetingSdkAgent.UnpublishWinCaptureVideo(resoureId, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("取消发布本地桌面视频流失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "取消发布本地桌面视频流失败。");
            }

            return callback.Task as Task<MeetingResult>;
        }
        public Task<MeetingResult> UnpublishMicAudio(int resoureId)
        {
            string cmdName = CallbackType.unpublish_mic_audio.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;
            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, 10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            try
            {
                ContextData cd = new ContextData()
                {
                    UniqueId = uniqueId,
                };

                ctxPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ContextData>());

                Marshal.StructureToPtr(cd, ctxPtr, true);

                int result = MeetingSdkAgent.UnpublishMicAudio(resoureId, ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("取消发布麦克风失败。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "取消发布麦克风失败。");
            }

            return callback.Task as Task<MeetingResult>;
        }

        public MeetingResult SubscribeVideo(SubscribeVideoModel subscribeVideoModel)
        {
            MeetingResult taskResult = new MeetingResult();
            try
            {
                int result = MeetingSdkAgent.SubscribeVideo(subscribeVideoModel.ToStruct(), false);

                taskResult.StatusCode = result;

                if (result != 0)
                {
                    throw new Exception("订阅视频失败。");
                }

            }
            catch (Exception e)
            {
                taskResult.StatusCode = -100;
                taskResult.Message = e.Message;
            }

            return taskResult;
        }
        public MeetingResult SubscribeAudio(SubscribeAudioModel subscribeAudioModel)
        {
            MeetingResult taskResult = new MeetingResult();

            try
            {
                int result = MeetingSdkAgent.SubscribeAudio(subscribeAudioModel.ToStruct());

                taskResult.StatusCode = result;

                if (result != 0)
                {
                    throw new Exception("订阅音频失败。");
                }
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -100;
                taskResult.Message = e.Message;
            }

            return taskResult;
        }
        public MeetingResult Unsubscribe(UserUnpublishModel userUnpublishModel)
        {
            MeetingResult taskResult = new MeetingResult();

            try
            {
                int result = MeetingSdkAgent.Unsubscribe(userUnpublishModel.AccountId, userUnpublishModel.ResourceId);

                taskResult.StatusCode = result;

                if (result != 0)
                {
                    throw new Exception("取消订阅失败。");
                }
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -100;
                taskResult.Message = e.Message;
            }

            return taskResult;
        }
        public MeetingResult PushMediaFrameData(int resourceId, FrameType frameType, string frameData,
            int orientation)
        {
            MeetingResult taskResult = new MeetingResult();

            try
            {
                int temp = MeetingSdkAgent.PushMediaFrameData(resourceId, (MEETINGMANAGE_FrameType)frameType, frameData,
                    frameData.BytesLength(), orientation);

                taskResult.StatusCode = temp;
                taskResult.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                taskResult.StatusCode = -100;
                taskResult.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "PushMediaFrameData调用错误。");
            }

            return taskResult;
        }

        public MeetingResult StartYUVDataCallBack(int accountId, int resourceId)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StartYUVDataCallBack(accountId, resourceId);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StartYUVDataCallBack调用错误。");
            }
            return result;
        }
        public MeetingResult StopYUVDataCallBack(int accountId, int resourceId)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StopYUVDataCallBack(accountId, resourceId);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StopYUVDataCallBack调用错误。");
            }
            return result;
        }

        [Obsolete]
        public MeetingResult StartLocalVideoRender(int resourceId, IntPtr displayWindow, int aspx, int aspy)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StartLocalVideoRender(resourceId, displayWindow, aspx, aspy);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StartLocalVideoRender调用错误。");
            }
            return result;
        }

        [Obsolete]
        public MeetingResult StopLocalVideoRender(int resourceId)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StopLocalVideoRender(resourceId);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StopLocalVideoRender调用错误。");
            }
            return result;
        }

        [Obsolete]
        public MeetingResult StartRemoteVideoRender(int accountId, int resourceId, IntPtr displayWindow,
            int aspx, int aspy)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StartRemoteVideoRender(accountId, resourceId, displayWindow, aspx, aspy);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StartRemoteVideoRender调用错误。");
            }
            return result;
        }

        [Obsolete]
        public MeetingResult StopRemoteVideoRender(int accountId, int resourceId)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StopRemoteVideoRender(accountId, resourceId);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StopRemoteVideoRender调用错误。");
            }
            return result;
        }

        #endregion

        #region 会议中相关操作
        /// <summary>
        /// 申请发言
        /// </summary>
        /// <param name="speakerId">被抢麦人的视讯号</param>
        /// <returns></returns>
        public Task<MeetingResult> AskForSpeak(string speakerId = null)
        {
            var cmdName = CallbackType.ask_for_speak.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                speakerId = speakerId ?? "";
                var temp = MeetingSdkAgent.AskForSpeak(speakerId, speakerId.Length, IntPtr.Zero);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在申请发言中..." });
            }
        }

        /// <summary>
        /// 申请停止发言
        /// </summary>
        /// <returns></returns>
        public Task<MeetingResult> AskForStopSpeak()
        {
            var cmdName = CallbackType.ask_for_stop_speak.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);


            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.AskForStopSpeak(IntPtr.Zero);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在申请停止发言中..." });
            }
        }


        /// <summary>
        /// 发送透传消息
        /// </summary>
        /// <param name="destAccount"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public MeetingResult SendUiTransparentMsg(int destAccount, string message)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SendUiTransparentMsg(destAccount, IntPtr.Zero);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "SendUiTransparentMsg调用错误。");
            }

            return result;
        }

        public async Task<MeetingResult> AsynSendUIMsg(int msgId, int targetAccountId, string data)
        {
            var result = new MeetingResult();
            try
            {
                var temp = await Task.Run(() => MeetingSdkAgent.AsynSendUIMsg(msgId, targetAccountId, data));
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "AsynSendUIMsg。");
            }

            return result;
        }

        public Task<MeetingResult> AsynMicSendReq(int toBeSpeakerAccountId)
        {
            string cmdName = CallbackType.mic_send_response.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);


            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.AsynMicSendReq(toBeSpeakerAccountId);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在发起传麦申请中..." });
            }
        }

        /// <summary>
        /// 主持人改变会议模式
        /// </summary>
        /// <param name="toMode"></param>
        /// <returns></returns>
        public Task<MeetingResult> HostChangeMeetingMode(MeetingMode toMode)
        {
            var cmdName = CallbackType.host_change_meeting_mode.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", 5000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.HostChangeMeetingMode((int)toMode);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在更改会议模式中..." });
            }
        }

        public Task<MeetingResult> HostKickoutUser(int accountId)
        {
            var cmdName = CallbackType.host_kickout_user.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", 5000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.HostKickoutUser(accountId);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "主持人正在踢人中..." });
            }
        }

        public Task<MeetingResult> RaiseHandReq()
        {
            var cmdName = CallbackType.raise_hand_request.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", 5000);


            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.RaiseHandReq();
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在申请举手中..." });
            }
        }

        public Task<MeetingResult> AskForMeetingLock(bool bToLock)
        {
            var cmdName = CallbackType.ask_for_meeting_lock.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", 5000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.AskForMeetingLock(bToLock);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在处理会议锁中..." });
            }
        }

        public Task<MeetingResult> HostOrderOneSpeak(string toAccountId, string kickAccountId)
        {
            string cmdName = CallbackType.host_order_one_speak.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.HostOrderOneSpeak(toAccountId, toAccountId.BytesLength(), kickAccountId,
                    kickAccountId.BytesLength());

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在指定用户发言中..." });
            }
        }

        public Task<MeetingResult> HostOrderOneStopSpeak(string toAccountId)
        {
            string cmdName = CallbackType.host_order_one_stop_speak.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.HostOrderOneStopSpeak(toAccountId, toAccountId.BytesLength());

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在指定用户停止发言中..." });
            }
        }


        public Task<MeetingResult> SendAudioSpeakerStatus(bool isOpen)
        {
            var cmdName = CallbackType.send_audio_speaker_status.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.SendAudioSpeakerStatus(isOpen ? 1 : 0, IntPtr.Zero);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在发送本地扬声器状态中..." });
            }
        }

        public Task<MeetingResult> HostOrderOneDoOpration(int toUserId, HostOprateType oprateType)
        {
            var cmdName = CallbackType.host_order_one_do_opration.ToString();
            var uniqueId = Guid.NewGuid().ToString();
            IntPtr ctxPtr = IntPtr.Zero;
            var callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, DefaultTimeout, FreePtr(ctxPtr));
            try
            {
                TaskCallbackInvoker.Register(callback);

                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);
                var cd = new ContextData()
                {
                    UniqueId = uniqueId
                };
                Marshal.StructureToPtr(cd, ctxPtr, true);

                var temp = MeetingSdkAgent.HostOrderOneDoOpration(toUserId, (int)oprateType, ctxPtr);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
            }
            catch (Exception e)
            {
                callback.SetException(e);
            }
            return (Task<MeetingResult>)callback.Task;
        }

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public Task<MeetingResult> ModifyNickName(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
                throw new ArgumentException("需提供accountName", nameof(accountName));

            var cmdName = CallbackType.modify_nick_name.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                var temp = MeetingSdkAgent.ModifyNickName(accountName, accountName.BytesLength(), IntPtr.Zero);
                if (temp != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在修改昵称中..." });
            }
        }


        #endregion

        #region 录制，推流和双屏渲染

        public MeetingResult StopMp4Record(int streamId)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StopMp4Record(streamId);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "StopMp4Record调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult StartMp4Record(int streamId, string filepath)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StartMp4Record(streamId, filepath);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "StartMp4Record调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult<int> PublishLiveStream(PublishLiveStreamParameter parameter)
        {
            var result = new MeetingResult<int>();
            try
            {
                var temp = MeetingSdkAgent.PublishLiveStream(parameter.ToStruct());

                result.StatusCode = temp > 0 ? 0 : -1;
                result.Result = temp;
                result.Message = temp > 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "PublishLiveStream调用错误。");
            }
            return result;
        }

        public MeetingResult<int> UnpublishLiveStream(int streamId)
        {
            var result = new MeetingResult<int>();
            try
            {
                var temp = MeetingSdkAgent.UnpublishLiveStream(streamId);

                result.Result = temp;
                result.Message = temp > 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "UnpublishLiveStream调用错误。");
            }
            return result;
        }

        public MeetingResult StartLiveRecord(int streamId, string url)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StartLiveRecord(streamId, url);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StartLiveRecord调用错误。");
            }
            return result;
        }

        public MeetingResult StopLiveRecord(int streamId)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.StopLiveRecord(streamId);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "StopLiveRecord调用错误。");
            }
            return result;
        }

        public MeetingResult UpdateLiveStreamVideoInfo(int streamId, VideoStreamModel[] streamModels)
        {
            var result = new MeetingResult();
            IntPtr streamInfoPtr = IntPtr.Zero;

            try
            {
                int count = streamModels.Length > 20 ? 20 : streamModels.Length;

                streamInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf<VideoStreamInfoArray>());

                VideoStreamInfoArray videoStreamInfoArray;
                videoStreamInfoArray.VideoStreamInfos = new MEETINGMANAGE_VideoStreamInfo[20];

                for (int i = 0; i < count; i++)
                {
                    videoStreamInfoArray.VideoStreamInfos[i] = streamModels[i].ToStruct();
                }

                Marshal.StructureToPtr(videoStreamInfoArray, streamInfoPtr, true);

                var temp = MeetingSdkAgent.UpdateLiveStreamVideoInfo(streamId, streamInfoPtr, count);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "UpdateLiveStreamVideoInfo调用错误。");
            }
            finally
            {
                FreePtr(streamInfoPtr);
            }

            return result;
        }

        public MeetingResult UpdateLiveStreamAudioInfo(int streamId, AudioStreamModel[] streamModels)
        {
            var result = new MeetingResult();
            IntPtr streamInfoPtr = IntPtr.Zero;

            try
            {
                int count = streamModels.Length > 20 ? 20 : streamModels.Length;

                streamInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf<AudioStreamInfoArray>());

                AudioStreamInfoArray audioStreamInfoArray;
                audioStreamInfoArray.AudioStreamInfos = new MEETINGMANAGE_AudioStreamInfo[20];

                for (int i = 0; i < count; i++)
                {
                    audioStreamInfoArray.AudioStreamInfos[i] = streamModels[i].ToStruct();
                }

                Marshal.StructureToPtr(audioStreamInfoArray, streamInfoPtr, true);

                var temp = MeetingSdkAgent.UpdateLiveStreamAudioInfo(streamId, streamInfoPtr, count);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "UpdateLiveStreamVideoInfo调用错误。");
            }
            finally
            {
                FreePtr(streamInfoPtr);
            }

            return result;
        }

        public MeetingResult AddDisplayWindow(int accountId, int resourceId, IntPtr displayPtr, int aspx, int aspy)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.AddDisplayWindow(accountId, resourceId, displayPtr, aspx, aspy);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "AddDisplayWindow调用错误。");
            }
            return result;
        }

        public MeetingResult RemoveDisplayWindow(int accountId, int resourceId, IntPtr displayPtr, int aspx,
            int aspy)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.RemoveDisplayWindow(accountId, resourceId, displayPtr, aspx, aspy);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -100;
                result.Message = e.Message;
                MeetingLogger.Logger.LogError(e, "RemoveDisplayWindow调用错误。");
            }
            return result;
        }

        #endregion

        #region 自适应接口

        public MeetingResult SetAudioMixRecvBufferNum(
    int audioMaxBufferNum,
    int audioStartVadBufferNum,
    int audioStopVadBufferNum)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetAudioMixRecvBufferNum(audioMaxBufferNum,
                    audioStartVadBufferNum, audioStopVadBufferNum);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetAudioMixRecvBufferNum调用出错。");
                throw;
            }
            return result;
        }


        public MeetingResult SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate,
            int frameRate)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetLowVideoStreamCodecParam(frameWidth, frameHeight, bitrate, frameRate);

                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetLowVideoStreamCodecParam调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult SetPublishDoubleVideoStreamStatus(int isEnabled)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetPublishDoubleVideoStreamStatus(isEnabled);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetPublishDoubleVideoStreamStatus调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult SetAutoAdjustEnableStatus(int isEnabled)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetAutoAdjustEnableStatus(isEnabled);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetAutoAdjustEnableStatus调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult SetVideoClarity(int accountId, int resourceId, int clarityLevel)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetVideoClarity(accountId, resourceId, clarityLevel);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";

            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetAutoAdjustEnableStatus调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult SetVideoDisplayMode(VideoDisplayMode videoDisplayMode)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetVideoDisplayMode((int)videoDisplayMode);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetVideoDisplayMode调用出错。");
                throw;
            }
            return result;
        }

        public MeetingResult SetCurCpuInfo(int processCpu, int totalCpu)
        {
            var result = new MeetingResult();
            try
            {
                var temp = MeetingSdkAgent.SetCurCpuInfo(processCpu, totalCpu);
                result.StatusCode = temp;
                result.Message = temp == 0 ? "成功" : "失败";
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "SetCurCpuInfo调用出错。");
                throw;
            }
            return result;
        }

        #endregion

        #region HostManage相关

        public Task<MeetingResult> StartHost(string devModel, string configPath)
        {
            string cmdName = CallbackType.start_host.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.StartHost(devModel, configPath, configPath.BytesLength(), IntPtr.Zero);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在启动中..." });
            }
        }

        public MeetingResult StopHost()
        {
            var result = new MeetingResult();
            try
            {
                int temp = MeetingSdkAgent.Stop();
                if (temp != 0)
                {
                    throw new Exception("调用StopHost时出错。");
                }
                result.StatusCode = temp;
                result.Message = temp == 0 ? "停止成功" : "停止失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public Task<MeetingResult> ConnectMeetingVDN(int accountId, string accountName, string token)
        {
            string cmdName = CallbackType.connect_meeting_vdn.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", 10000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.ConnectMeetingVDN(accountId, accountName, accountName.BytesLength(), token, token.BytesLength(), IntPtr.Zero);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在连接host服务器中..." });
            }
        }


        public MeetingResult DisConnectMeetingVDN()
        {
            var result = new MeetingResult();
            try
            {
                int temp = MeetingSdkAgent.DisConnectMeetingVDN();
                if (temp != 0)
                {
                    throw new Exception("调用DisConnectMeetingVDN时出错。");
                }
                result.StatusCode = temp;
                result.Message = temp == 0 ? "断开host服务器成功" : "断开host服务器失败";
            }
            catch (Exception e)
            {
                result.StatusCode = -1;
                result.Message = e.Message;
            }
            return result;
        }


        public Task<MeetingResult> SetAccountInfo(string accountName)
        {
            string cmdName = CallbackType.set_account_info.ToString();
            var callback = new TaskCallback<MeetingResult>(cmdName, "", 10000);

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                int result = MeetingSdkAgent.SetAccountInfo(accountName, accountName.BytesLength());

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在设置用户信息中..." });
            }
        }

        public Task<MeetingResult> ConnectMeetingVDNAfterMeetingInstStarted()
        {
            string cmdName = CallbackType.connect_meeting_vdn_after_instance_started.ToString();
            string uniqueId = Guid.NewGuid().ToString();

            IntPtr ctxPtr = IntPtr.Zero;


            var callback = new TaskCallback<MeetingResult>(cmdName, uniqueId, 10000, FreePtr(ctxPtr));

            TaskCallbackInvoker.Register(callback);

            try
            {
                var size = Marshal.SizeOf<ContextData>();
                ctxPtr = Marshal.AllocHGlobal(size);

                ContextData contextData = new ContextData()
                {
                    UniqueId = uniqueId,
                };

                Marshal.StructureToPtr(contextData, ctxPtr, true);

                int result = MeetingSdkAgent.ConnectMeetingVDNAfterMeetingInstStarted(ctxPtr);

                if (result != 0)
                {
                    callback.SetException(new ResultErrorException("调用出错。"));
                }
            }
            catch (Exception e)
            {
                MeetingLogger.Logger.LogError(e, "连接host服务器失败。");
            }

            return (Task<MeetingResult>)callback.Task;
        }


        public Task<MeetingResult> MeetingInvite(int meetingId, int[] inviters)
        {
            if (inviters == null)
            {
                throw new ArgumentNullException("参数inviters不能为空");
            }

            string cmdName = CallbackType.meeting_invite.ToString();

            TaskCallback<MeetingResult> callback = new TaskCallback<MeetingResult>(cmdName, "", DefaultTimeout);

            IntPtr invitees = IntPtr.Zero;

            if (TaskCallbackInvoker.RegisterSingle(callback))
            {
                try
                {
                    int maxCount = inviters.Length > 10 ? 10 : inviters.Length;
                    invitees = Marshal.AllocHGlobal(Marshal.SizeOf<IntArrayStruct>());
                    IntArrayStruct intArrayStruct;
                    intArrayStruct.IntArray = new int[10];

                    for (int i = 0; i < maxCount; i++)
                    {
                        intArrayStruct.IntArray[i] = inviters[i];
                    }

                    Marshal.StructureToPtr(intArrayStruct, invitees, true);

                    int result = MeetingSdkAgent.MeetingInvite(meetingId, invitees, maxCount);

                    if (result != 0)
                    {
                        callback.SetException(new ResultErrorException("调用出错。"));
                    }
                }
                catch (Exception e)
                {
                    callback.SetException(new Exception(e.Message));
                }
                finally
                {
                    if (invitees != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(invitees);
                    }
                }

                return (Task<MeetingResult>)callback.Task;
            }
            else
            {
                return Task.FromResult(new MeetingResult() { StatusCode = -1, Message = "正在邀请参会中..." });
            }
        }



        #endregion

        #endregion

        private Action<Task> FreePtr(IntPtr ptr)
        {
            var action = new Action<Task>(t =>
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                    ptr = IntPtr.Zero;
                }
            });
            return action;
        }

        private static readonly ConcurrentDictionary<string, object> Locks =
            new ConcurrentDictionary<string, object>();

        //private static object GetLockObj(string name)
        //{
        //    return Locks.GetOrAdd(name, key => new object());
        //}
    }
}
