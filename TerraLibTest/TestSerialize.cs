using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TerraLib;

namespace TerraLibTest
{
    [DataContract]
    public class TestSerialize
    {
        [DataMember]
        public int Test { get; set; }

        [DataMember]
        public float Test2 { get; set; }

        [DataMember]
        public string Test3 { get; set; }

        public string Test4 { get; set; }
    }
}
