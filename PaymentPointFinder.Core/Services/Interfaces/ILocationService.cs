using PaymentPointFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentPointFinder.Core.Services.Interfaces
{
    public interface ILocationService
    {
        Task<List<PaymentPoint>> GetNearbyPoints(double lat, double lng, double radius);
    }
}
