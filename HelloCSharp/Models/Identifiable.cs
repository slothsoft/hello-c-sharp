namespace HelloCSharp.Models
{
    public class Identifiable
    {
        
        protected Identifiable(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}