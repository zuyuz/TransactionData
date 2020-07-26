using CsvHelper.Configuration;
using TransactionData.Domain.Constants;
using TransactionData.Domain.Models;

namespace TransactionData.Service.CsvMap
{
    public sealed class CsvTransactionMap : ClassMap<CsvTransactionModel>
    {
        public CsvTransactionMap()
        {
            Map(m => m.Id).Name(CsvHeaders.Id);
            Map(m => m.Amount).Name(CsvHeaders.Amount);
            Map(m => m.CurrencyCode).Validate(field => !field.Contains("-")).Name(CsvHeaders.CurrencyCode);
            Map(m => m.Status).Name(CsvHeaders.Status);
            Map(m => m.TransactionDate).Name(CsvHeaders.TransactionDate);
        }
    }
}
