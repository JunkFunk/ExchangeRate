using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.EntityFramework.Interfaces
{
    public interface IEntityBaseRepository<T> where T : IEntityBase
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetSingle(Guid id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void AddRange(IEnumerable<T> entityList);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
    }
}