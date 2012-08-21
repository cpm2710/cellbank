
#include <windows.h>
#include <stdlib.h>
#include <assert.h>
#include <combaseapi.h>

#include <iostream>

class ImpersonateUtil{
public:
	BOOL PrintImpersonationLevel();
	BOOL EnablePrivilege();
};