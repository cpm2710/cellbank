#include "stdafx.h"
#include "Log.h"


using namespace std;
void Log::LogMessage(string a){

	/*
	打开文件的方式在类ios(是所有流式I/O类的基类)中定义，常用的值如下：

ios::app：　　　以追加的方式打开文件
ios::ate：　　　文件打开后定位到文件尾，ios:app就包含有此属性
ios::binary： 　以二进制方式打开文件，缺省的方式是文本方式。两种方式的区别见前文
ios::in：　　　 文件以输入方式打开（文件数据输入到内存）
ios::out：　　　文件以输出方式打开（内存数据输出到文件）
ios::nocreate： 不建立文件，所以文件不存在时打开失败
ios::noreplace：不覆盖文件，所以打开文件时如果文件存在失败
ios::trunc：　　如果文件存在，把文件长度设为0
　　可以用“或”把以上属性连接起来，如ios::out|ios::binary

　　打开文件的属性取值是：

0：普通文件，打开访问
1：只读文件
2：隐含文件
4：系统文件*/
	ofstream file1; 
	file1.open("c:\\record_log.txt" ,ios::app|ios::ate|ios::out);
	file1<<a<<endl;
	file1.close();
		/*char *str="haha";
		HANDLE hFile=CreateFile(TEXT(".\\record_log.txt"),GENERIC_WRITE,0,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL); 
		DWORD d_write_file; 
		SetFilePointer(hFile,0,0,FILE_END);
		WriteFile(
		int result=WriteFile(hFile,str,strlen(str),&d_write_file,NULL);
		CloseHandle(hFile);*/
	}