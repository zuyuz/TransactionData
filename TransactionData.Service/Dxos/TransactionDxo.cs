using System;
using System.Collections.Generic;
using AutoMapper;
using CSharpFunctionalExtensions;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Dtos;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Dxos
{
    public class TransactionDxo
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

        public Result<List<GetTransactionDto>> MapTransaction(IList<Transaction> model)
        {
            try
            {
                return Result.Success(_mapper.Map<List<GetTransactionDto>>(model));
            }
            catch (Exception e)
            {
                return Result.Failure<List<GetTransactionDto>>(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}