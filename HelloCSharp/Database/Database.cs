using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public class Database
    {
        private const string SELECT_PERSON = "SELECT person.*, city.name as city_name from person LEFT JOIN city ON city_id = city.id ";
        private const string SELECT_CITY = "SELECT * FROM city ";

        
        private static Database _instance;

        public static Database GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Database();
            }
            return _instance;
        }

        private SQLiteConnection _connection;
        
        private Database()
        {
            _connection = new SQLiteConnection("Data Source=:memory:");
            _connection.Open();

            InitDatabase();
        }
        
        private void InitDatabase()
        {
            using (var command = _connection.CreateCommand()) {
                command.CommandText = File.ReadAllText("C:\\Users\\sschulz\\IdeaProjects\\HelloCSharp\\HelloCSharp\\Database\\init-db.sql");
                command.ExecuteNonQuery();
            }
            FindAllPersons();
        }

        public List<Person> FindAllPersons()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = SELECT_PERSON;
                return ConvertToPersons(command.ExecuteReader());
            }
        }

        private List<Person> ConvertToPersons(SQLiteDataReader reader)
        {
            List<Person> result = new List<Person>();
            while (reader.Read())
            {
                result.Add(ConvertToPerson(reader));
            }
            return result;
        }
        
        private Person ConvertToPerson(SQLiteDataReader reader)
        {
           return new Person(
                    Convert.ToInt32(reader["id"]),
                    Convert.ToString(reader["name"]),
                    Convert.ToInt32(reader["age"]),
                    ConvertToCity(reader, "city_")
           );           
        }
        
        private City ConvertToCity(SQLiteDataReader reader, String prefix)
        {
            return new City(
                Convert.ToInt32(reader[prefix + "id"]),
                Convert.ToString(reader[prefix + "name"])
            );           
        }

        public Person GetPerson(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = SELECT_PERSON + "WHERE person.id = " + id;
               var reader = command.ExecuteReader();
                if (reader.Read())
                    return ConvertToPerson(reader);
                throw new Exception("Could not find person with ID " + id);
            }
        }
        
        public List<City> FindAllCities()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = SELECT_CITY;
                return ConvertToCities(command.ExecuteReader());
            }
        }
        
        private List<City> ConvertToCities(SQLiteDataReader reader)
        {
            List<City> result = new List<City>();
            while (reader.Read())
            {
                result.Add(ConvertToCity(reader, ""));
            }
            return result;
        }
        
        public City GetCity(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = SELECT_CITY + "WHERE id = " + id;
                var reader = command.ExecuteReader();
                if (reader.Read())
                    return ConvertToCity(reader, "");
                throw new Exception("Could not find city with ID " + id);
            }
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}