#include "stdafx.h"
#include "Log.h"


using namespace std;
void Log::LogMessage(string a){

	/*
	���ļ��ķ�ʽ����ios(��������ʽI/O��Ļ���)�ж��壬���õ�ֵ���£�

ios::app����������׷�ӵķ�ʽ���ļ�
ios::ate���������ļ��򿪺�λ���ļ�β��ios:app�Ͱ����д�����
ios::binary�� ���Զ����Ʒ�ʽ���ļ���ȱʡ�ķ�ʽ���ı���ʽ�����ַ�ʽ�������ǰ��
ios::in�������� �ļ������뷽ʽ�򿪣��ļ��������뵽�ڴ棩
ios::out���������ļ��������ʽ�򿪣��ڴ�����������ļ���
ios::nocreate�� �������ļ��������ļ�������ʱ��ʧ��
ios::noreplace���������ļ������Դ��ļ�ʱ����ļ�����ʧ��
ios::trunc����������ļ����ڣ����ļ�������Ϊ0
���������á��򡱰���������������������ios::out|ios::binary

�������ļ�������ȡֵ�ǣ�

0����ͨ�ļ����򿪷���
1��ֻ���ļ�
2�������ļ�
4��ϵͳ�ļ�*/
	ofstream file1; 
	file1.open(".\\record_log.txt" ,ios::app|ios::ate|ios::out);
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