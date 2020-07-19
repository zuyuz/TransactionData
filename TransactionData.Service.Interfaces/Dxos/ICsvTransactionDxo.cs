using System.Collections.Generic;
using CSharpFunctionalExtensions;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Dxos
{
    public interface ICsvTransactionDxo
    {
        Result<IList<Transaction>> MapTransaction(IList<CsvTransactionModel> model);
    }
}