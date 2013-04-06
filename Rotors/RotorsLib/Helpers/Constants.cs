using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Helpers
{
    public static class Constants
    {
        private static string serviceVariableName = "services";
        public static string ServiceVariableName
        {
            get { return serviceVariableName; }
            set
            {
                serviceVariableName = value;
            }
        }

        private static string fileVariableName = "files";
        public static string FileVariableName
        {
            get { return fileVariableName; }
            set
            {
                fileVariableName = value;
            }

        }

        private static string userName = @"a";
        public static string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
            }

        }

        public static string passWord = "User@123";

        public static string PassWord
        {
            get { return passWord; }
            set
            {
                passWord = value;
            }

        }

        public static string machineName = "andess1server";
        public static string MachineName
        {
            get { return machineName; }
            set
            {
                machineName = value;
            }

        }

        public static string domain = "andess1.local";
        public static string Domain
        {
            get { return domain; }
            set
            {
                domain = value;
            }
        }

        public static string sourceRootPath = @"D:\WorkSpace\BinaryRootSourcePath";
        public static string SourceRootPath
        {
            get { return sourceRootPath; }
            set
            {
                sourceRootPath = value;
            }
        }

        //@"\\andess1server\c$\windows\system32\Essentials"
        //public const string System32EssentialsPath = @"D:\WorkSpace\DestinationSystem32Files\Essentials";
        public static string system32EssentialsPath = @"\\andess1server\c$\windows\system32\Essentials";
        public static string System32EssentialsPath
        {
            get { return system32EssentialsPath; }
            set
            {
                system32EssentialsPath = value;
            }
        }

        public static string gacEssentialsPath = @"\\andess1server\c$\Windows\Microsoft.NET\assembly\GAC_MSIL";

        public static string GacEssentialsPath
        {
            get { return gacEssentialsPath; }
            set
            {
                gacEssentialsPath = value;
            }

        }
    }
}
