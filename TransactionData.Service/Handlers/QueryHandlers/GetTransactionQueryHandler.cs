using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Dtos;
using TransactionData.Service.Dxos;
using TransactionData.Service.ExtensionMethods;
using TransactionData.Service.Interfaces.Dxos;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.Handlers.QueryHandlers
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, TryAsync<List<GetTransactionDto>>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionDxo _transactionDxo;

        public GetTransactionQueryHandler(ITransactionRepository transactionRepository,
            ITransactionDxo transactionDxo)
        {
            _transactionRepository = transactionRepository;
            _transactionDxo = transactionDxo;
        }

        public async Task<TryAsync<List<GetTransactionDto>>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            return TryAsync(await _transactionRepository.GetAll()
                    .Where(transaction =>
                        (request.Currency == null || request.Currency == transaction.CurrencyCode)
                        && (!request.To.HasValue || request.To.Value >= transaction.TransactionDate)
                        && (!request.From.HasValue || request.From.Value <= transaction.TransactionDate)
                        && (request.Status == null ||
                            request.Status.Contains((GetTransactionStatusEnumQuery) transaction.Status)))
                    .ToListAsync(cancellationToken: cancellationToken))
                .Bind(transactions => _transactionDxo.MapTransaction(transactions));
        }
    }
}
