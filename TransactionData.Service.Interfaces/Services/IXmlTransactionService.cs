using System.Collections.Generic;
using CSharpFunctionalExtensions;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Services
{
    public interface IXmlTransactionService
    {
        Result<XmlTransactionModel> GetXmlTransactionModel(SaveXmlCommand command);
    }
}