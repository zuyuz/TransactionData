using System;
using System.Collections.Generic;
using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;
using TransactionData.Service.Interfaces.Dxos;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.Dxos
{
    public class XmlTransactionDxo : IXmlTransactionDxo
    {
        private readonly IMapper _mapper;

        public XmlTransactionDxo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransactionXmlElement, Transaction>()
                    .ForMember(transaction => transaction.Amount, m => m.MapFrom(src => src.PaymentDetails.Amount))
                    .ForMember(transaction => transaction.CurrencyCode, m => m.MapFrom(src => src.PaymentDetails.CurrencyCode));
            });

            _mapper = config.CreateMapper();
        }

        public TryAsync<List<Transaction>> MapTransaction(XmlTransactionModel model)
        {
            return TryAsync(() => _mapper.Map<List<Transaction>>(model.Transactions).AsTask());
        }
    }
}