using Movies.Entities.DataModels;
using Movies.Entities.Movie.DataModels.Movies;
using Movies.Entities.Movie.Definition;
using Orleans;
using Orleans.Providers;
using System;
using System.Threading.Tasks;

namespace Movies.Entities.Movie.Grains
{
	[StorageProvider(ProviderName = "Default")]
	public class MovieDataGrain : Grain<MovieDataModel>, IMovieGrain
	{
		public Task<MovieDataModel> Get()
			=> Task.FromResult(State);

		public Task Set(Guid id, NewMovieDetailsDTO newMovieDTO)
		{
			State = new MovieDataModel
			{
				Id = id,
				Name = newMovieDTO.Name,
				CreatedDate = DateTime.UtcNow,
				Description = newMovieDTO.Description,
				ReleaseDate = newMovieDTO.ReleaseDate,
				Synopisis = newMovieDTO.Synopsis
			};

			WriteStateAsync();

			return Task.CompletedTask;
		}
	}
}