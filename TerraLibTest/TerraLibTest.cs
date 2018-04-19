using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Reflection;
using TerraLib;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.DB;

namespace TerraLibTest
{
    [ApiVersion(2, 1)]
    public class TerraLibTest : TerrariaPlugin
    {
        #region Plugin Info

        public override string Name => "TerraLibTest";
        public override string Author => "Ryozuki";
        public override string Description => "Replace me!!!";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        #endregion Plugin Info

        public ConfigTest Config = new ConfigTest();

        public static Commands.TestCommand testCommand = new Commands.TestCommand();

        public TerraLibTest(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            Config = TerraLib.Config.Read<ConfigTest>("TerraLibTest");
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.GamePostInitialize.Register(this, OnPostInitialize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.GamePostInitialize.Deregister(this, OnPostInitialize);
            }
            base.Dispose(disposing);
        }

        #region Hooks

        private void OnInitialize(EventArgs args)
        {
            TerraLib.TerraLib.RegisterCommand(testCommand);
        }

        private void OnPostInitialize(EventArgs args)
        {
            Database.Connect("terralibtest");
            Database.CreateTable(new SqlTable("TestTable",
                new SqlColumn("Test", MySqlDbType.Int32) { DefaultValue = "2" },
                new SqlColumn("Test2", MySqlDbType.Float) { DefaultValue = "3.2" },
                new SqlColumn("Test3", MySqlDbType.String) { DefaultValue = "adsfas" }));

            var res = Database.Context.ExecuteQuery<TestSerialize>("SELECT * FROM TestTable");

            TShock.Log.ConsoleInfo("Test3: {0}", res.First().Test3);
        }

        #endregion Hooks
    }
}