#include "MeetingSdkClient.h"
#include "MeetingManageCB.h"
#include "MeetingManage.h"
#include "HostManage.h"


MeetingSdkClient* MeetingSdkClient::m_instance = NULL;
MeetingSdkClient::MeetingSdkClient()
{
	m_cb = NULL;
	m_callback = new MeetingManageCB(NULL);
	m_manage = CreateMeetingManageObject(m_callback);
	m_hostCallback = new HostManageCB(NULL);
	m_host = CreateHostManageObject(m_hostCallback);
}

MeetingSdkClient::~MeetingSdkClient()
{
	if (m_callback)
	{
		delete m_callback;
		m_callback = NULL;
	}
	if (m_manage)
	{
		delete m_manage;
		m_manage = NULL;
	}

	if (m_hostCallback)
	{
		delete m_hostCallback;
		m_hostCallback = NULL;
	}
	if (m_host)
	{
		delete m_host;
		m_host = NULL;
	}
}

MeetingSdkClient* MeetingSdkClient::Instance() 
{
	if (m_instance == NULL)
	{
		m_instance = new MeetingSdkClient();
	}
	return m_instance;
}

void MeetingSdkClient::DestroyInstance() 
{
	if (m_instance)
	{
		delete m_instance;
		m_instance = NULL;
	}
}

void MeetingSdkClient::SetCallback(PFunc_Callback cb)
{
	m_callback->SetCallback(cb);
	m_hostCallback->SetCallback(cb);
}

int MeetingSdkClient::Start(const char * devmodel, char * configPath, int pathLen, Context context)
{
	return m_manage->Start(devmodel, configPath, pathLen, context);
}

int MeetingSdkClient::SetNpsUrl(StringStruct * npsUrlList, int urlSize)
{
	char ** list = new char *[urlSize];
	for(int i=0;i<urlSize;i++)
	{
		list[i] = new char[256];
		strcpy_s(list[i], 256, npsUrlList[i].string);
	}
	
	int result = 0;
	try {
		result = m_manage->SetNpsUrl(list, urlSize);
	}
	catch (...)
	{

	}

	for (int i = 0; i < urlSize; i++)
	{
		delete list[i];
		list[i] = nullptr;
	}
	return result;
}

int MeetingSdkClient::Stop() 
{
	return m_manage->Stop();
}

int MeetingSdkClient::Login(const char * nube, int nubeLen, const char * pass, int passLen, const char * deviceType, int dtLen, Context context)
{
	return m_manage->Login(nube, nubeLen, pass, passLen, deviceType, dtLen, context);
}

int MeetingSdkClient::LoginThirdParty(const char* nube, const char* appkey, const char * uid, Context context) {
	return m_manage->Login(nube, appkey, uid, context);
}

int MeetingSdkClient::LoginViaImei(const char * imei, int imeiLen, Context context)
{
	return m_manage->Login(imei, imeiLen, context);
}

int MeetingSdkClient::IsMeetingExist(int meetingId, Context context)
{
	return m_manage->IsMeetingExist(meetingId, context);
}

int MeetingSdkClient::GetMeetingList(Context context)
{
	return m_manage->GetMeetingList(context);
}

int MeetingSdkClient::GetMeetingInfo(int meetId, Context context)
{
	return m_manage->GetMeetingInfo(meetId, context);
}

int MeetingSdkClient::GetAudioCaptureDeviceList(StringStruct* devicelist, int listsize)
{
	char ** devList = new char *[listsize];

	for (int i = 0; i < listsize; i++)
	{
		devList[i] = new char[256];
	}

	int realSize = m_manage->GetAudioCaptureDeviceList(devList, listsize);

	for (int j = 0; j < realSize; j++) {
		strcpy_s(devicelist[j].string, 256, devList[j]);
	}

	for (int k = 0; k < 10; k++) {
		delete devList[k];
		devList[k] = NULL;
	}

	return realSize;
}

