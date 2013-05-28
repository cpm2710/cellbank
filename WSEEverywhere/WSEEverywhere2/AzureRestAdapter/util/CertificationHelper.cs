using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AzureRestAdapter
{
    public class CertificationHelper
    {
        public static X509Certificate2 GetCertificate(string publishSettings)
        {
            XmlDocument xmlPublicSetting = new XmlDocument();
            xmlPublicSetting.LoadXml(publishSettings);

            X509Certificate2 cert = new X509Certificate2();
            XmlNode publishProfile = xmlPublicSetting.SelectSingleNode("/PublishData/PublishProfile");
            string certificateString = publishProfile.Attributes["ManagementCertificate"].Value;
            cert.Import(Convert.FromBase64String(certificateString));
            return cert;
        }
    }
}
