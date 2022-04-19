using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NitecoTest.Context;
using NitecoTest.Interfaces.IRepositories;
using NitecoTest.Models;

namespace NitecoTest.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly AppDataContext DataContext;

        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(AppDataContext db)
        {
            DataContext = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public virtual async Task RemoveRange(List<TEntity> listEntity)
        {
            DbSet.RemoveRange(listEntity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await DataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DataContext?.Dispose();
        }
    }
}