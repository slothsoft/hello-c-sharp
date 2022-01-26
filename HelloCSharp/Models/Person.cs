namespace HelloCSharp.Models
{
    public class Person : Identifiable
    {
        
        public Person(int? id, string name, int age, City city) : base(id)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public string Name { get; }
        public int Age { get; }
        public City City { get; }

        public override string ToString() => $"Person {Name} ({Id})";
    }
}