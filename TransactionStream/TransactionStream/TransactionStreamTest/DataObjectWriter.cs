using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using TransactionStream;

namespace TransactionStreamTest
{
    [DataContract]
    public class DeviceProperty : IEquatable<DeviceProperty>
    {
        /// <summary>
        /// protected constructor.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected DeviceProperty(String propertyName)
        {
            PropertyName = propertyName;
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        [DataMember]
        public String PropertyName { get; set; }

        /// <summary>
        /// It represent the UTC date and time of the property.
        /// </summary>
        /// <remarks>
        /// A property is normally time sensitive. meaning it must be associated with a state of the device
        /// in a certain time. So TimeStamp is generated with the current time when a property object is created.
        /// Devices provider will only pick the newer change of a same device.
        /// </remarks>
        [DataMember]
        public DateTime Timestamp { get; set; }

        #region IEquatable<DeviceProperty> Members

        public bool Equals(DeviceProperty other)
        {

            return other == this || (string.Equals(PropertyName, other.PropertyName));
        }

        #endregion
    }
    public class DataObjectWriter
    {
        public void Write(Dictionary<string, List<DeviceProperty>> data)
        {
            List<Type> types = new List<Type>();
            types.Add(typeof(DeviceProperty));
            Save(data, @".\abc.xml", types.AsQueryable());
        }
        private void Save(Dictionary<string, List<DeviceProperty>> target, string path, IEnumerable<Type> knownTypes)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                TransactionFileStream stream = new TransactionFileStream(path, FileAccess.Write);
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, List<DeviceProperty>>), knownTypes);
                    serializer.WriteObject(writer, target);
                }
                stream.Close();

                //stream = new TransactionFileStream(path);
                //using (XmlWriter writer = XmlWriter.Create(stream, settings))
                //{
                //    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, List<DeviceProperty>>), knownTypes);
                //    serializer.WriteObject(writer, target);
                //} stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
