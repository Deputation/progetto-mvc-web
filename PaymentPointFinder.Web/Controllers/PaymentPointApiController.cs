using Microsoft.AspNetCore.Mvc;
using PaymentPointFinder.Core.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Web.Models.Api;

namespace PaymentPointFinder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPointApiController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public PaymentPointApiController(ILocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentException(nameof(locationService));
        }

        [HttpGet("nearby-points")]
        [ProducesResponseType(typeof(List<PaymentPoint>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNearbyPoints([Range(-90, 90)] double lat, [Range(-180, 180)] double lng, [Range(0.1, 100)] double radius)
        {
            try
            {
                var points = await _locationService.GetNearbyPoints(lat, lng, radius);
                return Ok(points);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = ex.Message,
                });
            }
        }
    }
}
