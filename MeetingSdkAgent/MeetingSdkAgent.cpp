// MeetingSdkAgent.cpp: ???? DLL ??ó?????????????
//

#include "stdafx.h"
#include "MeetingManage.h"
#include "MeetingManageCB.h"
#include "MeetingSdkAgent.h"
#include "MeetingSdkClient.h"
#include "test.h"
#include <IPHlpApi.h>
//#include <Windows.h>
#include <Shlwapi.h>

#pragma comment(lib, "Shlwapi")
#pragma comment(lib, "IPHlpApi.lib")
#define MALLOC(x) HeapAlloc(GetProcessHeap(),0,(x))
#define FREE(x) HeapFree(GetProcessHeap(),0,(x))


void callback(char* cbType, void* sType);

int GetDeviceNum() 
{	
	PFunc_Callback _cb = NULL;
	MeetingManageCB * cb = new MeetingManageCB(_cb);
	IMeetingManage * manage = CreateMeetingManageObject(cb);

	// ????char???1
	char a[3][100];
	char * a1[3] = { a[0], a[1], a[2] };
	char ** a2 = a1;

	char devType[] = "pc";
	char config[100] = "c:\\aa\abc";
	void* context = NULL;

	int total = manage->GetAudioCaptureDeviceList(a2, 3);
	int check = manage->Start(devType, config, 100, context);
	
	delete cb;
	return total;
}

int GetDeviceNum2()
{
	// ????char???1
	char a[3][100];
	char * a1[3] = { a[0], a[1], a[2] };
	char ** a2 = a1;

	MeetingSdkClient* client = MeetingSdkClient::Instance();
//	int total = client->GetAudioCaptureDeviceList(a2, 3);	
	
	return 0;
}

bool GetNetInfo(bool *dhcp, char * ip, char * mask, char * gw, char * macaddr)
{
	bool bfindip = false;
	char lszInfo[256] = { 0 };
	try {
		PIP_ADAPTER_INFO pAdapterInfo;
		DWORD dwRetVal = 0;

		pAdapterInfo = (IP_ADAPTER_INFO *)MALLOC(sizeof(IP_ADAPTER_INFO));
		ULONG ulOutBufLen = sizeof(IP_ADAPTER_INFO);

		if (GetAdaptersInfo(pAdapterInfo, &ulOutBufLen) == ERROR_BUFFER_OVERFLOW)
		{
			FREE(pAdapterInfo);
			pAdapterInfo = NULL;
			pAdapterInfo = (IP_ADAPTER_INFO *)MALLOC(ulOutBufLen);
		}

		PIP_ADAPTER_INFO temppAdapterInfo = NULL;
		if ((dwRetVal = GetAdaptersInfo(pAdapterInfo, &ulOutBufLen)) == NO_ERROR)
		{
			temppAdapterInfo = pAdapterInfo;
			while (temppAdapterInfo != NULL)
			{
				PIP_ADDR_STRING tempIPAddrString = NULL;
				tempIPAddrString = &temppAdapterInfo->IpAddressList;
				//if( strstr(temppAdapterInfo->Description,"Wireless")  ) //排出无线网卡
				//{
				//	temppAdapterInfo = temppAdapterInfo->Next;
				//	continue;
				//}
				if (strstr(temppAdapterInfo->Description, "VMware") ||
					strstr(temppAdapterInfo->Description, "Virtual") ||
					strstr(temppAdapterInfo->Description, "VMnet")) //排出虚拟网卡
				{
					temppAdapterInfo = temppAdapterInfo->Next;
					continue;
				}

				if (temppAdapterInfo->Type == MIB_IF_TYPE_ETHERNET) // 判断是否为以太网接口  
				{
					while (tempIPAddrString != NULL)
					{
						if (ip)
						{
							strcpy(ip, tempIPAddrString->IpAddress.String);
						}
						if (strlen(tempIPAddrString->IpMask.String) > 0)
						{
							if (mask)
							{
								strcpy(mask, tempIPAddrString->IpMask.String);
							}
						}
						break;
					}

					tempIPAddrString = &temppAdapterInfo->GatewayList;
					while (tempIPAddrString != NULL)
					{
						if (gw)
						{
							strcpy(gw, tempIPAddrString->IpAddress.String);
						}
						break;
					}
					if (dhcp)
					{
						*dhcp = temppAdapterInfo->DhcpEnabled;
					}

					if (macaddr)
					{
						memset(macaddr, 0, 13);
						sprintf(macaddr, "%02X%02X%02X%02X%02X%02X",
							temppAdapterInfo->Address[0],
							temppAdapterInfo->Address[1],
							temppAdapterInfo->Address[2],
							temppAdapterInfo->Address[3],
							temppAdapterInfo->Address[4],
							temppAdapterInfo->Address[5]);

					}
					bfindip = true;
					break;
				}
				temppAdapterInfo = temppAdapterInfo->Next;

			}

			if (ip && strcmp("0.0.0.0", ip) == 0)
			{
				if (gw)
					strcpy(gw, "0.0.0.0");

			}

		}
		else
		{
			FREE(pAdapterInfo);
			sprintf(lszInfo, "[Meetingsdkagent]GetNetInfo failed with error: %d\n", dwRetVal);
			OutputDebugStringA(lszInfo);

			return false;
		}
		sprintf(lszInfo, "[Meetingsdkagent] GetNetInfo return with code: %d\n", bfindip);
		OutputDebugStringA(lszInfo);

		FREE(pAdapterInfo);
		return bfindip;
	}
	catch (...)
	{
		DWORD dwcd = GetLastError();
		sprintf(lszInfo, "[Meetingsdkagent] GetNetInfo return with exception: %d\n", dwcd);
		OutputDebugStringA(lszInfo);
		return false;
	}

	return false;
}


