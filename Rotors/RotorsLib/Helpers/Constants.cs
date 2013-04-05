using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public static class Constants
    {
        public const string ServiceVariableName = "services";
        public const string FileVariableName = "files";
        public const string UserName = @"a";
        public const string PassWord = "User@123";
        public const string MachineName = "andess1server";
        public const string Domain = "andess1.local";

        public const string SourceRootPath = @"D:\WorkSpace\BinaryRootSourcePath";

        //@"\\andess1server\c$\windows\system32\Essentials"
        //public const string System32EssentialsPath = @"D:\WorkSpace\DestinationSystem32Files\Essentials";
        public const string System32EssentialsPath = @"\\andess1server\c$\windows\system32\Essentials";

        public const string GacEssentialsPath = @"\\andess1server\c$\Windows\Microsoft.NET\assembly\GAC_MSIL";
    }
}
