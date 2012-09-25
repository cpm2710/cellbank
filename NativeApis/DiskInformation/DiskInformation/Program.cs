using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DiskInformation
{
    class Program
    {
       

        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            IntPtr hFile = Native.CreateFile(@"\\.\" + "Volume{55a2458d-f996-11e1-be65-806e6f6e6963}", Native.GENERIC_READ, Native.FILE_SHARE_READ | Native.FILE_SHARE_WRITE, IntPtr.Zero, Native.OPEN_EXISTING, 0, IntPtr.Zero);
            if (hFile.ToInt32() == Native.INVALID_HANDLE_VALUE)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            int size = 0x400; // some big size
            IntPtr buffer = Marshal.AllocHGlobal(size);
            int bytesReturned = 0;
            try
            {
                if (!Native.DeviceIoControl(hFile, Native.IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS, IntPtr.Zero, 0, buffer, size, out bytesReturned, IntPtr.Zero))
                {
                    // do nothing here on purpose
                }
            }
            finally
            {
                Native.CloseHandle(hFile);
            }

            if (bytesReturned > 0)
            {
                int numberOfDiskExtents = (int)Marshal.PtrToStructure(buffer, typeof(int));
                for (int i = 0; i < numberOfDiskExtents; i++)
                {
                    IntPtr extentPtr = new IntPtr(buffer.ToInt32() + Marshal.SizeOf(typeof(long)) + i * Marshal.SizeOf(typeof(Native.DISK_EXTENT)));
                    Native.DISK_EXTENT extent = (Native.DISK_EXTENT)Marshal.PtrToStructure(extentPtr, typeof(Native.DISK_EXTENT));
                   
                    numbers.Add(extent.DiskNumber);
                }
            }
            Marshal.FreeHGlobal(buffer);
            Console.WriteLine(numbers[0].ToString());
            Console.Read();
        }
    }
}
