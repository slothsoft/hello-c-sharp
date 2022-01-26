using System;
using System.Collections.Generic;
using System.Data.SQLite;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public class PersonRepository : AbstractRepository<Person>
    {
    
        private const string Select = "SELECT person.*, city.name as city_name from person LEFT JOIN city ON city_id = city.id ";
        
        public PersonRepository(SQLiteConnection connection) : base(connection, Select)
        {
        }

        protected override Person ConvertToT(SQLiteDataReader reader)
        {
            return new Person(
                Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["name"]),
                Convert.ToInt32(reader["age"]),
                ConvertToCity(reader)
            );           
        }
        
        private static City ConvertToCity(SQLiteDataReader reader)
        {
            return new City(
                Convert.ToInt32(reader["city_id"]),
                Convert.ToString(reader["city_name"])
            );           
        }
        
        protected override string CreateSelectById(Int32 id)
        {
            return CreateBasicSelect() + " WHERE person.id = " + id;
        }
       
        public List<Person> FindByCityId(int cityId)
        {
            return FindByFilter(p => p.City.Id == cityId);
        }
    }
}