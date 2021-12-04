using Movies.Entities.Core.Defintions;
using Movies.Entities.DataModels;
using Movies.Entities.Exceptions;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Movies.Entities.Movie.Grains
{
	public class MovieDataGrainClient : IBaseGuidGrainClient<MovieDataGrain, NewMovieDTO>
	{
		private readonly IGrainFactory _grainFactory;
		public MovieDataGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}


		public Task<MovieDataGrain> Get(Guid id)
		{
			var result = _grainFactory.GetGrain<IBaseGrain<MovieDataGrain, NewMovieDTO>>(id);
			return result.Get();
		}
		
		
		public Task<Guid> Set(NewMovieDTO newModelDataTransferObject)
		{			
				// Run the initial validation
			if (string.IsNullOrEmpty(newModelDataTransferObject.Name))
					throw new InvalidFieldValueException(nameof(newModelDataTransferObject.Name), newModelDataTransferObject.Name, "Name cannot be null");

			// synopsis needs to be no longer then 50 characters
			if (newModelDataTransferObject.Synopsis.Trim().Length > 50)
				throw new InvalidFieldLengthException(nameof(newModelDataTransferObject.Synopsis), newModelDataTransferObject.Synopsis, 0, 50, "Synopsis cannot be longer then 50 characters");

			if (string.IsNullOrEmpty(newModelDataTransferObject.Description))
				throw new InvalidFieldValueException(nameof(newModelDataTransferObject.Description), newModelDataTransferObject.Description, "Description cannot be null");

			if (newModelDataTransferObject.ReleaseDate == DateTime.MinValue)
				throw new InvalidFieldValueException(nameof(newModelDataTransferObject.ReleaseDate), newModelDataTransferObject.ReleaseDate.ToString("dd/mm/yyyy"), "Date needs to be provided");

			// get the "new" grain 
			var grain = _grainFactory.GetGrain<IBaseGrain<MovieDataGrain, NewMovieDTO>>(Guid.Empty);
			
			return grain.Set(newModelDataTransferObject);			
		}
	}
}
