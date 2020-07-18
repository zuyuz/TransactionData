using System;
using TransactionData.Data.Entities.Enums;

namespace TransactionData.Data.Entities.Entities
{
    public class Transaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public TransactionStatusEnum Status { get; set; }
    }
}
