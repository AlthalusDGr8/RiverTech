using Movies.CentralCore.Exceptions;
using System;

namespace Movies.Entities.Exceptions
{
	/// <summary>
	/// Anything related to the movies that needs to throw an exxeption should pass through here
	/// </summary>
	public abstract class MovieException : CentralCoreException
	{
		public MovieException(string message, Exception innerException = null) : base(message, innerException)
		{

		}
	}

	/// <summary>
	/// Throw this exception when a field has an invalid value
	/// </summary>
	public class InvalidFieldValueException : MovieException
	{
		public InvalidFieldValueException(string fieldName, string providedValue, string message, Exception innerException = null) : base(message, innerException)
		{
			ExtraDetails.Add("InvalidField", fieldName);
			ExtraDetails.Add("ProvidedValue", providedValue);
		}

		public override string UniqueErrorCode => "MOVIE_INVALID_FIELD";
	}


	/// <summary>
	/// Throw this exception when a field has an invalid length
	/// </summary>
	public class InvalidFieldLengthException : MovieException
	{
		public InvalidFieldLengthException(string fieldName, string providedValue, int minLenght, int maxLenght,  string message, Exception innerException = null) : base(message, innerException)
		{
			ExtraDetails.Add("InvalidField", fieldName);
			ExtraDetails.Add("ProvidedValue", providedValue);
			ExtraDetails.Add("MinLength", minLenght.ToString());
			ExtraDetails.Add("MaxLength", maxLenght.ToString());
		}

		public override string UniqueErrorCode => "MOVIE_INVALID_FIELD_LENGTH";
	}

	/// <summary>
	/// Throw this exception when movie already exists
	/// </summary>
	public class MovieIdAlreadyExistsException : MovieException
	{
		public MovieIdAlreadyExistsException(Guid movieId, string message, Exception innerException = null) : base(message, innerException)
		{
			ExtraDetails.Add("MovieId", movieId.ToString());
		}

		public override string UniqueErrorCode => "MOVIE_ALREADY_EXISTS";
	}

	/// <summary>
	/// Throw this exception when movie already exists
	/// </summary>
	public class MovieIdNotExistsException : MovieException
	{
		public MovieIdNotExistsException(Guid movieId, string message, Exception innerException = null) : base(message, innerException)
		{
			ExtraDetails.Add("MovieId", movieId.ToString());
		}

		public override string UniqueErrorCode => "MOVIE_NOT_EXISTS";
	}


}
