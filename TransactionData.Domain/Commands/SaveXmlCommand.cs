using System.IO;
using CSharpFunctionalExtensions;
using MediatR;

namespace TransactionData.Domain.Commands
{
    public class SaveXmlCommand : IRequest<Result<Unit>>
    {
        public SaveXmlCommand(Stream stream)
        {
            Stream = stream;
        }

        public static SaveXmlCommand CreateInstance(Stream stream)
        {
            return new SaveXmlCommand(stream);
        }

        public Stream Stream { get; set; }
    }
}