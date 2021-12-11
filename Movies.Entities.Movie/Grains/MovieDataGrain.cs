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

		public Task Set(Guid id, NewMovieDetailsDTO newMovieDTO, bool isUpdate = false)
		{
			State = new MovieDataModel
			{
				Id = id,
				Name = newMovieDTO.Name,
				Description = newMovieDTO.Description,
				Genres = newMovieDTO.Genres,
				ImgUrl = newMovieDTO.ImgUrl,
				Rating = newMovieDTO.Rating,
				RunTimeInMinutes = newMovieDTO.RunTimeInMinutes,
				UpdatedOnDate = DateTime.UtcNow
			};

			// If this is a new record then mark the create date timestamp
			if(!isUpdate)
			{
				State.CreatedDate = DateTime.UtcNow;
			}

			WriteStateAsync();

			return Task.CompletedTask;
		}
	}
}