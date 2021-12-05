using Movies.CentralCore.Caching;

namespace Movies.CentralCore.Repository
{
	/// <summary>
	/// Generic repo for all lookup info
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface ILookupRepository<TEntity> where TEntity : ILookupEntity
	{
		/// <summary>
		/// Returns all entities
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TEntity> GetAll();
		
		/// <summary>
		/// Returns an entity by Id
		/// </summary>
		/// <returns></returns>
		public TEntity GetById(int id);

		/// <summary>
		/// Returns an entity by Id
		/// </summary>
		/// <returns></returns>
		public TEntity GetByCode(string code);
	}

	public abstract class GenericLookupRepository<TEntity> : ILookupRepository<TEntity> where TEntity : ILookupEntity	
	{
		public readonly ICacheManager _cacheManager;
		public abstract string AllElementsCacheKey { get; }

		private static object _lockObject = new object();

		public GenericLookupRepository(ICacheManager cacheManager)
		{
			_cacheManager = cacheManager;
		}
		
		public abstract List<TEntity> FetchandCacheItems(string cacheKey);

		public IEnumerable<TEntity> GetAll() =>
			// Use the generic helper that will take care of locking while cache is being filled
			CacheHelper.BuildCacheAndFetchItems(_cacheManager, () => FetchandCacheItems(AllElementsCacheKey), AllElementsCacheKey, _lockObject);

		/// <summary>
		/// Fine element by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public TEntity GetById(int id) => GetAll().SingleOrDefault(x => x.Id == id);
		
		/// <summary>
		/// Find element by code
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public TEntity GetByCode(string code) => GetAll().SingleOrDefault(x => x.Code == code);
	}

}
