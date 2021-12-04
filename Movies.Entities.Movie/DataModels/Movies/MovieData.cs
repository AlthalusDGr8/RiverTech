using System;

namespace Movies.Entities.Movie.DataModels.Movies
{
	public class MovieData
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Synopisis { get; set; }
		public string Description { get; set; }
		public DateTime ReleaseDate { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
