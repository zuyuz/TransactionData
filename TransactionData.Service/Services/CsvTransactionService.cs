using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;
using TransactionData.Service.ExtensionMethods;
using TransactionData.Service.Interfaces.Services;

namespace TransactionData.Service.Services
{
    public class CsvTransactionService : ICsvTransactionService
    {
        public Result<List<CsvTransactionModel>> GetCsvTransactionModel(SaveCsvCommand command)
        {
            return command.GetCsvTransactionModel();
        }
    }
}
