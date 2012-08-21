// InstallationActions.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
 #include <process.h>

int _tmain(int argc, _TCHAR* argv[])
{
	//system("Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path \"C:\\Program Files\\SBSWMIv2\\sbswmiv2.dll\"");
	system("Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path \"C:\\Program Files\\SBSWMIv2\\sbswmiv2.dll\" -Decoupled O:BAG:BAD:(A;;0x1;;;BA)(A;;0x1;;;NS) -Impersonation False -verbose -ForceUpdate");

	system("\"C:\\Program Files\\SBSWMIv2\\SBSWMIv2Service.exe\" /Service");
	//system("C:\\Program Files\\SBSWMIv2\\SBSWMIv2Host.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -ProviderPath C:\\Program Files\\SBSWMIv2\\sbswmiv2.dll");
	
	//Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path ..\x64\Debug\sbswmiv2.dll -Decoupled O:BAG:BAD:(A;;0x1;;;BA)(A;;0x1;;;NS) -Impersonation True -verbose -ForceUpdate
	return 0;
}

