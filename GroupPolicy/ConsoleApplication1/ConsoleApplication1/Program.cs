﻿using System;
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
        #endregion

        static public void SerializeToXML(Files devices)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Files));
            TextWriter textWriter = new StreamWriter(@"Files2.xml");
            serializer.Serialize(textWriter, devices);
            textWriter.Close();
            //DataContractSerializer serializer = new DataContractSerializer(typeof(FilePushPolicyCollection));
            //FileStream fs = new FileStream(@"serialized.xml", FileMode.Create);
            //serializer.WriteObject(fs, movie);
            //fs.Close();
        }
        static public void SerializeFromXml()
        {

        }

        static void Main(string[] args)
        {
            Files devices = new Files();
            devices.clsid = "abc";
            SerializeToXML(devices);
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
