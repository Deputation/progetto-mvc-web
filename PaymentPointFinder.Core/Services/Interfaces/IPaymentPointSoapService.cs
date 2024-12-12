using CoreWCF;
using PaymentPointFinder.Core.Models;

namespace PaymentPointFinder.Services.Interfaces
{
    [ServiceContract(Namespace = "payment-point-soap-service")]
    public interface IPaymentPointSoapService
    {
        [OperationContract]
        PaymentPointCollection GetAllPaymentPoints();

        [OperationContract]
        PaymentPointSoapResponse GetPaymentPointById(string id);
    }
}