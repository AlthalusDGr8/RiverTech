namespace TechDemo.MoviesDb.API.Models.Response
{
	public class GenreResponseModel
	{
		/// <summary>
		/// Unique Id
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// The description of the genre
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Returns the approp display name 
		/// </summary>
		public string DisplayName { get; set; }

	}
}
