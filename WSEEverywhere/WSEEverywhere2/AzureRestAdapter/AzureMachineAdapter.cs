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
        RequestInvoker requestInvoker = null;
        public AzureMachineAdapter(string publishSettings)
        {
            requestInvoker = new RequestInvoker(publishSettings);
        }

        public string NewAzureDisk(string name, string label, string mediaLink)
        {
            string uriFormat = "https://management.core.windows.net/{0}/services/disks";
            Uri uri = new Uri(String.Format(uriFormat, requestInvoker.SubscriptionId));

            // Create the request XML document
            XDocument requestBody = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(
                    AzureRestAdapterConstants.NameSpaceWA + "Disk",
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "OS", "Windows"),
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "Label", label.ToBase64()),
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "MediaLink", mediaLink),
                    new XElement(AzureRestAdapterConstants.NameSpaceWA + "Name", name)));

            XDocument responseBody;
            return requestInvoker.InvokeRequest(
                uri, "POST", HttpStatusCode.Accepted, requestBody, out responseBody);
        }

        public string NewAzureVMImage(string name, string mediaLink, string label)
        {
            string uriFormat = "https://management.core.windows.net/{0}/services/images";
            Uri uri = new Uri(String.Format(uriFormat, requestInvoker.SubscriptionId));

            String requestID = String.Empty;

            XElement srcTree = new XElement(AzureRestAdapterConstants.NameSpaceWA + "OSImage",
            new XAttribute(XNamespace.Xmlns + "i", AzureRestAdapterConstants.SchemaInstance),
            new XElement(AzureRestAdapterConstants.NameSpaceWA + "Label", label.ToBase64()),
            new XElement(AzureRestAdapterConstants.NameSpaceWA + "MediaLink", mediaLink),
            new XElement(AzureRestAdapterConstants.NameSpaceWA + "Name", name),
            new XElement(AzureRestAdapterConstants.NameSpaceWA + "OS", "Windows"),
            new XElement(AzureRestAdapterConstants.NameSpaceWA + "IsPremium", false),
            new XElement(AzureRestAdapterConstants.NameSpaceWA + "ShowInGui", true)//,
                //new XElement("Language","en-us")
            );


            // Create the request XML document
            XDocument requestBody = new XDocument(srcTree);

            XDocument responseBody;
            return requestInvoker.InvokeRequest(
                uri, "POST", HttpStatusCode.OK, requestBody, out responseBody, AzureRestAdapterConstants.Version8_1);

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

        public string CreateMachine(string serviceName, string label)
        {
            Uri uri = new Uri("https://management.core.windows.net/" + requestInvoker.SubscriptionId
            + "/services/hostedservices/" + serviceName + "/deployments");

            XElement srcTree = new XElement("Deployment",
            new XAttribute(XNamespace.Xmlns + "i", AzureRestAdapterConstants.SchemaInstance),
            new XElement("Name", serviceName),
            new XElement("DeploymentSlot", "Production"),
            new XElement("Label", label.ToBase64()),
            new XElement("RoleList", null)
            );


            XDocument requestBody = new XDocument(srcTree);
            XElement roleListEle = requestBody.Descendants(AzureRestAdapterConstants.NameSpaceWA + "RoleList").FirstOrDefault();
            //XElement 

            XDocument responseBody;
            string result = requestInvoker.InvokeRequest(uri, "POST", HttpStatusCode.Created, requestBody, out responseBody);
            return result;
            //            <Deployment xmlns="http://schemas.microsoft.com/windowsazure" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
            //  <!--Your host service name-->
            //  <Name>SERVICE_NAME</Name>
            //  <DeploymentSlot>Production</DeploymentSlot>
            //  <Label>SERVICE_NAME</Label>
            //  <RoleList>
            //    <Role i:type="PersistentVMRole">
            //      <!--Your virtual Machine Name-->
            //      <RoleName>MyWinVM</RoleName>
            //      <OsVersion i:nil="true"/>
            //      <RoleType>PersistentVMRole</RoleType>
            //      <ConfigurationSets>
            //        <ConfigurationSet i:type="WindowsProvisioningConfigurationSet">
            //          <ConfigurationSetType>WindowsProvisioningConfiguration</ConfigurationSetType>
            //          <!--Your Computer Name-->
            //          <ComputerName>MyWinVM</ComputerName>
            //          <!--Computer pass word-->
            //          <AdminPassword>Password1!</AdminPassword>
            //          <EnableAutomaticUpdates>true</EnableAutomaticUpdates>
            //          <ResetPasswordOnFirstLogon>false</ResetPasswordOnFirstLogon>
            //        </ConfigurationSet>
            //        <ConfigurationSet i:type="NetworkConfigurationSet">
            //          <ConfigurationSetType>NetworkConfiguration</ConfigurationSetType>
            //          <InputEndpoints>
            //            <InputEndpoint>
            //              <LocalPort>3389</LocalPort>
            //              <Name>RemoteDesktop</Name>
            //              <Protocol>tcp</Protocol>
            //            </InputEndpoint>
            //          </InputEndpoints>
            //        </ConfigurationSet>
            //      </ConfigurationSets>
            //      <DataVirtualHardDisks/>
            //      <Label>bXlzdmMxZGlubzY=</Label>
            //      <OSVirtualHardDisk>
            //        <!--VHD address and  source image name-->
            //        <!--replace these two properties to avaliable value-->
            //        <MediaLink>https://portalvhds54h52qsyb55v4.blob.core.windows.net/vhds/mysvc-MyWinVM-2012-12-1-107.vhd</MediaLink>
            //        <SourceImageName>a699494373c04fc0bc8f2bb1389d6106__Win2K8R2SP1-Datacenter-201212.01-en.us-30GB.vhd</SourceImageName>
            //      </OSVirtualHardDisk>
            //    </Role>
            //  </RoleList>
            //</Deployment>

            //XDocument responseBody;
            //return requestInvoker.InvokeRequest(
            //    uri, "POST", HttpStatusCode.Accepted, requestBody, out responseBody);
            return string.Empty;
        }
    }
}
