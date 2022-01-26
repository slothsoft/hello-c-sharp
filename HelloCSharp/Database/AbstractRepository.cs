using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Web.UI.WebControls;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public abstract class AbstractRepository<T>  : Repository<T>
        where T : Identifiable
    {

        protected SQLiteConnection _connection;
        private String _select;

        internal AbstractRepository(SQLiteConnection connection, String select)
        {
            this._connection = connection;
            this._select = select;
        }

        public List<T> FindByFilter(Predicate<T> filter)
        {
            return FindAll().FindAll(filter);
        }
        
        public List<T> FindAll()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = CreateBasicSelect();
                return ConvertToList(command.ExecuteReader());
            }
        }

        protected virtual String CreateBasicSelect()
        {
            return this._select;
        }

        private List<T> ConvertToList(SQLiteDataReader reader)
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                result.Add(ConvertToT(reader));
            }
            return result;
        }

        protected abstract T ConvertToT(SQLiteDataReader reader);
        
        public T GetById(Int32 id)
        {
            var result = FindById(id);
            if (result == null)
            {
                throw new ArgumentException("Could not find entity with ID " + id);
            }
            return result;
        }

        public T FindById(Int32 id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = CreateSelectById(id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                    return ConvertToT(reader);
                return null;
            }
        }

        internal virtual String CreateSelectById(Int32 id)
        {
            return CreateBasicSelect() + " WHERE id = " + id;
        }
    }
}