using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;

namespace TechDemo.MoviesDb.Movies.Definitions
{
	public interface IMovieManger
	{
		/// <summary>
		/// Creates a new Movie in the system and returns the id
		/// </summary>
		/// <param name="newMovieDetails">The new details</param>
		/// <param name="cancellationToken">Cancellation Token</param>
		/// <exception cref="Exceptions.InvalidFieldValueException">Thrown when a field is invalid</exception>
		/// <returns></returns>
		public Task<long> CreateNewMovie(MovieDTO newMovieDetails, CancellationToken cancellationToken);

		/// <summary>
		/// Returns a movie if present 
		/// </summary>
		/// <param name="movieId">The movie unique id to look for</param>
		/// <returns>Thhe movie detials if found</returns>
		/// <exception cref="Exceptions.MovieIdNotExistsException">Thrown when a movie with specified ID does not exist</exception>
		public Task<MovieDTO> GetMovieById(long movieId, CancellationToken cancellationToken);

		/// <summary>
		/// updates a movie based on the id
		/// </summary>
		/// <param name="movieId">The movie unique id to look for</param>
		/// <param name="cancellationToken"></param>
		/// <param name="updatedMovieDetails">The movie details to be updated</param>
		/// <returns>Thhe movie detials if found</returns>
		/// <exception cref="Exceptions.MovieIdNotExistsException">Thrown when a movie with specified ID does not exist</exception>
		/// <exception cref="Exceptions.InvalidFieldValueException">Thrown when a field is invalid</exception>
		/// /// <exception cref="Exceptions.InvalidFieldLengthException">Thrown when a field is invalid</exception>
		public Task UpdateMovieById(long movieId, MovieDTO updatedMovieDetails,  CancellationToken cancellationToken);

		/// <summary>
		/// Return the top rated mvies based on the number of skipand take records
		/// </summary>
		/// <param name="skipRecords">the number of records to skip</param>
		/// <param name="takeRecords">How many to take back</param>
		/// <param name="cancellationToken"></param>
		/// <returns>NUll collection if nothing is found, else the record details</returns>
		public Task<IEnumerable<MovieDTO>> GetTopRatedMovies(int skipRecords, int takeRecords, CancellationToken cancellationToken);

		/// <summary>
		/// Returns a list of movies
		/// </summary>
		/// <param name="skipRecords">The number of records to skip</param>
		/// <param name="takeRecords">The number of records to take</param>
		/// <param name="cancellationToken"></param>
		/// <returns>NUll collection if nothing is found, else the record details</returns>
		public Task<IEnumerable<MovieDTO>> GetMovies(int? skipRecords, int? takeRecords, CancellationToken cancellationToken);
	}


}
