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
		/// <returns></returns>
		Task Set(Guid id, NewMovieDTO dto);
	}
}
