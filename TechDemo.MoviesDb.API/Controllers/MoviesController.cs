using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
			return new MovieResponseModel()
			{
				CriticRating = foundMovie.CriticRating,
				Description = foundMovie.Description,
				GenreCodes = foundMovie.GenreCodes,
				Id = foundMovie.Id,
				ImgUrl = foundMovie.ImgUrl,
				Name = foundMovie.Name,
				RunTime = MovieResponseModel.ConvertMovieLengthToRuntime(foundMovie.Length)
			};
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
