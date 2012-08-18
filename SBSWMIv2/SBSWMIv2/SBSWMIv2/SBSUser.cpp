#include <windows.h>
#include <stdlib.h>
#include <assert.h>
#include <atlbase.h>
#include <combaseapi.h>
#include "MSFT_SBSUser.h"
#include "SBSUser.h"

#include   "IAds.h"
#include   "Adshlp.h"
#include   "activeds.h"

#include "..\\HelpUtil\Log.h"
#pragma comment( lib, "..//x64/debug//HelpUtil.lib" ) 

#pragma comment( lib,  "ActiveDS.lib")



MI_Result EnumerateSBSUsers(
    _In_ MI_Context* context,
    _In_ MI_Boolean keysOnly)
{
	 
    MI_Result result = MI_RESULT_OK;
	int i=0;
	//HRESULT hr = E_FAIL; 

    // -*- use it later
    MI_UNREFERENCED_PARAMETER(keysOnly);



	

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