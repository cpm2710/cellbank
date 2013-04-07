// author: andyliuliming@outlook.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Helpers
{
    [Serializable]
    public class Constants
    {
        private string serviceVariableName = "services";
        public string ServiceVariableName
        {
            get { return serviceVariableName; }
            set
            {
                serviceVariableName = value;
            }
        }

        private string fileVariableName = "files";
        public string FileVariableName
        {
            get { return fileVariableName; }
            set
            {
                fileVariableName = value;
            }

        }

        private string userName = @"a";
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
            }

        }

        private string passWord = "User@123";
        public string PassWord
        {
            get { return passWord; }
            set
            {
                passWord = value;
            }

        }

        private string machineName = "andess1server";
        public string MachineName
        {
            get { return machineName; }
            set
            {
                machineName = value;
            }

        }

        private string domain = "";
        public string Domain
        {
            get { return domain; }
            set
            {
                domain = value;
            }
        }

        private string sourceRootPath = @"D:\WorkSpace\BinaryRootSourcePath";
        public string SourceRootPath
        {
            get { return sourceRootPath; }
            set
            {
                sourceRootPath = value;
            }
        }        
    }
}
