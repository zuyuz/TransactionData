using System;
using System.Collections.Generic;
using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Dtos;
using TransactionData.Domain.Models;
using TransactionData.Service.Interfaces.Dxos;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.Dxos
{
    public class TransactionDxo : ITransactionDxo
    {
        private readonly IMapper _mapper;

        public TransactionDxo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, GetTransactionDto>()
                    .ForMember(dto => dto.AmountAndCurrencyCode, 
                        m => m.MapFrom(transaction => $"{transaction.Amount} {transaction.CurrencyCode}"));
            });

            _mapper = config.CreateMapper();
        }

        public TryAsync<List<GetTransactionDto>> MapTransaction(IList<Transaction> model)
        {
            return TryAsync(() => _mapper.Map<List<GetTransactionDto>>(model).AsTask());
        }
    }
}