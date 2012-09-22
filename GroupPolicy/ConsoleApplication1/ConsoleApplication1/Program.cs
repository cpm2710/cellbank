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
        #region XML相关的静态方法
        /// <summary>
        /// 使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name=“T“>需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name=“obj“>需要序列化的对象</param>
        public static string XmlSerialize<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 使用XmlSerializer反序列化对象
        /// </summary>
        /// <param name=“xmlOfObject“>需要反序列化的xml字符串</param>
        public static T XmlDeserialize<T>(string xmlOfObject) where T : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sr = new StreamWriter(ms, Encoding.UTF8))
                {
                    sr.Write(xmlOfObject);
                    sr.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(ms) as T;
                }
            }
        }

        /// <summary>
        /// Serialize one object fo the file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obje"></param>
        /// <param name="filePath"></param>
        public static void XmlSerializeToFile<T>(T obje,string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter textWriter = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                serializer.Serialize(textWriter, obje);
            }
        }

        /// <summary>
        /// 使用XmlSerializer反序列化对象
        /// </summary>
        /// <param name=“xmlOfObject“>需要反序列化的xml字符串</param>
        public static T XmlDeserializeFromFile<T>(string filePath) where T : class
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(fs) as T;
            }
        }
        #endregion

        
        

        static void Main(string[] args)
        {
            Files files = new Files();
            files.clsid = "abc";
            XmlSerializeToFile(files,"Files2.xml");


            Files loadedFiles = (Files)XmlDeserializeFromFile<Files>(@"SampleXmls\Files.xml");
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
