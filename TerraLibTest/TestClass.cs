using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TerraLib;

namespace TerraLibTest
{
    public class TestClass
    {
        [Column]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }
    }
}
