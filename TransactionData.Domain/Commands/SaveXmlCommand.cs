using System.IO;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace TransactionData.Domain.Commands
{
    public class SaveXmlCommand : IRequest<EitherAsync<Error, Unit>>
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