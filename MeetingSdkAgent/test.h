#pragma once

#define HEIGHT 50	// �궨�壬���ֳ��������ڴ����滻����б���
#undef HEIGHT	// ȡ��

class ManageCB
{
public:
	virtual void OnStart(int statusCode, char * description);
};