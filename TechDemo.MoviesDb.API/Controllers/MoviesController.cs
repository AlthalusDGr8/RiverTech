using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechDemo.MoviesDb.API.Models.Request;
using TechDemo.MoviesDb.API.Models.Response;
using TechDemo.MoviesDb.Movies.Definitions;

namespace TechDemo.MoviesDb.API.Controllers
{
	/// <summary>
	/// Movies controller
	/// </summary>
	[Route("api/movies")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		public readonly IUserMovieRatingManager _userMovieRatingManager;
		public readonly IMovieManger _movieManger;
		public MoviesController(IUserMovieRatingManager userMovieRatingManager, IMovieManger movieManger)
		{
			_userMovieRatingManager = userMovieRatingManager;
			_movieManger = movieManger;
		}


		/// <summary>
		/// Returns a list of movies ordered  by id
		/// </summary>
		/// <param name="skip">Number of rows to skip (for paging)</param>
		/// <param name="take">Number of rows to take (for paging)</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("")]
		[HttpGet]
		public async Task<IEnumerable<MovieResponseModel>> ListMovies([FromQuery] int? skip, [FromQuery]int? take,  CancellationToken cancellationToken)
		{
			var response = new List<MovieResponseModel>(0);
			var result = await _movieManger.GetMovies(skip, take, cancellationToken);
			foreach (var item in result)
			{
				response.Add(MovieResponseModel.ConvertToMovieResponseModel(item));
			}

			return response;
		}

		/// <summary>
		/// Submits a new movie to the store
		/// </summary>
		/// <param name="newMovie">Details about the new movie</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("")]
		[HttpPost]
		public async Task<long> SubmitNewMovie([FromBody] NewMovieRequestModel newMovie, CancellationToken cancellationToken)
		{
			return await _movieManger.CreateNewMovie(NewMovieRequestModel.ConvertToMovieDTO(newMovie), cancellationToken);
		}



		/// <summary>
		/// Returns movie details
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("{id:long}")]
		[HttpGet]
		public async Task<MovieResponseModel> GetMovie([FromRoute] long id, CancellationToken cancellationToken)
		{
			var foundMovie = await _movieManger.GetMovieById(id, cancellationToken);
			return MovieResponseModel.ConvertToMovieResponseModel(foundMovie);
		}

		/// <summary>
		/// Update Movie Details
		/// </summary>
		/// <param name="id">Id of the movie to change</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("{id:long}")]
		[HttpPatch]
		public async Task UpdateMovie([FromRoute] long id, [FromBody] NewMovieRequestModel newMovieRequestModel, CancellationToken cancellationToken)
		{
			 _movieManger.UpdateMovieById(id,  NewMovieRequestModel.ConvertToMovieDTO(newMovieRequestModel), cancellationToken);			
		}


		/// <summary>
		/// Returns movie details
		/// </summary>
		/// <param name="skipRecords">Number of records to skip (for paging)</param>
		/// <param name="takeRecords">Number of records to return (for paging)</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("toprated")]
		[HttpGet]
		public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies([FromQuery] int skipRecords, [FromQuery] int takeRecords, CancellationToken cancellationToken)
		{
			List<MovieResponseModel> results = new List<MovieResponseModel>(0);
			var foundMovies = await _movieManger.GetTopRatedMovies(skipRecords, takeRecords, cancellationToken);
			foreach (var item in foundMovies)
			{
				results.Add(MovieResponseModel.ConvertToMovieResponseModel(item));
			}
			
			return results;
		}

		/// <summary>
		/// Returns a user rating for the movie
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("{id:long}/GetUserRating")]
		[HttpGet]
		public async Task<float> GetUserRatingForMovie([FromRoute]long id, CancellationToken cancellationToken)
		{
			return await _userMovieRatingManager.GetUserRatingforMovie(id, cancellationToken);
		}


		/// <summary>
		/// Submits a new user rating for the movie
		/// </summary>
		/// <param name="id">The movie id</param>
		/// <param name="newRating">New User Rating</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("{id:long}/SubmitNewRating")]
		[HttpPost]
		public async Task SubmitNewUserRating([FromRoute] long id, float newRating, CancellationToken cancellationToken)
		{
			_userMovieRatingManager.UpdateMovieUserRating(id, newRating, HttpContext.Connection?.RemoteIpAddress?.ToString(), cancellationToken);			
		}

	}
}
