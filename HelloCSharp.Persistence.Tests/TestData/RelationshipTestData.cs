using HelloCSharp.Api.Models;
using HelloCSharp.Api.Tests;
using HelloCSharp.Persistence.Database;
using HelloCSharp.Persistence.Entities;

namespace HelloCSharp.Persistence.Tests.TestData;

public class RelationshipTestData : ITestData<Relationship, SaveRelationship>
{
    private readonly DatabaseContext _context;

    public RelationshipTestData(DatabaseContext context)
    {
        _context = context;
    }
    
    public Relationship GetExampleObject()
    {
        return RelationshipExtensions.CreateExampleObject();
    }

    public SaveRelationship CreateRandomObject()
    {
        var city = _context.Add(new CityEntity { Name = "Dresden" });
        var stef = _context.Add(new PersonEntity { Name = "Stef", Age = 35, CityId = city.Entity.Id});
        var julchen = _context.Add(new PersonEntity { Name = "Julchen", Age = 21, CityId = city.Entity.Id });
        _context.SaveChanges();
        return new SaveRelationship
        {
            Type = RelationshipType.Siblings,
            FromId = stef.Entity.Id!.Value, 
            ToId = julchen.Entity.Id!.Value
        };
    }

    public void AssertAreEqual(Relationship expected, Relationship actual) =>  expected.AssertAreEqual(actual);
    
    public void AssertAreEqual(SaveRelationship expected, Relationship actual) => expected.AssertAreEqual(actual);
}