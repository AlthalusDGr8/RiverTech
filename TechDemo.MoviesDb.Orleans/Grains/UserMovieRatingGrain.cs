using Orleans;
using TechDemo.MoviesDb.Movies.Entities;
using TechDemo.MoviesDb.Orleans.Definitions;

namespace TechDemo.MoviesDb.Orleans.Grains
{
	public class UserMovieRatingGrain : Grain<MovieUserRatingModel>, IUserMovieRatingsGrain
	{
		/// <summary>
		/// Gets the current user rating for this movie
		/// </summary>
		/// <returns></returns>
		public Task<MovieUserRatingModel> Get() => Task.FromResult(State);

		/// <summary>
		/// SAves the new user rating
		/// </summary>
		/// <param name="newUserRating"></param>
		/// <returns></returns>
		public Task SubmitNewRating(UserVoteModel newUserRating)
		{
			 State.AllSubmittedRatings.Add( newUserRating);
			return Task.CompletedTask;
		}
	}
}
