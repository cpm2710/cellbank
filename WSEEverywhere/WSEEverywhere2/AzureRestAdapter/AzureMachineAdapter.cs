using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzureRestAdapter
{
    public class AzureMachineAdapter
    {
        private string StorageAccount = null;
        private string StorageKey = null;
        RequestInvoker requestInvoker = null;
        public AzureMachineAdapter(string publishSettings)
        {
            requestInvoker = new RequestInvoker(publishSettings);
        }

        public string NewAzureVMDeployment(String ServiceName, String VMName, String VNETName, XDocument VMXML, XDocument DNSXML)
        {
            String requestID = String.Empty;
            Uri uri = new Uri(String.Format("https://management.core.windows.net/{0}/services/hostedservices/{1}/deployments", requestInvoker.SubscriptionId, ServiceName));

            XElement srcTree = new XElement("Deployment",
            new XAttribute(XNamespace.Xmlns + "i", AzureRestAdapterConstants.SchemaInstance),
            new XElement("Name", ServiceName),
            new XElement("DeploymentSlot", "Production"),
            new XElement("Label", ServiceName),
            new XElement("RoleList", null)
            );

            if (String.IsNullOrEmpty(VNETName) == false)
            {
                srcTree.Add(new XElement("VirtualNetworkName", VNETName));
            }
            if (DNSXML != null)
            {
                srcTree.Add(new XElement("DNS", new XElement("DNSServers", DNSXML)));
            }

            XDocument deploymentXML = new XDocument(srcTree);
            deploymentXML.Descendants(AzureRestAdapterConstants.NameSpaceWA + "RoleList").FirstOrDefault().Add(VMXML.Root);

            XDocument responseBody;
            return requestInvoker.InvokeRequest(
                uri, "POST", HttpStatusCode.Created, deploymentXML, out responseBody);
        }

        public string NewAzureCloudService(String serviceName, String location, string label, String affinityGroup)
        {
            String requestID = String.Empty;
            Uri uri = new Uri(String.Format("https://management.core.windows.net/{0}/services/hostedservices", requestInvoker.SubscriptionId));

            XElement locationOrAffinityGroup = String.IsNullOrEmpty(location) ?
              new XElement(AzureRestAdapterConstants.NameSpaceWA + "AffinityGroup", affinityGroup) :
              new XElement(AzureRestAdapterConstants.NameSpaceWA + "Location", location);

            // Create the request XML document
            XDocument requestBody = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(
                    AzureRestAdapterConstants.NameSpaceWA + "CreateHostedService",
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "ServiceName", serviceName),
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "Label", label.ToBase64()),
                    locationOrAffinityGroup));

            XDocument responseBody;
            return requestInvoker.InvokeRequest(
                uri, "POST", HttpStatusCode.Created, requestBody, out responseBody);
        }

        public string CreateMachine()
        {
            //string uriFormat = "https://management.core.windows.net/{0}/services/images";
            //Uri uri = new Uri(String.Format(uriFormat, requestInvoker.SubscriptionId));

            //// Location and Affinity Group are mutually exclusive. 
            //// Use the location if it isn't null or empty.
            //XElement locationOrAffinityGroup = String.IsNullOrEmpty(location) ?
            //    new XElement(AzureRestAdapterConstants.NameSpaceWA + "AffinityGroup", affinityGroup) :
            //    new XElement(AzureRestAdapterConstants.NameSpaceWA + "Location", location);

            //// Create the request XML document
            //XDocument requestBody = new XDocument(
            //    new XDeclaration("1.0", "UTF-8", "no"),
            //    new XElement(
            //        AzureRestAdapterConstants.NameSpaceWA + "OSImage",
            //        new XElement(AzureRestAdapterConstants.NameSpaceWA + "ServiceName", serviceName),
            //        new XElement(AzureRestAdapterConstants.NameSpaceWA + "Description", description),
            //        new XElement(AzureRestAdapterConstants.NameSpaceWA + "Label", label.ToBase64()),
            //        locationOrAffinityGroup));

            //            <OSImage xmlns="http://schemas.microsoft.com/windowsazure" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
            //   <Label>image-label</Label>
            //   <MediaLink>uri-of-the-containing-blob</MediaLink>
            //   <Name>image-name</Name>
            //   <OS>Linux|Windows</OS>
            //   <Eula>image-eula</Eula>
            //   <Description>image-description</Description>
            //   <ImageFamily>image-family</ImageFamily>
            //   <PublishedDate>published-date</PublishedDate>
            //   <IsPremium>true/false</IsPremium>
            //   <ShowInGui>true/false</ShowInGui>
            //   <PrivacyUri>http://www.example.com/privacypolicy.html</PrivacyUri>
            //   <IconUri>http://www.example.com/favicon.png</IconUri>
            //   <RecommendedVMSize>Small/Large/Medium/ExtraLarge</RecommendedVMSize>
            //   <SmallIconUri>http://www.example.com/smallfavicon.png</SmallIconUri>
            //   <Language>language-of-image</Language>
            //</OSImage>

            //// Add the GeoReplicationEnabled element if the version supports it.
            //if ((geoReplicationEnabled != null) &&
            //    (String.CompareOrdinal(AzureRestAdapterConstants.Version, "2011-12-01") >= 0))
            //{
            //    requestBody.Element(
            //        AzureRestAdapterConstants.NameSpaceWA + "CreateStorageServiceInput").Add(
            //            new XElement(
            //                AzureRestAdapterConstants.NameSpaceWA + "GeoReplicationEnabled",
            //                geoReplicationEnabled.ToString().ToLowerInvariant()));
            //}

            //XDocument responseBody;
            //return requestInvoker.InvokeRequest(
            //    uri, "POST", HttpStatusCode.Accepted, requestBody, out responseBody);
            return string.Empty;
        }
    }
}
