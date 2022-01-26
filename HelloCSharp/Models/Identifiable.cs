namespace HelloCSharp.Models
{
    public class Identifiable
    {
        
        protected Identifiable(int? id)
        {
            Id = id;
        }

        public bool HasId()
        {
            return Id != null;
        }
        
        public int? Id { get; }
    }
}