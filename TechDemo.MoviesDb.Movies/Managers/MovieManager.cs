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

			return new MovieDTO() {
				Id = result.Id,
				Description = result.Description,
				GenreCodes =  result.Genres.Select(x => x.Code),
				ImgUrl = result.ImgUrl,
				Length = TimeSpan.FromMinutes(result.Length),
				Name = result.Name,
				CriticRating = result.CriticRating,
				UniqueKey = result.UniqueKey };			
		}
	}
}
