using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Movies.Definitions;
using TechDemo.MoviesDb.Movies.Entities;
using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;
using TechDemo.MoviesDb.Movies.Exceptions;

namespace TechDemo.MoviesDb.Movies.Managers
{
	public class MovieManager : IMovieManger
	{
		private readonly IEntityRepo<Movie> _entityRepo;
		public MovieManager(IEntityRepo<Movie> entityRepo)
		{
			_entityRepo = entityRepo;
		}

		/// <summary>
		/// Fetches a movie by id
		/// </summary>
		/// <param name="movieId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="MovieIdNotExistsException">Thrown when a movie with specified ID does not exist</exception>
		public async Task<MovieDTO> GetMovieById(long movieId, CancellationToken cancellationToken)
		{
			// try to get new movie
			var result = await _entityRepo.GetByKeyAsync(movieId, cancellationToken);
			if (result == null)
				throw new MovieIdNotExistsException(movieId, $"The movie with id {movieId} does not exist");

			return MovieDTO.ConvertFromMovie(result);
		}


		/// <summary>
		/// Returns the number of top rated movies
		/// </summary>
		/// <param name="skipRecords">The number of records to skip</param>
		/// <param name="takeRecords">The number of records tor eturn</param>
		/// <param name="cancellationToken">Cancelation token</param>
		/// <returns></returns>
		public async Task<IEnumerable<MovieDTO>> GetTopRatedMovies(int skipRecords, int takeRecords, CancellationToken cancellationToken)
		{
			List<MovieDTO> returnValue = new List<MovieDTO>(0);
			var result = await _entityRepo.GetByCustomParams(
							null,
							x => x.OrderBy(y => y.CriticRating),
							null,
							skipRecords,
							takeRecords, cancellationToken);
			
			// Return empty list if nothing is found
			if ((result == null) || (!result.Any()))
				return returnValue;

			foreach (Movie item in result)
			{
				returnValue.Add(MovieDTO.ConvertFromMovie(item));
			}

			return returnValue;
		}

	}
}
