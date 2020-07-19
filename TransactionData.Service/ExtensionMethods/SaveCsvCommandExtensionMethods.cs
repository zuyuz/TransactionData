using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CSharpFunctionalExtensions;
using CsvHelper;
using Microsoft.Extensions.Logging;
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
                var goodRows = new List<CsvTransactionModel>();
                var badRows = new List<string>(); 
                csv.Configuration.RegisterClassMap<CsvTransactionMap>();
                while (csv.Read())
                {
                    try
                    {
                        var record = csv.GetRecord<CsvTransactionModel>();
                        goodRows.Add(record);
                    }
                    catch (Exception e)
                    {
                        badRows.Add($"{e.InnerException?.Message ?? e.Message}\nBroken row:\n{csv.Context.RawRecord}");
                    }
                }

                if (badRows.Any())
                    return Result.Failure<List<CsvTransactionModel>>(badRows.Aggregate("",
                        (first, second) => $"{first}{second}\n"));

                return goodRows;
            }
            catch (Exception e)
            {
                return Result.Failure<List<CsvTransactionModel>>(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}