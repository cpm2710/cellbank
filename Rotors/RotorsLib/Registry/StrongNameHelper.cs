using RotorsLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Registry
{
    public static class StrongNameHelper
    {
        public static void DisableStrongName()
        {
            RotorsLib.Wmi.Registry.RegistryRemote remote = new Wmi.Registry.RegistryRemote(Constants.UserName, Constants.PassWord, Constants.MachineName);

            remote.DeleteValue(Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\StrongName\Verification\*,31bf3856ad364e35", "TestPublicKey");
            remote.DeleteKey(Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\StrongName\Verification\*,31bf3856ad364e35");
            remote.CreateKey(Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\StrongName\Verification\*,31BF3856AD364E35");

            remote.DeleteValue(Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, @"SOFTWARE\Wow6432Node\Microsoft\StrongName\Verification\*,31bf3856ad364e35", "TestPublicKey");
            remote.DeleteKey(Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, @"SOFTWARE\Wow6432Node\Microsoft\StrongName\Verification\*,31bf3856ad364e35");
            remote.CreateKey(Wmi.Registry.baseKey.HKEY_LOCAL_MACHINE, @"SOFTWARE\Wow6432Node\Microsoft\StrongName\Verification\*,31BF3856AD364E35");

            //SOFTWARE\Wow6432Node\Microsoft\StrongName\Verification\*,31BF3856AD364E35
            //remote.CreateKey("HKLM\Software\Microsoft\StrongName\Verification\*,31BF3856AD364E35",
        }
    }
}
