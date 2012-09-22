using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication1
{

    [DataContract(Name="Files")]
    public class FilePushPolicyCollection
    {
        private const string FilesClsid = "{215B2E53-57CE-475c-80FE-9EEC14635851}";
        private const string FileClsid = "{50BE44C8-567A-4ed1-B1D0-9234FE1F38AF}";
        public List<FilePushPolicy> FilePushPolicies { get; set; }

        public FilePushPolicyCollection()
        {
            FilePushPolicies = new List<FilePushPolicy>();
        }

        public void LoadFromPolicy(string filePath)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(filePath);
            XmlNodeList fileList = doc.SelectNodes("Files//File");
            foreach (XmlNode node in fileList)
            {
                FilePushPolicy policy = new FilePushPolicy();
                try
                {
                    policy.clsid = node.Attributes["clsid"].Value;
                    policy.name = node.Attributes["name"].Value;
                    policy.status = node.Attributes["status"].Value;
                    policy.image = int.Parse(node.Attributes["image"].Value);
                    policy.changed = node.Attributes["changed"].Value;
                    XmlNode propertiesNode = node.SelectSingleNode("Properties");
                    FilePushPolicyProperties properties = new FilePushPolicyProperties();
                    properties.action = propertiesNode.Attributes["action"].Value;
                    properties.fromPath = propertiesNode.Attributes["fromPath"].Value;
                    properties.targetPath = propertiesNode.Attributes["targetPath"].Value;
                    properties.readOnly = int.Parse(propertiesNode.Attributes["readOnly"].Value);
                    properties.archive = int.Parse(propertiesNode.Attributes["archive"].Value);
                    properties.hidden = int.Parse(propertiesNode.Attributes["hidden"].Value);
                    policy.Properties = properties;
                    this.FilePushPolicies.Add(policy);
                }
                catch (NullReferenceException)
                {
                    throw new WSSGGroupPolicyException();
                }
                //Console.WriteLine(node.Attributes["name"].Value);
            }
        }
        public void WritePolicy(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            //XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            //doc.AppendChild(declaration);
            XmlNode xmlnode = doc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            //XmlAttribute encodingAttr = doc.CreateAttribute("encoding");
            //encodingAttr.Value = "utf-8";
            //xmlnode.Attributes.Append(encodingAttr);
            //xmlnode.Attributes["encoding"].
            //<?xml version="1.0" encoding="utf-8"?>
            doc.AppendChild(xmlnode);

            XmlElement filesRoot = doc.CreateElement("Files");

            filesRoot.SetAttribute("clsid", "{215B2E53-57CE-475c-80FE-9EEC14635851}");
            foreach (FilePushPolicy policy in this.FilePushPolicies)
            {
                XmlElement fileElement = doc.CreateElement("File");
                fileElement.SetAttribute("clsid", policy.clsid);
                fileElement.SetAttribute("name", policy.name);
                fileElement.SetAttribute("status", policy.status);
                fileElement.SetAttribute("image", policy.image.ToString());
                fileElement.SetAttribute("changed", policy.changed);
                filesRoot.AppendChild(fileElement);

                XmlElement propertiesElement = doc.CreateElement("Properties");
                propertiesElement.SetAttribute("action", policy.Properties.action);
                propertiesElement.SetAttribute("fromPath", policy.Properties.fromPath);
                propertiesElement.SetAttribute("targetPath", policy.Properties.targetPath);
                propertiesElement.SetAttribute("readOnly", policy.Properties.readOnly.ToString());
                propertiesElement.SetAttribute("archive", policy.Properties.archive.ToString());
                propertiesElement.SetAttribute("hidden", policy.Properties.hidden.ToString());

                fileElement.AppendChild(propertiesElement);
            } doc.AppendChild(filesRoot);

            XmlTextWriter xmlWriter = new XmlTextWriter(filePath, Encoding.UTF8);
            doc.Save(xmlWriter);
        }
    }
    [DataContract(Name="File")]
    public class FilePushPolicy
    {
        //<Files clsid="{215B2E53-57CE-475c-80FE-9EEC14635851}">
        //<File clsid="{50BE44C8-567A-4ed1-B1D0-9234FE1F38AF}" name="ggg" status="ggg" image="0" changed="2012-09-20 07:38:43" 
        //uid="{8F79CDFA-23A5-44CF-849F-80E270F2301D}"><Properties action="C" fromPath="ggg" targetPath="ggg" readOnly="0" archive="1" hidden="0"/></File>
        [DataMember(Name="clsid")]
        public string clsid { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int image { get; set; }
        public string changed { get; set; }
        public FilePushPolicyProperties Properties { get; set; }
    }
    public class FilePushPolicyProperties
    {
        public string action { get; set; }
        public string fromPath { get; set; }
        public string targetPath { get; set; }
        public int readOnly { get; set; }
        public int archive { get; set; }
        public int hidden { get; set; }
    }
}
