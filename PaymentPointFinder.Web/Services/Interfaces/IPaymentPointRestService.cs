using PaymentPointFinder.Core.Models;

namespace PaymentPointFinder.Web.Services.Interfaces;

public interface IPaymentPointRestService
{
    Task<List<PaymentPoint>> FetchPaymentPoints();
    
    Task<PaymentPoint?> FetchPointById(string id);
}