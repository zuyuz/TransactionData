using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace TransactionData.Domain.Commands
{
    public class SaveCsvCommand : IRequest<EitherAsync<Error, Unit>>
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