int GetSerialNo(StringStruct* devSerialNo) {
	
	char strFilePath[500] = { 0 };
	//以前已经产生过，直接使用之前生成的
	DWORD dwError = GetModuleFileNameA(NULL, strFilePath, 500);
	if (dwError>0)
	{
		PathRemoveFileSpecA(strFilePath);
		strcat(strFilePath, "\\config\\sdkcfg.ini");
		char lszimei[64] = { 0 };
		::GetPrivateProfileStringA("IMEI", "DEVNO", "", lszimei, 63, strFilePath);
		if (strlen(lszimei) == 0)
		{
			char tmpmac[20] = { 0 };
			if (GetNetInfo(NULL, NULL, NULL, NULL, tmpmac))
			{
				sprintf(devSerialNo->string, "BOX%s", tmpmac);
			}
			else
			{
				return -1;
			}
		}
		else
		{
			strcpy(devSerialNo->string, lszimei);
		}
	}
	else
	{
		char tmpmac[20] = { 0 };
		if (GetNetInfo(NULL, NULL, NULL, NULL, tmpmac))
		{
			sprintf(devSerialNo->string, "BOX%s", tmpmac);
		}
		else
		{
			return -1;
		}
	}
	return 0;

}

void CallCB(ManageCB * cb)
{
	cb->OnStart(1, "asf");
}

void SetCallback(PFunc_Callback pFunc_Callback)
{
	MeetingSdkClient::Instance()->SetCallback(pFunc_Callback);
}

void Destroy()
{
	MeetingSdkClient::DestroyInstance();
}

int Start(const char * devmodel, char * configPath, int pathLen, Context context)
{
	return  MeetingSdkClient::Instance()->Start(devmodel, configPath, pathLen, context);
}

int SetNpsUrl(StringStruct * npsUrlList, int urlSize)
{
	return MeetingSdkClient::Instance()->SetNpsUrl(npsUrlList, urlSize);
}

int Stop() 
{
	return MeetingSdkClient::Instance()->Stop();
}


int IsMeetingExist(int meetingId, Context context)
{
	return MeetingSdkClient::Instance()->IsMeetingExist(meetingId, context);
}

