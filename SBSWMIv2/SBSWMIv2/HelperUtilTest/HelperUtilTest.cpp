// HelperUtilTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "..\\HelpUtil\\Log.h"
#pragma comment( lib, "..//x64/release//HelpUtil.lib" ) 


#if defined (USINGPROJECTSYSTEM)
#import "../Library/DotNetApi.tlb" raw_interfaces_only
#else  // Compiling from the command line, all files in the same directory
#import "DotNetApi.tlb" //raw_interfaces_only
#endif  


#include   "IAds.h"
#include   "Adshlp.h"
#include   "activeds.h"
#include "atlbase.h"
#include <combaseapi.h>
#include <iostream>
using namespace std;

#pragma comment( lib,  "ActiveDS.lib")
#pragma comment( lib,  "adsiid.lib")

HRESULT PrintAllObjects(IADsContainer* pContainer)
{
    HRESULT hr;
     
    if(NULL == pContainer) 
    {
        return E_INVALIDARG;
    }
     
    IEnumVARIANT *pEnum = NULL;

    // Create an enumerator object in the container.
    hr = ADsBuildEnumerator(pContainer, &pEnum);
    if(SUCCEEDED(hr))
    {
        VARIANT var;
        ULONG ulFetched = 0L;

        // Get the next contained object.
        while(S_OK == (hr = ADsEnumerateNext(pEnum, 1, &var, &ulFetched)) && (ulFetched > 0))
        {
            IADs *pADs;

            // Print the object
            hr = V_DISPATCH(&var)->QueryInterface(IID_IADs, (void**)&pADs);
            if(SUCCEEDED(hr))
            {
                CComBSTR sbstr;
                IADsContainer *pChildContainer;

                hr = pADs->get_Name(&sbstr);
                if(SUCCEEDED(hr))
                {
                    wprintf(sbstr);
                    wprintf(L"\n");
                }

                hr = pADs->QueryInterface(IID_IADsContainer, (void**)&pChildContainer);
                if(SUCCEEDED(hr))
                {
                    // If the retrieved object is a container, recursively print its contents as well.
                    PrintAllObjects(pChildContainer);
                }
                
                pADs->Release();
            }
            
            // Release the VARIANT.
            VariantClear(&var);
        }
        
        ADsFreeEnumerator(pEnum);
    }

    return hr;
}

int _tmain(int argc, _TCHAR* argv[])
{
	//Write Log begins.
	Log *log=new Log();
	string a("sss");
	log->LogMessage(a);
	delete(log);
	//Write Log ends.

	//DotNet COM Interface begins
	CoInitialize(NULL);
	CLSID ID=__uuidof(DotNetApi::DotNetApiImpl);
	DotNetApi::DotNetApiPtr ptr(ID);
	long asss=ptr->Add(112);
	asss++;
	cout<<asss<<endl;
	long result=asss;
	CoUninitialize();
	//DotNet COM Interface ends




	//IADs *pADs = NULL;
	IADsContainer *pCls = NULL;
	HRESULT hr = S_OK;
	BSTR bstr;

	hr = ADsGetObject(L"WinNT://.,computer", IID_IADsContainer, (void**)&pCls);
	if(FAILED(hr)){return -1;}

	//pADs->get_Schema(&bstr);
	//hr = ADsGetObject(bstr, IID_IADsClass, (void**)&pCls);
	//pADs->Release();
	//SysFreeString(bstr);

	//if(FAILED(hr)){return -1;}
	
	PrintAllObjects(pCls);
	VARIANT_BOOL isContainer;
	//pCls->get_Container(&isContainer);

	if(isContainer) 
		printf("Object is a container.\n");
	else
		printf("Object is not a container.\n");

	//pCls->Release();
	
	return 0;
}

