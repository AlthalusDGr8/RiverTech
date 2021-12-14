namespace TechDemo.MoviesDb.Movies.Entities.DataTransferObjects
{
	public class GenreDTO
	{
		/// <summary>
		/// The Genre Id
		/// </summary>		
		public long Id { get; set; }

		/// <summary>
		/// A unique ideitifying code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// This is the description of this Genre
		/// </summary>
		public string Description { get; set; }
	}
}
