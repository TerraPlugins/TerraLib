﻿using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using TShockAPI;
using TShockAPI.DB;

namespace TerraLib
{
    public class Database
    {
        private static IDbConnection db;

        public static void Connect()
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
                    string sql = Path.Combine(TShock.SavePath, "TerraLib.sqlite");
                    db = new SqliteConnection(string.Format("uri=file://{0},Version=3", sql));
                    break;
            }

            SqlTableCreator sqlcreator = new SqlTableCreator(db,
                db.GetSqlType() == SqlType.Sqlite ? (IQueryBuilder)new SqliteQueryCreator() : new MysqlQueryCreator());

            sqlcreator.EnsureTableStructure(new SqlTable("TerraLib",
                new SqlColumn("ID", MySqlDbType.Int32) { Primary = true, Unique = true, Length = 7, AutoIncrement = true },
                new SqlColumn("UserID", MySqlDbType.Int32) { Length = 6 },
                new SqlColumn("Skillz", MySqlDbType.Int32) { Length = 6, DefaultValue = "0" }));
        }

        // Your methods here:

        public static void addSomething(int something)
        {
            try
            {
                db.Query("INSERT INTO TerraLib (Name) VALUES (@0)",
                    something
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void delSomething(int something)
        {
            try
            {
                db.Query("DELETE FROM TerraLib WHERE UserID = @0", something);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}