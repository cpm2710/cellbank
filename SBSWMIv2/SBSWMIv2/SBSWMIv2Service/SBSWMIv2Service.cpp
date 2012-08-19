// SBSWMIv2Service.cpp : Implementation of WinMain


#include "stdafx.h"
#include "resource.h"
#include "SBSWMIv2Service_i.h"


using namespace ATL;

#include <stdio.h>

class CSBSWMIv2ServiceModule : public ATL::CAtlServiceModuleT< CSBSWMIv2ServiceModule, IDS_SERVICENAME >
{
public :
	DECLARE_LIBID(LIBID_SBSWMIv2ServiceLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_SBSWMIV2SERVICE, "{BC5BECA8-CAB3-4D0B-839E-7A7EBCAE2C33}")
	HRESULT InitializeSecurity() throw()
	{
		return CoInitializeSecurity( NULL, -1, NULL, NULL, RPC_C_AUTHN_LEVEL_NONE,
			RPC_C_IMP_LEVEL_IDENTIFY, NULL, EOAC_NONE, NULL );
	}
	HRESULT Run(int nShowCmd = SW_HIDE) throw()
	{
		HRESULT hr = S_OK;
		hr = __super::PreMessageLoop(nShowCmd);
		if (hr == S_OK)
		{
			if (m_bService)
			{
				//可以在这里启动线程，或者什么其他东西来做自己的工作的啦
				//这里是什么都没有做了，只输出一条信息
				LogEvent(_T("widebright 的服务启动咯，呵呵 "));
				SetServiceStatus(SERVICE_RUNNING);
			}  //进入消息循环，不停的处理消息，可能最后分发到Handler去处理，调用了OnShutdown等函数的。
			__super::RunMessageLoop();
		}
		if (SUCCEEDED(hr))
		{
			hr = __super::PostMessageLoop();
		}

		//可以在适当的时候调用Uninstall函数来卸载掉服务
		//__super::Uninstall();
		return hr;
		}
};

CSBSWMIv2ServiceModule _AtlModule;



//
extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
								LPTSTR /*lpCmdLine*/, int nShowCmd)
{
	return _AtlModule.WinMain(nShowCmd);
}

