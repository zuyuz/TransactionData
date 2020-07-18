using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionData.Data.Interfaces.Interfaces;

namespace TransactionData.Data.Repositories
{
    public abstract class BaseReadGenericRepository<TKey, TContext, TEntity> :
        IReadGenericRepository<TKey, TEntity> where TEntity : class
        where TContext : DbContext
    {
        protected BaseReadGenericRepository(TContext context)
        {
            Context = context;
        }

        public TContext Context { get; set; }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            try
            {
                return await Context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
