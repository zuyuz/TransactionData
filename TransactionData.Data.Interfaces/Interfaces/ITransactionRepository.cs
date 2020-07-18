using System;
using System.Collections.Generic;
using System.Text;
using TransactionData.Data.Entities.Entities;

namespace TransactionData.Data.Interfaces.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<string, Transaction>
    {
    }
}
