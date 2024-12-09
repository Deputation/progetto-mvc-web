using Microsoft.AspNetCore.Mvc;
using PaymentPointFinder.Web.Models;
using PaymentPointFinder.Web.Services.Interfaces;

namespace PaymentPointFinder.Web.Controllers;

public class MapController : Controller
{
    private readonly IPaymentPointRestService _paymentPointRestService;
    
    public MapController(IPaymentPointRestService paymentPointRestService)
    {
        _paymentPointRestService = paymentPointRestService ?? 
                                   throw new ArgumentNullException(nameof(paymentPointRestService));    
    }
    
    public async Task<IActionResult> Index()
    {
        var model = new MapViewModel()
        {
            Points = await _paymentPointRestService.FetchPaymentPoints(),
            DefaultLatitude = 45.4641,
            DefaultLongitude = 9.1919,
            DefaultZoom = 100
        };
        
        return View(model);
    }
}