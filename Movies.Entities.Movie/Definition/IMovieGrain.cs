using Movies.Entities.DataModels;
using Movies.Entities.Movie.DataModels.Movies;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Movies.Entities.Movie.Definition
{
	public interface IMovieGrain : IGrainWithGuidKey
	{
		/// <summary>
		/// Return the Movie Data 
		/// </summary>
		/// <returns></returns>
		Task<MovieDataModel> Get();
		
		/// <summary>
		/// Create a new one
		/// </summary>
		/// <param name="newMovieDTO">Data transfer object</param>
		/// <returns></returns>
		Task Set(Guid id, NewMovieDTO newMovieDTO);
	}
}
