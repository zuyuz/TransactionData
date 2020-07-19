using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionData.Domain.Dtos
{
    public class GetTransactionDto
    {
        public string Id { get; set; }
        public string AmountAndCurrencyCode { get; set; }
        public GetTransactionStatusEnumDto Status { get; set; }
    }
}
