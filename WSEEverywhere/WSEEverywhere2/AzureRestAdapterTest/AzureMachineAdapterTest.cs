using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureRestAdapter;
using System.IO;

namespace AzureRestAdapterTest
{
    [TestClass]
    public class AzureMachineAdapterTest
    {
        [TestMethod]
        public void TestCreateMachine()
        {
            AzureMachineAdapter azureMachineAdapter = null;
            OperationAdapter operationHelper = null;
            using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\Windows Azure MSDN - Visual Studio Ultimate-5-28-2013-credentials.publishsettings", FileMode.Open, FileAccess.Read)))
            {
                string publishSettings = reader.ReadToEnd();
                azureMachineAdapter = new AzureMachineAdapter(publishSettings);
                operationHelper = new OperationAdapter(publishSettings);
            }

            // Create the new storage account with the following values:
            string label = "My example storage account label";
            string location = "East Asia";
            string ServiceName = "andyliudeploymentservicename";
            string requestId = azureMachineAdapter.NewAzureCloudService(ServiceName,location,label,null);
                
                
            Console.WriteLine(
                "Called Create Storage Account operation: requestId {0}",
                requestId);
        }

        [TestMethod]
        public void TestNewAzureDisk()
        {
            AzureMachineAdapter azureMachineAdapter = null;
            OperationAdapter operationHelper = null;
            using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\Windows Azure MSDN - Visual Studio Ultimate-5-28-2013-credentials.publishsettings", FileMode.Open, FileAccess.Read)))
            {
                string publishSettings = reader.ReadToEnd();
                azureMachineAdapter = new AzureMachineAdapter(publishSettings);
                operationHelper = new OperationAdapter(publishSettings);
            }

            // Create the new storage account with the following values:
            string label = "My example storage account label";
            string diskName = "andyliudiskname";
            string mediaLink = "http://andyliuservicename.blob.core.windows.net/anddyyyycontainer/a.vhd";
            string requestId = azureMachineAdapter.NewAzureDisk(diskName, label, mediaLink);


            Console.WriteLine(
                "Called Create Storage Account operation: requestId {0}",
                requestId);
        }
    }
}
