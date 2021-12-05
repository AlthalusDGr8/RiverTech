using Movies.CentralCore.Repository;

namespace Movies.Entities.Movie.DataModels.Genres
{
	public class GenreDTO : ILookupEntity
	{
		/// <summary>
		/// Unique Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The description of the genre
		/// </summary>
		public string Description { get; set; }
		public string Code { get; set; }
	}
}
