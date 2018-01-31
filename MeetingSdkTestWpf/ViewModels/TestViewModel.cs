using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.IO;
using MeetingSdk.NetAgent;
using MeetingSdk.NetAgent.Models;
using MeetingSdk.Wpf;

namespace MeetingSdkTestWpf.ViewModels
{
    public class TestViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeetingSdkAgent _sdkWrapper;
        private readonly IMeetingWindowManager _windowManager;
        public TestViewModel(IEventAggregator eventAggregator, IMeetingSdkAgent sdkWrapper)
        {
            _eventAggregator = eventAggregator;
            _sdkWrapper = sdkWrapper;
            _windowManager = IoC.Get<IMeetingWindowManager>();

            _eventAggregator.GetEvent<NetStatusNoticeEvent>().Subscribe(OnNetStatusNotice);
            _eventAggregator.GetEvent<NetCheckedEvent>().Subscribe(OnNetChecked);


            var p1 = StreamParameterProviders.GetProvider<PublishMicStreamParameter>();
            var p2 = StreamParameterProviders.GetProvider<PublishCameraStreamParameter>();

            var parameter = new PublishCameraStreamParameter();
            StreamParameterProviders.ProviderParameter(parameter);

            Started = sdkWrapper.IsStarted;


            VideoDevices = new BindableCollection<string>();
            AudioInputDevices = new BindableCollection<string>();
            AudioOutputDevices = new BindableCollection<string>();


            //var accessor = IoC.Get<IDeviceNameAccessor>();

            //string deviceName;
            //if (accessor.TryGetName(DeviceName.Camera, m => m.Option == "First", out deviceName))
            //{
            //    // deviceName   
            //}

        }

        private bool _started;

        public bool Started
        {
            get => _started;
            set
            {
                _started = value;
                this.NotifyOfPropertyChange(() => this.Started);
            }
        }

        private string _imei;

        public string Imei
        {
            get => _imei;
            set
            {
                _imei = value;
                this.NotifyOfPropertyChange(() => this.Imei);
            }
        }

        private int _meetingId;

        public int MeetingId
        {
            get => _meetingId;
            set
            {
                _meetingId = value;
                this.NotifyOfPropertyChange(() => this.MeetingId);
            }
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set
            {
                _nickName = value;
                this.NotifyOfPropertyChange(() => this.NickName);
            }
        }

        private string _netBandDetectResult;
        public string NetBandDetectResult
        {
            get => _netBandDetectResult;
            set
            {
                _netBandDetectResult = value;
                this.NotifyOfPropertyChange(() => this.NetBandDetectResult);
            }
        }


        

        public BindableCollection<string> VideoDevices { get; private set; }

        private string _selectedVideoDevice;
        public string SelectedVideoDevice
        {
            get { return _selectedVideoDevice; }
            set
            {
                _selectedVideoDevice = value;
                NotifyOfPropertyChange(() => SelectedVideoDevice);
            }
        }


        public BindableCollection<string> AudioInputDevices { get; private set; }

        private string _selectedAudioInputDevice;

        public string SelectedAudioInputDevice
        {
            get { return _selectedAudioInputDevice; }
            set
            {
                _selectedAudioInputDevice = value;
                NotifyOfPropertyChange(() => SelectedAudioInputDevice);
            }
        }



        public BindableCollection<string> AudioOutputDevices { get; private set; }

        private string _selectedAudioOutputDevice;

        public string SelectedAudioOutputDevice
        {
            get { return _selectedAudioOutputDevice; }
            set
            {
                _selectedAudioOutputDevice = value;
                NotifyOfPropertyChange(() => SelectedAudioOutputDevice);
            }
        }



        public IntPtr PreviewWindowHandle { get; set; }


        public void SetNpsUrl()
        {
            MeetingResult result = _sdkWrapper.SetNpsUrl("http://www.baidu.com", "http://www.google.com");
            ShowResult(result);
        }

        public void SetRkPath()
        {
            string path = Environment.CurrentDirectory;
            MeetingResult result = _sdkWrapper.SetRkPath(path);
            ShowResult(result);
        }

