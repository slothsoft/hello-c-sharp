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
        
        internal Database()
        {
            _connection = new SQLiteConnection("Data Source=:memory:");
            _connection.Open();

            InitDatabase(_connection);

            this.CityRepository = new CityRepository(_connection);
            this.PersonRepository = new PersonRepository(_connection);
            this.RelationshipRepository = new RelationshipRepository(_connection);
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

        public CityRepository CityRepository { get; }
        
        public PersonRepository PersonRepository { get; }
        
        public RelationshipRepository RelationshipRepository { get; }

        public void Close()
        {
            _connection.Close();
        }
    }
}