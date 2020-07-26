using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using TransactionData.Data.Interfaces.Interfaces;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

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

        public virtual TryAsync<TEntity> CreateAsync(TEntity entity)
        {
            return TryAsync(async () =>
            {
                await Context.Set<TEntity>().AddAsync(entity);
                return entity;
            });
        }

        public virtual TryAsync<List<TEntity>> CreateAsync(List<TEntity> entity)
        {
            return TryAsync(async () =>
            {
                await Context.Set<TEntity>().AddRangeAsync(entity);
                return entity;
            });
        }

        public virtual TryAsync<TEntity> DeleteAsync(TEntity entity)
        {
            return TryAsync(async () =>
            {
                Context.Set<TEntity>().Remove(entity);
                return entity;
            });
        }

        public virtual TryAsync<TEntity> UpdateAsync(TEntity entity)
        {
            return TryAsync(async () =>
            {
                Context.Entry(entity).State = EntityState.Modified;
                return entity;
            });
        }

        public virtual TryAsync<Unit> SaveAsync()
        {
            return TryAsync(async () => await Context.SaveChangesAsync().ToUnit());
        }
    }
}
