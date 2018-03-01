using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Repository.Interface;
using Lfg.UnifyAuthorization.Base;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Lfg.UnifyAuthorization.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity>,IDisposable where TEntity : class, IAggregateRoot
    {
        protected virtual DbContext DbContext { get; set; }
        protected virtual DbSet<TEntity> DbSet { get => DbContext.Set<TEntity>(); }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public void Dispose()
        {
            ((IDisposable)DbContext).Dispose();
        }

        public TEntity Find(object keyValue)
        {
            return DbSet.Find(keyValue);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Find(predicate);
        }

        public async Task<TEntity> FindAsync(object keyValue)
        {
            return await DbSet.FindAsync(keyValue);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FindAsync(predicate);
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
            SaveChanges();
        }

        public async Task InsertAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChangesAsync();
        }

        public void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw new ArgumentException("保存数据验证错误", ex);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                throw new ArgumentException("保存数据时发生异常", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw new ArgumentException("保存数据验证错误", ex);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                throw new ArgumentException("保存数据时发生异常", ex);
            }
        }
    }
}
