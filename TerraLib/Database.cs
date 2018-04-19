using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Reflection;
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
                    db = new SqliteConnection(string.Format("Data Source={0};Version=3", sql));
                    break;
            }

            sqlCreator = new SqlTableCreator(db,
                db.GetSqlType() == SqlType.Sqlite ? (IQueryBuilder)new SqliteQueryCreator() : new MysqlQueryCreator());
        }

        public static bool EnsureTable(SqlTable table)
        {
            return sqlCreator.EnsureTableStructure(table);
        }

        public static int Query(string query, params object[] args)
        {
            return db.Query(query, args);
        }

        public static IEnumerable<T> QueryResult<T>(string query, params object[] args) where T : new()
        {
            using (var res = db.QueryReader(query, args))
            {
                List<PropertyInfo> props = new List<PropertyInfo>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.IsDefined(typeof(Column), true))
                        props.Add(prop);
                }

                while(res.Read())
                {
                    T obj = new T();
                    foreach (var prop in props)
                        SetProperty(prop, obj, res);

                    yield return obj;
                }
            }
        }

        private static void SetProperty(PropertyInfo prop, object obj, QueryResult res)
        {
            if (prop.PropertyType == typeof(int))
                prop.SetValue(obj, res.Get<int>(prop.Name));
            else if (prop.PropertyType == typeof(float))
                prop.SetValue(obj, res.Get<float>(prop.Name));
            else if (prop.PropertyType == typeof(string))
                prop.SetValue(obj, res.Get<string>(prop.Name));
            else if (prop.PropertyType == typeof(long))
                prop.SetValue(obj, res.Get<long>(prop.Name));
            else if (prop.PropertyType == typeof(short))
                prop.SetValue(obj, res.Get<short>(prop.Name));
            else if (prop.PropertyType == typeof(byte))
                prop.SetValue(obj, res.Get<byte>(prop.Name));
            else if (prop.PropertyType == typeof(bool))
                prop.SetValue(obj, res.Get<bool>(prop.Name));
            else if (prop.PropertyType == typeof(DateTime))
                prop.SetValue(obj, res.Get<DateTime>(prop.Name));
        }
    }
}