using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TransactionData.Domain.Events;

namespace TransactionData.Service.Handlers.EventHandlers
{
    public class SaveCsvFailedHandler : INotificationHandler<SaveCsvFailedEvent>
    {
        private readonly ILogger<SaveCsvFailedHandler> _logger;

        public SaveCsvFailedHandler(ILogger<SaveCsvFailedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaveCsvFailedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogError(notification.Error);

            return Task.CompletedTask;
        }
    }
}