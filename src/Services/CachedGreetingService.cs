using System;

namespace DIPoC.Services
{
    using System.Runtime.Caching;
    
    public class CachedGreetingService : IGreetingService
    {
        private const string CacheKey = "CachedGreetings";
        private readonly IGreetingService _greetingService;
        private readonly ObjectCache _cache;

        public CachedGreetingService(IGreetingService greetingService, ObjectCache cache)
        {
            _greetingService = greetingService;
            _cache = cache;
        }
        public string GetGreetings()
        {
            if (!_cache.Contains(CacheKey))
            {
                var greetings = _greetingService.GetGreetings();
                _cache.Set(CacheKey, greetings, DateTimeOffset.UtcNow.AddMinutes(5));
            }
            return _cache.Get("CachedGreetings") as string;
        }
    }
}