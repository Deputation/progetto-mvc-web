using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Core.Services.Interfaces;

namespace PaymentPointFinder.Core.Services;

public class PaymentPointRestService : IPaymentPointRestService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PaymentPointRestService> _logger;
    private readonly string _baseUrl;
    private readonly IPaymentPointCacheService _paymentPointCacheService;

    public PaymentPointRestService(HttpClient httpClient,
        ILogger<PaymentPointRestService> logger,
        IPaymentPointCacheService paymentPointCacheService,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _paymentPointCacheService = paymentPointCacheService ??
                                    throw new ArgumentNullException(nameof(paymentPointCacheService));
        _baseUrl = configuration["PaymentPointsApi:BaseUrl"] ??
                   throw new ArgumentException("PaymentPointsApi:BaseUrl not configured");
    }

    private string RenameJsonField(string jsonArrayString, string oldFieldName, string newFieldName)
    {
        using var document = JsonDocument.Parse(jsonArrayString);
        var writer = new MemoryStream();
        using (var jsonWriter = new Utf8JsonWriter(writer))
        {
            jsonWriter.WriteStartArray();
            foreach (var element in document.RootElement.EnumerateArray())
            {
                jsonWriter.WriteStartObject();
                foreach (var property in element.EnumerateObject())
                {
                    var name = property.Name == oldFieldName ? newFieldName : property.Name;
                    jsonWriter.WritePropertyName(name);
                    property.Value.WriteTo(jsonWriter);
                }

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        return Encoding.UTF8.GetString(writer.ToArray());
    }

    private async Task<List<PaymentPoint>> FetchPaymentPointsFromApi()
    {
        var response = await _httpClient.GetAsync(_baseUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // This resolves a naming collision.
        content = RenameJsonField(content, "XWGS84", "XWGS84String");

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = false
        };

        var points = JsonSerializer.Deserialize<List<PaymentPoint>>(content, options);
        
        return points ?? throw new HttpRequestException("Could not source API data");
    }

    public async Task<List<PaymentPoint>> FetchPaymentPoints()
    {
        try
        {
            if (!_paymentPointCacheService.NeedsRefresh())
            {
                return _paymentPointCacheService.GetPoints()!;
            }

            var points = await FetchPaymentPointsFromApi();
            _paymentPointCacheService.SetPoints(points);
            return points;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching payment points from API");
            return new List<PaymentPoint>();
        }
    }

    public async Task<PaymentPoint?> FetchPointById(string id)
    {
        try
        {
            var points = await FetchPaymentPoints();
            return points.FirstOrDefault(p => p.IDEXT == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching payment point with ID {id}", id);
            return null;
        }
    }
}