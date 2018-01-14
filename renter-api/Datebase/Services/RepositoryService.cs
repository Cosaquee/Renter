using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Database.Services
{
    public class RepositoryService<TEntity> : IRepositoryService<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> dbSet;
        protected DbContext dbContext;

        public RepositoryService(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return dbSet.AsEnumerable();
        }

        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            dbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return dbSet.AsQueryable();
        }
    }
}