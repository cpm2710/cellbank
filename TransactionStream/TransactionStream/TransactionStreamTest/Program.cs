using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Dictionary<string, List<DeviceProperty>> lists=new Dictionary<string,List<DeviceProperty>>();
            lists.Add("sss",new List<DeviceProperty>());
            lists.Add("sssf",new List<DeviceProperty>());
            lists.Add("sssfg",new List<DeviceProperty>());
            lists.Add("sssfgg",new List<DeviceProperty>());
            DataObjectWriter writer = new DataObjectWriter();
            writer.Write(lists);
        }
    }
}
