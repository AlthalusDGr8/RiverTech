using Orleans;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.Orleans.Definitions
{
	public interface IUserMovieRatingsGrain : IGrainWithIntegerKey
    {
		/// <summary>
		/// Return the User Rating Data 
		/// </summary>
		/// <returns></returns>
		Task<MovieUserRatingModel> Get();
		
		/// <summary>
		/// Submits a new rating
		/// </summary>
		/// <param name="newUserRating">The new rating to add to the list</param>
		/// <returns></returns>
		Task SubmitNewRating(UserVoteModel newUserRating);
	}
}
