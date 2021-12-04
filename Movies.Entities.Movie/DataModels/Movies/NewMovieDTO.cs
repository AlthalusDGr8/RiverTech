using Movies.Entities.Core.Defintions;
using System;

namespace Movies.Entities.DataModels
{
	/// <summary>
	/// Use this class to create a new movie
	/// </summary>
	public class NewMovieDTO : IBaseNewModelDTO
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
