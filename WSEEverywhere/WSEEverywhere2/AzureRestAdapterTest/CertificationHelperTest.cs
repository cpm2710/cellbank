using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using AzureRestAdapter;
using System.Security.Cryptography.X509Certificates;

namespace AzureRestAdapterTest
{
    [TestClass]
    public class CertificationHelperTest
    {
        [TestMethod]
        public void TestGetCertificate()
        {
            using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\Windows Azure MSDN - Visual Studio Ultimate-5-28-2013-credentials.publishsettings", FileMode.Open, FileAccess.Read)))
            {
                string publishSettings = reader.ReadToEnd();
                X509Certificate2 certi = CertificationHelper.GetCertificate(publishSettings);
                Assert.IsTrue(certi != null);

            }
        }
    }
}
