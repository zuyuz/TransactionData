using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Services
{
    public interface ICsvTransactionService
    {
        Result<List<CsvTransactionModel>> GetCsvTransactionModel(SaveCsvCommand command);
    }
}
