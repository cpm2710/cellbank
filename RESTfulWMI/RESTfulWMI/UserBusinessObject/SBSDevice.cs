using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;
using System.Runtime.Serialization;

namespace UserBusinessObject
{
    [ManagementEntity(Name = "SBS_Device", Singleton = false)]
    [DataContract]
    public class SBSDevice
    {

    }
}
