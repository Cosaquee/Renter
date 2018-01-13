using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IRepositoryService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        TEntity Get(object id);

        void Delete(object id);

        void Delete(TEntity entity);

        TEntity Insert(TEntity entity);

        IQueryable<TEntity> Queryable();

        TEntity Update(TEntity entity);
    }
}