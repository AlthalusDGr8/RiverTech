using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;

namespace TechDemo.MoviesDb.Movies.Definitions
{
	/// <summary>
	/// Serves as an inbetween the data access layer and anything above
	/// </summary>
	public interface IGenreManager
	{
		/// <summary>
		/// Returns all Genres
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<IEnumerable<GenreDTO>> GetAll(CancellationToken cancellationToken);

		/// <summary>
		/// Returns Genre by Id
		/// </summary>
		/// <param name="genreId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		///<exception cref="GenreNotExistsException">Thrown when the specified genre does not exist</exception>
		Task<GenreDTO> GetById(long genreId, CancellationToken cancellationToken);

		/// <summary>
		/// Returns Genre by Code
		/// </summary>
		/// <param name="genreCode"></param>
		/// <param name="cancellationToken"></param>
		///<exception cref="GenreNotExistsException">Thrown when the specified genre does not exist</exception>
		Task<GenreDTO> GetByCode(string genreCode, CancellationToken cancellationToken);

	}
}
