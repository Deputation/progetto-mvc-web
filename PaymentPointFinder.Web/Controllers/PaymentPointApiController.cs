using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentPointFinder.Core.Services.Interfaces;

namespace PaymentPointFinder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPointApiController : ControllerBase
    {
        private readonly IPaymentPointRestService _paymentPointRestService;
        private readonly ILocationService _locationService;
        public PaymentPointApiController(IPaymentPointRestService paymentPointRestService, ILocationService locationService)
        {
            _paymentPointRestService = paymentPointRestService ?? throw new ArgumentException(nameof(paymentPointRestService));
            _locationService = locationService ?? throw new ArgumentException(nameof(locationService));
        }

        [HttpGet("nearby-points")]
        public async Task<IActionResult> GetNearbyPoints(double lat, double lng, double radius)
        {
            try
            {
                var points = await _locationService.GetNearbyPoints(lat, lng, radius);
                return Ok(points);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message,
                });
            }
        }
    }
}
