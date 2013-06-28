using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AzureRestAdapter
{
    public class RequestInvoker
    {
        public string SubscriptionId = "subscription-identifier";
        public string SubscriptionName = "unique-storage-account-name";
        private X509Certificate2 Certificate { get; set; }

        public RequestInvoker(string publishSettings)
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
        /// A helper function to invoke a Service Management REST API operation.
        /// Throws an ApplicationException on unexpected status code results.
        /// </summary>
        /// <param name="uri">The URI of the operation to invoke using a web request.</param>
        /// <param name="method">The method of the web request, GET, PUT, POST, or DELETE.</param>
        /// <param name="expectedCode">The expected status code.</param>
        /// <param name="requestBody">The XML body to send with the web request. Use null to send no request body.</param>
        /// <param name="responseBody">The XML body returned by the request, if any.</param>
        /// <returns>The requestId returned by the operation.</returns>
        public string InvokeRequest(
            Uri uri,
            string method,
            HttpStatusCode expectedCode,
            XDocument requestBody,
            out XDocument responseBody, string version = AzureRestAdapterConstants.Version)
        {
            responseBody = null;
            string requestId = String.Empty;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = method;
            request.Headers.Add("x-ms-version", version);
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
                string respStr = response.ToString();
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
}
