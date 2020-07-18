using System;
using System.Text;
using CsvHelper.Configuration.Attributes;
using TransactionData.Domain.Constants;
using TransactionData.Domain.Enums;

namespace TransactionData.Domain.Models
{
    public class CsvTransactionModel
    {
        [Name(CsvHeaders.Id)]
        public string Id { get; set; }

        [Name(CsvHeaders.Amount)]
        public decimal Amount { get; set; }

        [Name(CsvHeaders.CurrencyCode)]
        public string CurrencyCode { get; set; }

        [Name(CsvHeaders.TransactionDate)]
        public DateTimeOffset TransactionDate { get; set; }

        [Name(CsvHeaders.Status)]
        public CsvTransactionStatusEnum Status { get; set; }
    }
}
