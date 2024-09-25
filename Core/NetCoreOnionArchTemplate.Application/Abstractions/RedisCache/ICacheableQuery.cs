

namespace NetCoreOnionArchTemplate.Application.Abstractions.RedisCache
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        double CacheTime { get; }
    }
}
