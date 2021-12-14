using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechDemo.MoviesDb.API.Models.Response;
using TechDemo.MoviesDb.Movies.Definitions;

namespace TechDemo.MoviesDb.API.Controllers
{
	/// <summary>
	/// COntroller that helps set up stuff
	/// </summary>
	[Route("api/genres")]
	[ApiController]
	public class GenresContoller : ControllerBase
	{	
		private readonly IGenreManager _genreManager;		
		public GenresContoller(IGenreManager genreManager)
		{
			_genreManager = genreManager;			
		}


		/// <summary>
		/// Returns all the Genres in the system
		/// </summary>
		/// <returns></returns>
		[Route("")]
		[HttpGet]
		public async Task<IEnumerable<GenreResponseModel>> GetAllGenres(CancellationToken cancellationToken)
		{
			var allGenres =  await _genreManager.GetAll(cancellationToken);
			var genreResponseModels = new List<GenreResponseModel>(0);
			foreach (var genre in allGenres)
			{
				genreResponseModels.Add(GenreResponseModel.ConverFromGenreDTO(genre));
			}

			return genreResponseModels;			
		}

		/// <summary>
		/// Returns a Genre with a specifcied id
		/// </summary>
		/// <returns></returns>
		[Route("{id:long}")]
		[HttpGet]
		public async Task<GenreResponseModel> GetById([FromRoute]int id, CancellationToken cancellationToken)
		{
			var result = await _genreManager.GetById(id, cancellationToken);
			return GenreResponseModel.ConverFromGenreDTO(result);		
		}
	}
}
