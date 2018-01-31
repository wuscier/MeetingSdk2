


#include <iostream>
using namespace std;

void pointer_test() 
{
	// 二级char指针1
	char a[3][100];
	char * a1[3] = { a[0], a[1], a[2] };
	char ** a2 = a1;

	cout << "a0:" << (int)a[0] << "a1:" << (int)a[1] << endl;

	char * b0 = a[0];
	char * b1 = a[1];
	char * b2 = a[2];

	
	int iarr[20] = {};
	int (*p0)[20] = &iarr;	// p0是指针，其类型是int(*)[20]，指向iarr整个数组
	int *p1[2];		// p1是数组，里面包含2个int*
	
	
	// * a2 = "justlucky";
	strcpy(*a2, "justlucky");
	strcpy(*(a2 + 1), "justlucky2");
	strcpy(*(a2 + 2), "justlucky2");


	// 二级char指针2
	char ** p = NULL;
	p = (char **)calloc(10, sizeof(char *));
	for (int i = 0; i < 10; i++)
	{
		p[i] = (char *)calloc(100, sizeof(char));
	}


	char aa = 'A';
	const char * ch1;	// 字符常量不可修改
	char * const ch2 = &aa;	// 指针常量不可修改
	*ch2 = 'B';
}