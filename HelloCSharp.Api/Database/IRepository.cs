using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface IRepository<TValue, in TSave>
    where TValue : Identifiable
{
    TValue Create(TSave value);
    
    List<TValue> FindByFilter(Predicate<TValue> filter);
        
    List<TValue> FindAll();
        
    TValue GetById(int id);

    TValue? FindById(int id);
    
    TValue Update(int id, TSave value);
}