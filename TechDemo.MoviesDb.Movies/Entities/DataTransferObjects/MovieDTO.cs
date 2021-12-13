namespace TechDemo.MoviesDb.Movies.Entities.DataTransferObjects
{
	public class MovieDTO
	{
		/// <summary>
		/// Unique Movie Id
		/// </summary>		
		public long Id { get; set; }

		/// <summary>
		/// The unique key to search with
		/// </summary>		
		public string UniqueKey { get; set; }

		/// <summary>
		/// The Movie name
		/// </summary>		
		public string Name { get; set; }

		/// <summary>
		/// Move Description
		/// </summary>		
		public string Description { get; set; }

		/// <summary>
		/// Rating
		/// </summary>		
		public decimal CriticRating { get; set; }

		/// <summary>
		/// The length in minutes of the Movie
		/// </summary>		
		public TimeSpan Length { get; set; }

		/// <summary>
		/// The URL of the image
		/// </summary>		
		public string ImgUrl { get; set; }

		/// <summary>
		/// A lost of codes representing genres
		/// </summary>
		public IEnumerable<string> GenreCodes { get; set; }

		internal static MovieDTO ConvertFromMovie(Movie movie) => new MovieDTO()
		{
			Id = movie.Id,
			Description = movie.Description,
			GenreCodes = movie.Genres.Select(x => x.Code),
			ImgUrl = movie.ImgUrl,
			Length = TimeSpan.FromMinutes(movie.Length),
			Name = movie.Name,
			CriticRating = movie.CriticRating,
			UniqueKey = movie.UniqueKey
		};
	}
}
