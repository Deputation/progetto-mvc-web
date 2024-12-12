using CoreWCF;
using Microsoft.Extensions.Logging;
using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Core.Services.Interfaces;
using PaymentPointFinder.Services.Interfaces;

namespace PaymentPointFinder.Core.Services;

public class PaymentPointSoapService : IPaymentPointSoapService
{
    private readonly IPaymentPointRestService _restService;
    private readonly ILogger<PaymentPointSoapService> _logger;

    public PaymentPointSoapService(
        IPaymentPointRestService restService,
        ILogger<PaymentPointSoapService> logger)
    {
        _restService = restService ?? throw new ArgumentNullException(nameof(restService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public PaymentPointCollection GetAllPaymentPoints()
    {
        try
        {
            var points = _restService.FetchPaymentPoints().Result;
            return new PaymentPointCollection { Points = points };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all payment points");
            throw new FaultException("An error occurred while processing your request");
        }
    }

    public PaymentPointSoapResponse GetPaymentPointById(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new FaultException("Invalid ID provided");
            }

            var point = _restService.FetchPointById(id).Result;

            if (point == null)
            {
                throw new FaultException("ID does not exist");
            }

            return new PaymentPointSoapResponse
            {
                Point = point
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching payment point {Id}", id);
            throw new FaultException("An error occurred while processing your request");
        }
    }
}