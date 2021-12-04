using Orleans;

namespace Movies.Entities.Core.Defintions
{
	public interface IBaseGrain<T, TNewModel> : IGrainWithGuidKey where TNewModel : IBaseNewModelDTO
	{
		/// <summary>
		/// Get the current instance
		/// </summary>
		/// <returns></returns>
		Task<T> Get();
		
		/// <summary>
		/// Create a new instance
		/// </summary>
		/// <param name="baseNewModelDTO">The DTO required to create a new instance</param>
		/// <returns></returns>
		Task<Guid> Set(TNewModel baseNewModelDTO);
	}
}
