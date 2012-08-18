// HelperUtilTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "..\\HelpUtil\\Log.h"
#pragma comment( lib, "..//x64/debug//HelpUtil.lib" ) 
int _tmain(int argc, _TCHAR* argv[])
{
	Log *log=new Log();
	string a("sss");
	log->LogMessage(a);
	delete(log);
	return 0;
}

