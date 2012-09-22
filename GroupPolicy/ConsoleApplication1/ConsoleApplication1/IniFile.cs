using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class IniFile
    {
        /// <summary>
        /// String to store Ini file path
        /// </summary>
        private string path;

        [SuppressUnmanagedCodeSecurityAttribute]
        internal static class SafeNativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WritePrivateProfileString(
                string section,
                string key,
                string val,
                string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern uint GetPrivateProfileString(
                string section,
                string key,
                string def,
                StringBuilder retVal,
                int size,
                string filePath);
        }

        /// <summary>
        /// IniFile Constructor.
        /// </summary>
        /// <param name="filepath">String of Ini file path</param>
        public IniFile(string filepath)
        {
            this.path = filepath;
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="section">Section name</param>
        /// <param name="key">Key Name</param>
        /// <param name="value">Value Name</param>
        public bool WriteValue(string section, string key, string value)
        {
            return SafeNativeMethods.WritePrivateProfileString(section, key, value, this.path);
        }

        /// <summary>
        /// Read Value From the Ini File
        /// </summary>
        /// <param name="section">Section name</param>
        /// <param name="key">Key Name</param>
        /// <returns>Value</returns>
        public string ReadValue(string section, string key)
        {
            StringBuilder ret = new StringBuilder(1024);
            uint read = SafeNativeMethods.GetPrivateProfileString(section, key, "", ret, ret.Capacity, this.path);
            if (0 != read)
            {
                return ret.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