int GetMeetingList(Context context)
{
	return MeetingSdkClient::Instance()->GetMeetingList(context);
}

int GetMeetingInfo(int meetId, Context context)
{
	return MeetingSdkClient::Instance()->GetMeetingInfo(meetId, context);
}

int Login(const char * nube, int nubeLen, const char * pass, int passLen, const char * deviceType, int dtLen, Context context)
{
	return MeetingSdkClient::Instance()->Login(nube, nubeLen, pass, passLen, deviceType, dtLen, context);
}

int LoginThirdParty(const char* nube, const char* appkey, const char * uid, Context context) {
	return MeetingSdkClient::Instance()->LoginThirdParty(nube, appkey, uid, context);
}


int LoginViaImei(const char * imei, int imeiLen, Context context)
{
	return  MeetingSdkClient::Instance()->LoginViaImei(imei, imeiLen, context);
}

int GetVideoDeviceList(MEETINGMANAGE_VideoDeviceInfo * devInfo, int maxCount)
{
	return MeetingSdkClient::Instance()->GetVideoDeviceList(devInfo, maxCount);
}

int GetAudioCaptureDeviceList(StringStruct* devicelist, int listsize) {

	return MeetingSdkClient::Instance()->GetAudioCaptureDeviceList(devicelist, listsize);
}

int GetAudioRenderDeviceList(StringStruct* devicelist, int listsize) {
	return MeetingSdkClient::Instance()->GetAudioRenderDeviceList(devicelist, listsize);

}

int CreateMeeting(char * appType, int typeLen, Context context) {
	return MeetingSdkClient::Instance()->CreateMeeting(appType, typeLen, context);
}

int JoinMeeting(int meetingId, bool autoSpeak, Context context) {
	return MeetingSdkClient::Instance()->JoinMeeting(meetingId, autoSpeak, context);
}


int GetJoinMeetingInfo(int meetingId, JoinMeetingInfo * joinMeetingInfo) 
{
	return MeetingSdkClient::Instance()->GetJoinMeetingInfo(meetingId, joinMeetingInfo);
}

int GenericSyncId() 
{
	return MeetingSdkClient::Instance()->GenericSyncId();
}

int LeaveMeeting() {
	return MeetingSdkClient::Instance()->LeaveMeeting();
}


int PublishCameraVideo(MEETINGMANAGE_PublishCameraParam param,
	bool isNeedCallBackMedia, Context context) {
	return	MeetingSdkClient::Instance()->PublishCameraVideo(param, isNeedCallBackMedia, context);
}

int PublishDataCardVideo(MEETINGMANAGE_PublishCameraParam param,
	bool isNeedCallBackMedia, Context context) {
	return MeetingSdkClient::Instance()->PublishDataCardVideo(param, isNeedCallBackMedia, context);
}

int PublishWinCaptureVideo(MEETINGMANAGE_WinCaptureVideoParam param,
	bool isNeedCallBackMedia, Context context) {
	return MeetingSdkClient::Instance()->PublishWinCaptureVideo(param, isNeedCallBackMedia, context);
}

int PublishMicAudio(MEETINGMANAGE_publishMicParam param,
	Context context) {
	return MeetingSdkClient::Instance()->PublishMicAudio(param, context);
}

int UnpublishCameraVideo(int resourceId, Context context) {
	return MeetingSdkClient::Instance()->UnpublishCameraVideo(resourceId, context);
}

 int UnpublishDataCardVideo(int resourceId, Context context){
	 return MeetingSdkClient::Instance()->UnpublishDataCardVideo(resourceId, context);

 }

 int UnpublishWinCaptureVideo(int resourceId, Context context){
	 return MeetingSdkClient::Instance()->UnpublishWinCaptureVideo(resourceId, context);
 }

 int UnpublishMicAudio(int resourceId, Context context){
	 return MeetingSdkClient::Instance()->UnpublishMicAudio(resourceId, context);
 }


