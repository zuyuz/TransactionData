using System;
using System.Collections.Generic;
using AutoMapper;
using CSharpFunctionalExtensions;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Dxos
{
    public class XmlTransactionDxo
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

        public Result<IList<Transaction>> MapTransaction(XmlTransactionModel model)
        {
            try
            {
                return Result.Success(_mapper.Map<IList<Transaction>>(model.Transactions));
            }
            catch (Exception e)
            {
                return Result.Failure<IList<Transaction>>(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}