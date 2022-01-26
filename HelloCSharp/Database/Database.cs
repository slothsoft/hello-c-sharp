using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public class Database
    {
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

        private CityRepository _cityRepository;
        private PersonRepository _personRepository;
        
        internal Database()
        {
            _connection = new SQLiteConnection("Data Source=:memory:");
            _connection.Open();

            InitDatabase(_connection);

            this._cityRepository = new CityRepository(_connection);
            this._personRepository = new PersonRepository(_connection);
        }
        
        internal static void InitDatabase(SQLiteConnection connection)
        {
            using (var command = connection.CreateCommand()) {
                command.CommandText = File.ReadAllText("C:\\Users\\sschulz\\IdeaProjects\\HelloCSharp\\HelloCSharp\\Database\\init-db.sql");
                command.ExecuteNonQuery();
            }
        }

        public List<Person> FindAllPersons()
        {
            return _personRepository.FindAll();
        }

        public Person GetPerson(int id)
        {
            return _personRepository.GetById(id);
        }
        
        public List<City> FindAllCities()
        {
            return _cityRepository.FindAll();
        }
        
        public City GetCity(int id)
        {
            return _cityRepository.GetById(id);
        }
        
        public void Close()
        {
            _connection.Close();
        }
    }
}