convert-moftoprovider.exe -MofFile SBSUser.mof -ClassList MSFT_SBSUser MSFT_SBSMedia -IncludePath ..\cim_schema_2.26.0Final-MOFs . -OutPath . -SkipLocalize

convert-moftoprovider.exe -MofFile SBSUser.mof -ClassList MSFT_SBSUser MSFT_SBSMedia MSFT_SBSUserCreated -IncludePath ..\cim_schema_2.26.0Final-MOFs . -OutPath . -SkipLocalize



Register-CimProvider.exe -Namespace root/sbs -ProviderName sbsuser -Path ..\x64\Debug\sbswmiv2.dll -Verbose -ForceUpdate



Register-CimProvider.exe -Namespace root/sbs -ProviderName sbsuser -Path .\sbswmiv2.dll -Decoupled O:BAG:BAD:(A;;0x1;;;BA)(A;;0x1;;;NS) -Impersonation False -Verbose -ForceUpdate
SBSWMIv2Host.exe -Namespace root/sbs -ProviderName sbsuser -ProviderPath .\sbswmiv2.dll

..\x64\Debug\SBSWMIv2Host.exe -Namespace root/sbs -ProviderName sbsuser -ProviderPath ..\x64\Debug\sbswmiv2.dll

SBSWMIv2Host.exe -Namespace root/sbs -ProviderName sbsuser -ProviderPath sbswmiv2.dll

Get-CimInstance -ClassName MSFT_SBSUser -Namespace root/sbs
Invoke-CimMethod -MethodName EnableMediaServer -ClassName MSFT_SBSMedia -Arguments @{Status=[string]abc} -Namespace root/sbs



select * from MSFT_SBSUserCreated