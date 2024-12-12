using System.Runtime.Serialization;

namespace PaymentPointFinder.Core.Models;

[DataContract(Namespace = "payment-point-soap-service")]
public class PaymentPointSoapResponse
{
    [DataMember]
    public PaymentPoint Point { get; set; } = default!;
}