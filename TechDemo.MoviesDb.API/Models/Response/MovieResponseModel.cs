using System;
using System.Collections.Generic;

namespace TechDemo.MoviesDb.API.Models.Response
{
	public class MovieResponseModel
	{
		/// <summary>
		/// The unique Id
		/// </summary>
		public long Id { get; set; }

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
		public IEnumerable<string> GenreCodes { get; set; }
		/// <summary>
		/// Rating
		/// </summary>
		public decimal CriticRating { get; set; }
		/// <summary>
		/// Total Run Time
		/// </summary>
		public string RunTime { get; set; }
		/// <summary>
		/// Image URl
		/// </summary>
		public string ImgUrl { get; set; }

		internal static string ConvertMovieLengthToRuntime(TimeSpan movieLength) => $"{movieLength.TotalHours} Hours and {movieLength.TotalMinutes} Minutes";
	}
}
