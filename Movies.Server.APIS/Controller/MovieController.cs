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

			var runTime = new TimeSpan(0, result.RunTimeInMinutes, 0);
			return new MovieResponseModel() { Id = result.Id, 
				Description = result.Description, 
				Name = result.Name, 
				Rating = result.Rating,
				ImgUrl = result.ImgUrl,	
				RunTime = $"{runTime.Hours} hrs and {runTime.Minutes} minutes",
				Genres = result.Genres				
			};
		}

		[HttpPost]		
		public async Task<Guid> CreateNew([FromBody] NewMovieRequestModel newMovieRequestModel)
		{
			var newId = Guid.NewGuid();

			await _clusterClient.Set(newId, new NewMovieDetailsDTO()
			{
				Name = newMovieRequestModel.Name,
				Description = newMovieRequestModel.Description,
				Genres = newMovieRequestModel.Genres,
				ImgUrl = newMovieRequestModel.ImgUrl,
				Rating = newMovieRequestModel.Rating,
				RunTimeInMinutes = newMovieRequestModel.RunTimeInMinutes
			});
			
			return newId;
		}

		[HttpPatch]
		[Route("{id}")]
		public async Task UpdateExisting(Guid id, [FromBody] NewMovieRequestModel newMovieRequestModel)
		{			
			await _clusterClient.UpdateMovie(id, new NewMovieDetailsDTO()
			{
				Name = newMovieRequestModel.Name,
				Description = newMovieRequestModel.Description,
				Genres = newMovieRequestModel.Genres,
				RunTimeInMinutes = newMovieRequestModel.RunTimeInMinutes,
				ImgUrl=newMovieRequestModel.ImgUrl,
				Rating =newMovieRequestModel.Rating,
			});			
		}
	}
}
