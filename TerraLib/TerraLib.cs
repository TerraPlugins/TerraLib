using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Reflection;

namespace TerraLib
{
    [ApiVersion(2, 1)]
    public class TerraLib : TerrariaPlugin
    {
        #region Plugin Info
        public override string Name => "TerraLib";
        public override string Author => "Ryozuki";
        public override string Description => "A plugin to help other plugins.";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        #endregion

        public TerraLib(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        public static void RegisterCommand(ChatCommand cmd)
        {
            Commands.ChatCommands.Add(new Command(cmd.Permissions, cmd.Command, cmd.Names.ToArray()));
        }
    }
}
