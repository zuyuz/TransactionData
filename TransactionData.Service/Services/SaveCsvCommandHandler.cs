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
using TransactionData.Domain.Commands;
using TransactionData.Domain.Models;
using TransactionData.Service.Dxos;

namespace TransactionData.Service.Services
{
    public class SaveCsvCommandHandler : IRequestHandler<SaveCsvCommand, Result>
    {
        public async Task<Result> Handle(SaveCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using TextReader reader = new StreamReader(request.Stream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                csv.Configuration.RegisterClassMap<CsvTransactionMap>();
                var transactions = csv.GetRecords<CsvTransactionModel>().ToList();

                return Result.Success();
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Failure(e.Message);
            }
            catch (FieldValidationException e)
            {
                return Result.Failure(e.Message);
            }
            catch (CsvHelperException e)
            {
                return Result.Failure(e.Message);
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }
    }
}
