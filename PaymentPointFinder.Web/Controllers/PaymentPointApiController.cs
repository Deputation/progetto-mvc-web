﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentPointFinder.Core.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNearbyPoints([Range(-90, 90)] double lat, [Range(-180, 180)] double lng, [Range(0.1, 100)] double radius)
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
