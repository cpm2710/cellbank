using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace jinlin2
{
    [DataContract]
    class Line
    {
        [DataMember]
        public long Id { get; set; }
    }
}
