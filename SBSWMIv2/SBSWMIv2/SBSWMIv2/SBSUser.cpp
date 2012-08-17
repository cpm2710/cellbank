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
	HRESULT hr = E_FAIL; 

    // -*- use it later
    MI_UNREFERENCED_PARAMETER(keysOnly);

	//DotNetAPI::DotNetApiPtr
	//apifff.Add((long)123);

	CComPtr<DotNetApi> spTmp; 
	hr = spTmp.CoCreateInstance(__uuidof(DotNetApi)); 
	spTmp->Add((long)123);
    // Print the names of the modules for each process.
	
    for ( i = 0; i < 20; i++ )
    {
		MSFT_SBSUser instance;

		 MSFT_SBSUser_Construct(&instance, context);
		
       

        // Post instance to wmi server
        result = MSFT_SBSUser_Post(&instance, context);
            
        // Now we can free the instance which will free the resources allocated as part of setting the properties.
        MSFT_SBSUser_Destruct(&instance);
        if(result != MI_RESULT_OK)
            break;
    }

    return result;
}