int MeetingSdkClient::GetAudioRenderDeviceList(StringStruct* devicelist, int listsize)
{
	char ** devList = new char *[listsize];

	for (int i = 0; i < listsize; i++) {
		devList[i] = new char[256];
	}

	int realSize = m_manage->GetAudioRenderDeviceList(devList, listsize);

	for (int j = 0; j < realSize; j++) {
		strcpy_s(devicelist[j].string, 256, devList[j]);
	}

	for (int k = 0; k < 10; k++) {
		delete devList[k];
		devList[k] = NULL;
	}

	return realSize;
}

int MeetingSdkClient::GetVideoDeviceList(MEETINGMANAGE_VideoDeviceInfo * devInfo, int maxCount)
{
	if (devInfo == NULL || maxCount <= 0) {
		return -1;
	}

	MEETINGMANAGE_VideoDeviceInfo *videoDeviceList = new MEETINGMANAGE_VideoDeviceInfo[maxCount];

	memset(videoDeviceList, 0, sizeof(MEETINGMANAGE_VideoDeviceInfo)*maxCount);

	int realSize = m_manage->GetVideoDeviceList(videoDeviceList, maxCount);

	for (int i = 0; i < realSize; i++)
	{
		memcpy(devInfo + i, &videoDeviceList[i], sizeof(MEETINGMANAGE_VideoDeviceInfo));
	}

	delete[]videoDeviceList;

	return realSize;
}

int MeetingSdkClient::CreateMeeting(char * appType, int typeLen, Context context) {
	return m_manage->CreateMeeting(appType, typeLen, context);
}

int MeetingSdkClient::JoinMeeting(int meetingId, bool autoSpeak, Context context) {
	return m_manage->JoinMeeting(meetingId, autoSpeak, context);
}

int MeetingSdkClient::GetJoinMeetingInfo(int meetingId, JoinMeetingInfo * joinMeetingInfo)
{
	return m_manage->GetJoinMeetingInfo(meetingId, joinMeetingInfo);
}

int MeetingSdkClient::GenericSyncId()
{
	return m_manage->GenericSyncId();
}

int MeetingSdkClient::LeaveMeeting() {
	return m_manage->LeaveMeeting();
}

int MeetingSdkClient::PublishCameraVideo(MEETINGMANAGE_PublishCameraParam param,
	bool isNeedCallBackMedia, Context context) {
	return m_manage->PublishCameraVideo(&param, isNeedCallBackMedia, context);
}

int MeetingSdkClient::PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam param,
	bool isNeedCallBackMedia, Context context) {
	return m_manage->PublishDataCardVideo(&param, false, context);
}

int MeetingSdkClient::PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam param,
	bool isNeedCallBackMedia, Context context) {
	return m_manage->PublishWinCaptureVideo(&param, false, context);
}

int MeetingSdkClient::PublishMicAudio(MEETINGMANAGE_publishMicParam param,
	Context context) {
	return m_manage->PublishMicAudio(&param, context);

}


int MeetingSdkClient::UnpublishCameraVideo(int resourceId, Context context) {
	return m_manage->UnpublishCameraVideo(resourceId, context);
}

int MeetingSdkClient::UnpublishDataCardVideo(int resourceId, Context context) {
	return m_manage->UnpublishDataCardVideo(resourceId, context);

}

int MeetingSdkClient::UnpublishWinCaptureVideo(int resourceId, Context context) {
	return m_manage->UnpublishWinCaptureVideo(resourceId, context);

}

int MeetingSdkClient::UnpublishMicAudio(int resourceId, Context context) {
	return m_manage->UnpublishMicAudio(resourceId, context);

}

int	MeetingSdkClient::SubscribeVideo(MEETINGMANAGE_subscribeVideoParam param,
	bool isNeedCallBackMedia) {
	return m_manage->SubscribeVideo(&param, isNeedCallBackMedia);
}

