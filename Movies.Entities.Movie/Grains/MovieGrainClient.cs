using Movies.CentralCore.Repository;
using Movies.Entities.DataModels;
using Movies.Entities.Exceptions;
using Movies.Entities.Movie.DataModels.Genres;
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

		private readonly ILookupRepository<GenreDTO> _genreLookupRepository;

		public MovieGrainClient(IGrainFactory grainFactory, ILookupRepository<GenreDTO> genreLookupRepository)
		{
			_grainFactory = grainFactory;
			_genreLookupRepository = genreLookupRepository;
		}
		
		public async Task<MovieDataModel> Get(Guid id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);

			var result = await grain.Get();

			//Use this to verify that the movie is not a totally new one
			if (result.CreatedDate == null)
				throw new MovieIdNotExistsException(id, "The Movie you are looking for does not exist");

			return result;
		}

		public async Task Set(Guid id, NewMovieDetailsDTO dto, bool isUpdate = false)
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

			// need to verify if the genres provided are part of our lookups
			foreach (string item in dto.Genres)
			{
				var checkGenreExists = _genreLookupRepository.GetByCode(item);
				if (checkGenreExists == null)
				{
					throw new InvalidFieldValueException("Genre", item, "Genre does not exist");
				}
			}

			var grain = _grainFactory.GetGrain<IMovieGrain>(id);			
			await grain.Set(id, dto, isUpdate);			
		}

		public async Task UpdateMovie(Guid id, NewMovieDetailsDTO dto)
		{
			// fetch if exists
			_ = await Get(id);
			await Set(id, dto, true);
		}
	}
}