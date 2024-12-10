using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentPointFinder.Core.Services
{
    public class LocationService : ILocationService
    {
        public LocationService() { }

        public Task<List<PaymentPoint>> GetNearbyPoints(double lat, double lng, double radius)
        {
            throw new NotImplementedException();
        }
    }
}