int MeetingSdkClient::SubscribeAudio(MEETINGMANAGE_subscribeAudioParam param) {
	return m_manage->SubscribeAudio(&param);
}


int	MeetingSdkClient::Unsubscribe(int accountId, int resourceId) {
	return m_manage->Unsubscribe(accountId, resourceId);
}


int MeetingSdkClient::AskForSpeak(char * speakerId, int speakerIdLen, Context context) {
	return m_manage->AskForSpeak(speakerId, speakerIdLen, context);
}

int MeetingSdkClient::AskForStopSpeak(Context context) {
	return m_manage->AskForStopSpeak(context);
}

int MeetingSdkClient::GetSpeakerList(MeetingSpeakerInfo * speakerInfos, int maxCount) 
{
	return m_manage->GetSpeakerList(speakerInfos, maxCount);
}

int MeetingSdkClient::GetSpeakerInfo(int accountId, MeetingSpeakerInfo * speakerInfo)
{
	return m_manage->GetSpeakerInfo(accountId, speakerInfo);
}

int MeetingSdkClient::GetMeetingQos(LongStringStruct * outdata)
{
	char qos[4096] = {};
	int length = 4096;
	int temp = m_manage->GetMeetingQos(qos, length);

	strcpy_s(outdata->string, sizeof(outdata->string), qos);

	return temp;
}


int MeetingSdkClient::StartLocalVideoRender(int resourceId, void* displayWindow, int aspx, int aspy) 
{
	return m_manage->StartLocalVideoRender(resourceId, displayWindow, aspx, aspy);
}

int MeetingSdkClient::StopLocalVideoRender(int resourceId) 
{
	return m_manage->StopLocalVideoRender(resourceId);
}

int MeetingSdkClient::StartRemoteVideoRender(int accountId, int resourceId, void* displayWindow, int aspx, int aspy) 
{
	return m_manage->StartRemoteVideoRender(accountId, resourceId, displayWindow, aspx, aspy);
}

int MeetingSdkClient::StopRemoteVideoRender(int accountId, int resourceId) 
{
	return m_manage->StopRemoteVideoRender(accountId, resourceId);
}

int MeetingSdkClient::StartYUVDataCallBack(int accountId, int resourceId)
{
	return m_manage->StartYUVDataCallBack(accountId, resourceId);
}

int MeetingSdkClient::StopYUVDataCallBack(int accountId, int resourceId)
{
	return m_manage->StopYUVDataCallBack(accountId, resourceId);
}

int MeetingSdkClient::SendUiTransparentMsg(int destAccount, StringStruct * data) 
{
	return m_manage->SendUiTransparentMsg(destAccount, data->string, 256);
}


int MeetingSdkClient::AsynSendUIMsg(int msgId, int dstUserAccount, const char *msgData) {
	return m_manage->AsynSendUIMsg(msgId, dstUserAccount, msgData);
}

int MeetingSdkClient::HostChangeMeetingMode(int toMode)
{
	return m_manage->HostChangeMeetingMode(toMode);
}

int MeetingSdkClient::GetCurMeetingMode()
{
	int temp = 0;
	m_manage->GetCurMeetingMode(temp);
	return temp;
}

int MeetingSdkClient::RaiseHandReq() 
{
	return m_manage->RaiseHandReq();
}

int MeetingSdkClient::HostKickoutUser(int accountId)
{
	return m_manage->HostKickoutUser(accountId);
}

int MeetingSdkClient::AskForMeetingLock(bool bToLock)
{
	return m_manage->AskForMeetingLock(bToLock);
}

bool MeetingSdkClient::GetMeetingLockStatus()
{
	return m_manage->GetMeetingLockStatus();
}

int MeetingSdkClient::SendAudioSpeakerStatus(int isOpen, Context context) {
	return m_manage->SendAudioSpeakerStatus(isOpen, context);
}

