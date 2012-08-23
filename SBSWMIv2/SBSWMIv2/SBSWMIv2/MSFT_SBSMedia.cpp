/* @migen@ */
#include <MI.h>
#include "MSFT_SBSMedia.h"
#include "SBSMedia.h"
void MI_CALL MSFT_SBSMedia_Load(
    _Outptr_result_maybenull_ MSFT_SBSMedia_Self** self,
    _In_opt_ MI_Module_Self* selfModule,
    _In_ MI_Context* context)
{
    MI_UNREFERENCED_PARAMETER(selfModule);

    *self = NULL;
    MI_Context_PostResult(context, MI_RESULT_OK);
}

void MI_CALL MSFT_SBSMedia_Unload(
    _In_opt_ MSFT_SBSMedia_Self* self,
    _In_ MI_Context* context)
{
    MI_UNREFERENCED_PARAMETER(self);

    MI_Context_PostResult(context, MI_RESULT_OK);
}

void MI_CALL MSFT_SBSMedia_Invoke_EnableMediaServer(
    _In_opt_ MSFT_SBSMedia_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_opt_z_ const MI_Char* methodName,
    _In_ const MSFT_SBSMedia* instanceName,
    _In_opt_ const MSFT_SBSMedia_EnableMediaServer* in)
{
    MI_UNREFERENCED_PARAMETER(self);
    MI_UNREFERENCED_PARAMETER(nameSpace);
    MI_UNREFERENCED_PARAMETER(className);
    MI_UNREFERENCED_PARAMETER(methodName);
    MI_UNREFERENCED_PARAMETER(instanceName);
    MI_UNREFERENCED_PARAMETER(in);

	MI_Result result;
	result = EnableMediaServer(context, NULL);


    MI_Context_PostResult(context, result);
}

