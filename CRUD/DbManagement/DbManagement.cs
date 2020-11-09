using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace CRUD
{
    public class DbManagement
    {
        public void CreateDb()
        {
            string sqlCreate;

            using (StreamReader streamReader = new StreamReader(".\\Scripts\\CreateDb.sql", Encoding.UTF8))
            {
                sqlCreate = streamReader.ReadToEnd();
            }

            using (var command = new MySqlCommand(sqlCreate, DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public void InsertValues(string name, string surname, int age)
        {
            using (var command = new MySqlCommand($"INSERT INTO PFSwChO.dane(name, surname, age)VALUES(\"{ name }\", \"{ surname }\", \"{ age }\")", DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public void UpdateValue(string columnName, string value, string whereColumnName, string whereValue)
        {
            using (var command = new MySqlCommand($"UPDATE PFSwChO.dane SET {columnName}=\"{value}\" WHERE {whereColumnName}=\"{whereValue}\"", DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public List<User> SelectValues()
        {
            var users = new List<User>();

            using (var command = new MySqlCommand("SELECT id, name, surname, age FROM PFSwChO.dane", DbConnection.GetConnection()))
            {
                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        users.Add(new User
                        {
                            Id = int.Parse(rdr["Id"].ToString()),
                            Name = rdr["Name"].ToString(),
                            Surname = rdr["Surname"].ToString(),
                            Age = int.Parse(rdr["Age"].ToString())
                        });
                    }
                }
            }

            return users;
        }

        public void DeleteValue(int id)
        {
            using (var command = new MySqlCommand($"DELETE FROM PFSwChO.dane WHERE id = {id}", DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}