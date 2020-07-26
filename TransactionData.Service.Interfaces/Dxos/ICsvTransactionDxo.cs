using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Dxos
{
    public interface ICsvTransactionDxo
    {
        TryAsync<List<Transaction>> MapTransaction(IList<CsvTransactionModel> model);
    }
}