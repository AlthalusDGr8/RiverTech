namespace TechDemo.MoviesDb.Core.Caching
{
	/// <summary>
	/// Generic definition of cache manager
	/// </summary>
	public interface ICacheManager
	{
		/// <summary>
		/// Get something from Cache
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="key">The key under which this item is stored in cache</param>
		/// <returns>The object if found, otherwise null</returns>
		public T GetFromCache<T>(string key);

		/// <summary>
		/// Save Object in Cache
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="cacheKey">The cache key under which to store it</param>
		/// <param name="cacheItem">The object itself to cache</param>
		/// <param name="duration">The amount of time tis item is left in cache</param>
		public void SetInCache<T>(string cacheKey, T cacheItem, TimeSpan? duration = null);

		/// <summary>
		/// Deletes a key from cache
		/// </summary>
		/// <param name="key">the key to remove</param>
		void RemoveFromCache(string key);
	}
}
