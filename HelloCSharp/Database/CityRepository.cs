using System;
using System.Data.SQLite;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public class CityRepository : AbstractRepository<City>
    {
    
        private const string Select = "SELECT * FROM city ";
        
        public CityRepository(SQLiteConnection connection) : base(connection, Select)
        {
        }

        protected override City ConvertToT(SQLiteDataReader reader)
        {
            return new City(
                Convert.ToInt32(reader[ "id"]),
                Convert.ToString(reader["name"])
            );    
        }
    }
}