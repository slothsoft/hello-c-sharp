using System;

namespace HelloCSharp.Models
{
    public class City : Identifiable
    {
        
        public City(Int32? id, String name) : base(id)
        {
            Name = name;
        }

        public String Name { get; }

        public override string ToString() => $"City {Name} ({Id})";
    }
}