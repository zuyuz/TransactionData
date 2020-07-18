using System.Xml.Serialization;

namespace TransactionData.Domain.Enums
{
    public enum XmlTransactionStatusEnum
    {
        [XmlEnum(Name = "Approved")]
        Approved,
        [XmlEnum(Name = "Rejected")]
        Rejected,
        [XmlEnum(Name = "Done")]
        Done
    }
}