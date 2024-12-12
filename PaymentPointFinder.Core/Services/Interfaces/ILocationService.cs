using PaymentPointFinder.Core.Models;

namespace PaymentPointFinder.Core.Services.Interfaces
{
    public interface ILocationService
    {
        Task<List<PaymentPoint>> GetNearbyPoints(double lat, double lng, double radius);
    }
}
