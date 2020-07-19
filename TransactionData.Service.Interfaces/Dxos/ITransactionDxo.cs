using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Dtos;

namespace TransactionData.Service.Interfaces.Dxos
{
    public interface ITransactionDxo
    {
        Result<List<GetTransactionDto>> MapTransaction(IList<Transaction> model);
    }
}