int MeetingSdkClient::GetMeetingInvitationSMS(int meetId, int inviterPhoneId, const char* inviterName, int inviterNameLen, int meetingType, const char* app, int appLen, int urlType, Context context)
{
	return m_manage->GetMeetingInvitationSMS(meetId, inviterPhoneId, inviterName, inviterNameLen, meetingType, app, appLen, urlType, context);
}

int MeetingSdkClient::GetSpeakerVideoStreamParam(int accountId, int resourceID, SpeakerVideoStreamParamData * data)
{
	int width;
	int height;
	int result = m_manage->GetSpeakerVideoStreamParam(accountId, resourceID, width, height);
	data->width = width;
	data->height = height;
	return result;
}

int MeetingSdkClient::HostOrderOneDoOpration(int toUserId, int oprateType, Context context)
{
	return m_manage->HostOrderOneDoOpration(toUserId, oprateType, context);
}

int MeetingSdkClient::ModifyNickName(const char * accountName, int nameLen, Context context)
{
	return m_manage->ModifyNickName(accountName, nameLen, context);
}

int MeetingSdkClient::GetParticipants(ParticipantInfo* participants, int maxCount)
{
	return m_manage->GetParticipants(participants, maxCount);
}

int MeetingSdkClient::BindToken(const char* token, int tokenLen,
	int accountId, const char* accountName,
	int accountNameLen, Context context)
{
	return m_manage->BindToken(token, tokenLen, accountId, accountName, accountNameLen, context);
}

int MeetingSdkClient::AsynPlayVideoTest(int colorsps, int fps,
	int width, int height, void * previewWindow, char videoCapName[256]) {
	return m_manage->AsynPlayVideoTest(colorsps, fps, width, height, previewWindow, videoCapName);
}

void MeetingSdkClient::StopVideoTest() {
	m_manage->StopVideoTest();
}

int MeetingSdkClient::AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
	int height, char videoCapName[256], FUN_VIDEO_PREVIEW fun) {
	return m_manage->AsynPlayVideoTestYUVCB(colorsps, fps, width, height, videoCapName, fun);
}

int MeetingSdkClient::StopVideoTestYUVCB() {
	return m_manage->StopVideoTestYUVCB();
}

int MeetingSdkClient::AsynPlaySoundTest(char wavFile[256], char renderName[256]) {
	return m_manage->AsynPlaySoundTest(wavFile, renderName);
}

void MeetingSdkClient::StopPlaySoundTest() {
	m_manage->StopPlaySoundTest();
}

int MeetingSdkClient::RecordSoundTest(char micName[256], char wavFile[256]){
	return m_manage->RecordSoundTest(micName,wavFile);
} 

void MeetingSdkClient::StopRecordSoundTest(){
	m_manage->StopRecordSoundTest();
}

int MeetingSdkClient::AsynStartNetDiagnosticSerialCheck(){
	return m_manage->AsynStartNetDiagnosticSerialCheck();
}


int MeetingSdkClient::StopNetBandDetect(){
	return m_manage->StopNetBandDetect();
}


int MeetingSdkClient::GetNetBandDetectResult(BandWidthData* bandWidthData) {
	int upwidth, downwidth;
	int result = m_manage->GetNetBandDetectResult(upwidth, downwidth);
	bandWidthData->upWidth = upwidth;
	bandWidthData->downWidth = downwidth;

	return result;
}

int MeetingSdkClient::ResetMeetingPassword(int meetingid, const char* encode) {
	return m_manage->ResetMeetingPassword(meetingid, encode);
}

int MeetingSdkClient::GetMeetingPassword(int meetingid) {
	return m_manage->GetMeetingPassword(meetingid);
}

int MeetingSdkClient::CheckMeetingHasPassword(int meetingid) {
	return m_manage->CheckMeetingHasPassword(meetingid);
}

