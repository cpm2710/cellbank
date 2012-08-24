/* @migen@ */
#include <MI.h>
#include "MSFT_SBSUserCreated.h"
#include "SBSUser.h"

void MI_CALL MSFT_SBSUserCreated_Load(
    _Outptr_result_maybenull_ MSFT_SBSUserCreated_Self** self,
    _In_opt_ MI_Module_Self* selfModule,
    _In_ MI_Context* context)
{
    MI_UNREFERENCED_PARAMETER(selfModule);

    *self = NULL;
    MI_Context_PostResult(context, MI_RESULT_OK);
}

void MI_CALL MSFT_SBSUserCreated_Unload(
    _In_opt_ MSFT_SBSUserCreated_Self* self,
    _In_ MI_Context* context)
{
    MI_UNREFERENCED_PARAMETER(self);

    MI_Context_PostResult(context, MI_RESULT_OK);
}

void MI_CALL MSFT_SBSUserCreated_EnableIndications(
    _In_opt_ MSFT_SBSUserCreated_Self* self,
    _In_ MI_Context* indicationsContext,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className)
{
    /* TODO: store indicationsContext for later usage */
    /* NOTE: Do not call MI_Context_PostResult on this context */

    MI_UNREFERENCED_PARAMETER(self);
    MI_UNREFERENCED_PARAMETER(indicationsContext);
    MI_UNREFERENCED_PARAMETER(nameSpace);
    MI_UNREFERENCED_PARAMETER(className);

	EnableSBSUserCreatedIndication(indicationsContext, nameSpace);
}

void MI_CALL MSFT_SBSUserCreated_DisableIndications(
    _In_opt_ MSFT_SBSUserCreated_Self* self,
    _In_ MI_Context* indicationsContext,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className)
{
    /* TODO: stop background thread that monitors subscriptions */

    MI_UNREFERENCED_PARAMETER(self);
    MI_UNREFERENCED_PARAMETER(nameSpace);
    MI_UNREFERENCED_PARAMETER(className);

    MI_Context_PostResult(indicationsContext, MI_RESULT_OK);
}

void MI_CALL MSFT_SBSUserCreated_Subscribe(
    _In_opt_ MSFT_SBSUserCreated_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_opt_ const MI_Filter* filter,
    _In_opt_z_ const MI_Char* bookmark,
    _In_ MI_Uint64  subscriptionID,
    _Outptr_result_maybenull_ void** subscriptionSelf)
{
    MI_UNREFERENCED_PARAMETER(self);
    MI_UNREFERENCED_PARAMETER(nameSpace);
    MI_UNREFERENCED_PARAMETER(className);
    MI_UNREFERENCED_PARAMETER(filter);
    MI_UNREFERENCED_PARAMETER(bookmark);
    MI_UNREFERENCED_PARAMETER(subscriptionID);
    MI_UNREFERENCED_PARAMETER(subscriptionSelf);

	*subscriptionSelf = NULL;
	SBSUserCreatedSubscribe(context);
    MI_Context_PostResult(context, MI_RESULT_OK);
}

void MI_CALL MSFT_SBSUserCreated_Unsubscribe(
    _In_opt_ MSFT_SBSUserCreated_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_ MI_Uint64  subscriptionID,
    _In_opt_ void* subscriptionSelf)
{
    MI_UNREFERENCED_PARAMETER(self);
    MI_UNREFERENCED_PARAMETER(nameSpace);
    MI_UNREFERENCED_PARAMETER(className);
    MI_UNREFERENCED_PARAMETER(subscriptionID);
    MI_UNREFERENCED_PARAMETER(subscriptionSelf);

    MI_Context_PostResult(context, MI_RESULT_NOT_SUPPORTED);
}

