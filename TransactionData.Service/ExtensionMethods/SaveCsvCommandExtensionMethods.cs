using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;
using TransactionData.Service.CsvMap;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.ExtensionMethods
{
    public static class SaveCsvCommandExtensionMethods
    {
        public static TryAsync<List<CsvTransactionModel>> GetCsvTransactionModel(this SaveCsvCommand command)
        {
            return TryAsync(() =>
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
                    throw new ArgumentException(badRows.Aggregate("CSV import failed with message:\n",
                        (first, second) => $"{first}{second}\n"));

                return goodRows.AsTask();
            });
        }
    }
}