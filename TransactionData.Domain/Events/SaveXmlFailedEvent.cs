using MediatR;
using TransactionData.Domain.Models;

namespace TransactionData.Domain.Events
{
    public class SaveXmlFailedEvent : INotification
    {
        public static SaveXmlFailedEvent CreateInstance(string error)
        {
            return new SaveXmlFailedEvent(error);
        }

        private SaveXmlFailedEvent(string error)
        {
            Error = error;
        }
        public string Error { get; }
    }
}