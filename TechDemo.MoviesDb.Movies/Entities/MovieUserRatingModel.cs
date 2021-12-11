namespace TechDemo.MoviesDb.Movies.Entities
{
	/// <summary>
	/// A model to store the user ratings for a movie
	/// </summary>
	public class MovieUserRatingModel
	{
		public MovieUserRatingModel()
		{
			// Init empty list
			AllSubmittedRatings = new List<UserVoteModel>();
		}

		/// <summary>
		/// Represents the Movie Id
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Represents the list of user votes
		/// </summary>
		public List<UserVoteModel> AllSubmittedRatings { get; set; }

		/// <summary>
		/// The average User Rating
		/// </summary>
		public float AverageUserRating => (float)Math.Round(AllSubmittedRatings.Sum(x => x.Rating) / AllSubmittedRatings.Count);
	}


	/// <summary>
	/// Class that stores the user vote model
	/// </summary>
	public class UserVoteModel
	{
		/// <summary>
		/// IP Address for the user who submitted the vote
		/// </summary>
		public string SubmittedIpAddress { get; set; }

		/// <summary>
		/// The rating gviev
		/// </summary>
		public float Rating { get; set; }
	}
}
