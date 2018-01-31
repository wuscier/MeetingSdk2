#include "HostManageCB.h"
#include "MeetingStruct.h"

void HostManageCB::cb(int cmdId, void * pData, int dataLen, Context context)
{
	if (m_cb)
	{
		try {
			m_cb(cmdId, pData, dataLen, context);
		}
		catch (...) {
			// logging
		}
	}
}

HostManageCB::HostManageCB(PFunc_Callback cb)
{
	m_cb = cb;
}

HostManageCB::~HostManageCB()
{
}

void HostManageCB::SetCallback(PFunc_Callback cb)
{
	m_cb = cb;
}

void HostManageCB::OnStart(int statusCode, char * description, int descLen, Context context)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);
	cb(start_host, &result, 0, 0);
}

void HostManageCB::OnConnectMeetingVDN(int statusCode, char * description, int descLen, Context context)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);
	cb(connect_meeting_vdn, &result, 0, 0);
}

void HostManageCB::OnSetAccountInfo(int StatusCode, char * description, int descLen)
{
	AsyncCallResult result;
	result.m_statusCode = StatusCode;
	strcpy_s(result.m_message, sizeof(result.m_message), description);
	cb(set_account_info, &result, 0, 0);
}

void HostManageCB::OnMeetingInvite(int statusCode, Context context)
{
	AsyncCallResult result;
	result.m_statusCode = statusCode;
	cb(meeting_invite, &result, 0, 0);
}

void HostManageCB::OnMeetingInviteEvent(int inviterAccountId, char * inviterAccountName, int accountNameLen, int meetingId)
{
	MeetingInvitationResult result;
	result.inviterAccountId = inviterAccountId;
	strcpy_s(result.inviterAccountName, sizeof(result.inviterAccountName), inviterAccountName);
	result.meetingId = meetingId;
	cb(meeting_invite_event, &result, 0, 0);
}


void HostManageCB::OnContactRecommendEvent(RecommendContactInfo & info)
{
	cb(contact_recommend_event, &info, 0, 0);
}

void HostManageCB::OnForcedOfflineEvent(int accountId, char * token, int tokenLen)
{
	ForcedOfflineResult result;
	result.accountId = accountId;
	strcpy_s(result.token, sizeof(result.token), token);
	cb(force_offline_event, &result, 0, 0);
}


