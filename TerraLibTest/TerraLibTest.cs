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
        public override string Description => "A test plugin";
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
            Database.EnsureTable(new SqlTable("Tests",
                new SqlColumn("ID", MySqlDbType.Int32) { Primary = true, AutoIncrement = true, NotNull = true },
                new SqlColumn("Name", MySqlDbType.VarChar, 32) { DefaultValue = "3.2" }));

            Database.Query("INSERT INTO Tests (Name) VALUES (@0)", "hi");

            var t = Database.QueryResult<TestClass>("select * from Tests");

            TShock.Log.ConsoleError(t.First().Name);
        }

        #endregion Hooks
    }
}