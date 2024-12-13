using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Core.Services.Interfaces;

namespace PaymentPointFinder.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IPaymentPointRestService _paymentPointRestService;
        public LocationService(IPaymentPointRestService paymentPointRestService) 
        {
            _paymentPointRestService = paymentPointRestService ?? throw new ArgumentException(nameof(paymentPointRestService));
        }
        
        private static double CalculateDistance(double lat1, double lng1, double lat2, double lng2)
        {
            const double earthRadius = 6371; // in kilometers
            
            var lat1Rad = lat1 * Math.PI / 180;
            var lat2Rad = lat2 * Math.PI / 180;
            var lng1Rad = lng1 * Math.PI / 180;
            var lng2Rad = lng2 * Math.PI / 180;
            
            var deltaLat = lat2Rad - lat1Rad;
            var deltaLng = lng2Rad - lng1Rad;

            // the result is in kilometers
            return 2 * earthRadius *
                   Math.Asin(Math.Sqrt(
                           (1 - Math.Cos(deltaLat) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * 
                               (1 - Math.Cos(deltaLng))) / 2.0));
        }
        
        public async Task<List<PaymentPoint>> GetNearbyPoints(double lat, double lng, double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be greater than zero", nameof(radius));

            if (lat < -90 || lat > 90)
                throw new ArgumentException("Latitude must be between -90 and 90 degrees", nameof(lat));

            if (lng < -180 || lng > 180)
                throw new ArgumentException("Longitude must be between -180 and 180 degrees", nameof(lng));

            var points = await _paymentPointRestService.FetchPaymentPoints();
    
            return points
                .Select(point => new 
                { 
                    Point = point, 
                    Distance = CalculateDistance(point.YWGS84, point.xWGS84, lat, lng) 
                })
                .Where(x => x.Distance <= radius)
                .OrderBy(x => x.Distance)
                .Select(x => x.Point)
                .ToList();
        }
    }
}
