using System;
using System.Collections.Generic;
using HelloCSharp.Models;

namespace HelloCSharp.Database;

public interface IRepository<TValue>
    where TValue : Identifiable
{
    List<TValue> FindByFilter(Predicate<TValue> filter);
        
    List<TValue> FindAll();
        
    TValue GetById(int id);

    TValue? FindById(int id);
}