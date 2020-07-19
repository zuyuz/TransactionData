using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CSharpFunctionalExtensions;
using MediatR;

namespace TransactionData.Domain.Commands
{
    public class SaveCsvCommand : IRequest<Result<Unit>>
    {
        public SaveCsvCommand(Stream stream)
        {
            Stream = stream;
        }

        public static SaveCsvCommand CreateInstance(Stream stream)
        {
            return new SaveCsvCommand(stream);
        }

        public Stream Stream { get; set; }
    }
}
