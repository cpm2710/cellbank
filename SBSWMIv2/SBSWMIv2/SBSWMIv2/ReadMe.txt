convert-moftoprovider.exe -MofFile SBSUser.mof -ClassList MSFT_SBSUser -IncludePath %CIM2260DIR% . -OutPath . -SkipLocalize



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path sbswmiv2.dll -verbose -ForceUpdate



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path sbswmiv2.dll -Decoupled O:BAG:BAD:(A;;0x1;;;BA)(A;;0x1;;;NS) -Impersonation True -verbose -ForceUpdate
