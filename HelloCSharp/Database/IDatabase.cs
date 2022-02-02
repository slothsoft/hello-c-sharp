namespace HelloCSharp.Database;

/**
 * The root interface to get all the repositories this application entails.
 */

public interface IDatabase
{
   
     ICityRepository CityRepository { get; }

     IPersonRepository PersonRepository { get; }

     IRelationshipRepository RelationshipRepository { get; }
}