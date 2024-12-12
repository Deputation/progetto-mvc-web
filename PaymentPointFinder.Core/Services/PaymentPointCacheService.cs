using Microsoft.Extensions.Caching.Memory;
using PaymentPointFinder.Core.Models;
using PaymentPointFinder.Core.Services.Interfaces;

namespace PaymentPointFinder.Core.Services;

public class PaymentPointCacheService : IPaymentPointCacheService
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "PaymentPoints";
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public PaymentPointCacheService(IMemoryCache cache)
    {
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
    }

    public List<PaymentPoint>? GetPoints()
    {
        return _cache.Get<List<PaymentPoint>>(CacheKey);
    }

    public void SetPoints(List<PaymentPoint> points)
    {
        _cache.Set(CacheKey, points, _cacheOptions);
    }

    public bool NeedsRefresh()
    {
        return GetPoints() == null;
    }
}