using Movies.Entities.Core.Defintions;
using Movies.Entities.DataModels;
using Movies.Entities.Movie.DataModels.Movies;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Movies.Entities.Movie.Grains
{
	/// <summary>
	/// Movie Data Grain
	/// </summary>
	public class MovieDataGrain : Grain<MovieData>, IBaseGrain<MovieData, NewMovieDTO>
	{
		public Task<MovieData> Get() => Task.FromResult(State);
		
		public async Task<Guid> Set(NewMovieDTO baseNewModelDTO)
		{
			State = new MovieData { Id = Guid.NewGuid(),
				Name = baseNewModelDTO.Name, 
				CreatedDate = DateTime.UtcNow, 
				Description = baseNewModelDTO.Description, 
				ReleaseDate = baseNewModelDTO.ReleaseDate, 
				Synopisis = baseNewModelDTO.Synopsis };

			await WriteStateAsync();

			return State.Id;
		}
	}
}
