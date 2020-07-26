using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Dxos
{
    public interface IXmlTransactionDxo
    {
        TryAsync<List<Transaction>> MapTransaction(XmlTransactionModel model);
    }
}
