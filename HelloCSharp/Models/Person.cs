using System;

namespace HelloCSharp.Models
{
    public struct Person
    {
        
        public Person(Int32? id, String name, int age, City city)
        {
            Id = id;
            Name = name;
            Age = age;
            City = city;
        }

        public bool HasId()
        {
            return Id != null;
        }
        public Int32? Id { get; }
        public String Name { get; }
        public int Age { get; }
        public City City { get; }

        public override string ToString() => $"Person {Name} ({Id})";
    }
}