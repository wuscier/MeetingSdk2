#pragma once

#include "HostManageCallBack.h"
#include "MeetingStruct.h"

class HostManageCB :public IHostManageCB
{
private:
	PFunc_Callback m_cb;
	void cb(int cmdId, void * pData, int dataLen, Context context);

public:
	HostManageCB(PFunc_Callback cb);
	~HostManageCB();
	void SetCallback(PFunc_Callback cb);



	void OnStart(int statusCode, char * description,
		int descLen, Context context);

	void OnConnectMeetingVDN(int statusCode, char * description,
		int descLen, Context context);

	void OnSetAccountInfo(int StatusCode, char * description, int descLen);

	void OnMeetingInvite(int statusCode, Context context);

	void OnMeetingInviteEvent(int inviterAccountId,
		char * inviterAccountName, int accountNameLen, int meetingId);

	void OnContactRecommendEvent(RecommendContactInfo & info);

	void OnForcedOfflineEvent(int accountId, char * token, int tokenLen);
};

