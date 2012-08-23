#include <windows.h>
#include <Psapi.h>
#include <strsafe.h>
#include <MI.h>

MI_Result EnableMediaServer(
    _In_ MI_Context* context,
    _In_ MI_ConstStringField* commandLine);