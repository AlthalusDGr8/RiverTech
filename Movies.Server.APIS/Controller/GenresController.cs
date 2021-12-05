using Microsoft.AspNetCore.Mvc;
using Movies.CentralCore.Repository;
using Movies.Entities.Movie.DataModels.Genres;
using Movies.Server.APIS.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Movies.Server.APIS.Controller
{
	/// <summary>
	/// COntroller that helps set up stuff
	/// </summary>
	[Route("api/Genres")]
	[ApiController]
	public class GenresController : ControllerBase
	{
		private readonly ILookupRepository<GenreDTO> _lookupRepository;

		public GenresController(ILookupRepository<GenreDTO> lookupRepository)
		{
			_lookupRepository = lookupRepository;
		}


		/// <summary>
		/// Returns all the Genres in the system
		/// </summary>
		/// <returns></returns>
		[Route("")]
		[HttpGet]
		public Task<IEnumerable<GenreResponseModel>> GetAllGenres() => Task.FromResult(_lookupRepository.GetAll().Select(x => new GenreResponseModel() { Id = x.Id, DisplayName = x.Description, Code = x.Code }));


	}
}
