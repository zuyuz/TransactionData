using LanguageExt;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Interfaces.Services
{
    public interface IXmlTransactionService
    {
        TryAsync<XmlTransactionModel> GetXmlTransactionModel(SaveXmlCommand command);
    }
}