using Microsoft.EntityFrameworkCore;
using TransactionData.Data.Entities;
using TransactionData.Data.Entities.Entities;
using TransactionData.Data.EntityConfigurations;

namespace TransactionData.Data
{
    public class TransactionDataContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public TransactionDataContext(DbContextOptions<TransactionDataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Configurations

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            #endregion
        }
    }
}
