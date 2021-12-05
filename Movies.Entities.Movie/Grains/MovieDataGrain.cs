using Movies.Entities.DataModels;
using Movies.Entities.Movie.DataModels.Movies;
using Movies.Entities.Movie.Definition;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Movies.Entities.Movie.Grains
{
	[StorageProvider(ProviderName = "Default")]
	public class MovieDataGrain : Grain<MovieDataModel>, IMovieGrain
	{
		private readonly IPersistentState<MovieDataModel> _persistentState;
		
		public MovieDataGrain([PersistentState("def", "Default")] IPersistentState<MovieDataModel> persistentState)
		{
			_persistentState = persistentState;
		}

		public Task<MovieDataModel> Get() => Task.FromResult(State);

		public Task Set(Guid id, NewMovieDetailsDTO newMovieDTO)
		{
			State = new MovieDataModel
			{
				Id = id,
				Name = newMovieDTO.Name,
				CreatedDate = DateTime.UtcNow,
				Description = newMovieDTO.Description,
				Genres = newMovieDTO.Genres,
				ImgUrl = newMovieDTO.ImgUrl,
				Rating = newMovieDTO.Rating,
				RunTimeInMinutes = newMovieDTO.RunTimeInMinutes				
			};

			WriteStateAsync();

			return Task.CompletedTask;
		}
	}
}