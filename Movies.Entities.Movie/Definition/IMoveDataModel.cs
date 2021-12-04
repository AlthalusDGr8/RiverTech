using Orleans;
using System;

namespace Movies.Entities.Movie.Definition
{
	public interface IMovieDataModel : IGrainWithGuidKey
	{
		/// <summary>
		/// Unique Identifer
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The name of the movie
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A short info about the film
		/// </summary>
		public string Synopisis { get; set; }

		/// <summary>
		/// The description
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// The release date
		/// </summary>
		public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Date when it was created
		/// </summary>
		public DateTime? CreatedDate { get; set; }
	}
}