int MeetingSdkClient::CheckMeetingPasswordValid(int meetingid,
	const char* encryptcode) {
	return m_manage->CheckMeetingPasswordValid(meetingid, encryptcode);
}

int MeetingSdkClient::CreateAndInviteMeeting(char * appType, int typeLen,
	int * inviteeList, int inviteeCount, Context context) {
	return m_manage->CreateAndInviteMeeting(appType, typeLen, inviteeList, inviteeCount, context);
}

int MeetingSdkClient::CreateDatedMeeting(char * appType, int typeLen, unsigned int year,
	unsigned int month, unsigned int day, unsigned int hour,
	unsigned int minute, unsigned int second, const char * endtime,
	const char * topic, const char * encryptcode) {
	return m_manage->CreateDatedMeeting(appType, typeLen, year, month, day, hour, minute, second, endtime, topic, encryptcode);
}

int MeetingSdkClient::CreateAndInviteDatedMeeting(char * appType, int typeLen, unsigned int year,
	unsigned int month, unsigned int day, unsigned int hour,
	unsigned int minute, unsigned int second, const char * endtime,
	const char * topic, int * inviteeList, int inviteeCount, const char * encryptcode) {
	return m_manage->CreateAndInviteDatedMeeting(appType, typeLen, year, month, day, hour, minute, second, endtime, topic, inviteeList, inviteeCount, encryptcode);
}

int MeetingSdkClient::ModifyMeetingInviters(int meetId, const char * appType, int smsType, int *accountList, int accountNum, Context context) {
	return m_manage->ModifyMeetingInviters(meetId, appType, smsType, accountList, accountNum, context);
}

int MeetingSdkClient::SetAudioMixRecvBufferNum(int AudioMaxBufferNum, int AudioStartVadBufferNum, int AudioStopVadBufferNum)
{
	return m_manage->SetAudioMixRecvBufferNum(AudioMaxBufferNum, AudioStartVadBufferNum, AudioStopVadBufferNum);
}

int MeetingSdkClient::HostOrderOneSpeak(char * toAccountId, int toLen,
	char * kickAccountId, int kickLen) {
	return m_manage->HostOrderOneSpeak(toAccountId, toLen, kickAccountId, kickLen);
}

int MeetingSdkClient::HostOrderOneStopSpeak(char * toAccountId, int toLen) {
	return m_manage->HostOrderOneStopSpeak(toAccountId, toLen);
}

int MeetingSdkClient::GetUserPublishStreamInfo(int accountId,
	MeetingUserStreamInfo * streamsInfo, int maxCount) {
	return m_manage->GetUserPublishStreamInfo(accountId, streamsInfo, maxCount);
}

int MeetingSdkClient::GetCurrentSubscribleStreamInfo(
	MeetingUserStreamInfo * streamsInfo, int maxCount) {
	return m_manage->GetCurrentSubscribleStreamInfo(streamsInfo, maxCount);
}

int MeetingSdkClient::GetParticipants(ParticipantInfo * participants,
	int pageNum, int countPerPage) {
	return m_manage->GetParticipants(participants, pageNum, countPerPage);
}

int MeetingSdkClient::PushMediaFrameData(int resourceId,
	MEETINGMANAGE_FrameType frameType, char * frameData, int dataLen, int orientation) {
	return m_manage->PushMediaFrameData(resourceId, frameType, frameData, dataLen, orientation);
}

int MeetingSdkClient::SetCurCpuInfo(int processCpu, int totalCpu)
{
	return m_manage->SetCurCpuInfo(processCpu, totalCpu);
}

int MeetingSdkClient::SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate, int frameRate)
{
	return m_manage->SetLowVideoStreamCodecParam(frameWidth, frameHeight, bitrate, frameRate);
}


int MeetingSdkClient::GetMicSendList(ParticipantInfo* participants, int maxCount) {
	return m_manage->GetMicSendList(participants, maxCount);
}

