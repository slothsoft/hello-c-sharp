using System;
using System.Collections.Generic;
using HelloCSharp.Models;

namespace HelloCSharp.Database
{
    public interface IRepository<T>
        where T : Identifiable
    {
        List<T> FindByFilter(Predicate<T> filter);
        
        List<T> FindAll();
        
        T GetById(Int32 id);

        T FindById(Int32 id);
    }
}