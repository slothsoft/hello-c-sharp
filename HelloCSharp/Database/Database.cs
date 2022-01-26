using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public class Database
    {
        private static Database _instance;

        public static Database GetInstance()
        {
            return _instance ?? (_instance = new Database());
        }

        private readonly SQLiteConnection _connection;

        private readonly CityRepository _cityRepository;
        private readonly PersonRepository _personRepository;
        
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
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"HelloCSharp.Database.init-db.sql");
                using (var reader = new StreamReader(stream))
                {
                    command.CommandText = reader.ReadToEnd();
                }

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

        public List<Person> FindPersonsByCityId(int cityId)
        {
            return _personRepository.FindByCityId(cityId);
        }
        
        public void Close()
        {
            _connection.Close();
        }
    }
}