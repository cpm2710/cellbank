using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Channels;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceContract]
    public class Service1
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        public DomainList GetRoot()
        {
            DomainList ret = new DomainList();
            string[] domains = new string[] { "Archaea", "Eubacteria", "Eukaryota" };
            foreach (string domain in domains)
            {
                ret.Add(new Domain { Name = domain, Uri = domain });

            }
            //Message realRet = Message.CreateMessage(MessageVersion.None, "*", ret);
            return ret;
        }
        [OperationContract]
        [WebGet(UriTemplate = "/{Domain}", ResponseFormat = WebMessageFormat.Json)]
        public KingdomList GetDomain(string Domain)
        {
            KingdomList list = new KingdomList();
            string[] kingdoms = new string[] { "Animalia", "Fungi", "Amoebozoa", "Plantae", "Chromalveolata", "Rhizaria", "Excavata" };
            list.AddRange((from s in kingdoms
                           select new Kingdom { Name = s, Uri = s }));
            //switch (Domain)
            //{
            //    case "Eukaryota":
            //        string[] kingdoms = new string[] { "Animalia", "Fungi", "Amoebozoa", "Plantae", "Chromalveolata", "Rhizaria", "Excavata" };
            //        list.AddRange((from s in kingdoms
            //                       select new Kingdom { Name = s, Uri = s }));
            //        break;
            //    default:
            //        break;
            //}
            //Message realRet = Message.CreateMessage(MessageVersion.None, "*", list);
            return list;
        }
    }

    [DataContract(Namespace = "")]
    public class Domain
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string Uri;
    }
    [CollectionDataContract(Name = "Domains", Namespace = "")]
    public class DomainList : List<Domain>
    {
    }
    [DataContract(Namespace = "")]
    public class Kingdom
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string Uri;
    }
    [CollectionDataContract(Name = "Kingdoms", Namespace = "")]
    public class KingdomList : List<Kingdom>
    {
    }

