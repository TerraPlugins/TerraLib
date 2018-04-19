using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Linq;
using System.IO;
using TShockAPI;
using TShockAPI.DB;

namespace TerraLib
{
    /// <summary>
    /// Class for handling database operations, asynchronously.
    /// </summary>
    public class Database
    {
        private static IDbConnection db;
        private static SqlTableCreator sqlCreator;
        public static DataContext Context;

        public static void Connect(string sqliteDBName)
        {
            switch (TShock.Config.StorageType.ToLower())
            {
                case "mysql":
                    string[] dbHost = TShock.Config.MySqlHost.Split(':');
                    db = new MySqlConnection()
                    {
                        ConnectionString = string.Format("Server={0}; Port={1}; Database={2}; Uid={3}; Pwd={4};",
                            dbHost[0],
                            dbHost.Length == 1 ? "3306" : dbHost[1],
                            TShock.Config.MySqlDbName,
                            TShock.Config.MySqlUsername,
                            TShock.Config.MySqlPassword)
                    };
                    break;

                case "sqlite":
                    string sql = Path.Combine(TShock.SavePath, string.Format("{0}.sqlite", sqliteDBName));
                    db = new SqliteConnection(string.Format("uri=file://{0},Version=3", sql));
                    break;
            }

            sqlCreator = new SqlTableCreator(db,
                db.GetSqlType() == SqlType.Sqlite ? (IQueryBuilder)new SqliteQueryCreator() : new MysqlQueryCreator());

            Context = new DataContext(db);
        }

        public static bool CreateTable(SqlTable table)
        {
            return sqlCreator.EnsureTableStructure(table);
        }
    }
}