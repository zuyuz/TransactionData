using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TransactionData.Domain.Events;

namespace TransactionData.Service.Handlers.EventHandlers
{
    public class SaveXmlFailedHandler : INotificationHandler<SaveXmlFailedEvent>
    {
        private readonly ILogger<SaveXmlFailedHandler> _logger;

        public SaveXmlFailedHandler(ILogger<SaveXmlFailedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaveXmlFailedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogError(notification.Error);

            return Task.CompletedTask;
        }
    }
}
