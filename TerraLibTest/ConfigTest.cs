using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraLibTest
{
    public class ConfigTest
    {
        public int Half = 22;
        public float Life = 2.2f;
        public string Is = "ss";
        public List<int> Good = new List<int>();

        public ConfigTest()
        {
            Good.Add(2);
            Good.Add(2);
            Good.Add(3);
            Good.Add(4);
        }
    }
}