int	SubscribeVideo(MEETINGMANAGE_subscribeVideoParam param,
	bool isNeedCallBackMedia) {
	return MeetingSdkClient::Instance()->SubscribeVideo(param, isNeedCallBackMedia);
}

int SubscribeAudio(MEETINGMANAGE_subscribeAudioParam param) {
	return MeetingSdkClient::Instance()->SubscribeAudio(param);
}

int	Unsubscribe(int accountId, int resourceId) {
	return	MeetingSdkClient::Instance()->Unsubscribe(accountId, resourceId);
}

 int AskForSpeak(char * speakerId, int speakerIdLen, Context context){
	 return MeetingSdkClient::Instance()->AskForSpeak(speakerId, speakerIdLen, context);
 }

 int AskForStopSpeak(Context context) {
	 return MeetingSdkClient::Instance()->AskForStopSpeak(context);
 }

 int GetSpeakerList(MeetingSpeakerInfo * speakerInfos, int maxCount)
 {
	 return MeetingSdkClient::Instance()->GetSpeakerList(speakerInfos, maxCount);
 }

 int GetSpeakerInfo(int accountId, MeetingSpeakerInfo * speakerInfo)
 {
	 return MeetingSdkClient::Instance()->GetSpeakerInfo(accountId, speakerInfo);
 }

 int GetMeetingQos(LongStringStruct * outdata)
 {
	 return MeetingSdkClient::Instance()->GetMeetingQos(outdata);
 }

 int StartLocalVideoRender(int resourceId, void* displayWindow, int aspx, int aspy)
 {
	 return MeetingSdkClient::Instance()->StartLocalVideoRender(resourceId, displayWindow, aspx, aspy);
 }

 int StopLocalVideoRender(int resourceId)
 {
	 return MeetingSdkClient::Instance()->StopLocalVideoRender(resourceId);
 }

 int StartRemoteVideoRender(int accountId, int resourceId, void* displayWindow, int aspx, int aspy)
 {
	 return MeetingSdkClient::Instance()->StartRemoteVideoRender(accountId, resourceId, displayWindow, aspx, aspy);
 }

 int StopRemoteVideoRender(int accountId, int resourceId)
 {
	 return MeetingSdkClient::Instance()->StopRemoteVideoRender(accountId, resourceId);
 }

 int StartYUVDataCallBack(int accountId, int resourceId)
 {
	 return MeetingSdkClient::Instance()->StartYUVDataCallBack(accountId, resourceId);
 }

 int StopYUVDataCallBack(int accountId, int resourceId)
 {
	 return MeetingSdkClient::Instance()->StopYUVDataCallBack(accountId, resourceId);
 }

 int SendUiTransparentMsg(int destAccount, StringStruct * data)
 {
	 return MeetingSdkClient::Instance()->SendUiTransparentMsg(destAccount, data);
 }


 int AsynSendUIMsg(int msgId, int dstUserAccount, const char *msgData) {
	 return MeetingSdkClient::Instance()->AsynSendUIMsg(msgId, dstUserAccount, msgData);
 }


 int HostChangeMeetingMode(int toMode)
 {
	 return MeetingSdkClient::Instance()->HostChangeMeetingMode(toMode);
 }

 int GetCurMeetingMode()
 {
	 return MeetingSdkClient::Instance()->GetCurMeetingMode();
 }

 int HostKickoutUser(int accountId)
 {
	 return MeetingSdkClient::Instance()->HostKickoutUser(accountId);
 }

 int RaiseHandReq()
 {
	 return MeetingSdkClient::Instance()->RaiseHandReq();
 }

 int AskForMeetingLock(bool bToLock)
 {
	 return MeetingSdkClient::Instance()->AskForMeetingLock(bToLock);
 }

 bool GetMeetingLockStatus()
 {
	 return MeetingSdkClient::Instance()->GetMeetingLockStatus();
 }

 int GetParticipants(ParticipantInfo* participants, int maxCount) {
	 return MeetingSdkClient::Instance()->GetParticipants(participants, maxCount);
 }

 int SendAudioSpeakerStatus(int isOpen, Context context) {
	 return MeetingSdkClient::Instance()->SendAudioSpeakerStatus(isOpen, context);
 }

 int GetMeetingInvitationSMS(int meetId, int inviterPhoneId, const char* inviterName, int inviterNameLen, int meetingType, const char* app, int appLen, int urlType, Context context)
 {
	 return MeetingSdkClient::Instance()->GetMeetingInvitationSMS(meetId, inviterPhoneId, inviterName, inviterNameLen, meetingType, app, appLen, urlType, context);
 }

 int GetSpeakerVideoStreamParam(int accountId, int resourceID, SpeakerVideoStreamParamData * data)
 {
	 return MeetingSdkClient::Instance()->GetSpeakerVideoStreamParam(accountId, resourceID, data);
 }

 int HostOrderOneDoOpration(int toUserId, int oprateType, Context context)
 {
	 return MeetingSdkClient::Instance()->HostOrderOneDoOpration(toUserId, oprateType, context);
 }

 int ModifyNickName(const char * accountName, int nameLen, Context context)
 {
	 return MeetingSdkClient::Instance()->ModifyNickName(accountName, nameLen, context);
 }

 int BindToken(const char* token, int tokenLen,
	 int accountId, const char* accountName,
	 int accountNameLen, Context context)
 {
	 return MeetingSdkClient::Instance()->BindToken(token, tokenLen, accountId, accountName, accountNameLen, context);
 }

 int AsynPlayVideoTest(int colorsps, int fps,
	 int width, int height, void * previewWindow, char videoCapName[256]) {
	 return MeetingSdkClient::Instance()->AsynPlayVideoTest(colorsps, fps, width, height, previewWindow, videoCapName);
 }

 void StopVideoTest() {
	 MeetingSdkClient::Instance()->StopVideoTest();
 }

 int AsynPlayVideoTestYUVCB(int colorsps, int fps, int width,
	 int height, char videoCapName[256], FUN_VIDEO_PREVIEW fun) {
	 return MeetingSdkClient::Instance()->AsynPlayVideoTestYUVCB(colorsps, fps, width, height, videoCapName, fun);
 }


 int StopVideoTestYUVCB() {
	 return MeetingSdkClient::Instance()->StopVideoTestYUVCB();
 }

 int AsynPlaySoundTest(char wavFile[256], char renderName[256]) {
	 return MeetingSdkClient::Instance()->AsynPlaySoundTest(wavFile, renderName);
 }

 void StopPlaySoundTest() {
	 MeetingSdkClient::Instance()->StopPlaySoundTest();
 }

