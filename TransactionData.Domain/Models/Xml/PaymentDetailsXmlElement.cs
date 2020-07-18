using System;
using System.Xml.Serialization;

namespace TransactionData.Domain.Models
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class PaymentDetailsXmlElement
    {
        [XmlElement("Amount")]
        public decimal Amount { get; set; }
        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}