using System;

namespace HelloCSharp.Models
{
    public class Identifiable
    {
        
        public Identifiable(Int32? id)
        {
            Id = id;
        }

        public bool HasId()
        {
            return Id != null;
        }
        
        public Int32? Id { get; }
    }
}