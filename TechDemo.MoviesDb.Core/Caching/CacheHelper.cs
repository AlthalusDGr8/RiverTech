namespace TechDemo.MoviesDb.Core.Caching
{
	public static class CacheHelper
	{
		/// <summary>
		/// Manages caching of items through double locking system
		/// </summary>
		/// <typeparam name="TResult">The items that will eventually be cached</typeparam>
		/// <param name="cacheManager">The cache manager to use</param>
		/// <param name="fetchItemsFunction">The funciton which contians what needs to be cached</param>
		/// <param name="cacheManagerFunction">TThe funciton of how the cache manager should cache these items</param>
		/// <param name="cacheKey">The cache key to use for checking for the object</param>
		/// <param name="lockObject">The lock object to use for ensureng single fetch</param>
		public static TResult BuildCacheAndFetchItems<TResult>(ICacheManager cacheManager, Func<TResult> fetchAndCacheFunction, string cacheKey, object lockObject) where TResult : new()
		{
			var cacheItems = cacheManager.GetFromCache<TResult>(cacheKey);
			if (EqualityComparer<TResult>.Default.Equals(cacheItems, default))
				lock (lockObject)
				{
					cacheItems = cacheManager.GetFromCache<TResult>(cacheKey);
					if (EqualityComparer<TResult>.Default.Equals(cacheItems, default))
						cacheItems = fetchAndCacheFunction.Invoke();
				}

			return cacheItems;
		}

	}
}
