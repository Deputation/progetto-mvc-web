using PaymentPointFinder.Core.Models;

namespace PaymentPointFinder.Web.Models;

public class MapViewModel
{
    public List<PaymentPoint> Points { get; set; }
    public double DefaultLatitude { get; set;  }
    public double DefaultLongitude { get; set;  }
    public int DefaultZoom { get; set;  }
}