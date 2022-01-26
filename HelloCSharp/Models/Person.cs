using System;

namespace HelloCSharp.Models
{
    public class Person : Identifiable
    {
        
        public Person(Int32? id, String name, int age, City city) : base(id)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public String Name { get; }
        public int Age { get; }
        public City City { get; }

        public override string ToString() => $"Person {Name} ({Id})";
    }
}