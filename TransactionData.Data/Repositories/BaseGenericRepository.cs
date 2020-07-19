using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionData.Data.Interfaces.Interfaces;

namespace TransactionData.Data.Repositories
{

    public abstract class BaseGenericRepository<TKey, TContext, TEntity> : BaseReadGenericRepository<TKey, TContext, TEntity>,
        IGenericRepository<TKey, TEntity> where TEntity : class
        where TContext : DbContext
    {
        protected BaseGenericRepository(TContext context) : base(context)
        {
            Context = context;
        }

        public virtual async Task<Result<TEntity>> CreateAsync(TEntity entity)
        {
            try
            {
                await Context.Set<TEntity>().AddAsync(entity);
                return Result.Success(entity);
            }
            catch (Exception e)
            {
                return Result.Failure<TEntity>(e.InnerException?.Message ?? e.Message);
            }
        }

        public async Task<Result<IList<TEntity>>> CreateAsync(IList<TEntity> entity)
        {
            try
            {
                await Context.Set<TEntity>().AddRangeAsync(entity);
                return Result.Success(entity);
            }
            catch (Exception e)
            {
                return Result.Failure<IList<TEntity>>(e.InnerException?.Message ?? e.Message);
            }
        }

        public virtual Task<Result<TEntity>> DeleteAsync(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
                return Task.FromResult(Result.Success(entity));
            }
            catch (Exception e)
            {
                return Task.FromResult(Result.Failure<TEntity>(e.InnerException?.Message ?? e.Message));
            }
        }

        public virtual Task<Result<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                return Task.FromResult(Result.Success(entity));
            }
            catch (Exception e)
            {
                return Task.FromResult(Result.Failure<TEntity>(e.InnerException?.Message ?? e.Message));
            }
        }

        public virtual async Task<Result<Unit>> SaveAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
                return Result.Success(Unit.Value);
            }
            catch (Exception e)
            {
                return Result.Failure<Unit>(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
