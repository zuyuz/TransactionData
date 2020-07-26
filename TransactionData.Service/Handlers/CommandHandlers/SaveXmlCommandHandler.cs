using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CsvHelper;
using LanguageExt;
using LanguageExt.Common;
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
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TransactionData.Service.Services
{
    public class SaveXmlCommandHandler : IRequestHandler<SaveXmlCommand, EitherAsync<Error, Unit>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IXmlTransactionDxo _xmlTransactionDxo;
        private readonly IMediator _mediator;
        private readonly IXmlTransactionService _xmlTransactionService;

        public SaveXmlCommandHandler(ITransactionRepository transactionRepository,
            IXmlTransactionDxo xmlTransactionDxo,
            IMediator mediator,
            IXmlTransactionService xmlTransactionService)
        {
            _transactionRepository = transactionRepository;
            _xmlTransactionDxo = xmlTransactionDxo;
            _mediator = mediator;
            _xmlTransactionService = xmlTransactionService;
        }

        public Task<EitherAsync<Error, Unit>> Handle(SaveXmlCommand request, CancellationToken cancellationToken)
        {
            return _xmlTransactionService.GetXmlTransactionModel(request)
                .Bind(xmlTransactionModel => _xmlTransactionDxo.MapTransaction(xmlTransactionModel))
                .Bind(transactions =>  _transactionRepository.CreateAsync(transactions))
                .Bind(transactions => _transactionRepository.SaveAsync())
                .Match(EitherAsync<Error, Unit>.Right,
                    error =>
                    {
                        _mediator.Publish(SaveXmlFailedEvent.CreateInstance(error.Message),
                            cancellationToken).ToUnit();
                        return EitherAsync<Error, Unit>.Left(error);
                    });
        }
    }
}
