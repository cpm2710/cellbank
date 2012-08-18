#include <windows.h>
#include <stdlib.h>
#include <assert.h>
#include <atlbase.h>
#include <combaseapi.h>
#include "MSFT_SBSUser.h"
#include "SBSUser.h"

#include "..\\HelpUtil\Log.h"
#pragma comment( lib, "..//x64/debug//HelpUtil.lib" ) 

#include   "IAds.h"
#include   "Adshlp.h"
#include   "activeds.h"
#include "atlbase.h"
#include <combaseapi.h>

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

MI_Result EnumerateSBSUsers(
    _In_ MI_Context* context,
    _In_ MI_Boolean keysOnly)
{
	 
    MI_Result result = MI_RESULT_OK;
	int i=0;
	//HRESULT hr = E_FAIL; 
	IADsContainer *pCls = NULL;
	HRESULT hr = S_OK;
	//BSTR bstr;

    // -*- use it later
    MI_UNREFERENCED_PARAMETER(keysOnly);

	hr = ADsGetObject(L"WinNT://.,computer", IID_IADsContainer, (void**)&pCls);
		
	PrintAllObjects(pCls);

	Log *log=new Log();
	
	

    // Print the names of the modules for each process.
	
    for ( i = 0; i < 20; i++ )
    {
		MSFT_SBSUser instance;

		MI_Result result = MSFT_SBSUser_Construct(&instance, context);

		MI_Char szUserName[MAX_PATH] = L"andy";

		string a("returns sbs user:");
		log->LogMessage(a);

		MSFT_SBSUser_Set_UserName(&instance,szUserName);
		MSFT_SBSUser_Set_PassWord(&instance,szUserName);
       
//		 instance.UserName="";
        // Post instance to wmi server
        result = MSFT_SBSUser_Post(&instance, context);
            
        // Now we can free the instance which will free the resources allocated as part of setting the properties.
        MSFT_SBSUser_Destruct(&instance);
        if(result != MI_RESULT_OK)
            break;
    }
	delete(log);
    return result;
}