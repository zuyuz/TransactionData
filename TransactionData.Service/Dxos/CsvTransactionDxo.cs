using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CSharpFunctionalExtensions;
using TransactionData.Data.Entities.Entities;
using TransactionData.Domain.Models;

namespace TransactionData.Service.Dxos
{
    public class CsvTransactionDxo
    {
        private readonly IMapper _mapper;

        public CsvTransactionDxo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CsvTransactionModel, Transaction>();
            });

            _mapper = config.CreateMapper();
        }

        public Result<IList<Transaction>> MapTransaction(IList<CsvTransactionModel> model)
        {
            try
            {
                return Result.Success(_mapper.Map<IList<Transaction>>(model));
            }
            catch (Exception e)
            {
                return Result.Failure<IList<Transaction>>(e.Message);
            }
        }
    }
}
