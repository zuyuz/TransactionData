using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CSharpFunctionalExtensions;
using CsvHelper;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;
using TransactionData.Service.CsvMap;

namespace TransactionData.Service.ExtensionMethods
{
    public static class SaveCsvCommandExtensionMethods
    {
        public static Result<List<CsvTransactionModel>> GetCsvTransactionModel(this SaveCsvCommand command)
        {
            try
            {
                using TextReader reader = new StreamReader(command.Stream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                csv.Configuration.RegisterClassMap<CsvTransactionMap>();
                return csv.GetRecords<CsvTransactionModel>().ToList();
            }
            catch (Exception e)
            {
                return Result.Failure<List<CsvTransactionModel>>(e.Message);
            }
        }
    }
}