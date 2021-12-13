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

		/// <summary>
		/// Return the top rated mvies based on the number of skipand take records
		/// </summary>
		/// <param name="skipRecords">the number of records to skip</param>
		/// <param name="takeRecords">How many to take back</param>
		/// <param name="cancellationToken"></param>
		/// <returns>NUll collection if nothing is found, else the record details</returns>
		public Task<IEnumerable<MovieDTO>> GetTopRatedMovies(int skipRecords, int takeRecords, CancellationToken cancellationToken);
	}
}
