using System.Runtime.Caching;
using TechDemo.MoviesDb.Core.Caching;
namespace TechDemo.MoviesDb.Caching.Memory
{
	public class MemoryCacheManager : ICacheManager
	{
		/// <summary>
		/// Mem Cache store
		/// </summary>
		private MemoryCache Cache { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public MemoryCacheManager()
		{
			Cache = MemoryCache.Default;
		}

		/// <summary>
		/// Get items from cache
		/// </summary>
		/// <typeparam name="T">The type it should be in</typeparam>
		/// <param name="key">The key Under what this item is stored</param>
		/// <returns></returns>
		public T GetFromCache<T>(string key) => (T)Cache.Get(key);

		/// <summary>
		/// Set item in cache
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="cacheKey">The cache key udner which to store it</param>
		/// <param name="cacheItem">The item that needs to be cached</param>
		/// <param name="duration">The duarion how long its kept in cache</param>
		public void SetInCache<T>(string cacheKey, T cacheItem, TimeSpan? duration = null)
		{
			// If no duration specified then set to 24 hrs
			if (duration == null)
				duration = new TimeSpan(1, 0, 0, 0);
			Cache.Set(cacheKey, cacheItem, new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.Now.Add(duration.Value)) });
		}

		/// <summary>
		/// remove item from cache
		/// </summary>
		/// <param name="key"></param>
		public void RemoveFromCache(string key) => Cache.Remove(key);

	}
}