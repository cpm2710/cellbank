using Microsoft.Isam.Esent.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TransactionStreamTest
{
    class Program
    {

        static void Main(string[] args)
        {

            var dict = new PersistentDictionary<string, int>("DataDir");
            // insert some data
            int total = (from x in dict where x.Key.StartsWith("Mar") select x.Value).Sum();

            string file1 = "file1.txt";
            string file2 = "file2.txt";
            if (File.Exists(file1))
            {
                File.Delete("file1.txt");
            }
            if (!File.Exists(file2))
            {
                File.Create("file2.txt");
            }

            IntPtr transaction = TransactionInterop.CreateTransaction(IntPtr.Zero, IntPtr.Zero, 0, 0, 0, 0, null);
            SafeFileHandle handle = TransactionInterop.CreateFileTransacted(file1, TransactionInterop.GENERIC_READ | TransactionInterop.GENERIC_WRITE, 0, IntPtr.Zero, TransactionInterop.CREATE_NEW, 0, IntPtr.Zero, transaction, IntPtr.Zero, IntPtr.Zero);

            //
            // Check handle.
            //
            if (handle.IsInvalid)
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());

            //        
            // Using pattern for stream operations.
            //
            using (FileStream fs = new FileStream(handle,  FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Hello");
                }
            }
            Console.WriteLine("Press Y to commit");

            char c = Console.ReadKey().KeyChar;
            
            if (c == 'y' || c == 'Y')
                TransactionInterop.CommitTransaction(transaction);
            else
                TransactionInterop.RollbackTransaction(transaction);

            //   using (FileManager manager = new FileManager())
            //{
            //       manager.DeleteFile("file1.txt");
            //       Console.WriteLine("file1.txt is marked for deletion in current transaction. Press Enter...");
            //       Console.ReadLine();

            //       //throw new Exception("something very bad happens here");

            //       manager.DeleteFile("file2.txt");
            //       Console.WriteLine("file2.txt is marked for deletion in current transaction.");

            //       manager.Commit();
            //   }

            //Dictionary<string, List<DeviceProperty>> lists=new Dictionary<string,List<DeviceProperty>>();
            //lists.Add("sss",new List<DeviceProperty>());
            //lists.Add("sssf",new List<DeviceProperty>());
            //lists.Add("sssfg",new List<DeviceProperty>());
            //lists.Add("sssfgg",new List<DeviceProperty>());
            //DataObjectWriter writer = new DataObjectWriter();
            //writer.Write(lists);
        }
    }
}