        public async void BindToken()
        {
            MeetingResult result = await _sdkWrapper.BindToken("ba0bf768-7049-41ec-9152-3783a7a9ed58", new AccountModel(67005003, "Device5"));

            ShowResult(result);
        }

        public async void Stop()
        {
            await _sdkWrapper.Stop();
        }

        public void Login()
        {
            var aa = Imei;
        }

        public void GetSerialNo()
        {
            var result = _sdkWrapper.GetSerialNo();
            ShowResult(result, result.Result);
        }

        public async Task CheckMeetExist()
        {
            try
            {
                var result = await _sdkWrapper.IsMeetingExist(MeetingId);
                ShowResult(result);
            }
            catch (Exception e)
            {

            }
        }

        public async void GetMeetingList()
        {
            try
            {
                var result = await _sdkWrapper.GetMeetingList();

                ShowResult(result, result.Result);

                //var metroWindow = App.Current.MainWindow as MetroWindow;
                //await metroWindow.ShowMessageAsync("提示", $"sttaucCode:{result.StatusCode},message:{result.Message}");
            }
            catch (Exception e)
            {

            }
        }

        public async void GetMeetingInfo()
        {
            try
            {
                var result = await _sdkWrapper.GetMeetingInfo(MeetingId);

                ShowResult(result, result.Result);

                //var metroWindow = App.Current.MainWindow as MetroWindow;
                //await metroWindow.ShowMessageAsync("提示", $"sttaucCode:{result.StatusCode},message:{result.Message},info:{JsonConvert.SerializeObject(result.Result)}");
            }
            catch (Exception e)
            {

            }
        }

        public void GetJoinMeetingInfo()
        {
            try
            {
                var result = _sdkWrapper.GetJoinMeetingInfo(MeetingId);

                ShowResult(result, result.Result);
            }
            catch (Exception e)
            {

            }
        }

        public async void GetMeetingPassword()
        {
            try
            {
                var result = await _sdkWrapper.GetMeetingPassword(MeetingId);

                ShowResult(result, result.Result);
            }
            catch (Exception e)
            {

            }
        }


        public async void ResetMeetingPassword()
        {
            try
            {
                var result = await _sdkWrapper.ResetMeetingPassword(MeetingId, "111111");

                ShowResult(result,$"密码已重置为：111111");
            }
            catch (Exception e)
            {

            }
        }


        public async void CheckMeetingHasPassword()
        {
            try
            {
                var result = await _sdkWrapper.CheckMeetingHasPassword(MeetingId);

                ShowResult(result,result.Result);
            }
            catch (Exception e)
            {

            }
        }

        public async void CheckMeetingPasswordValid()
        {
            try
            {
                var result = await _sdkWrapper.CheckMeetingPasswordValid(MeetingId, "222222");

                ShowResult(result);
            }
            catch (Exception e)
            {

            }
        }
        


        public async void GetMeetingInvitationSMS()
        {
            try
            {
                var result = await _sdkWrapper.GetMeetingInvitationSMS(MeetingId, 61001023, "吴叙", MeetingType.JustMeeting, "JIHY", InviterUrlType.Http);

                ShowResult(result, result.Result);
            }
            catch (Exception e)
            {

            }
        }

        public void GetMeetingQos()
        {
            try
            {
                var result =  _sdkWrapper.GetMeetingQos();

                ShowResult(result, result.Result);
            }
            catch (Exception e)
            {

            }
        }

        public async void ModifyName()
        {
            try
            {
                if (string.IsNullOrEmpty(NickName))
                    throw new Exception("请输入名称。");

                var result = await _sdkWrapper.ModifyNickName(NickName);

                var metroWindow = App.Current.MainWindow as MetroWindow;
                await metroWindow.ShowMessageAsync("提示", $"sttaucCode:{result.StatusCode},message:{result.Message}");
            }
            catch (Exception e)
            {

            }
        }

        public void BtnGetVideoDevices()
        {
            GetVideoDevices();
        }

