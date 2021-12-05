using Movies.Entities.DataModels;
using Movies.Entities.Exceptions;
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

		public MovieGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}
		
		public async Task<MovieDataModel> Get(Guid id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);

			var result = await grain.Get();

			if (result.CreatedDate == null)
				throw new MovieIdNotExistsException(id, "The Movie you are looking for does not exist");

			return result;
		}

		public async Task Set(Guid id, NewMovieDetailsDTO dto)
		{
			// If movie run time is less then 1 minute then throw exception
			if (dto.RunTimeInMinutes < 1)
				throw new InvalidFieldLengthException(nameof(dto.RunTimeInMinutes), dto.RunTimeInMinutes.ToString(), 1, 999, "Run time cannot be less then 1 minute");

			if(dto.Rating < 1)
				throw new InvalidFieldLengthException(nameof(dto.Rating), dto.Rating.ToString(), 1, 10, "Rating cannot be less then 1");

			if (dto.Rating > 10)
				throw new InvalidFieldLengthException(nameof(dto.Rating), dto.Rating.ToString(), 1, 10, "Rating cannot be more then 10");

			if(string.IsNullOrEmpty(dto.Name.Trim()))
				throw new InvalidFieldValueException(nameof(dto.Name), dto.Name, "Name cannot be empty");

			var grain = _grainFactory.GetGrain<IMovieGrain>(id);			
			await grain.Set(id, dto);			
		}

		public async Task UpdateMovie(Guid id, NewMovieDetailsDTO dto)
		{
			var existingMovie = await Get(id);
			

			await Set(id, dto);
		}
	}
}