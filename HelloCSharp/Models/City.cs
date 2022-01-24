using System;

namespace HelloCSharp.Models
{
    public struct City
    {
        
        public City(int id, String name)
        {
            Id = id;
            Name = name;
        }

        public bool HasId()
        {
            return Id != null;
        }
        public int Id { get; }
        public String Name { get; }

        public override string ToString() => $"City {Name} ({Id})";
    }
}