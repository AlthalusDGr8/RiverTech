using Microsoft.AspNetCore.Mvc;
using Movies.Entities.DataModels;
using Movies.Entities.Movie.Definition;
using Movies.Server.APIS.Models.Request;
using Movies.Server.APIS.Models.Response;
using System;
using System.Threading.Tasks;

namespace Movies.Server.APIS.Controller
{
	/// <summary>
	/// COntroller that helps set up stuff
	/// </summary>
	[Route("api/Movies")]
	[ApiController]
	public class MovieController : ControllerBase
	{
		private readonly IMovieGrainClient _clusterClient;

		/// <summary>
		/// Consstructor
		/// </summary>		
		public MovieController(IMovieGrainClient clusterClient)
		{
			_clusterClient = clusterClient;
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<MovieResponseModel> GetById(Guid id)
		{
			var result = await _clusterClient.Get(id);			
			return new MovieResponseModel() { Id = result.Id, Description = result.Description, Name = result.Name, ReleaseDate = result.ReleaseDate, Synopisis = result.Synopisis };
		}

		[HttpPost]		
		public async Task<Guid> CreateNew([FromBody] NewMovieRequestModel newMovieRequestModel)
		{
			var newId = Guid.NewGuid();

			await _clusterClient.Set(newId, new NewMovieDTO()
			{
				Name = newMovieRequestModel.Name,
				Description = newMovieRequestModel.Description,
				ReleaseDate = newMovieRequestModel.ReleaseDate,
				Synopsis = newMovieRequestModel.Synopsis
			});
			
			return newId;
		}	
	}
}
