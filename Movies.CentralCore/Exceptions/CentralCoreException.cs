namespace Movies.CentralCore.Exceptions
{
	/// <summary>
	/// Base exception for all application
	/// </summary>
	public abstract class CentralCoreException : Exception
    {
		/// <summary>
		/// Includes extra detials that are sent along with the exception
		/// </summary>
		public Dictionary<string,string> ExtraDetails { get; private set; }

		/// <summary>
		/// A unique error code to help identify quickly what the issue could be
		/// </summary>
		public abstract string UniqueErrorCode { get; }

		/// <summary>
		/// Default constructor for any exception
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="innerException">Any Inner Exception</param>
		public CentralCoreException(string message, Exception? innerException = null) : base(message, innerException)	
		{
			ExtraDetails = new Dictionary<string,string>();
		}
    }
}
