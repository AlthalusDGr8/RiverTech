using System.ComponentModel.DataAnnotations;
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
		[Key]
		public long GenreId { get; set; }

		/// <summary>
		/// A unique ideitifying code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// This is the description of this Genre
		/// </summary>
		public string Description { get; set; }

		/* EF Relations */
		public ICollection<Movie> Movies{ get; set; }
		
		public long Id { get { return GenreId; } }
	}
}
