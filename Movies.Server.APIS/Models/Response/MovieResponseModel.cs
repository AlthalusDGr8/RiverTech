using System;

namespace Movies.Server.APIS.Models.Response
{
	public class MovieResponseModel
	{
		/// <summary>
		/// The unique Id
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// THe name of the movie
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

	}
}
