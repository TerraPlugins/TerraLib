using System.Runtime.Serialization;

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