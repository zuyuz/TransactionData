using System;
using System.Collections.Generic;
using System.Text;
using TransactionData.Data.Entities;
using TransactionData.Data.Entities.Entities;
using TransactionData.Data.Interfaces.Interfaces;

namespace TransactionData.Data.Repositories
{
    public class TransactionRepository : BaseGenericRepository<string, TransactionDataContext, Transaction>, ITransactionRepository
    {
        public TransactionRepository(TransactionDataContext context) : base(context)
        {
            
        }
    }
}
