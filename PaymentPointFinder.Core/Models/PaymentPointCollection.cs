using System.Runtime.Serialization;

namespace PaymentPointFinder.Core.Models;

[DataContract(Namespace = "payment-point-soap-service")]
public class PaymentPointCollection
{
    [DataMember]
    public List<PaymentPoint> Points { get; set; } = new List<PaymentPoint>();
}