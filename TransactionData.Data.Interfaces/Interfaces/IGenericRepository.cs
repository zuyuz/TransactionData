using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;

namespace TransactionData.Data.Interfaces.Interfaces
{
    public interface IGenericRepository<in TKey, TEntity> : IReadGenericRepository<TKey, TEntity>
        where TEntity : class
    {
        Task<Result<TEntity>> CreateAsync(TEntity entity);
        Task<Result<IList<TEntity>>> CreateAsync(IList<TEntity> entity);
        Task<Result<TEntity>> DeleteAsync(TEntity entity);
        Task<Result<TEntity>> UpdateAsync(TEntity entity);
        Task<Result<Unit>> SaveAsync();
    }
}
