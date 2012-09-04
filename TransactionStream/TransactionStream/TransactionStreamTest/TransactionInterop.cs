using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TransactionStreamTest
{
    class TransactionInterop
    {
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint CREATE_NEW = 1;
        public uint CREATE_ALWAYS = 2;
        public const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
           string lpFileName,
          uint dwDesiredAccess,
          uint dwShareMode,
          IntPtr lpSecurityAttributes, uint dwCreationDisposition,
         uint dwFlagsAndAttributes,
           IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFileTransacted(
         string lpFileName,
           uint dwDesiredAccess,
          uint dwShareMode,
          IntPtr lpSecurityAttributes,
          uint dwCreationDisposition,
          uint dwFlagsAndAttributes,
          IntPtr hTemplateFile,
          IntPtr hTransaction,
          IntPtr pusMiniVersion,
          IntPtr pExtendedParameter);

        [DllImport("ktmw32.dll", SetLastError = true)]
        public static extern IntPtr CreateTransaction(
           IntPtr lpTransactionAttributes,
           IntPtr uow,
           uint createOptions,
           uint isolationLevel,
          uint isolationFlags,
           uint timeout,
           string description);

        [DllImport("ktmw32.dll", SetLastError = true)]
        public static extern bool CommitTransaction(
           IntPtr transaction);

        [DllImport("ktmw32.dll", SetLastError = true)]
        public static extern bool RollbackTransaction(
            IntPtr transaction);

        [DllImport("Kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            int nLength;
            IntPtr lpSecurityDescriptor;
            int bInheritHandle;
        }

        //[DllImport("ktmw32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern SafeFileHandle CreateTransaction(SECURITY_ATTRIBUTES securityAttributes, IntPtr guid, int options, int isolationLevel, int isolationFlags, int milliSeconds, string description);

        //[DllImport("ktmw32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern bool CommitTransaction(SafeFileHandle transaction);

        //[DllImport("ktmw32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern bool RollbackTransaction(SafeFileHandle transaction);

        //[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern bool DeleteFileTransacted(string filename, SafeFileHandle transaction);
    }
}
