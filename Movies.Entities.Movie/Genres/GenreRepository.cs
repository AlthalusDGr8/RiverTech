using Movies.CentralCore.Caching;
using Movies.CentralCore.Repository;
using Movies.Entities.Movie.DataModels.Genres;
using System.Collections.Generic;

namespace Movies.Entities.Movie.Genres
{
	/// <summary>
	/// Genre Repository
	/// </summary>
	public class GenreRepository : GenericLookupRepository<GenreDTO>
	{
		public GenreRepository(ICacheManager cacheManager) : base(cacheManager)
		{
		}

		public override string AllElementsCacheKey => "ALL_GENRES";

		public override List<GenreDTO> FetchandCacheItems(string cacheKey)
		{
			//TODO: replace with getting from database
			var itemList = new List<GenreDTO>
			{
				new GenreDTO() { Id = 1, Description = "Action" ,  Code ="Action" },
				new GenreDTO() { Id = 2, Description = "Horror" ,  Code ="Horror" },
				new GenreDTO() { Id = 3, Description = "Suspense", Code ="Suspense" },
				new GenreDTO() { Id = 4, Description = "SciFi",    Code ="SciFi"   },
			};

			_cacheManager.SetInCache(AllElementsCacheKey, itemList);
			return itemList;
		}
	}
}
