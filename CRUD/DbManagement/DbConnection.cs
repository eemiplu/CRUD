using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRUD
{
    public class DbConnection : IDisposable
    {
        private static string connectionString => SetConnectionString();

        private static MySqlConnection SqlConnection;

        private DbConnection()
        {
        }

        public static MySqlConnection GetConnection()
        {
            return SqlConnection ??= new MySqlConnection(connectionString);
        }

        public static void OpenConnection(MySqlConnection sqlConnection)
        {
            sqlConnection.Open();
        }

        public void Dispose() //to chyba nie działa KEKW
        {
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        private static string SetConnectionString()
        {
            using (StreamReader r = new StreamReader(".\\dbConfig.json"))
            {
                string json = r.ReadToEnd();
                var a =  JObject.Parse(json);// ?? throw new Exception("Fail to read and load connection string from json file.");
                return a["connectionString"].ToString();
            }
        }
    }
}