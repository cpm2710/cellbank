#include <windows.h>
#include <stdlib.h>
#include <assert.h>
#include <atlbase.h>
#include <combaseapi.h>
#include "MSFT_SBSMedia.h"
#include "SBSUser.h"
#include "Helper.h"
#include "..\\HelpUtil\Log.h"
#pragma comment( lib, "..//x64/release//HelpUtil.lib" ) 

#include   "IAds.h"
#include   "Adshlp.h"
#include   "activeds.h"
#include "atlbase.h"
#include <combaseapi.h>
#pragma comment( lib,  "ActiveDS.lib")
#pragma comment( lib,  "adsiid.lib")

MI_Result EnableMediaServer(
    _In_ MI_Context* context,
    _In_ MI_ConstStringField *keysOnly){
		Log *log=new Log();
		//log->LogMessage(keysOnly.value);
		MI_Result result;
	MSFT_SBSMedia_EnableMediaServer instanceForReturnValue;
	result = MSFT_SBSMedia_EnableMediaServer_Construct(&instanceForReturnValue, context);

	if(result != MI_RESULT_OK)
	{
		MI_Context_PostResult(context, result);
		log->LogMessage("error");
		return MI_RESULT_FAILED;
	}
	MSFT_SBSMedia_EnableMediaServer_Set_MIReturn(&instanceForReturnValue,123);

	result=MSFT_SBSMedia_EnableMediaServer_Destruct(&instanceForReturnValue);

	 if(result != MI_RESULT_OK)
        {
            MI_Context_PostResult(context, result);
			log->LogMessage("error2");
			return MI_RESULT_FAILED;
        }
	//MI_Context_PostResult(context, MI_RESULT_OK);
	delete(log);
	return result;
}