using System;

namespace TransactionData.Domain.Dtos
{
    [Flags]
    public enum GetTransactionStatusEnumQuery
    {
        A = 1,
        R = 2,
        D = 4
    }
}