        private void GetVideoDevices()
        {
            MeetingResult<IList<VideoDeviceModel>> videoDevices =
                _sdkWrapper.GetVideoDevices();

            VideoDevices.Clear();

            foreach (var videoDevice in videoDevices.Result)
            {
                VideoDevices.Add(videoDevice?.DeviceName);
            }
        }

        public  void BtnGetAudioInputDevices()
        {
             GetAudioInputDevices();
        }

        private void GetAudioInputDevices()
        {
            MeetingResult<IList<string>> mics = _sdkWrapper.GetMicrophones();
            AudioInputDevices.Clear();
            foreach (var mic in mics.Result)
            {
                AudioInputDevices.Add(mic);
            }

        }

        public void BtnGetAudioOutputDevices()
        {
            GetAudioOutputDevices();
        }

        private void GetAudioOutputDevices()
        {
            MeetingResult<IList<string>> speakers = _sdkWrapper.GetLoudSpeakers();
            AudioOutputDevices.Clear();
            foreach (var speaker in speakers.Result)
            {
                AudioOutputDevices.Add(speaker);
            }
        }


        public async void PlayVideoTest()
        {
            var result = await _sdkWrapper.PlayVideoTest(3, 15, 640, 480, PreviewWindowHandle, SelectedVideoDevice);
            ShowResult(result);
        }

        public async void StopPlayVideoTest()
        {
            var result = await _sdkWrapper.StopVideoTest();
            ShowResult(result);
        }

        public async void PlayVideoTestYUV()
        {
            var result = await _sdkWrapper.PlayVideoTestYUVCB(3, 15, 640, 480, SelectedVideoDevice);
            ShowResult(result);
        }

        public async void StopPlayVideoTestYUV()
        {
            var result = await _sdkWrapper.StopVideoTestYUVCB();
            ShowResult(result);
        }

        public async void PlaySoundTest()
        {
            try
            {
                string wavFile = Path.Combine(Environment.CurrentDirectory, "audiotest.wav");
                var result = await _sdkWrapper.AsynPlaySoundTest(string.Empty, SelectedAudioOutputDevice);
                ShowResult(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StopPlaySoundTest()
        {
            var result = _sdkWrapper.StopPlaySoundTest();
            ShowResult(result);
        }

        public void RecordSoundTest()
        {
            string wavFile = Path.Combine(Environment.CurrentDirectory, "audiotest.wav");
            var result = _sdkWrapper.RecordSoundTest(SelectedAudioInputDevice, wavFile);
            ShowResult(result);
        }

        public void StopRecordSoundTest()
        {
            var result = _sdkWrapper.StopRecordSoundTest();
            ShowResult(result);
        }

        public void StartNetCheck()
        {
            NetBandDetectResult = null;
            var result = _sdkWrapper.AsynStartNetDiagnosticSerialCheck();
            ShowResult(result);
        }

        private void OnNetStatusNotice(int obj)
        {

        }

        private async void OnNetChecked(NetType netType)
        {
            if (netType == NetType.NetConnection)
            {
                NetBandDetectResult += ("网络连通性正常\r\n");
            }

            if (netType == NetType.MeetingConnection)
            {
                NetBandDetectResult += ("会议连通性正常\r\n");
            }

            if (netType == NetType.BandDetect)
            {
                //NetBandDetectResult += ("正在检测带宽，请稍候......\r\n");
                MeetingResult<BandWidthModel> taskResult = await _sdkWrapper.GetNetBandDetectResult();
                string bandWidthInfo = $"上行带宽：{taskResult.Result.UpWidth}，下行带宽：{taskResult.Result.DownWidth}\r\n";
                NetBandDetectResult += bandWidthInfo;
            }
        }

        public void StopNetBandDetect()
        {
            NetBandDetectResult = null;
            var result = _sdkWrapper.StopNetBandDetect();
            ShowResult(result);
        }



        private void ShowResult(MeetingResult result, object info = null)
        {
            string sInfo = info == null ? "" : JsonConvert.SerializeObject(info);
            MessageBox.Show($"statusCode={result.StatusCode}, msg={result.Message}, info:{sInfo}");
        }

    }
}
