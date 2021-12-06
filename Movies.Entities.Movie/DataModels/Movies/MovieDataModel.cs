using GraphQL.Types;
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


	public class MovieDataModelGraphType : ObjectGraphType<MovieDataModel>
	{
		public MovieDataModelGraphType()
		{
			Field(x => x.Id).Description("The Unique Id of the Movie.");
			Field(x => x.Name).Description("The name of the movie.");
			Field(x => x.Description).Description("short descrption of the movie.");
			Field(x => x.Rating).Description("Movie rating from 1 to 10");
			Field(x => x.RunTimeInMinutes).Description("Movie runtime duration");
			Field(x => x.ImgUrl).Description("The Url where to find the poster");
			Field(x => x.Genres).Description("All possible genres that this movie belongs to");			
		}
	}
}
