﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace jinlinModel
{
    [DataContract]
    public class Circle
    {
        [DataMember]
        public long Id { get; set; }
    }
}
