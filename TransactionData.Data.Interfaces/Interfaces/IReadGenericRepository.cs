using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TransactionData.Data.Interfaces.Interfaces
{
    public interface IReadGenericRepository<in TKey, TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(TKey id);
    }
}