convert-moftoprovider.exe -MofFile SBSUser.mof -ClassList MSFT_SBSUser MSFT_SBSMedia -IncludePath ..\cim_schema_2.26.0Final-MOFs . -OutPath . -SkipLocalize



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path sbswmiv2.dll -verbose -ForceUpdate



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path sbswmiv2.dll -Decoupled O:BAG:BAD:(A;;0x1;;;BA)(A;;0x1;;;NS) -Impersonation True -verbose -ForceUpdate


SBSWMIv2Host.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -ProviderPath sbswmiv2.dll