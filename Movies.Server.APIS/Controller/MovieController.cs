using Microsoft.AspNetCore.Mvc;
using Movies.Entities.DataModels;
using Movies.Server.APIS.Models.Request;
using Movies.Server.APIS.Models.Response;
using Orleans;
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
		private readonly IClusterClient _clusterClient;

		/// <summary>
		/// Consstructor
		/// </summary>		
		public MovieController(IClusterClient clusterClient)
		{
			_clusterClient = clusterClient;
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<MovieResponseModel> GetById(Guid id)
		{
			var result = _clusterClient.GetGrain<_MovieDataModel>(id);
			return new MovieResponseModel() { Id = result.Id, Description = result.Description, Name = result.Name, ReleaseDate = result.ReleaseDate, Synopisis = result.Synopisis };
		}

		[HttpPost]		
		public async Task<Guid> CreateNew([FromBody] NewMovieRequestModel newMovieRequestModel)
		{
			var result = _clusterClient.GetGrain<_MovieDataModel>(Guid.Empty);
			
			return await result.CreateNew(new NewMovieDTO() { 
				Name = newMovieRequestModel.Name, 
				Description = newMovieRequestModel.Description, 
				ReleaseDate = newMovieRequestModel.ReleaseDate, 
				Synopsis = newMovieRequestModel.Synopsis});			
		}	
	}
}
