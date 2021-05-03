using System.Collections.Generic;

namespace Matrix.PA.Repository
{
    public interface IRepository<T> 
    {
        List<T> All();
        T ById(long id);
        bool Save(T obj);
        bool Delete(long id);
    }
}