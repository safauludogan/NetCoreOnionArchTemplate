using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreOnionArchTemplate.Application.Abstractions.RedisCache;

namespace NetCoreOnionArchTemplate.Application.Behaviors
{
    public class RedisCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ILogger<RedisCacheBehavior<TRequest, TResponse>> _logger;
        public RedisCacheBehavior(IRedisCacheService redisCacheService, ILogger<RedisCacheBehavior<TRequest, TResponse>> logger)
        {
            _redisCacheService = redisCacheService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (request is ICacheableQuery query)
                {
                    var cacheKey = query.CacheKey;
                    var cacheTime = query.CacheTime;

                    var cachedData = await _redisCacheService.GetAsync<TResponse>(cacheKey);
                    if (cachedData is not null) return cachedData;

                    var response = await next();
                    if (response is not null)
                        await _redisCacheService.SetAsync(cacheKey, response, DateTime.Now.AddMinutes(cacheTime));

                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return await next();
        }
    }
}
