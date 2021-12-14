using TechDemo.MoviesDb.Core.Exceptions;

namespace TechDemo.MoviesDb.Movies.Exceptions
{
	/// <summary>
	/// Anything related to the movies that needs to throw an exxeption should pass through here
	/// </summary>
	public abstract class GenreException : CentralCoreException
	{
		public GenreException(string message, Exception innerException = null) : base(message, innerException)
		{

		}
	}

	/// <summary>
	/// Throw this exception when the genre being looked for does not exists
	/// </summary>
	public class GenreNotExistsException : GenreException
	{
		public GenreNotExistsException(long id, string message, Exception innerException = null) : base(message, innerException)
		{
			ExtraDetails.Add("Id", id.ToString());	
		}


		public GenreNotExistsException(string code, string message, Exception innerException = null) : base(message, innerException)
		{
			ExtraDetails.Add("Code", code);
		}

		public override string UniqueErrorCode => "GENRE_NOT_EXISTS";
	}
}
