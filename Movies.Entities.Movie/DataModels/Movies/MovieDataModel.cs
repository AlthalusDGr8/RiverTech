using System;

namespace Movies.Entities.Movie.DataModels.Movies
{
	[Serializable]
	public class MovieDataModel
	{
		/// <summary>
		/// Movie Unique Id
		/// </summary>
		public Guid Id { get; set; }		
		/// <summary>
		/// Movie Name
		/// </summary>
		public string Name { get; set; }		
		/// <summary>
		/// Movie Description
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// Genres that this movie is assoicated with
		/// </summary>
		public string[] Genres { get; set; }		
		/// <summary>
		/// Rating
		/// </summary>
		public decimal Rating { get; set; }		
		/// <summary>
		/// Total Run Time
		/// </summary>
		public int RunTimeInMinutes{ get; set; }
		/// <summary>
		/// Image URl
		/// </summary>
		public string ImgUrl { get; set; }

		public DateTime? CreatedDate { get; set; }

		public DateTime? UpdatedOnDate { get; set; }
	}
}
