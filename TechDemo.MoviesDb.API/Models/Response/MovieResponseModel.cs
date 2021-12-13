using System;
using System.Collections.Generic;
using TechDemo.MoviesDb.Movies.Entities.DataTransferObjects;

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


		internal static MovieResponseModel ConvertToMovieResponseModel(MovieDTO movieDTO) => new MovieResponseModel()
		{
			CriticRating = movieDTO.CriticRating,
			Description = movieDTO.Description,
			GenreCodes = movieDTO.GenreCodes,
			Id = movieDTO.Id,
			ImgUrl = movieDTO.ImgUrl,
			Name = movieDTO.Name,
			RunTime = ConvertMovieLengthToRuntime(movieDTO.Length)
		};

		private static string ConvertMovieLengthToRuntime(TimeSpan movieLength) => $"{movieLength.Hours} hrs and {movieLength.Minutes} minutes";
	}
}
