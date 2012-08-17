convert-moftoprovider.exe -MofFile SBSUser.mof -ClassList MSFT_SBSUser -IncludePath %CIM2260DIR% . -OutPath . -SkipLocalize



Register-CimProvider.exe -Namespace root/Standardcimv2/sbs -ProviderName sbsuser -Path sbsuserwmi.dll -verbose -ForceUpdate



