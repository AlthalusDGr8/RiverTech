using System.ComponentModel.DataAnnotations;
using TechDemo.MoviesDb.Core.DbEntities;

namespace TechDemo.MoviesDb.Movies.Entities
{
	public class Movie : IEntity
	{		
		/// <summary>
		/// Unique Movie Id
		/// </summary>
		[Key]
		public long MovieId { get; set; }
		
		/// <summary>
		/// The unique key to search with
		/// </summary>		
		public string UniqueKey  { get; set; }

		/// <summary>
		/// The Movie name
		/// </summary>
		[Required]
		public string Name{ get; set; }

		/// <summary>
		/// Move Description
		/// </summary>
		[Required]
		public string Description { get; set; }

		/// <summary>
		/// Rating
		/// </summary>
		[Required]
		public decimal CriticRating { get; set; }

		/// <summary>
		/// The length in minutes of the Movie
		/// </summary>
		[Required]
		public int Length { get; set; }

		/// <summary>
		/// The URL of the image
		/// </summary>		
		public string ImgUrl { get; set; }

		/* EF Relations */

		/// <summary>
		/// A list of genres for this movie
		/// </summary>
		public ICollection<Genre> Genres { get; set; }

		public long Id { get { return MovieId; } }
	}
}
