convert-moftoprovider.exe -MofFile SBSUser.mof -ClassList MSFT_SBSUser MSFT_SBSMedia -IncludePath ..\cim_schema_2.26.0Final-MOFs . -OutPath . -SkipLocalize



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path ..\x64\Debug\sbswmiv2.dll -Verbose -ForceUpdate



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path ..\x64\Debug\sbswmiv2.dll -Decoupled O:BAG:BAD:(A;;0x1;;;BA)(A;;0x1;;;NS) -Impersonation True -Verbose -ForceUpdate


SBSWMIv2Host.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -ProviderPath .\sbswmiv2.dll

..\x64\Debug\SBSWMIv2Host.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -ProviderPath ..\x64\Debug\sbswmiv2.dll