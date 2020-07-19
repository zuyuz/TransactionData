using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
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

namespace TransactionData.Service.Services
{
    public class SaveXmlCommandHandler : IRequestHandler<SaveXmlCommand, Result<Unit>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly XmlTransactionDxo _xmlTransactionDxo;
        private readonly IMediator _mediator;

        public SaveXmlCommandHandler(ITransactionRepository transactionRepository,
            XmlTransactionDxo xmlTransactionDxo,
            IMediator mediator)
        {
            _transactionRepository = transactionRepository;
            _xmlTransactionDxo = xmlTransactionDxo;
            _mediator = mediator;
        }

        public async Task<Result<Unit>> Handle(SaveXmlCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await request.GetXmlTransactionModel()
                    .Bind(xmlTransactionModel => _xmlTransactionDxo.MapTransaction(xmlTransactionModel)
                        .Bind(async transactions =>
                        {
                            var result = await _transactionRepository.CreateAsync(transactions);
                            return result.Bind(list => Result.Success(Unit.Value));
                        })
                        .Bind(async transactions => await _transactionRepository.SaveAsync()))
                    .OnFailure(error =>
                        _mediator.Publish(SaveXmlFailedEvent.CreateInstance(error), cancellationToken));
                
            }
            catch (Exception e)
            {
                await _mediator.Publish(SaveXmlFailedEvent.CreateInstance(e.Message), cancellationToken);

                return Result.Failure<Unit>(e.Message);
            }
        }
    }
}
