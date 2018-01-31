#pragma once

#define HEIGHT 50	// 宏定义，这种常量用于在代码替换后进行编译
#undef HEIGHT	// 取消

class ManageCB
{
public:
	virtual void OnStart(int statusCode, char * description);
};