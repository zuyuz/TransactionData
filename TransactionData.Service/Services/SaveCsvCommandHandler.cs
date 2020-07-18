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
using TransactionData.Domain.ExtensionMethods;
using TransactionData.Domain.Models;
using TransactionData.Service.CsvMap;
using TransactionData.Service.Dxos;

namespace TransactionData.Service.Services
{
    public class SaveCsvCommandHandler : IRequestHandler<SaveCsvCommand, Result<Unit, string>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly CsvTransactionDxo _csvTransactionDxo;

        public SaveCsvCommandHandler(ITransactionRepository transactionRepository,
            CsvTransactionDxo csvTransactionDxo)
        {
            _transactionRepository = transactionRepository;
            _csvTransactionDxo = csvTransactionDxo;
        }

        public async Task<Result<Unit, string>> Handle(SaveCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using TextReader reader = new StreamReader(request.Stream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                csv.Configuration.RegisterClassMap<CsvTransactionMap>();
                var csvTransactions = csv.GetRecords<CsvTransactionModel>().ToList();

                return await (await _csvTransactionDxo.MapTransaction(csvTransactions)
                    .Bind(async transactions =>
                    {
                        var result = await _transactionRepository.CreateAsync(transactions);
                        return result.Bind(list => Result.Success(Unit.Value));
                    }))
                    .Match(async r =>
                        {
                            var result = await _transactionRepository.SaveAsync();
                            return result.Bind(Result.Success<Unit, string>);
                        }, 
                        s1 => Task.FromResult(Result.Failure<Unit, string>(s1)));
                
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Failure<Unit, string>(e.Message);
            }
            catch (FieldValidationException e)
            {
                return Result.Failure<Unit, string>(e.Message);
            }
            catch (CsvHelperException e)
            {
                return Result.Failure<Unit, string>(e.Message);
            }
            catch (Exception e)
            {
                return Result.Failure<Unit, string>(e.Message);
            }
        }
    }
}
