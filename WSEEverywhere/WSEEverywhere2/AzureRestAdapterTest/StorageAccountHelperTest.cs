using AzureRestAdapter;
//----------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageAccountHelperTest.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>Defines the StorageAccountHelperTest type.</summary>
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;

using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AzureRestAdapter.DotNetAdapter;

namespace AzureRestAdapterTest
{

    [TestClass]
    public class StorageAccountHelperTest
    {
        [TestMethod]
        public void TestCreateStorageAccount()
        {
            try
            {
                StorageAccountAdapter storageAccountAdapter = null;
                OperationAdapter operationHelper = null;
                using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\Windows Azure MSDN - Visual Studio Ultimate-5-28-2013-credentials.publishsettings", FileMode.Open, FileAccess.Read)))
                {
                    string publishSettings = reader.ReadToEnd();
                    storageAccountAdapter = new StorageAccountAdapter(publishSettings);
                    operationHelper = new OperationAdapter(publishSettings);
                }

                // Create the new storage account with the following values:
                string description = "Description for my example storage account";
                string label = "My example storage account label";
                string location = "East Asia";
                string ServiceName = "andyliuservicename";
                bool? enableGeoReplication = true;
                string requestId = storageAccountAdapter.CreateStorageAccount(
                    ServiceName,
                    description,
                    label,
                    null,
                    location,
                    enableGeoReplication);
                Console.WriteLine(
                    "Called Create Storage Account operation: requestId {0}",
                    requestId);

                // Loop on Get Operation Status for result of storage creation
                OperationResult result = operationHelper.PollGetOperationStatus(
                    requestId,
                    pollIntervalSeconds: 20,
                    timeoutSeconds: 180);
                switch (result.Status)
                {
                    case OperationStatus.TimedOut:
                        Console.WriteLine(
                            "Poll of Get Operation Status timed out: " +
                            "Operation {0} is still in progress after {1} seconds.",
                            requestId,
                            (int)result.RunningTime.TotalSeconds);
                        break;

                    case OperationStatus.Failed:
                        Console.WriteLine(
                            "Failed: Operation {0} failed after " +
                            "{1} seconds with status {2} ({3}) - {4}: {5}",
                            requestId,
                            (int)result.RunningTime.TotalSeconds,
                            (int)result.StatusCode,
                            result.StatusCode,
                            result.Code,
                            result.Message);
                        break;

                    case OperationStatus.Succeeded:
                        Console.WriteLine(
                            "Succeeded: Operation {0} completed " +
                            "after {1} seconds with status {2} ({3})",
                            requestId,
                            (int)result.RunningTime.TotalSeconds,
                            (int)result.StatusCode,
                            result.StatusCode);
                        break;
                }

                // Display the property values for the new storage account.
                // Convert the Label property to a readable value for display.
                XElement updatedProperties =
                    storageAccountAdapter.GetStorageAccountProperties(ServiceName);
                XElement labelElement = updatedProperties.Descendants(AzureRestAdapterConstants.NameSpaceWA + "Label").First();
                labelElement.Value = labelElement.Value.FromBase64();
                Console.WriteLine(
                    "New Storage Account Properties for {0}:{1}{2}",
                    ServiceName,
                    Environment.NewLine,
                    updatedProperties.ToString(SaveOptions.OmitDuplicateNamespaces));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in Main:");
                Console.WriteLine(ex.Message);
            }

            Console.Write("Press any key to continue:");
            Console.ReadKey();

        }

        [TestMethod]
        public void TestCreateStorageContainer()
        {
            using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\storagekey.txt", FileMode.Open, FileAccess.Read)))
            {
                string keys = reader.ReadToEnd();
                string[] args = keys.Split('\n');
                string storageAccount = args[0].Trim(new char[] { '\r', '\n' });
                string storageKey = args[1].Trim(new char[] { '\r', '\n' });

                StorageAccountAdapter storageAccountAdapter = new StorageAccountAdapter(storageAccount, storageKey);
                storageAccountAdapter.CreateStorageContainer("anddyyyycontainer");
                //OperationAdapter operationHelper = new OperationAdapter(publishSettings);
            }
        }

        [TestMethod]
        public void TestCreateStorageBlob()
        {
            using (StreamReader reader = new StreamReader(new FileStream(@"D:\andyzone\WSEEverywhere2\AzureRestAdapter\data\storagekey.txt", FileMode.Open, FileAccess.Read)))
            {
                string keys = reader.ReadToEnd();
                string[] args = keys.Split('\n');
                string storageAccount = args[0].Trim(new char[] { '\r', '\n' });
                string storageKey = args[1].Trim(new char[] { '\r', '\n' });

                VhdUploader vhdUploader = new VhdUploader();
                vhdUploader.UploadVhd(storageAccount, storageKey, @"d:\w7zh-twx86sta_disk_0.vhd");
                //OperationAdapter operationHelper = new OperationAdapter(publishSettings);
            }
        }

    }
}