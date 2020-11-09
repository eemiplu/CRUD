using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            DbConnection.OpenConnection(DbConnection.GetConnection());
            DbManagement db = new DbManagement();
            
            db.CreateDb();

            var isAlive = true;

            while (isAlive)
            {
                Console.WriteLine("Prosty CRUD dla tabeli Users");
                Console.WriteLine("");

                Console.WriteLine("----Menu----");
                Console.WriteLine("1) Dodaj");
                Console.WriteLine("2) Wyświetl wszystkich użytkowników");
                Console.WriteLine("3) Modyfikuj");
                Console.WriteLine("4) Usuń");
                Console.WriteLine("5) Zamknij");

                var data = Console.ReadLine();

                if (data == "5")
                    isAlive = false;
                else if (data == "1") //Dodaj
                {
                    Console.WriteLine("Podaj imie: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Podaj nazwisko: ");
                    var surname = Console.ReadLine();
                    Console.WriteLine("Podaj wiek: ");
                    var age = Int32.Parse(Console.ReadLine());

                    db.InsertValues(name, surname, age);
                }
                else if (data == "2") //Wyświetl
                {
                    List<User> users = db.SelectValues();

                    foreach (var user in users)
                    {
                        Console.WriteLine("------------");
                        Console.WriteLine("Id: " + user.Id);
                        Console.WriteLine("Imię: " + user.Name);
                        Console.WriteLine("Nazwisko: " + user.Surname);
                        Console.WriteLine("Wiek: " + user.Age);
                        Console.WriteLine("------------");
                    }
                }
                else if (data == "3") //Edytuj
                {
                    Console.WriteLine("Podaj id użytkownika: ");

                    var id = Console.ReadLine();

                    Console.WriteLine("Wybierz wartość, która ma być modyfikowana");
                    Console.WriteLine("1) Imie");
                    Console.WriteLine("2) Nazwisko");
                    Console.WriteLine("3) Wiek");

                    data = Console.ReadLine();
                    
                    string columnName = "";

                    if (data == "1")
                        columnName = "Name";
                    if (data == "2")
                        columnName = "Surname";
                    if (data == "3")
                        columnName = "Age";

                    Console.WriteLine("Podaj wartość " + columnName);

                    string value = Console.ReadLine();

                    db.UpdateValue(columnName, value, "Id", id);

                    Console.WriteLine("Zmodyfikowano pomyślnie");
                }
                else if (data == "4") //Usuń
                {
                    Console.WriteLine("Podaj id: ");
                    int id = Int32.Parse(Console.ReadLine());

                    db.DeleteValue(id);

                    Console.WriteLine("Usunięto pomyślnie");
                }
            }
        }
    }
}
