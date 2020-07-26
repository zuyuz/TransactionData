using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using LanguageExt;
using MediatR;
using Moq;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Domain.Commands;
using TransactionData.Service.Dxos;
using TransactionData.Service.Interfaces.Services;
using TransactionData.Service.Services;
using Xunit;

namespace TransactionData.UnitTests.Service.Handlers.CommandHandlers
{
    public class SaveXmlCommandHandlerUnitTests
    {
        [Fact]
        public async Task Handle_WithValidParams_ShouldReturnSuccess()
        {
            #region Arrange

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var xmlTransactionDxoMock = new Mock<XmlTransactionDxo>();
            var mediatorMock = new Mock<IMediator>();
            var xmlTransactionServiceMock = new Mock<IXmlTransactionService>();
            await using var stream = new MemoryStream();
            var writer = XmlWriter.Create(stream);
            var saveXmlCommand = new SaveXmlCommand(stream);

            #endregion

            #region Act

            var sut = new SaveXmlCommandHandler(transactionRepositoryMock.Object, 
                xmlTransactionDxoMock.Object, 
                mediatorMock.Object, 
                xmlTransactionServiceMock.Object);
            var Result = await sut.Handle(saveXmlCommand, CancellationToken.None);

            #endregion

            #region Assert

            Assert.False(Result.IsDefault());

            #endregion
        }
    }
}
