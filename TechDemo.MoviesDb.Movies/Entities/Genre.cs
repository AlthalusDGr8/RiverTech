using TechDemo.MoviesDb.Core.DbEntities;

namespace TechDemo.MoviesDb.Movies.Entities
{
	/// <summary>
	/// Represents a Genre
	/// </summary>
	public class Genre : IEntity
	{
		/// <summary>
		/// The Genre Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// A unique ideitifying code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// This is the description of this Genre
		/// </summary>
		public string Description { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
