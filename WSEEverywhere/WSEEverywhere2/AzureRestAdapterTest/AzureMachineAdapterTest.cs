﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureRestAdapter;
using System.IO;

namespace AzureRestAdapterTest
{
    [TestClass]
    public class AzureMachineAdapterTest
    {
        private const string vmImageName="andyliuimagename";
        private const string osDiskName = "andyliuosdisk";
        private const string mediaLink = "http://andyliustorageaccount.blob.core.windows.net/andyliustoragecontainer/forupload-fixed.vhd";
        private const string machineName = "andyliumachinename";
        private const string serviceName = "andyliudeploymentservicename";

        [TestMethod]
        public void TestCreateCloudService()
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
            string label = "My example cloud service label";
            string location = "East Asia";
            
            string requestId = azureMachineAdapter.NewAzureCloudService(serviceName, location, label, null);


            Console.WriteLine(
                "Called Create Storage Account operation: requestId {0}",
                requestId);
        }

        [TestMethod]
        public void TestNewAzureVMImage()
        {
            AzureMachineAdapter azureMachineAdapter = null;
            OperationAdapter operationHelper = null;
            using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\Windows Azure MSDN - Visual Studio Ultimate-5-28-2013-credentials.publishsettings", FileMode.Open, FileAccess.Read)))
            {
                string publishSettings = reader.ReadToEnd();
                azureMachineAdapter = new AzureMachineAdapter(publishSettings);
                operationHelper = new OperationAdapter(publishSettings);
            }
            string label = "andy liu vm image label";
            string requestId = azureMachineAdapter.NewAzureVMImage(vmImageName, mediaLink, label);

        }

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
            string label = "My example service";
            azureMachineAdapter.CreateMachine(serviceName, label, machineName, "aurorauser", "Quattro!", mediaLink, osDiskName);
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
            string label = "andyliu os disk label";
            string diskName = "andyliuosdisk";
            string requestId = azureMachineAdapter.NewAzureDisk(diskName, label, mediaLink);


            Console.WriteLine(
                "Called Create Storage Account operation: requestId {0}",
                requestId);
        }
    }
}
