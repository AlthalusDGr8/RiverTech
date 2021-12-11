namespace TechDemo.MoviesDb.Movies.Definitions
{
	/// <summary>
	/// USe this Manager to interact with 
	/// </summary>
	public interface IUserMovieRatingManager
	{
		/// <summary>
		/// Get the current User Rating
		/// </summary>
		/// <param name="movieId"></param>
		/// <returns></returns>
		Task<float> GetUserRatingforMovie(long movieId, CancellationToken cancellationToken);

		/// <summary>
		/// Update the user rating for a movie
		/// </summary>
		/// <param name="movieId"></param>
		/// <param name="submittedUserRating"></param>
		/// <returns></returns>
		Task UpdateMovieUserRating(long movieId, float submittedUserRating, string ipAddress, CancellationToken cancellationToken);
	}
}
