using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;
using TransactionData.Service.ExtensionMethods;
using TransactionData.Service.Interfaces.Services;

namespace TransactionData.Service.Services
{
    public class CsvTransactionService : ICsvTransactionService
    {
        public TryAsync<List<CsvTransactionModel>> GetCsvTransactionModel(SaveCsvCommand command)
        {
            return command.GetCsvTransactionModel();
        }
    }
}
