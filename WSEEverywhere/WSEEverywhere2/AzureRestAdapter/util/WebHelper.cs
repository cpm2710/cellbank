using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AzureRestAdapter.util
{
    public class WebHelper
    {
        public static string AuthorizationHeader(string StorageAccount, string StorageKey, string method, DateTime now, HttpWebRequest request, bool IsTableStorage, string ifMatch = "", string md5 = "")
        {
            if (string.IsNullOrEmpty(StorageAccount))
            {
                throw new ArgumentNullException(StorageAccount);
            }
            string MessageSignature;

            if (IsTableStorage)
            {
                MessageSignature = String.Format("{0}\n\n{1}\n{2}\n{3}",
                    method,
                    "application/atom+xml",
                    now.ToString("R", System.Globalization.CultureInfo.InvariantCulture),
                    GetCanonicalizedResource(request.RequestUri, StorageAccount, IsTableStorage)
                    );
            }
            else
            {
                MessageSignature = String.Format("{0}\n\n\n{1}\n{5}\n\n\n\n{2}\n\n\n\n{3}{4}",
                    method,
                    (method == "GET" || method == "HEAD") ? String.Empty : request.ContentLength.ToString(),
                    ifMatch,
                   GetCanonicalizedHeaders(request),
                    GetCanonicalizedResource(request.RequestUri, StorageAccount, IsTableStorage),
                    md5
                    );
            }
            byte[] SignatureBytes = System.Text.Encoding.UTF8.GetBytes(MessageSignature);
            System.Security.Cryptography.HMACSHA256 SHA256 = new System.Security.Cryptography.HMACSHA256(Convert.FromBase64String(StorageKey));
            String AuthorizationHeader = "SharedKey " + StorageAccount + ":" + Convert.ToBase64String(SHA256.ComputeHash(SignatureBytes));
            return AuthorizationHeader;
        }

        // Get header values.

        public static ArrayList GetHeaderValues(NameValueCollection headers, string headerName)
        {
            ArrayList list = new ArrayList();
            string[] values = headers.GetValues(headerName);
            if (values != null)
            {
                foreach (string str in values)
                {
                    list.Add(str.TrimStart(null));
                }
            }
            return list;
        }

        public static string GetCanonicalizedHeaders(HttpWebRequest request)
        {
            ArrayList headerNameList = new ArrayList();
            StringBuilder sb = new StringBuilder();
            foreach (string headerName in request.Headers.Keys)
            {
                if (headerName.ToLowerInvariant().StartsWith("x-ms-", StringComparison.Ordinal))
                {
                    headerNameList.Add(headerName.ToLowerInvariant());
                }
            }
            headerNameList.Sort();
            foreach (string headerName in headerNameList)
            {
                StringBuilder builder = new StringBuilder(headerName);
                string separator = ":";
                foreach (string headerValue in GetHeaderValues(request.Headers, headerName))
                {
                    string trimmedValue = headerValue.Replace("\r\n", String.Empty);
                    builder.Append(separator);
                    builder.Append(trimmedValue);
                    separator = ",";
                }
                sb.Append(builder.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static string GetCanonicalizedResource(Uri address, string accountName, bool IsTableStorage)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder builder = new StringBuilder("/");
            builder.Append(accountName);
            builder.Append(address.AbsolutePath);
            str.Append(builder.ToString());
            NameValueCollection values2 = new NameValueCollection();
            if (!IsTableStorage)
            {
                NameValueCollection values = HttpUtility.ParseQueryString(address.Query);
                foreach (string str2 in values.Keys)
                {
                    ArrayList list = new ArrayList(values.GetValues(str2));
                    list.Sort();
                    StringBuilder builder2 = new StringBuilder();
                    foreach (object obj2 in list)
                    {
                        if (builder2.Length > 0)
                        {
                            builder2.Append(",");
                        }
                        builder2.Append(obj2.ToString());
                    }
                    values2.Add((str2 == null) ? str2 : str2.ToLowerInvariant(), builder2.ToString());
                }
            }
            ArrayList list2 = new ArrayList(values2.AllKeys);
            list2.Sort();
            foreach (string str3 in list2)
            {
                StringBuilder builder3 = new StringBuilder(string.Empty);
                builder3.Append(str3);
                builder3.Append(":");
                builder3.Append(values2[str3]);
                str.Append("\n");
                str.Append(builder3.ToString());
            }
            return str.ToString();
        }
    }
}
