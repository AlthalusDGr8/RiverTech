using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
		/// Returns movie details
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("/{id:long}/")]
		[HttpGet]
		public async Task<MovieResponseModel> GetMovie([FromRoute] long id, CancellationToken cancellationToken)
		{
			var foundMovie = await _movieManger.GetMovieById(id, cancellationToken);
			return MovieResponseModel.ConvertToMovieResponseModel(foundMovie);
		}


		/// <summary>
		/// Returns movie details
		/// </summary>
		/// <param name="skipRecords">Number of records to skip</param>
		/// <param name="takeRecords">Number of records to return</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Route("/toprated/")]
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
		[Route("/{id:long}/GetUserRating")]
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
		[Route("/{id:long}/SubmitNewRating")]
		[HttpPost]
		public async Task SubmitNewUserRating([FromRoute] long id, float newRating, CancellationToken cancellationToken)
		{
			_userMovieRatingManager.UpdateMovieUserRating(id, newRating, HttpContext.Connection?.RemoteIpAddress?.ToString(), cancellationToken);			
		}

	}
}
