using System;
using System.Collections.Generic;
using System.Data.SQLite;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public class RelationshipRepository : AbstractRepository<Relationship>
    {
    
        private const string Select = "SELECT relationship.*, from_person.name AS from_name, to_person.name AS to_name FROM relationship LEFT JOIN person AS from_person ON from_person.id = from_id LEFT JOIN person AS to_person ON to_person.id = to_id  ";
        
        public RelationshipRepository(SQLiteConnection connection) : base(connection, Select)
        {
        }

        protected override Relationship ConvertToT(SQLiteDataReader reader)
        {
            return new Relationship(
                Convert.ToInt32(reader[ "id"]),
                (RelationshipType) Enum.Parse(typeof (RelationshipType),Convert.ToString(reader["type"])),
                Convert.ToInt32(reader["from_id"]),
                Convert.ToString(reader["from_name"]),
                Convert.ToInt32(reader["to_id"]),
                Convert.ToString(reader["to_name"])
            );    
        }
        
        protected override string CreateSelectById(Int32 id)
        {
            return CreateBasicSelect() + " WHERE relationship.id = " + id;
        }

        internal List<Relationship> FindAllIncludingOpposites()
        {
            var baseResult= base.FindAll();
            var result = new List<Relationship>(baseResult);
            foreach (var relationship in baseResult)
            {
                var opposite = relationship.Type.Opposite();
                if (opposite != null)
                {
                    result.Add(new Relationship(relationship.Id,(RelationshipType) opposite, relationship.ToId, relationship.ToName, relationship.FromId, relationship.FromName));
                }
            }
            return result;
        }

        public List<Relationship> FindByPersonId(int personId)
        {
            return FindAllIncludingOpposites().FindAll(r => r.FromId.Equals(personId) || (r.ToId.Equals(personId) && !r.Type.Opposite().HasValue));
        }

        public List<Relationship> FindByType(RelationshipType relationshipType)
        {
            return FindAllIncludingOpposites().FindAll(r => r.Type == relationshipType);
        }
    }
}