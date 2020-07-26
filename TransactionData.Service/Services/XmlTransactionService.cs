using LanguageExt;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;
using TransactionData.Service.ExtensionMethods;
using TransactionData.Service.Interfaces.Services;

namespace TransactionData.Service.Services
{
    public class XmlTransactionService : IXmlTransactionService
    {
        public TryAsync<XmlTransactionModel> GetXmlTransactionModel(SaveXmlCommand command)
        {
            return command.GetXmlTransactionModel();
        }
    }
}