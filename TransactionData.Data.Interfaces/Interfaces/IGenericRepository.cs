using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;

namespace TransactionData.Data.Interfaces.Interfaces
{
    public interface IGenericRepository<in TKey, TEntity> : IReadGenericRepository<TKey, TEntity>
        where TEntity : class
    {
        TryAsync<TEntity> CreateAsync(TEntity entity);
        TryAsync<List<TEntity>> CreateAsync(List<TEntity> entity);
        TryAsync<TEntity> DeleteAsync(TEntity entity);
        TryAsync<TEntity> UpdateAsync(TEntity entity);
        TryAsync<Unit> SaveAsync();
    }
}
