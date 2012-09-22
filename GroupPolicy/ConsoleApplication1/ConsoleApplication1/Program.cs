using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    class Program
    {
        




        static void Main(string[] args)
        {
            FilesFile filePolicy=new FilesFile();
            GroupPlicyUtil.CreateFileGroupPolicy(Guid.NewGuid(), false,filePolicy);


            ShortcutsShortcut shortcut = new ShortcutsShortcut();
            GroupPlicyUtil.CreateShortcutGroupPolicy(Guid.NewGuid(), false, shortcut);


            GroupPlicyUtil.SetSecurityTemplatePolicy(Guid.NewGuid(), true, new Dictionary<string, string>());
           // Files files = new Files();
           // files.clsid = "abc";
           // XmlSerializeHelper.XmlSerializeToFile(files, "Files2.xml");


           // Files loadedFiles = (Files)XmlSerializeHelper.XmlDeserializeFromFile<Files>(@"SampleXmls\Files.xml");

           //XmlSerializeHelper.XmlSerializeToFile<Files>(loadedFiles, "Files2.xml");
            //FilePushPolicyCollection collection = new FilePushPolicyCollection();

            //collection.LoadFromPolicy("Files.xml");
            //collection.WritePolicy("Files2.xml");


            //SerializeToXML(collection);

            //ConnectionOptions options = new ConnectionOptions();
            //ManagementScope scope = new ManagementScope(@"\\.\root\RSOP\Computer", options);

            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM RSOP_SecuritySettingBoolean"));

            //ManagementObjectCollection mos=searcher.Get();
            //foreach (ManagementObject o in mos)
            //{
            //    Console.WriteLine("Key Name: {0}", o["KeyName"]);
            //    Console.WriteLine("Precedence: {0}", o["Precedence"]);
            //    Console.WriteLine("Setting: {0}", o["Setting"]);
            //}

            //Console.Read();
            //ManagementClass mc = new ManagementClass("win32_share");
            //ManagementBaseObject inParams = mc.GetMethodParameters("Create");
            //inParams["Description"] = "MySharedFolder";
            //inParams["Name"] = "SharedFolderName3";
            //inParams["Path"] = "D:\\ShareFolder3";
            //inParams["Type"] = 0;
            //inParams["MaximumAllowed"] = null;
            //inParams["Password"] = null;
            //inParams["Access"] = null; // Make Everyone has full control access.
            //mc.InvokeMethod("Create", inParams, null);
        }
    }
}
