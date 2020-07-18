using System;
using System.Xml.Serialization;
using TransactionData.Domain.Enums;

namespace TransactionData.Domain.Models
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class TransactionXmlElement
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDateForXml // format: 2011-11-11T15:05:46.4733406+01:00
        {
            get => TransactionDate.ToString("o");  // o = yyyy-MM-ddTHH:mm:ss.fffffffzzz
            set => TransactionDate = DateTimeOffset.Parse(value);
        }
        [XmlIgnore]
        public DateTimeOffset TransactionDate { get; set; }

        [XmlElement("PaymentDetails")]
        public virtual PaymentDetailsXmlElement PaymentDetails { get; set; }
        public XmlTransactionStatusEnum Status { get; set; }

    }
}