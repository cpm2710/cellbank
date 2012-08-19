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
		// TODO : Call CoInitializeSecurity and provide the appropriate security settings for your service
		// Suggested - PKT Level Authentication, 
		// Impersonation Level of RPC_C_IMP_LEVEL_IDENTIFY 
		// and an appropriate Non NULL Security Descriptor.

		return S_OK;
	}
	};

CSBSWMIv2ServiceModule _AtlModule;



//
extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
								LPTSTR /*lpCmdLine*/, int nShowCmd)
{
	return _AtlModule.WinMain(nShowCmd);
}

