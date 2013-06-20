﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzureRestAdapter
{
    public class AzureRestAdapterConstants
    {
        public const string Version = "2012-03-01";
        public const string Version8_1 = "2012-08-01";
        // This is the common namespace for all Service Management REST API XML data.
        public static XNamespace NameSpaceWA = "http://schemas.microsoft.com/windowsazure";
        public static XNamespace SchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";
    }
}