int RecordSoundTest(char micName[256], char wavFile[256]){
	return MeetingSdkClient::Instance()->RecordSoundTest(micName,wavFile);
} 

void StopRecordSoundTest(){
	MeetingSdkClient::Instance()->StopRecordSoundTest();
}

int AsynStartNetDiagnosticSerialCheck(){
	return MeetingSdkClient::Instance()->AsynStartNetDiagnosticSerialCheck();
}

int StopNetBandDetect(){
	return MeetingSdkClient::Instance()->StopNetBandDetect();
}

int GetNetBandDetectResult(BandWidthData* bandWidthData) {
	return MeetingSdkClient::Instance()->GetNetBandDetectResult(bandWidthData);
}

int ResetMeetingPassword(int meetingid, const char* encode) {
	return MeetingSdkClient::Instance()->ResetMeetingPassword(meetingid, encode);
}

int GetMeetingPassword(int meetingid) {
	return MeetingSdkClient::Instance()->GetMeetingPassword(meetingid);
}

int CheckMeetingHasPassword(int meetingid) {
	return MeetingSdkClient::Instance()->CheckMeetingHasPassword(meetingid);
}

int CheckMeetingPasswordValid(int meetingid,
	const char* encryptcode) {
	return MeetingSdkClient::Instance()->CheckMeetingPasswordValid(meetingid, encryptcode);
}

