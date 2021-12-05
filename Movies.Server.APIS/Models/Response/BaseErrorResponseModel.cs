namespace Movies.Server.APIS.Models.Response
{
	/// <summary>
	/// Represents the default error response model
	/// </summary>
	public class BaseErrorResponseModel
	{
		/// <summary>
		/// The unique request id (can be used to corelate back to the logs)
		/// </summary>
		public string UniqueRequestId { get; set; }

		/// <summary>
		/// Unique error code, which would halp any front end applicaiton know EXACTLY the problem source
		/// </summary>
		public string UniqueErrorCode { get; set; }

		/// <summary>
		/// Friendly error message
		/// </summary>
		public string ErrorMessage { get; set; }
	}
}
