using Movies.Entities.DataModels;
using Movies.Entities.Movie.DataModels.Movies;
using Movies.Entities.Movie.Definition;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Movies.GrainClients
{
	public class MovieGrainClient : IMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public MovieGrainClient(
			IGrainFactory grainFactory
		)
		{
			_grainFactory = grainFactory;
		}
		
		public Task<MovieDataModel> Get(Guid id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			return grain.Get();
		}

		public async Task Set(Guid id, NewMovieDTO dto)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			await grain.Set(id, dto);			
		}
	}
}