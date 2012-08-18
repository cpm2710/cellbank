#include "stdafx.h"
#include "Log.h"
using namespace std;
void Log::LogMessage(string a){
		char *str="haha";
		HANDLE hFile=CreateFile(TEXT(".\\record_log.txt"),GENERIC_WRITE,0,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL); 
		DWORD d_write_file; 
		SetFilePointer(hFile,0,0,FILE_END);
		int result=WriteFile(hFile,str,strlen(str)+1,&d_write_file,NULL);
		CloseHandle(hFile);
	}