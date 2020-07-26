using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Domain.Commands;
using TransactionData.Domain.Events;
using TransactionData.Service.Interfaces.Dxos;
using TransactionData.Service.Interfaces.Services;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.Handlers.CommandHandlers
{
    public class SaveCsvCommandHandler : IRequestHandler<SaveCsvCommand, EitherAsync<Error, Unit>>
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

        public Task<EitherAsync<Error, Unit>> Handle(SaveCsvCommand request, CancellationToken cancellationToken)
        {
            return _csvTransactionService.GetCsvTransactionModel(request)
                .Bind(csvTransactionModel => _csvTransactionDxo.MapTransaction(csvTransactionModel))
                .Bind(transactions => _transactionRepository.CreateAsync(transactions))
                .Bind(transactions => _transactionRepository.SaveAsync())
                .Match(unit1 => unit1,
                    error =>
                    {
                        _mediator.Publish(SaveCsvFailedEvent.CreateInstance(error.Message),
                            cancellationToken).ToUnit();
                        return EitherAsync<Error, Unit>.Left(error);
                    });
        }
    }
}
