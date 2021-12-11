using Orleans;
using TechDemo.MoviesDb.Movies.Definitions;
using TechDemo.MoviesDb.Movies.Entities;
using TechDemo.MoviesDb.Orleans.Definitions;

namespace TechDemo.MoviesDb.Orleans.Managers
{
	/// <summary>
	/// Implementaiton of the user move rating
	/// </summary>
	public class UserMovieRatingManager : IUserMovieRatingManager
	{
		private readonly IGrainFactory _grainFactory;
		private readonly IMovieManger _movieManager;

		public UserMovieRatingManager(IGrainFactory grainFactory, IMovieManger movieManager)
		{
			_grainFactory = grainFactory;
			_movieManager = movieManager;	
		}

		public async Task<float> GetUserRatingforMovie(long movieId, CancellationToken cancellationToken)
		{
			// First check that the movie actually exists
			_ = await _movieManager.GetMovieById(movieId, cancellationToken);
			
			// if no exceptions where thrown, then feel free to create a new grain with this new id
			var grain = _grainFactory.GetGrain<IUserMovieRatingsGrain>(movieId);
			var currentRating = await grain.Get();

			return currentRating.AverageUserRating;
		}


		public Task UpdateMovieUserRating(long movieId, float submittedUserRating,string ipAddress, CancellationToken cancellationToken)
		{
			// First check that the movie actually exists
			_ = _movieManager.GetMovieById(movieId, cancellationToken);

			// if no exceptions where thrown, then feel free to create a new grain with this new id
			var grain = _grainFactory.GetGrain<IUserMovieRatingsGrain>(movieId);
			grain.SubmitNewRating(new UserVoteModel() { Rating = submittedUserRating, SubmittedIpAddress = ipAddress });

			return Task.CompletedTask;
		}
	}
}
