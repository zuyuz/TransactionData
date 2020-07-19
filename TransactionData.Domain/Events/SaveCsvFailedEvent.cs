using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MediatR;
using TransactionData.Domain.Models;

namespace TransactionData.Domain.Events
{
    public class SaveCsvFailedEvent : INotification
    {
        public static SaveCsvFailedEvent CreateInstance(string error)
        {
            return new SaveCsvFailedEvent(error);
        }

        private SaveCsvFailedEvent(string error)
        {
            Error = error;
        }
        public string Error { get; }
    }
}