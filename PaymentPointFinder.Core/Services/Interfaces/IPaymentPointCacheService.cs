using PaymentPointFinder.Core.Models;

namespace PaymentPointFinder.Core.Services.Interfaces;

public interface IPaymentPointCacheService
{
    List<PaymentPoint>? GetPoints();
    void SetPoints(List<PaymentPoint> points);
    bool NeedsRefresh();
}