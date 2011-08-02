// WebDesktopAgent.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <objidl.h>
#include <stdio.h>
#include <gdiplus.h>
#define ULONG_PTR unsigned long

ULONG_PTR        m_gdiplusToken;

#pragma comment (lib,"Gdiplus.lib")
using namespace Gdiplus;
int GetEncoderClsid(const WCHAR* format, CLSID* pClsid)  
{  
    UINT num = 0;                     // number of image encoders  
    UINT size = 0;                   // size of the image encoder array in bytes  
    ImageCodecInfo* pImageCodecInfo = NULL;  
    GetImageEncodersSize(&num, &size);  
    if(size == 0)  
        return -1;     //   Failure  
   
    pImageCodecInfo = (ImageCodecInfo*)(malloc(size));  
    if(pImageCodecInfo == NULL)  
        return -1;     //   Failure  
   
    GetImageEncoders(num, size, pImageCodecInfo);  
    for(UINT j = 0; j < num; ++j)  
    {  
        if( wcscmp(pImageCodecInfo[j].MimeType, format) == 0 )  
        {  
            *pClsid = pImageCodecInfo[j].Clsid;  
            free(pImageCodecInfo);  
            return j;     //   Success  
        }          
    }  
    free(pImageCodecInfo);  
    return -1;     //   Failure  
}

int _tmain(int argc, _TCHAR* argv[])
{
	GdiplusStartupInput     m_gdiplusStartupInput;
GdiplusStartup(&m_gdiplusToken, &m_gdiplusStartupInput, NULL);

	 CLSID             encoderClsid;
	 
	 GetEncoderClsid(L"image/jpeg", &encoderClsid);

   EncoderParameters encoderParameters;
   encoderParameters.Count = 1;
   encoderParameters.Parameter[0].Guid = EncoderQuality;
   encoderParameters.Parameter[0].Type = EncoderParameterValueTypeLong;
   encoderParameters.Parameter[0].NumberOfValues = 1;

   // Save the image as a JPEG with quality level 0.
   ULONG             quality = 50;
   encoderParameters.Parameter[0].Value = &quality;
   long t1=GetTickCount();
   HDC screenDC = CreateDC(_T("DISPLAY"), NULL, NULL, NULL);

   Graphics graphics(screenDC);
	int x = GetDeviceCaps(screenDC, HORZRES);  
    int y = GetDeviceCaps(screenDC, VERTRES); 
	for(int i=0;i<100;i++){
	t1=GetTickCount();

	Bitmap outputImage(x,y,&graphics);
	
	Graphics * imgGraphics=Graphics::FromImage(&outputImage);
	HDC imageDC=imgGraphics->GetHDC();
	::BitBlt(imageDC,0,0,x,y,screenDC,0,0,0xCC0020);
	imgGraphics->ReleaseHDC(imageDC);
	
	//outputImage.Save(L"d:\\abc.png",&encoderClsid,&encoderParameters);
	
	printf("%dms\n",GetTickCount()-t1);
	t1=GetTickCount();
   }
   DeleteDC(screenDC);
	Gdiplus::GdiplusShutdown(m_gdiplusToken);
	return 0;
}

