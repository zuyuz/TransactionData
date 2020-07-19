using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsvHelper;
using MediatR;
using TransactionData.Data.Entities.Entities;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Events;
using TransactionData.Domain.Models;
using TransactionData.Service.CsvMap;
using TransactionData.Service.Dxos;
using TransactionData.Service.ExtensionMethods;
using TransactionData.Service.Interfaces.Dxos;
using TransactionData.Service.Interfaces.Services;

namespace TransactionData.Service.Services
{
    public class SaveCsvCommandHandler : IRequestHandler<SaveCsvCommand, Result<Unit>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICsvTransactionDxo _csvTransactionDxo;
        private readonly IMediator _mediator;
        private readonly ICsvTransactionService _csvTransactionService;

        public SaveCsvCommandHandler(ITransactionRepository transactionRepository,
            ICsvTransactionDxo csvTransactionDxo,
            IMediator mediator,
            ICsvTransactionService csvTransactionService)
        {
            _transactionRepository = transactionRepository;
            _csvTransactionDxo = csvTransactionDxo;
            _mediator = mediator;
            _csvTransactionService = csvTransactionService;
        }

        public async Task<Result<Unit>> Handle(SaveCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _csvTransactionService.GetCsvTransactionModel(request)
                    .Bind(csvTransactionModel => _csvTransactionDxo.MapTransaction(csvTransactionModel))
                    .Bind(async transactions =>
                    {
                        var result = await _transactionRepository.CreateAsync(transactions);
                        return result.Bind(list => Result.Success(Unit.Value));
                    })
                    .Bind(async transactions => await _transactionRepository.SaveAsync())
                    .OnFailure(error =>
                        _mediator.Publish(SaveCsvFailedEvent.CreateInstance(error), cancellationToken));
            }
            catch (Exception e)
            {
                await _mediator.Publish(SaveCsvFailedEvent.CreateInstance(e.Message), cancellationToken);

                return Result.Failure<Unit>(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
