#include <windows.h>
#include <stdlib.h>
#include <assert.h>
#include <combaseapi.h>
#include "MSFT_SBSUser.h"
#include "SBSUser.h"

MI_Result EnumerateSBSUsers(
    _In_ MI_Context* context,
    _In_ MI_Boolean keysOnly)
{
	 
    MI_Result result = MI_RESULT_OK;

    // -*- use it later
    MI_UNREFERENCED_PARAMETER(keysOnly);


    // Print the names of the modules for each process.
	int i=0;
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