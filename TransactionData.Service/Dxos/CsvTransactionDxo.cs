using System;
using System.Collections.Generic;
using System.Text;
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
    public class CsvTransactionDxo : ICsvTransactionDxo
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

        public TryAsync<List<Transaction>> MapTransaction(IList<CsvTransactionModel> model)
        {
            return TryAsync(() => _mapper.Map<List<Transaction>>(model).AsTask());
        }
    }
}
