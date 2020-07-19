using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Dtos;
using TransactionData.Service.Dxos;
using TransactionData.Service.ExtensionMethods;

namespace TransactionData.Service.Handlers.QueryHandlers
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Result<List<GetTransactionDto>, string>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionDxo _transactionDxo;

        public GetTransactionQueryHandler(ITransactionRepository transactionRepository,
            TransactionDxo transactionDxo)
        {
            _transactionRepository = transactionRepository;
            _transactionDxo = transactionDxo;
        }

        public async Task<Result<List<GetTransactionDto>, string>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _transactionRepository.GetAll()
                    .Where(transaction =>
                        (request.Currency == null || request.Currency == transaction.CurrencyCode)
                        && (!request.To.HasValue || request.To.Value >= transaction.TransactionDate)
                        && (!request.From.HasValue || request.From.Value <= transaction.TransactionDate)
                        && (request.Status == null || request.Status.Contains((GetTransactionStatusEnumQuery) transaction.Status)))
                    .ToListAsync(cancellationToken: cancellationToken);

                return _transactionDxo.MapTransaction(transactions)
                    .Bind(Result.Success<List<GetTransactionDto>, string>);
            }
            catch (Exception e)
            {
                return Result.Failure< List<GetTransactionDto>, string>(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
