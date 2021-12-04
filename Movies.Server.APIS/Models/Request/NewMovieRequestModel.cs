using System;

namespace Movies.Server.APIS.Models.Request
{
	public class NewMovieRequestModel
	{		
		/// <summary>
		/// The name of the new move
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A short description of the film
		/// </summary>
		public string Synopsis { get; set; }

		/// <summary>
		/// The associated description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The date when it was released
		/// </summary>
		public DateTime ReleaseDate { get; set; }
	}
}
