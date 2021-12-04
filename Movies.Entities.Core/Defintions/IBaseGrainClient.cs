namespace Movies.Entities.Core.Defintions
{
	public interface IBaseGuidGrainClient<T, TNewTDTO> where TNewTDTO : IBaseNewModelDTO
	{
		/// <summary>
		/// Returns an object based on its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<T> Get(Guid id);

		/// <summary>
		/// Creates a new T Object
		/// </summary>
		/// <typeparam name="TNewTDTO">The DTO type used to create a new T</typeparam>
		/// <param name="newModelDataTransferObject">The actual instance</param>
		/// <returns></returns>
		Task<Guid> Set(TNewTDTO newModelDataTransferObject);
	}
}
