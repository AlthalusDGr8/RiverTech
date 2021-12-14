using TechDemo.MoviesDb.Core.Caching;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Movies.Definitions;
using TechDemo.MoviesDb.Movies.Entities;
using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;
using TechDemo.MoviesDb.Movies.Exceptions;

namespace TechDemo.MoviesDb.Movies.Managers
{
	public class GenreManager : IGenreManager
	{
		private const string CACHEKEY_ALLGENRES = "ALL_GENRES";

		private readonly IEntityRepo<Genre> _entityRepo;
		private readonly ICacheManager _cacheManager;

		public GenreManager(IEntityRepo<Genre> entityRepo, ICacheManager cacheManager)
		{
			_entityRepo = entityRepo;
			_cacheManager = cacheManager;
		}

		/// <summary>
		/// Get all Genres
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<GenreDTO>> GetAll(CancellationToken cancellationToken)
		{
			var alreadyCached = _cacheManager.GetFromCache<IEnumerable<GenreDTO>>(CACHEKEY_ALLGENRES);
			if (alreadyCached == null)
			{
				var allGenres = await _entityRepo.GetAllAsync(cancellationToken);
				alreadyCached = allGenres.Select(x => new GenreDTO() { Id = x.Id, Description = x.Description, Code = x.Code });
				_cacheManager.SetInCache(CACHEKEY_ALLGENRES, alreadyCached);
			}

			return alreadyCached;

		}

		/// <summary>
		/// Get Genre By Code
		/// </summary>
		/// <param name="genreCode"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="GenreNotExistsException"></exception>
		public async Task<GenreDTO> GetByCode(string genreCode, CancellationToken cancellationToken)
		{
			var allSet = await GetAll(cancellationToken);
			var foundGenre = allSet.FirstOrDefault(x => x.Code.Equals(genreCode, StringComparison.OrdinalIgnoreCase));
			if (foundGenre == null)
				throw new GenreNotExistsException(genreCode, $"Genre with Code {genreCode} does not exist");

			return foundGenre;
		}

		/// <summary>
		/// Get Genre By Id
		/// </summary>
		/// <param name="genreId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="GenreNotExistsException"></exception>
		public async Task<GenreDTO> GetById(long genreId, CancellationToken cancellationToken)
		{
			var allSet = await GetAll(cancellationToken);
			var foundGenre = allSet.FirstOrDefault(x => x.Id == genreId);
			if (foundGenre == null)
				throw new GenreNotExistsException(genreId, $"Genre with id {genreId} does not exist");

			return foundGenre;
		}
	}
}
