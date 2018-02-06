using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common.EntityFramework.Interfaces;

namespace Common.EntityFramework.Implement
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase
    {
        protected EntityBaseRepository(DbContext dbContext)
        {
            Entities = dbContext.Set<T>();
        }

        protected DbSet<T> Entities { get; }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Entities.AsQueryable();

            foreach (var inculdeProperty in includeProperties)
                query = Entities.Include(inculdeProperty);

            return query.AsEnumerable();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.AsEnumerable().ToList();
        }

        public T GetSingle(Guid id)
        {
            return Entities.FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Entities.AsQueryable();

            query = includeProperties.Aggregate(query, (current, inculdeProperty) => current.Include(inculdeProperty));

            return query.Where(predicate).FirstOrDefault();
        }

        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entityList)
        {
            Entities.AddRange(entityList);
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = Entities.Where(predicate);

            foreach (var entity in entities)
            {
                Entities.Remove(entity);
            }
        }
    }
}