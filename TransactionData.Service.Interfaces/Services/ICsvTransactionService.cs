using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Services
{
    public interface ICsvTransactionService
    {
        TryAsync<List<CsvTransactionModel>> GetCsvTransactionModel(SaveCsvCommand command);
    }
}
