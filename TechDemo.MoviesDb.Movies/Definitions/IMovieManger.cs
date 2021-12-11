using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;

namespace TechDemo.MoviesDb.Movies.Definitions
{
	public interface IMovieManger
	{
		/// <summary>
		/// Returns a movie if present 
		/// </summary>
		/// <param name="movieId">The movie unique id to look for</param>
		/// <returns>Thhe movie detials if found</returns>
		/// <exception cref="MovieIdNotExistsException">Thrown when a movie with specified ID does not exist</exception>
		public Task<MovieDTO> GetMovieById(long movieId, CancellationToken cancellationToken);
	}
}
