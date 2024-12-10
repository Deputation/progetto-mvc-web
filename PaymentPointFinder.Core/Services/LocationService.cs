using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PaymentPointFinder.Core.Services
{
    public class LocationService : ILocationService
    {
        public LocationService() { }
        
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
        
        public Task<List<PaymentPoint>> GetNearbyPoints(double lat, double lng, double radius)
        {
            throw new NotImplementedException();
        }
    }
}
