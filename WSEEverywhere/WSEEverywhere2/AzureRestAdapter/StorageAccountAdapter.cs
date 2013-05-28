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
    /// <summary>
    /// The operation status values from PollGetOperationStatus.
    /// </summary>
    public enum OperationStatus
    {
        InProgress,
        Failed,
        Succeeded,
        TimedOut
    }

    /// <summary>
    /// The results from PollGetOperationStatus are passed in this struct.
    /// </summary>
    public struct OperationResult
    {
        // The status: InProgress, Failed, Succeeded, or TimedOut.
        public OperationStatus Status { get; set; }

        // The http status code of the requestId operation, if any.
        public HttpStatusCode StatusCode { get; set; }

        // The approximate running time for PollGetOperationStatus.
        public TimeSpan RunningTime { get; set; }

        // The error code for the failed operation.
        public string Code { get; set; }

        // The message for the failed operation.
        public string Message { get; set; }
    }

    public class StorageAccountAdapter
    {
        // Set these constants with your values to run the sample.
        private const string Version = "2012-03-01";
        private string SubscriptionId = "subscription-identifier";
        private string SubscriptionName = "unique-storage-account-name";
        

        private X509Certificate2 Certificate { get; set; }

        public StorageAccountAdapter(string publishSettings)
        {
            XmlDocument xmlPublicSetting = new XmlDocument();
            xmlPublicSetting.LoadXml(publishSettings);

            X509Certificate2 cert = new X509Certificate2();
            XmlNode publishProfile = xmlPublicSetting.SelectSingleNode("/PublishData/PublishProfile");
            string certificateString = publishProfile.Attributes["ManagementCertificate"].Value;
            cert.Import(Convert.FromBase64String(certificateString));
            this.Certificate = cert;

            XmlNode subscriptionNode = xmlPublicSetting.SelectSingleNode("/PublishData/PublishProfile/Subscription");
            this.SubscriptionId = subscriptionNode.Attributes["Id"].Value;
            this.SubscriptionName = subscriptionNode.Attributes["Name"].Value;
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
            Uri uri = new Uri(String.Format(uriFormat, SubscriptionId, serviceName));
            XDocument responseBody;
            InvokeRequest(uri, "GET", HttpStatusCode.OK, null, out responseBody);
            return responseBody.Element(AzureRestAdapterConstants.NameSpaceWA + "StorageService");
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
            Uri uri = new Uri(String.Format(uriFormat, SubscriptionId));

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
                (String.CompareOrdinal(Version, "2011-12-01") >= 0))
            {
                requestBody.Element(
                    AzureRestAdapterConstants.NameSpaceWA + "CreateStorageServiceInput").Add(
                        new XElement(
                            AzureRestAdapterConstants.NameSpaceWA + "GeoReplicationEnabled",
                            geoReplicationEnabled.ToString().ToLowerInvariant()));
            }

            XDocument responseBody;
            return InvokeRequest(
                uri, "POST", HttpStatusCode.Accepted, requestBody, out responseBody);
        }

        /// <summary>
        /// Calls the Get Operation Status operation in the Service 
        /// Management REST API for the specified subscription and requestId 
        /// and returns the Operation XML element from the response.
        /// </summary>
        /// <param name="requestId">The requestId of the operation to track.</param>
        /// <returns>The Operation XML element from the response.</returns>
        private XElement GetOperationStatus(
            string requestId)
        {
            string uriFormat = "https://management.core.windows.net/{0}" +
                "/operations/{1}";
            Uri uri = new Uri(String.Format(uriFormat, SubscriptionId, requestId));
            XDocument responseBody;
            InvokeRequest(uri, "GET", HttpStatusCode.OK, null, out responseBody);
            return responseBody.Element(AzureRestAdapterConstants.NameSpaceWA + "Operation");
        }



        /// <summary>
        /// Polls Get Operation Status for the operation specified by requestId
        /// every pollIntervalSeconds until timeoutSeconds have passed or the
        /// operation has returned a Failed or Succeeded status. 
        /// </summary>
        /// <param name="requestId">The requestId of the operation to get status for.</param>
        /// <param name="pollIntervalSeconds">The interval between calls to Get Operation Status.</param>
        /// <param name="timeoutSeconds">The maximum number of seconds to poll.</param>
        /// <returns>An OperationResult structure with status or error information.</returns>
        public OperationResult PollGetOperationStatus(
            string requestId,
            int pollIntervalSeconds,
            int timeoutSeconds)
        {
            OperationResult result = new OperationResult();
            DateTime beginPollTime = DateTime.UtcNow;
            TimeSpan pollInterval = new TimeSpan(0, 0, pollIntervalSeconds);
            DateTime endPollTime = beginPollTime + new TimeSpan(0, 0, timeoutSeconds);

            bool done = false;
            while (!done)
            {
                XElement operation = GetOperationStatus(requestId);
                result.RunningTime = DateTime.UtcNow - beginPollTime;
                try
                {
                    // Turn the Status string into an OperationStatus value
                    result.Status = (OperationStatus)Enum.Parse(
                        typeof(OperationStatus),
                        operation.Element(AzureRestAdapterConstants.NameSpaceWA + "Status").Value);
                }
                catch (Exception)
                {
                    throw new ApplicationException(string.Format(
                        "Get Operation Status {0} returned unexpected status: {1}{2}",
                        requestId,
                        Environment.NewLine,
                        operation.ToString(SaveOptions.OmitDuplicateNamespaces)));
                }

                switch (result.Status)
                {
                    case OperationStatus.InProgress:
                        Console.WriteLine(
                            "In progress for {0} seconds",
                            (int)result.RunningTime.TotalSeconds);
                        Thread.Sleep((int)pollInterval.TotalMilliseconds);
                        break;

                    case OperationStatus.Failed:
                        result.StatusCode = (HttpStatusCode)Convert.ToInt32(
                            operation.Element(AzureRestAdapterConstants.NameSpaceWA + "HttpStatusCode").Value);
                        XElement error = operation.Element(AzureRestAdapterConstants.NameSpaceWA + "Error");
                        result.Code = error.Element(AzureRestAdapterConstants.NameSpaceWA + "Code").Value;
                        result.Message = error.Element(AzureRestAdapterConstants.NameSpaceWA + "Message").Value;
                        done = true;
                        break;

                    case OperationStatus.Succeeded:
                        result.StatusCode = (HttpStatusCode)Convert.ToInt32(
                            operation.Element(AzureRestAdapterConstants.NameSpaceWA + "HttpStatusCode").Value);
                        done = true;
                        break;
                }

                if (!done && DateTime.UtcNow > endPollTime)
                {
                    result.Status = OperationStatus.TimedOut;
                    done = true;
                }
            }

            return result;
        }


        /// <summary>
        /// A helper function to invoke a Service Management REST API operation.
        /// Throws an ApplicationException on unexpected status code results.
        /// </summary>
        /// <param name="uri">The URI of the operation to invoke using a web request.</param>
        /// <param name="method">The method of the web request, GET, PUT, POST, or DELETE.</param>
        /// <param name="expectedCode">The expected status code.</param>
        /// <param name="requestBody">The XML body to send with the web request. Use null to send no request body.</param>
        /// <param name="responseBody">The XML body returned by the request, if any.</param>
        /// <returns>The requestId returned by the operation.</returns>
        private string InvokeRequest(
            Uri uri,
            string method,
            HttpStatusCode expectedCode,
            XDocument requestBody,
            out XDocument responseBody)
        {
            responseBody = null;
            string requestId = String.Empty;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = method;
            request.Headers.Add("x-ms-version", Version);
            request.ClientCertificates.Add(Certificate);
            request.ContentType = "application/xml";

            if (requestBody != null)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(
                        requestStream, System.Text.UTF8Encoding.UTF8))
                    {
                        requestBody.Save(streamWriter, SaveOptions.DisableFormatting);
                    }
                }
            }

            HttpWebResponse response;
            HttpStatusCode statusCode = HttpStatusCode.Unused;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // GetResponse throws a WebException for 4XX and 5XX status codes
                response = (HttpWebResponse)ex.Response;
            }

            try
            {
                statusCode = response.StatusCode;
                if (response.ContentLength > 0)
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        responseBody = XDocument.Load(reader);
                    }
                }

                if (response.Headers != null)
                {
                    requestId = response.Headers["x-ms-request-id"];
                }
            }
            finally
            {
                response.Close();
            }

            if (!statusCode.Equals(expectedCode))
            {
                throw new ApplicationException(string.Format(
                    "Call to {0} returned an error:{1}Status Code: {2} ({3}):{1}{4}",
                    uri.ToString(),
                    Environment.NewLine,
                    (int)statusCode,
                    statusCode,
                    responseBody.ToString(SaveOptions.OmitDuplicateNamespaces)));
            }

            return requestId;
        }
    }

    /// <summary>
    /// Helpful extension methods for converting strings to and from Base-64.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a UTF-8 string to a Base-64 version of the string.
        /// </summary>
        /// <param name="s">The string to convert to Base-64.</param>
        /// <returns>The Base-64 converted string.</returns>
        public static string ToBase64(this string s)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts a Base-64 encoded string to UTF-8.
        /// </summary>
        /// <param name="s">The string to convert from Base-64.</param>
        /// <returns>The converted UTF-8 string.</returns>
        public static string FromBase64(this string s)
        {
            byte[] bytes = Convert.FromBase64String(s);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
