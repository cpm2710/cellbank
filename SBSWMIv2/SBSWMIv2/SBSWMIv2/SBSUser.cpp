#include <windows.h>
#include <stdlib.h>
#include <assert.h>
#include <atlbase.h>
#include <combaseapi.h>
#include "MSFT_SBSUser.h"
#include "SBSUser.h"




// Ignoring the server namespace and using named guids:
#if defined (USINGPROJECTSYSTEM)
#import "../Library/DotNetAPI.tlb" no_namespace named_guids
#else  // Compiling from the command line, all files in the same directory
#import "DotNetAPI.tlb" no_namespace named_guids
#endif  

MI_Result EnumerateSBSUsers(
    _In_ MI_Context* context,
    _In_ MI_Boolean keysOnly)
{
	 
    MI_Result result = MI_RESULT_OK;
	int i=0;
	//HRESULT hr = E_FAIL; 

    // -*- use it later
    MI_UNREFERENCED_PARAMETER(keysOnly);

	/*CoInitialize(NULL);
	CLSID ID=__uuidof(DotNetApi::DotNetApiImpl);

	DotNetApi::DotNetApiPtr ptr(ID);

	long asss=ptr->Add(112);
	
	

	CoUninitialize();*/

    // Print the names of the modules for each process.
	
    for ( i = 0; i < 20; i++ )
    {
		MSFT_SBSUser instance;

		MI_Result result = MSFT_SBSUser_Construct(&instance, context);

		MI_Char szUserName[MAX_PATH] = L"andy";
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

    return result;
}