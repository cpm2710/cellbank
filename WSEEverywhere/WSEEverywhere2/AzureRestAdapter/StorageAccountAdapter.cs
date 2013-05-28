using AzureRestAdapter.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AzureRestAdapter
{
    public class StorageAccountAdapter
    {
        private string StorageAccount = null;
        private string StorageKey = null;
        RequestInvoker requestInvoker = null;
        public StorageAccountAdapter(string publishSettings)
        {
            requestInvoker = new RequestInvoker(publishSettings);
        }

        public StorageAccountAdapter( string storageAccount, string storageKey)
        {
            StorageAccount = storageAccount;
            StorageKey = storageKey;
        }

        public string CreateStorageContainer(string container)
        {
            string requestId = string.Empty;
            DateTime now = DateTime.UtcNow;
            string uri = "https://" + StorageAccount + ".blob.core.windows.net/" + container + "?restype=container";
            string method = "PUT";
            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            request.Method = method;
            request.ContentLength = 0;
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2009-09-19");

            request.Headers.Add("Authorization", WebHelper.AuthorizationHeader(StorageAccount, StorageKey, method, now, request, false, "", ""));

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            response.Close();
            if (response.Headers != null)
            {
                requestId = response.Headers["x-ms-request-id"];
            }
            return requestId;
        }

        public string CreateStorageBlob(string blob)
        {

            return string.Empty;
        }

        /// <summary>
        /// Calls the Create Storage Account operation in the Service Management 
        /// REST API for the specified subscription, storage account name, 
        /// description, label, location or affinity group, and geo-replication 
        /// enabled setting.
        /// </summary>
        /// <param name="serviceName">The name of the storage account to update.</param>
        /// <param name="description">The new description for the storage account.</param>
        /// <param name="label">The new label for the storage account.</param>
        /// <param name="affinityGroup">The affinity group name, or null to use a location.</param>
        /// <param name="location">The location name, or null to use an affinity group.</param>
        /// <param name="geoReplicationEnabled">The new geo-replication setting, if applicable. 
        /// This optional parameter defaults to null.</param>
        /// <returns>The requestId for the operation.</returns>
        public string CreateStorageAccount(
            string serviceName,
            string description,
            string label,
            string affinityGroup,
            string location,
            bool? geoReplicationEnabled = null)
        {
            string uriFormat = "https://management.core.windows.net/{0}" +
                "/services/storageservices";
            Uri uri = new Uri(String.Format(uriFormat, requestInvoker.SubscriptionId));

            // Location and Affinity Group are mutually exclusive. 
            // Use the location if it isn't null or empty.
            XElement locationOrAffinityGroup = String.IsNullOrEmpty(location) ?
                new XElement(AzureRestAdapterConstants.NameSpaceWA + "AffinityGroup", affinityGroup) :
                new XElement(AzureRestAdapterConstants.NameSpaceWA + "Location", location);

            // Create the request XML document
            XDocument requestBody = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(
                    AzureRestAdapterConstants.NameSpaceWA + "CreateStorageServiceInput",
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "ServiceName", serviceName),
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "Description", description),
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "Label", label.ToBase64()),
                    locationOrAffinityGroup));

            // Add the GeoReplicationEnabled element if the version supports it.
            if ((geoReplicationEnabled != null) &&
                (String.CompareOrdinal(AzureRestAdapterConstants.Version, "2011-12-01") >= 0))
            {
                requestBody.Element(
                    AzureRestAdapterConstants.NameSpaceWA + "CreateStorageServiceInput").Add(
                        new XElement(
                            AzureRestAdapterConstants.NameSpaceWA + "GeoReplicationEnabled",
                            geoReplicationEnabled.ToString().ToLowerInvariant()));
            }

            XDocument responseBody;
            return requestInvoker.InvokeRequest(
                uri, "POST", HttpStatusCode.Accepted, requestBody, out responseBody);
        }


        /// <summary>
        /// Calls the Get Storage Account Properties operation in the Service 
        /// Management REST API for the specified subscription and storage account 
        /// name and returns the StorageService XML element from the response.
        /// </summary>
        /// <param name="serviceName">The name of the storage account.</param>
        /// <returns>The StorageService XML element from the response.</returns>
        public XElement GetStorageAccountProperties(
            string serviceName)
        {
            string uriFormat = "https://management.core.windows.net/{0}" +
                "/services/storageservices/{1}";
            Uri uri = new Uri(String.Format(uriFormat, requestInvoker.SubscriptionId, serviceName));
            XDocument responseBody;
            requestInvoker.InvokeRequest(uri, "GET", HttpStatusCode.OK, null, out responseBody);
            return responseBody.Element(AzureRestAdapterConstants.NameSpaceWA + "StorageService");
        }
    }
}
