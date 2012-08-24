#include <windows.h>
#include <Psapi.h>
#include <strsafe.h>
#include <MI.h>

MI_Result EnumerateSBSUsers(
    _In_ MI_Context* context,
    _In_ MI_Boolean keysOnly);

MI_Result EnableSBSUserCreatedIndication(__in MI_Context *context,
                                         _In_opt_z_ const MI_Char *lpwszNamespace);


MI_Result SBSUserCreatedSubscribe(__in MI_Context *context);