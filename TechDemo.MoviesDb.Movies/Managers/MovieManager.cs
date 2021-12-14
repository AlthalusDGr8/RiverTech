using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Movies.Definitions;
using TechDemo.MoviesDb.Movies.Entities;
using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;
using TechDemo.MoviesDb.Movies.Exceptions;

namespace TechDemo.MoviesDb.Movies.Managers
{
	/// <summary>
	/// Implementation of movie Manager
	/// </summary>
	public class MovieManager : IMovieManger
	{
		private readonly IEntityRepo<Movie> _entityRepo;
		private readonly IEntityRepo<Genre> _genreEntityRepo;
		private readonly IGenreManager _genreManager;
		public MovieManager(IEntityRepo<Movie> entityRepo, IGenreManager genreManager, IEntityRepo<Genre> genreEntityRepo)
		{
			_entityRepo = entityRepo;
			_genreManager = genreManager;
			_genreEntityRepo = genreEntityRepo;
		}

		public async Task<long> CreateNewMovie(MovieDTO newMovieDetails, CancellationToken cancellationToken)
		{			
			// If movie run time is less then 1 minute then throw exception
			if (newMovieDetails.Length.TotalMinutes < 1)
				throw new InvalidFieldLengthException(nameof(newMovieDetails.Length.TotalMinutes), newMovieDetails.Length.TotalMinutes.ToString(), 1, 999, "Run time cannot be less then 1 minute");

			if (newMovieDetails.CriticRating < 1)
				throw new InvalidFieldLengthException(nameof(newMovieDetails.CriticRating), newMovieDetails.CriticRating.ToString(), 1, 10, "Rating cannot be less then 1");

			if (newMovieDetails.CriticRating > 10)
				throw new InvalidFieldLengthException(nameof(newMovieDetails.CriticRating), newMovieDetails.CriticRating.ToString(), 1, 10, "Rating cannot be more then 10");

			if (string.IsNullOrEmpty(newMovieDetails.Name.Trim()))
				throw new InvalidFieldValueException(nameof(newMovieDetails.Name), newMovieDetails.Name, "Name cannot be empty");

			// need to verify if the genres provided are part of our lookups
			var allGenres = await _genreEntityRepo.GetAllAsync();			
			ICollection<Genre> foundGenreInstances = new List<Genre>(0);
			foreach (var item in newMovieDetails.GenreCodes)
			{
				try
				{
					var checkGenreExists = _genreManager.GetByCode(item, cancellationToken);
					foundGenreInstances.Add(allGenres.Single(x => x.Code.Equals(item, StringComparison.OrdinalIgnoreCase)));
				}
				catch (GenreNotExistsException exp)
				{
					throw new InvalidFieldValueException("Genre", item, "Genre does not exist", exp);					
				}									
			}

			// all checks have passed, let us now proceed to create the entity
			var newMovieEntity = new Movie() { 
				CriticRating = newMovieDetails.CriticRating, 
				Description = newMovieDetails.Description, 
				ImgUrl = GenerateMovieUniqueKey(newMovieDetails.Name) + ".jpg", 
				Length = (int)newMovieDetails.Length.TotalMinutes,
				Name = newMovieDetails.Name,
				UniqueKey = GenerateMovieUniqueKey(newMovieDetails.Name),				
				Genres = foundGenreInstances
			};

			newMovieEntity = await _entityRepo.CreateAsync(newMovieEntity, cancellationToken);
			return newMovieEntity.Id;
		}

		/// <summary>
		/// Creates the movie unique key
		/// </summary>
		/// <param name="movieName"></param>
		/// <returns></returns>
		private static string GenerateMovieUniqueKey(string movieName)
		{
			var finalResult = movieName;			
			// replace fullstops with nothing
			finalResult = finalResult.Replace(".", "");
			
			// replace spaces with dashes
			finalResult = finalResult.Replace(" ", "-");
			
			// In the end all should be to lower
			return finalResult.ToLowerInvariant() ;
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
