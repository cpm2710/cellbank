using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RotorsLib.Helpers
{
    public static class ConfigHelper
    {
        public static void SaveConfig()
        {
            try
            {
                using (Stream stream = new FileStream(@".\Data\config.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Constants));
                    xs.Serialize(stream, Singleton<Constants>.UniqueInstance);
                }
            }
            catch (Exception E)
            {

            }
        }

        public static void LoadConfig()
        {
            try
            {
                using (Stream stream = new FileStream(@".\Data\config.xml", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Constants));
                    Constants constants = (Constants)(xs.Deserialize(stream));
                    Singleton<Constants>.UniqueInstance.CopyFrom(constants);
                }
            }
            catch (Exception e)
            {
                Singleton<ReportMediator>.UniqueInstance.ReportStatus("Error loading the config file, will use the default values");
            }
        }
    }
}