int MeetingSdkClient::AsynMicSendReq(int beSpeakedUserId) {
	return m_manage->AsynMicSendReq(beSpeakedUserId);
}

int MeetingSdkClient::AddDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx, int aspy) {
	return m_manage->AddDisplayWindow(accountId, resourceId, displayWindow, aspx, aspy);
}

int MeetingSdkClient::RemoveDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx, int aspy) {
	return m_manage->RemoveDisplayWindow(accountId, resourceId, displayWindow, aspx, aspy);
}

int MeetingSdkClient::PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam param) {
	return m_manage->PublishLiveStream(&param);
}

int MeetingSdkClient::UnpublishLiveStream(int streamID) {
	return m_manage->UnpublishLiveStream(streamID);
}

int MeetingSdkClient::SetPublishDoubleVideoStreamStatus(int isEnabled)
{
	return m_manage->SetPublishDoubleVideoStreamStatus(isEnabled);
}

int  MeetingSdkClient::SetAutoAdjustEnableStatus(int isEnabled)
{
	return m_manage->SetAutoAdjustEnableStatus(isEnabled);
}

int MeetingSdkClient::StartLiveRecord(int streamID, char *url) {
	return m_manage->StartLiveRecord(streamID, url);
}

int MeetingSdkClient::StopLiveRecord(int streamID) {
	return m_manage->StopLiveRecord(streamID);
}

int MeetingSdkClient::UpdateLiveStreamVideoInfo(int streamID, MEETINGMANAGE_VideoStreamInfo *streamInfo, int streamnum) {
	return m_manage->UpdateLiveStreamVideoInfo(streamID, streamInfo, streamnum);
}

int MeetingSdkClient::UpdateLiveStreamAudioInfo(int streamID, MEETINGMANAGE_AudioStreamInfo *streamInfo, int streamnum) {
	return m_manage->UpdateLiveStreamAudioInfo(streamID, streamInfo, streamnum);
}

int MeetingSdkClient::SetVideoClarity(int accountId, int resourceId, int clarityLevel)
{
	return m_manage->SetVideoClarity(accountId, resourceId, clarityLevel);
}

int MeetingSdkClient::SetVideoDisplayMode(int videoDisplayMode)
{
	return m_manage->SetVideoDisplayMode(videoDisplayMode);
}

int MeetingSdkClient::StopMp4Record(int streamID)
{
	return m_manage->StopMp4Record(streamID);
}

int MeetingSdkClient::StartMp4Record(int streamID, char *filepath)
{
	return m_manage->StartMp4Record(streamID, filepath);
}

int MeetingSdkClient::SetRkPath(const char* rkPath, int pathLen)
{
	return m_manage->SetRkPath(rkPath, pathLen);
}

int MeetingSdkClient::StartHost(const char * devmodel, char * configPath, int pathLen, Context context)
{
	return m_host->Start(devmodel, configPath, pathLen, context);
}

int MeetingSdkClient::StopHost()
{
	return m_host->Stop();
}

int MeetingSdkClient::ConnectMeetingVDN(int accountId, char * accountName, int nameLen, char * token, int tokenLen, Context context)
{
	return m_host->ConnectMeetingVDN(accountId, accountName, nameLen, token, tokenLen, context);
}

int MeetingSdkClient::SetAccountInfo(const char * accountName, int nameLen)
{
	return m_host->SetAccountInfo(accountName, nameLen);
}

int MeetingSdkClient::ConnectMeetingVDNAfterMeetingInstStarted(Context context)
{
	return m_host->ConnectMeetingVDNAfterMeetingInstStarted(context);
}

int MeetingSdkClient::DisConnectMeetingVDN()
{
	return m_host->DisConnectMeetingVDN();
}

int MeetingSdkClient::MeetingInvite(int meetingId, int * accountIdList, int accountSize)
{
	return m_host->MeetingInvite(meetingId, accountIdList, accountSize);
}
