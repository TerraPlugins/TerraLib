using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraLib;
using TShockAPI;

namespace TerraLibTest.Commands
{
    public class TestCommand : ChatCommand
    {
        public TestCommand() : base("testlib", "testlib.test")
        {
        }

        public override void Command(CommandArgs args)
        {
            TShock.Log.ConsoleInfo("command executed");
        }
    }
}
