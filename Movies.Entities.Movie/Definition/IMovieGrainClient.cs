using Movies.Entities.DataModels;
using Movies.Entities.Movie.DataModels.Movies;
using System;
using System.Threading.Tasks;

namespace Movies.Entities.Movie.Definition
{
	/// <summary>
	/// Grain Client to get and set
	/// </summary>
	public interface IMovieGrainClient
	{
		/// <summary>
		/// Get by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<MovieDataModel> Get(Guid id);
		
		/// <summary>
		/// Creates a new Movie
		/// </summary>		
		/// <param name="dto">The data transfer object</param>
		/// <param name="isUpdate">determiens if we are doing an update or create</param>
		/// <returns></returns>
		Task Set(Guid id, NewMovieDetailsDTO dto, bool isUpdate = false);

		/// <summary>
		/// Updates an exisitng Movie
		/// </summary>
		/// <param name="id">The id if present</param>
		/// <param name="dto">The new details</param>
		/// <returns></returns>
		/// <exception cref="MovieIdNotExistsException">Thrown when the movie you want to update does not exist</exception>
		Task UpdateMovie(Guid id, NewMovieDetailsDTO dto);
	}
}
