using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechDemo.MoviesDb.API.Models.Response;
using TechDemo.MoviesDb.Core.Caching;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.API.Controllers
{
	/// <summary>
	/// COntroller that helps set up stuff
	/// </summary>
	[Route("api/Genres")]
	[ApiController]
	public class GenresContoller : ControllerBase
	{		
		private readonly IEntityRepo<Genre> _entityRepo;
		private readonly ICacheManager _cacheManager;
		public GenresContoller(IEntityRepo<Genre> entityRepo, ICacheManager cacheManager)
		{
			_entityRepo = entityRepo;
			_cacheManager = cacheManager;
		}


		// <summary>
		/// Returns all the Genres in the system
		/// </summary>
		/// <returns></returns>
		[Route("")]
		[HttpGet]
		public async Task<IEnumerable<GenreResponseModel>> GetAllGenres(CancellationToken cancellationToken)
		{
			var alreadyCached = _cacheManager.GetFromCache<IEnumerable<GenreResponseModel>>("ALL_GENRES");
			if (alreadyCached == null)
			{
				var allGenres = await _entityRepo.GetAllAsync(cancellationToken);
				alreadyCached = allGenres.Select(x => new GenreResponseModel() { Id = x.Id, DisplayName = x.Description, Code = x.Code });
				_cacheManager.SetInCache("ALL_GENRES", alreadyCached);
			}

			return alreadyCached;
		}
	}
}
