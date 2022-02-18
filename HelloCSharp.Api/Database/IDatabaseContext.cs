namespace HelloCSharp.Api.Database;

/**
 * The root interface to get all the repositories this application entails.
 */

public interface IDatabaseContext
{
   
     ICityRepository CityRepository { get; }

     IPersonRepository PersonRepository { get; }

     IRelationshipRepository RelationshipRepository { get; }
}