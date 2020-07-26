using System;
using System.IO;
using System.Xml.Serialization;
using CsvHelper;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Events;
using TransactionData.Domain.Models;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.ExtensionMethods
{
    public static class SaveXmlCommandExtensionMethods
    {
        public static TryAsync<XmlTransactionModel> GetXmlTransactionModel(this SaveXmlCommand command)
        {
            return TryAsync(() =>
            {
                using TextReader reader = new StreamReader(command.Stream);
                XmlSerializer serializer = new XmlSerializer(typeof(XmlTransactionModel));
                return ((XmlTransactionModel) serializer.Deserialize(reader)).AsTask();
            });
        }
    }
}
