using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TransactionData.Domain.Models
{
    [Serializable()]
    [XmlRoot("Transactions")]
    public class XmlTransactionModel
    {
        [XmlElement("Transaction")]
        public virtual List<TransactionXmlElement> Transactions { get; set; }
    }
}