using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NitecoTest.Models;

namespace NitecoTest.Interfaces.IRepositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task RemoveRange(List<TEntity> listEntity);
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
