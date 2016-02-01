using System.Collections.Generic;
using Gp.Data.Entities;

namespace Gp.Data.Common
{
    public interface Repository<T>
    {
        T Get(int id);
        List<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
    }
}