int CreateAndInviteMeeting(char * appType, int typeLen,
	int * inviteeList, int inviteeCount, Context context) {
	return MeetingSdkClient::Instance()->CreateAndInviteMeeting(appType, typeLen, inviteeList, inviteeCount, context);
}

int CreateDatedMeeting(char * appType, int typeLen, unsigned int year,
	unsigned int month, unsigned int day, unsigned int hour,
	unsigned int minute, unsigned int second, const char * endtime,
	const char * topic, const char * encryptcode) {
	return MeetingSdkClient::Instance()->CreateDatedMeeting(appType, typeLen, year, month, day, hour, minute, second, endtime, topic, encryptcode);
}

int CreateAndInviteDatedMeeting(char * appType, int typeLen, unsigned int year,
	unsigned int month, unsigned int day, unsigned int hour,
	unsigned int minute, unsigned int second, const char * endtime,
	const char * topic, int * inviteeList, int inviteeCount, const char * encryptcode) {
	return MeetingSdkClient::Instance()->CreateAndInviteDatedMeeting(appType, typeLen, year, month, day, hour, minute, second, endtime, topic, inviteeList, inviteeCount, encryptcode);
}


int ModifyMeetingInviters(int meetId, const char * appType, int smsType, int *accountList, int accountNum, Context context) {
	return MeetingSdkClient::Instance()->ModifyMeetingInviters(meetId, appType, smsType, accountList, accountNum, context);
}

int SetAudioMixRecvBufferNum(int AudioMaxBufferNum, int AudioStartVadBufferNum, int AudioStopVadBufferNum)
{
	return MeetingSdkClient::Instance()->SetAudioMixRecvBufferNum(AudioMaxBufferNum, AudioStartVadBufferNum, AudioStopVadBufferNum);
}


int HostOrderOneSpeak(char * toAccountId, int toLen,
	char * kickAccountId, int kickLen) {
	return MeetingSdkClient::Instance()->HostOrderOneSpeak(toAccountId, toLen, kickAccountId, kickLen);
}

int HostOrderOneStopSpeak(char * toAccountId, int toLen) {
	return MeetingSdkClient::Instance()->HostOrderOneStopSpeak(toAccountId, toLen);
}

int GetUserPublishStreamInfo(int accountId,
	MeetingUserStreamInfo * streamsInfo, int maxCount) {
	return MeetingSdkClient::Instance()->GetUserPublishStreamInfo(accountId, streamsInfo, maxCount);
}


int GetCurrentSubscribleStreamInfo(
	MeetingUserStreamInfo * streamsInfo, int maxCount) {
	return MeetingSdkClient::Instance()->GetCurrentSubscribleStreamInfo(streamsInfo, maxCount);
}

int GetParticipantsByPage(ParticipantInfo * participants,
	int pageNum, int countPerPage) {
	return MeetingSdkClient::Instance()->GetParticipants(participants, pageNum, countPerPage);
}


int PushMediaFrameData(int resourceId,
	MEETINGMANAGE_FrameType frameType, char * frameData, int dataLen, int orientation) {
	return MeetingSdkClient::Instance()->PushMediaFrameData(resourceId, frameType, frameData, dataLen, orientation);
}

int SetCurCpuInfo(int processCpu, int totalCpu)
{
	return MeetingSdkClient::Instance()->SetCurCpuInfo(processCpu, totalCpu);
}

int SetLowVideoStreamCodecParam(int frameWidth, int frameHeight, int bitrate, int frameRate)
{
	return MeetingSdkClient::Instance()->SetLowVideoStreamCodecParam(frameWidth, frameHeight, bitrate, frameRate);
}

int GetMicSendList(ParticipantInfo* participants, int maxCount) {
	return MeetingSdkClient::Instance()->GetMicSendList(participants, maxCount);
}

int AsynMicSendReq(int beSpeakedUserId) {
	return MeetingSdkClient::Instance()->AsynMicSendReq(beSpeakedUserId);
}

int AddDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx, int aspy) {
	return MeetingSdkClient::Instance()->AddDisplayWindow(accountId, resourceId, displayWindow, aspx, aspy);
}

int RemoveDisplayWindow(int accountId, int resourceId, void *displayWindow, int aspx, int aspy) {
	return MeetingSdkClient::Instance()->RemoveDisplayWindow(accountId, resourceId, displayWindow, aspx, aspy);
}

int PublishLiveStream(MEETINGMANAGE_PubLiveStreamParam param) {
	return MeetingSdkClient::Instance()->PublishLiveStream(param);
}

int UnpublishLiveStream(int streamID) {
	return MeetingSdkClient::Instance()->UnpublishLiveStream(streamID);
}

int SetPublishDoubleVideoStreamStatus(int isEnabled)
{
	return MeetingSdkClient::Instance()->SetPublishDoubleVideoStreamStatus(isEnabled);
}

int  SetAutoAdjustEnableStatus(int isEnabled)
{
	return MeetingSdkClient::Instance()->SetAutoAdjustEnableStatus(isEnabled);
}

int StartLiveRecord(int streamID, char *url) {
	return MeetingSdkClient::Instance()->StartLiveRecord(streamID, url);
}

int StopLiveRecord(int streamID) {
	return MeetingSdkClient::Instance()->StopLiveRecord(streamID);
}

int UpdateLiveStreamVideoInfo(int streamID, MEETINGMANAGE_VideoStreamInfo* streamInfo, int streamnum) {
	return MeetingSdkClient::Instance()->UpdateLiveStreamVideoInfo(streamID, streamInfo, streamnum);
}

int UpdateLiveStreamAudioInfo(int streamID, MEETINGMANAGE_AudioStreamInfo *streamInfo, int streamnum) {
	return MeetingSdkClient::Instance()->UpdateLiveStreamAudioInfo(streamID, streamInfo, streamnum);
}

int SetVideoClarity(int accountId, int resourceId, int clarityLevel)
{
	return MeetingSdkClient::Instance()->SetVideoClarity(accountId, resourceId, clarityLevel);
}

int SetVideoDisplayMode(int videoDisplayMode)
{
	return MeetingSdkClient::Instance()->SetVideoDisplayMode(videoDisplayMode);
}

int StopMp4Record(int streamID)
{
	return MeetingSdkClient::Instance()->StopMp4Record(streamID);
}

int StartMp4Record(int streamID, char *filepath)
{
	return MeetingSdkClient::Instance()->StartMp4Record(streamID, filepath);
}

int SetRkPath(const char* rkPath, int pathLen)
{
	return MeetingSdkClient::Instance()->SetRkPath(rkPath, pathLen);
}

void callback(char* cbType, void* sType)
{
	std::cout << "cbType:" << cbType << std::endl;
}

int StartHost(const char * devmodel, char * configPath,
	int pathLen, Context context) {
	return MeetingSdkClient::Instance()->StartHost(devmodel, configPath, pathLen, context);
}

int StopHost() {
	return MeetingSdkClient::Instance()->StopHost();
}

int ConnectMeetingVDN(int accountId, char * accountName,
	int nameLen, char * token, int tokenLen, Context context) {
	return MeetingSdkClient::Instance()->ConnectMeetingVDN(accountId, accountName, nameLen, token, tokenLen, context);
}

int SetAccountInfo(const char * accountName, int nameLen) {
	return MeetingSdkClient::Instance()->SetAccountInfo(accountName, nameLen);
}

int ConnectMeetingVDNAfterMeetingInstStarted(Context context) {
	return MeetingSdkClient::Instance()->ConnectMeetingVDNAfterMeetingInstStarted(context);
}

int DisConnectMeetingVDN() {
	return MeetingSdkClient::Instance()->DisConnectMeetingVDN();
}

int MeetingInvite(int meetingId, int * accountIdList,
	int accountSize) {
	return MeetingSdkClient::Instance()->MeetingInvite(meetingId, accountIdList, accountSize);
}

