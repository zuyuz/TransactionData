using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
    public class SaveCsvCommandHandlerUnitTests
    {
        [Fact]
        public async Task Handle_WithValidParams_ShouldReturnSuccess()
        {
            #region Arrange

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var csvTransactionDxoMock = new Mock<CsvTransactionDxo>();
            var mediatorMock = new Mock<IMediator>();
            var csvTransactionServiceMock = new Mock<ICsvTransactionService>();
            await using var stream = new MemoryStream();
            var saveCsvCommand = new SaveCsvCommand(stream);

            #endregion

            #region Act

            var sut = new SaveCsvCommandHandler(transactionRepositoryMock.Object,
                csvTransactionDxoMock.Object,
                mediatorMock.Object,
                csvTransactionServiceMock.Object);
            var result = await sut.Handle(saveCsvCommand, CancellationToken.None);

            #endregion

            #region Assert

            Assert.True(result.IsSuccess);

            #endregion
        }
    }
}