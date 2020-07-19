using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Dxos
{
    public interface IXmlTransactionDxo
    {
        Result<IList<Transaction>> MapTransaction(XmlTransactionModel model);
    }
}
