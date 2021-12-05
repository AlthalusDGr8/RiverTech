namespace Movies.CentralCore.Repository
{
	/// <summary>
	/// Base Interface for all Lookup fields
	/// </summary>
	public interface ILookupEntity
	{
		/// <summary>
		/// DB ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Unique Code
		/// </summary>
		public string Code { get; set; }
	}
}
