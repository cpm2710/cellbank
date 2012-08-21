#include "Helper.h"
using namespace std;
BOOL ImpersonateUtil::
	 PrintImpersonationLevel(){
	DWORD dwLen = 0;
    BOOL bRes = FALSE;
    HANDLE hToken = NULL;
    LPVOID pBuffer = NULL;

    
    bRes = OpenThreadToken(GetCurrentThread(), TOKEN_QUERY , TRUE, &hToken); 
    if(!bRes) return FALSE;

    bRes = GetTokenInformation(hToken, TokenImpersonationLevel, NULL, 0, &dwLen);
    
    pBuffer = HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, dwLen);
    if(NULL == pBuffer) return FALSE;
    
    if (!GetTokenInformation(hToken, TokenImpersonationLevel, pBuffer, dwLen, &dwLen)) 
    {
        CloseHandle(hToken);
        HeapFree(GetProcessHeap(), 0, pBuffer);
        return FALSE;
    }    
	cout<<*(SECURITY_IMPERSONATION_LEVEL *)pBuffer<<endl;
    CloseHandle(hToken);
    HeapFree(GetProcessHeap(), 0, pBuffer);    
    return bRes;
};
BOOL ImpersonateUtil:: EnablePrivilege()
{
    LUID PrivilegeRequired ;
    DWORD dwLen = 0, iCount = 0;
    BOOL bRes = FALSE;
    HANDLE hToken = NULL;
    LPVOID pBuffer = NULL;
    TOKEN_PRIVILEGES* pPrivs = NULL;

    bRes = LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &PrivilegeRequired);
    if( !bRes) return FALSE;
    
    bRes = OpenThreadToken(GetCurrentThread(), TOKEN_QUERY | TOKEN_ADJUST_PRIVILEGES, TRUE, &hToken); 
    if(!bRes) return FALSE;

    bRes = GetTokenInformation(hToken, TokenPrivileges, NULL, 0, &dwLen);
    if (TRUE == bRes)
    {
        CloseHandle(hToken);
        return FALSE;
    }
    pBuffer = HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, dwLen);
    if(NULL == pBuffer) return FALSE;
    
    if (!GetTokenInformation(hToken, TokenPrivileges, pBuffer, dwLen, &dwLen)) 
    {
        CloseHandle(hToken);
        HeapFree(GetProcessHeap(), 0, pBuffer);
        return FALSE;
    }

    // Iterate through all the privileges and enable the one required
    bRes = FALSE;
    pPrivs = (TOKEN_PRIVILEGES*)pBuffer;
    for(iCount = 0; iCount < pPrivs->PrivilegeCount; iCount++)
    {
        if (pPrivs->Privileges[iCount].Luid.LowPart == PrivilegeRequired.LowPart &&
          pPrivs->Privileges[iCount].Luid.HighPart == PrivilegeRequired.HighPart )
        {
            pPrivs->Privileges[iCount].Attributes |= SE_PRIVILEGE_ENABLED;
            // here it's found
            bRes = AdjustTokenPrivileges(hToken, FALSE, pPrivs, dwLen, NULL, NULL);
            break;
        }
    }

    CloseHandle(hToken);
    HeapFree(GetProcessHeap(), 0, pBuffer);    
    return bRes;
};
