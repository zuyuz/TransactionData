using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using TransactionData.Domain.Constants;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Dxos
{
    public sealed class CsvTransactionMap : ClassMap<CsvTransactionModel>
    {
        public CsvTransactionMap()
        {
            Map(m => m.Id).Name(CsvHeaders.Id);
            Map(m => m.Amount).Name(CsvHeaders.Amount);
            Map(m => m.CurrencyCode).Name(CsvHeaders.CurrencyCode);
            Map(m => m.Status).Name(CsvHeaders.Status);
            Map(m => m.TransactionDate).Name(CsvHeaders.TransactionDate);
        }
    }
}
