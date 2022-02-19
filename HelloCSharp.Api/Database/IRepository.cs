using HelloCSharp.Api.Models;

namespace HelloCSharp.Api.Database;

public interface IRepository<TValue>
    where TValue : Identifiable
{
    TValue Create(TValue value);
    
    List<TValue> FindByFilter(Predicate<TValue> filter);
        
    List<TValue> FindAll();
        
    TValue GetById(int id);

    TValue? FindById(int id);
    
    TValue Update(TValue value);
}