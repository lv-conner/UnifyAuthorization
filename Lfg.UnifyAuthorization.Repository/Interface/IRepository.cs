using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Base;

namespace Lfg.UnifyAuthorization.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity:IAggregateRoot
    {
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        TEntity Find(object keyValue);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> FindAsync(object keyValue);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
