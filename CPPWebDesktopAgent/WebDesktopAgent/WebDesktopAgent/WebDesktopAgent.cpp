// WebDesktopAgent.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
#pragma comment (lib,"Gdiplus.lib")
using namespace Gdiplus;
int _tmain(int argc, _TCHAR* argv[])
{
	HDC hdc = CreateDC(_T("DISPLAY"), NULL, NULL, NULL);
	Graphics graphics(hdc);
	new Bitmap();
	DeleteDC(hdc);
	return 0;
}

