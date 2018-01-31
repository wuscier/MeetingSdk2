using System;
using System.Runtime.InteropServices;
using System.Text;
using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.NetAgent
{
    internal static class Extensions
    {
        internal static MeetingUserStreamModel ToModel(this MeetingUserStreamInfo info)
        {
            var model = new MeetingUserStreamModel();
            model.Account = new AccountModel(info.AccountId, info.AccountName);
            model.DeviceName = info.DeviceName;
            model.ResourceId = info.ResourceId;
            model.SyncGroupId = info.SynGroupId;
            model.MediaType = info.MediaType;
            return model;
        }

        internal static JoinMeetingModel ToModel(this JoinMeetingInfo info)
        {
            var model = new JoinMeetingModel()
            {
                HasLock = info.LockInfo > 0,
                AttendeeType = info.UserType == 2 ? AttendeeType.Host : AttendeeType.Normal,
                Account = new AccountModel(info.PresenterId, info.PresenterName),
                LiveStatus = info.LiveStatus == 2 ? LiveStatus.Normal : LiveStatus.IsLive,
                MeetingMode = info.MeetingStyle == 2 ? MeetingMode.HostMode : MeetingMode.FreeMode
            };
            var size = Marshal.SizeOf<MeetingSpeakerInfo>();
            for (int i = 0; i < info.SpeakerCount; i++)
            {
                var speakerInfo = Marshal.PtrToStructure<MeetingSpeakerInfo>(info.SpeakersPtr + (i * size));
                var speakerModel = new MeetingSpeakerModel()
                {
                    Account = new AccountModel(speakerInfo.AccountId, speakerInfo.AccountName)
                };
                for (int j = 0; j < speakerInfo.StreamCount; j++)
                {
                    speakerModel.MeetingUserStreamInfos.Add(speakerInfo.StreamInfos[j].ToModel());
                }
                model.MeetingSpeakerModels.Add(speakerModel);
            }
            return model;
        }

        internal static MEETINGMANAGE_PublishCameraParam ToStruct(this PublishVideoModel model)
        {
            MEETINGMANAGE_PublishCameraParam publishCameraParam = new MEETINGMANAGE_PublishCameraParam();

            try
            {
                //视频采集参数
                MEETINGMANAGEVideoCapParam videoCapParam = new MEETINGMANAGEVideoCapParam()
                {
                    left = model.VideoSendModel.CaptureModel.Left,
                    right = model.VideoSendModel.CaptureModel.Right,
                    top = model.VideoSendModel.CaptureModel.Top,
                    bottom = model.VideoSendModel.CaptureModel.Bottom,
                    fps = model.VideoSendModel.CaptureModel.Fps,
                    capWinHandle = model.VideoSendModel.CaptureModel.CapWinHandle,
                    colorSpace =
                    (MEETINGMANAGE_VideoColorSpace)model.VideoSendModel.CaptureModel.VideoColorSpace,
                };

                publishCameraParam.sParam.vsParam.capParam = Marshal.AllocHGlobal(Marshal.SizeOf(videoCapParam));

                Marshal.StructureToPtr(videoCapParam, publishCameraParam.sParam.vsParam.capParam, true);

                publishCameraParam.sParam.vsParam.fillMode =
                  (MEETINGMANAGE_DisplayFillMode)model.VideoSendModel.DisplayFillMode;
                publishCameraParam.sParam.vsParam.displayWindow = model.VideoSendModel.DisplayWindow; //本地预览的窗口句柄

                //视频编码参数
                publishCameraParam.sParam.vsParam.encParam = IntPtr.Zero;

                MEETINGMANAGEVideoEncParam videoEncParam = new MEETINGMANAGEVideoEncParam()
                {
                    bitrate = model.VideoSendModel.EncodeModel.Bitrate,
                    fps = model.VideoSendModel.EncodeModel.Fps,
                    width = model.VideoSendModel.EncodeModel.Width,
                    height = model.VideoSendModel.EncodeModel.Height,
                    level =
                    (MEETINGMANAGEVideoCodecLevel)model.VideoSendModel.EncodeModel.VideoCodeLevel,
                    codecID =
                    (MEETINGMANAGEVideoCodecID)model.VideoSendModel.EncodeModel.VideoCodeId,
                    codecType =
                    (MEETINGMANAGEVideoCodecType)model.VideoSendModel.EncodeModel.VideoCodeType,
                };

                publishCameraParam.sParam.vsParam.encParam = Marshal.AllocHGlobal(Marshal.SizeOf(videoEncParam));

                Marshal.StructureToPtr(videoEncParam, publishCameraParam.sParam.vsParam.encParam, true);


                publishCameraParam.sParam.sourceType = (MEETINGMANAGESourceType)model.VideoSendModel.SourceType;
                publishCameraParam.sParam.sourceName = model.VideoSendModel.SourceName; //摄像头名称

                //媒体类型
                publishCameraParam.mediaType = (MEETINGMANAGE_MediaType)model.MediaType;

                //媒体流类型
                publishCameraParam.sType = (MEETINGMANAGE_StreamType)model.StreamType;

                publishCameraParam.avSynGroupID = model.AvSyncGroupId;
                publishCameraParam.sParam.extraInfo = model.VideoSendModel.ExtraInfo;

                publishCameraParam.transParam.checkRetransSendCount = model.VideoTransModel.CheckRetransSendCount;
                publishCameraParam.transParam.checkSendCount = model.VideoTransModel.CheckSendCount;
                publishCameraParam.transParam.dataResendCount = model.VideoTransModel.DataResendCount;
                publishCameraParam.transParam.dataRetransSendCount = model.VideoTransModel.DataRetransSendCount;
                publishCameraParam.transParam.dataSendCount = model.VideoTransModel.DataSendCount;
                publishCameraParam.transParam.delayTimeWinsize = model.VideoTransModel.DelayTimeWinsize;
                publishCameraParam.transParam.fecCheckCount = model.VideoTransModel.FecCheckCount;
                publishCameraParam.transParam.fecDataCount = model.VideoTransModel.FecDataCount;
            }
            catch (Exception e)
            {
                throw new Exception($"发布视频流结构转换失败。{e.Message}");
            }

            return publishCameraParam;
        }

        internal static MEETINGMANAGE_WinCaptureVideoParam ToWinCaptureStruct(this PublishVideoModel model)
        {
            MEETINGMANAGE_WinCaptureVideoParam publishCameraParam = new MEETINGMANAGE_WinCaptureVideoParam();

            try
            {
                //视频采集参数
                MEETINGMANAGEVideoCapParam videoCapParam = new MEETINGMANAGEVideoCapParam()
                {
                    left = model.VideoSendModel.CaptureModel.Left,
                    right = model.VideoSendModel.CaptureModel.Right,
                    top = model.VideoSendModel.CaptureModel.Top,
                    bottom = model.VideoSendModel.CaptureModel.Bottom,
                    fps = model.VideoSendModel.CaptureModel.Fps,
                    capWinHandle = model.VideoSendModel.CaptureModel.CapWinHandle,
                    colorSpace =
                    (MEETINGMANAGE_VideoColorSpace)model.VideoSendModel.CaptureModel.VideoColorSpace,
                };

                publishCameraParam.sParam.vsParam.capParam = Marshal.AllocHGlobal(Marshal.SizeOf(videoCapParam));

                Marshal.StructureToPtr(videoCapParam, publishCameraParam.sParam.vsParam.capParam, true);

                publishCameraParam.sParam.vsParam.fillMode =
                  (MEETINGMANAGE_DisplayFillMode)model.VideoSendModel.DisplayFillMode;
                publishCameraParam.sParam.vsParam.displayWindow = model.VideoSendModel.DisplayWindow; //本地预览的窗口句柄

                //视频编码参数
                publishCameraParam.sParam.vsParam.encParam = IntPtr.Zero;

                MEETINGMANAGEVideoEncParam videoEncParam = new MEETINGMANAGEVideoEncParam()
                {
                    bitrate = model.VideoSendModel.EncodeModel.Bitrate,
                    fps = model.VideoSendModel.EncodeModel.Fps,
                    width = model.VideoSendModel.EncodeModel.Width,
                    height = model.VideoSendModel.EncodeModel.Height,
                    level =
                    (MEETINGMANAGEVideoCodecLevel)model.VideoSendModel.EncodeModel.VideoCodeLevel,
                    codecID =
                    (MEETINGMANAGEVideoCodecID)model.VideoSendModel.EncodeModel.VideoCodeId,
                    codecType =
                    (MEETINGMANAGEVideoCodecType)model.VideoSendModel.EncodeModel.VideoCodeType,
                };

                publishCameraParam.sParam.vsParam.encParam = Marshal.AllocHGlobal(Marshal.SizeOf(videoEncParam));

                Marshal.StructureToPtr(videoEncParam, publishCameraParam.sParam.vsParam.encParam, true);


                publishCameraParam.sParam.sourceType = (MEETINGMANAGESourceType)model.VideoSendModel.SourceType;
                publishCameraParam.sParam.sourceName = model.VideoSendModel.SourceName; //摄像头名称

                //媒体类型
                publishCameraParam.mediaType = (MEETINGMANAGE_MediaType)model.MediaType;

                //媒体流类型
                publishCameraParam.sType = (MEETINGMANAGE_StreamType)model.StreamType;

                publishCameraParam.avSynGroupID = model.AvSyncGroupId;
                publishCameraParam.sParam.extraInfo = model.VideoSendModel.ExtraInfo;

                publishCameraParam.transParam.checkRetransSendCount = model.VideoTransModel.CheckRetransSendCount;
                publishCameraParam.transParam.checkSendCount = model.VideoTransModel.CheckSendCount;
                publishCameraParam.transParam.dataResendCount = model.VideoTransModel.DataResendCount;
                publishCameraParam.transParam.dataRetransSendCount = model.VideoTransModel.DataRetransSendCount;
                publishCameraParam.transParam.dataSendCount = model.VideoTransModel.DataSendCount;
                publishCameraParam.transParam.delayTimeWinsize = model.VideoTransModel.DelayTimeWinsize;
                publishCameraParam.transParam.fecCheckCount = model.VideoTransModel.FecCheckCount;
                publishCameraParam.transParam.fecDataCount = model.VideoTransModel.FecDataCount;
            }
            catch (Exception e)
            {
                throw new Exception($"本地桌面视频流结构转换失败。{e.Message}");
            }

            return publishCameraParam;
        }


        internal static MEETINGMANAGE_publishMicParam ToStruct(this PublishAudioModel model)
        {
            MEETINGMANAGE_publishMicParam publishMicParam = new MEETINGMANAGE_publishMicParam();

            try
            {
                publishMicParam.mediaType = (MEETINGMANAGE_MediaType)model.MediaType;
                publishMicParam.sType = (MEETINGMANAGE_StreamType)model.StreamType;
                publishMicParam.transParam.checkRetransSendCount = model.TransModel.CheckRetransSendCount;
                publishMicParam.transParam.checkSendCount = model.TransModel.CheckSendCount;
                publishMicParam.transParam.dataResendCount = model.TransModel.DataResendCount;
                publishMicParam.transParam.dataRetransSendCount = model.TransModel.DataRetransSendCount;
                publishMicParam.transParam.dataSendCount = model.TransModel.DataSendCount;
                publishMicParam.transParam.delayTimeWinsize = model.TransModel.DelayTimeWinsize;
                publishMicParam.transParam.fecCheckCount = model.TransModel.FecCheckCount;
                publishMicParam.transParam.fecDataCount = model.TransModel.FecDataCount;

                publishMicParam.sParam.sourceName = model.AudioSendModel.SourceName;
                publishMicParam.sParam.sourceType = (MEETINGMANAGESourceType)model.AudioSendModel.SourceType;
                publishMicParam.sParam.asParam.isMix = model.AudioSendModel.IsMix;

                publishMicParam.sParam.extraInfo = model.AudioSendModel.ExtraInfo;
                publishMicParam.avSynGroupID = model.AvSyncGroupId;


                MEETINGMANAGEAudioCapParam audioCapParam = new MEETINGMANAGEAudioCapParam()
                {
                    bitspersample = model.AudioSendModel.AudioCapModel.BitsPerSample,
                    channels = model.AudioSendModel.AudioCapModel.Channels,
                    samplerate = model.AudioSendModel.AudioCapModel.SampleRate,

                };

                MEETINGMANAGEAudioEncParam audioEncParam = new MEETINGMANAGEAudioEncParam()
                {
                    bitrate = model.AudioSendModel.AudioEncModel.Bitrate,
                    bitspersample = model.AudioSendModel.AudioEncModel.BitsPerSample,
                    channels = model.AudioSendModel.AudioEncModel.Channels,
                    codecID = (MEETINGMANAGEAudioCodecID)model.AudioSendModel.AudioEncModel.AudioCodeId,
                    samplerate = model.AudioSendModel.AudioEncModel.SampleRate,

                };

                publishMicParam.sParam.asParam.capParam = Marshal.AllocHGlobal(Marshal.SizeOf(audioCapParam));
                publishMicParam.sParam.asParam.encParam = Marshal.AllocHGlobal(Marshal.SizeOf(audioEncParam));

                Marshal.StructureToPtr(audioCapParam, publishMicParam.sParam.asParam.capParam, true);
                Marshal.StructureToPtr(audioEncParam, publishMicParam.sParam.asParam.encParam, true);
            }
            catch (Exception exception)
            {
                throw new Exception($"发布音频流结构转换失败。{exception}");
            }

            return publishMicParam;
        }

        internal static MEETINGMANAGE_subscribeVideoParam ToStruct(this SubscribeVideoModel model)
        {
            MEETINGMANAGE_subscribeVideoParam subscribeVideoParam = new MEETINGMANAGE_subscribeVideoParam();

            subscribeVideoParam.AVSynGroupID = model.AvSyncGroupId;
            subscribeVideoParam.mediaType = (MEETINGMANAGE_MediaType)model.MediaType;
            subscribeVideoParam.resourceID = model.ResourceId;


            subscribeVideoParam.sParam.vrParam.displayWindow = model.VideoRecvModel.DisplayWindow;

            subscribeVideoParam.sParam.vrParam.fillMode =
              (MEETINGMANAGE_DisplayFillMode)model.VideoRecvModel.DisplayFillMode;

            subscribeVideoParam.sType = (MEETINGMANAGE_StreamType)model.StreamType;
            subscribeVideoParam.userid = model.UserId;

            subscribeVideoParam.transParam.checkRetransSendCount = model.TransModel.CheckRetransSendCount;
            subscribeVideoParam.transParam.checkSendCount = model.TransModel.CheckSendCount;
            subscribeVideoParam.transParam.dataResendCount = model.TransModel.DataResendCount;
            subscribeVideoParam.transParam.dataRetransSendCount = model.TransModel.DataRetransSendCount;
            subscribeVideoParam.transParam.dataSendCount = model.TransModel.DataSendCount;
            subscribeVideoParam.transParam.delayTimeWinsize = model.TransModel.DelayTimeWinsize;
            subscribeVideoParam.transParam.fecCheckCount = model.TransModel.FecCheckCount;
            subscribeVideoParam.transParam.fecDataCount = model.TransModel.FecDataCount;

            return subscribeVideoParam;
        }

        internal static MEETINGMANAGE_subscribeAudioParam ToStruct(this SubscribeAudioModel model)
        {
            MEETINGMANAGE_subscribeAudioParam subscribeAudioParam = new MEETINGMANAGE_subscribeAudioParam();

            subscribeAudioParam.mediaType = (MEETINGMANAGE_MediaType)model.MediaType;
            subscribeAudioParam.AVSynGroupID = model.AvSyncGroupId;
            subscribeAudioParam.resourceID = model.ResourceId;
            subscribeAudioParam.sType = (MEETINGMANAGE_StreamType)model.StreamType;
            subscribeAudioParam.userid = model.UserId;

            subscribeAudioParam.transParam.checkRetransSendCount = model.TransModel.CheckRetransSendCount;
            subscribeAudioParam.transParam.checkSendCount = model.TransModel.CheckSendCount;
            subscribeAudioParam.transParam.dataResendCount = model.TransModel.DataResendCount;
            subscribeAudioParam.transParam.dataRetransSendCount = model.TransModel.DataRetransSendCount;
            subscribeAudioParam.transParam.dataSendCount = model.TransModel.DataSendCount;
            subscribeAudioParam.transParam.delayTimeWinsize = model.TransModel.DelayTimeWinsize;
            subscribeAudioParam.transParam.fecCheckCount = model.TransModel.FecCheckCount;
            subscribeAudioParam.transParam.fecDataCount = model.TransModel.FecDataCount;

            subscribeAudioParam.sParam.sourceType = (MEETINGMANAGESourceType)model.AudioRecvModel.SourceType;
            subscribeAudioParam.sParam.sourceName = model.AudioRecvModel.SourceName;
            subscribeAudioParam.sParam.arParam.isMix = model.AudioRecvModel.IsMix;

            return subscribeAudioParam;
        }

        internal static MEETINGMANAGE_PubLiveStreamParam ToStruct(this PublishLiveStreamParameter model)
        {
            MEETINGMANAGE_PubLiveStreamParam param = new MEETINGMANAGE_PubLiveStreamParam();

            param.mediaType = (MEETINGMANAGE_MediaType)model.MediaType;
            param.sType = (MEETINGMANAGE_StreamType)model.StreamType;

            param.sParam.Url1 = model.LiveParameter.Url1;
            param.sParam.Url2 = model.LiveParameter.Url2;
            param.sParam.vBitrate = model.LiveParameter.VideoBitrate;
            param.sParam.width = model.LiveParameter.Width;
            param.sParam.aBitrate = model.LiveParameter.AudioBitrate;
            param.sParam.bitspersample = model.LiveParameter.BitsPerSample;
            param.sParam.channels = model.LiveParameter.Channels;
            param.sParam.filepath = model.LiveParameter.FilePath;
            param.sParam.height = model.LiveParameter.Height;
            param.sParam.isLive = model.LiveParameter.IsLive ? 1 : 0;
            param.sParam.isRecord = model.LiveParameter.IsRecord ? 1 : 0;
            param.sParam.samplerate = model.LiveParameter.SampleRate;

            return param;
        }

        internal static MEETINGMANAGE_VideoStreamInfo ToStruct(this VideoStreamModel model)
        {
            MEETINGMANAGE_VideoStreamInfo streamInfo = new MEETINGMANAGE_VideoStreamInfo()
            {
                height = model.Height,
                streamID = model.StreamId,
                width = model.Width,
                xPos = model.X,
                yPos = model.Y,
                userid = model.AccountId,
                videoType = (MEETINGMANAGE_MixVideoType)model.VideoType,
            };

            return streamInfo;
        }

        internal static MEETINGMANAGE_AudioStreamInfo ToStruct(this AudioStreamModel model)
        {
            MEETINGMANAGE_AudioStreamInfo streamInfo = new MEETINGMANAGE_AudioStreamInfo()
            {
                bitspersample = model.BitsPerSameple,
                channels = model.Channels,
                samplerate = model.SampleRate,
                streamID = model.StreamId,
                userid = model.AccountId,
            };

            return streamInfo;
        }


        public static int BytesLength(this string input)
        {
            if (input == null)
                return 0;
            return Encoding.Default.GetBytes(input).Length;
        }

        public static bool ToBool(this byte input)
        {
            return input == 1;
        }


        private static readonly DateTime Dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 返回距1970年以来的毫秒数
        /// </summary>
        /// <param name="dt">当前系统本地时间</param>
        /// <returns></returns>
        public static long GetTimestamp(this DateTime dt)
        {
            if (dt == DateTime.MinValue)
                dt = DateTime.Now;
            return (long)Math.Round(dt.ToUniversalTime().Subtract(Dt1970).TotalMilliseconds, 0);
        }

        /// <summary>
        /// 根据1970年以来的毫秒数设置当前日期
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this long timestamp)
        {
            return Dt1970.AddMilliseconds(timestamp).ToLocalTime();
        }
    }
}
