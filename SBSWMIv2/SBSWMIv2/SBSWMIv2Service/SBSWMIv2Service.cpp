// SBSWMIv2Service.cpp : Implementation of WinMain


#include "stdafx.h"
#include "resource.h"
#include "SBSWMIv2Service_i.h"
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <mi.h>
#include <strsafe.h>
#include <assert.h>
#include <atlbase.h>
#include <WinSvc.h>
#include <string>

using namespace ATL;
using namespace std;
#include <stdio.h>

#define STR_NAMESPACE       L"-Namespace"
#define STR_PROVIDERNAME    L"-ProviderName"
#define STR_PROVIDERPATH    L"-ProviderPath"
#define MI_MAIN_FUNCNAME    "MI_Main"

//
// DecoupledHostArgument structure holds all
// requested parameter
//
typedef struct _DecoupledHostArgument
{
    LPWSTR lpNamespace;
    LPWSTR lpProviderName;
    LPWSTR lpProviderPath;
}DecoupledHostArgument;


//
// Shutdown the provider by closing hosted provider,
// closing the application and releasing the library handle.
//
// Argument:
//  pApplication        - MI_Application object
//  pHostedProvider     - The started decoupled host provider object
//  hProviderModule     - The provider DLL handle
//
void ShutdownDecoupledHost(
    MI_Application *pApplication,
    MI_HostedProvider *pHostedProvider,
    HMODULE hProviderModule)
{
    MI_Result result;
    BOOL bFreeLibrary;

    fprintf(stdout, "%s\r\n", "Trying to shutdown decoupled provider.");

    // closing hosted provider
    result = MI_HostedProvider_Close(pHostedProvider);
    fprintf(stdout, "%s %d.\r\n", "MI_HostedProvider_Close returned", result);

    result = MI_Application_Close(pApplication);
    fprintf(stdout, "%s %d.\r\n", "MI_Application_Close returned", result);

    bFreeLibrary = FreeLibrary(hProviderModule);
    fprintf(stdout, "%s %d.\r\n", "Free provider module result = ", bFreeLibrary);
}

int HostDecoupledProvider(__in DecoupledHostArgument * pArgument)
{
    HMODULE hProvider = NULL;
    MI_MainFunction mi_main = NULL;
    MI_Application application = MI_APPLICATION_NULL;
    MI_HostedProvider hostedProvider = MI_HOSTEDPROVIDER_NULL;
    MI_Result result = MI_RESULT_OK;
    
    // load provider DLL
    hProvider = LoadLibraryExW(pArgument->lpProviderPath, NULL, 0);
    if(hProvider == NULL)
    {
        DWORD dwErrorCode = GetLastError();
        fwprintf(stderr, L"%s %s %s %d.\r\n",
            L"Failed to load library",
            pArgument->lpProviderPath,
            L"with error code",
            dwErrorCode);
        return dwErrorCode;
    }

    // query MI_Main function from provider DLL
    mi_main = (MI_MainFunction)GetProcAddress(hProvider, MI_MAIN_FUNCNAME);
    if(mi_main == NULL)
    {
        DWORD dwErrorCode = GetLastError();
        fprintf(stderr, "%s %s %s %d.\r\n",
            "Cannot find procedure",
            MI_MAIN_FUNCNAME,
            "from the provider module with error code",
            dwErrorCode);
        return dwErrorCode;
    }

    // initialize MI_Application object
    result = MI_Application_Initialize(0, NULL, NULL, &application);
    if(result != MI_RESULT_OK)
    {
        fprintf(stderr, "%s %d.\r\n",
            "Failed to initialize MI_Application with error code",
            result);
        return result;
    }

    // host the given provider as decoupled provider
    result = MI_Application_NewHostedProvider(&application,
        pArgument->lpNamespace,
        pArgument->lpProviderName,
        mi_main,
        NULL,
        &hostedProvider);
    if(result != MI_RESULT_OK)
    {
        fprintf(stderr, "%s %d.\r\n",
            "Failed to host decoupled provider with error code",
            result);
        return result;
    }

    // Successfully hosted the provider as a decoupled one,
    // now wait for the exit / quit command
    {
        BOOL quit = FALSE;
        char inputBuffer[MAX_PATH];
        do
        {
            fprintf(stdout, "\r\n%s \r\n",
            "enter 'exit' or 'quit' to terminate the decoupled host process.");
            {
                size_t finalLength = 0;
                while (finalLength == 0)
                {
                    _cgets_s(inputBuffer, sizeof(inputBuffer)/sizeof(char), &finalLength);
                }
                inputBuffer[MAX_PATH -1] = '\0';
                if ((_stricmp(inputBuffer, "exit") == 0) || (_stricmp(inputBuffer, "quit") == 0))
                {
                    // Free all resources
                    ShutdownDecoupledHost(&application, &hostedProvider, hProvider);
                    quit = TRUE;
                }
            }
        }
        while(!quit);
    }
    return 0;
}

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

				DecoupledHostArgument argument;
				int result;
				WCHAR a[]=(L"root/Standardcimv2/sbs");
				WCHAR b[]=(L"sbsuser");
				WCHAR c[]=(L"C:\\Program Files\\SBSWMIv2\\sbswmiv2.dll");
				// sbsuser -ProviderPath ..\x64\Debug\sbswmiv2.dll
				argument.lpNamespace=a;
				argument.lpProviderName=b;
				argument.lpProviderPath=c;


				result = HostDecoupledProvider(&argument);

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

