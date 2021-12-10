namespace TechDemo.MoviesDb.Core.DbEntities
{
	/// <summary>
	/// This is the generic Entity Repository
	/// </summary>
	public interface IEntityRepo<TEntity> where TEntity : IEntity
	{
		/// <summary>
		/// Return all entities
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Get entity by primary key
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> GetByKeyAsync(int id, CancellationToken cancellationToken);

		/// <summary>
		/// Create a new entity
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

		/// <summary>
		/// Update Entity
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

		/// <summary>
		/// Delete Entity
		/// </summary>
		/// <param name="key"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task DeleteAsync(int id, CancellationToken cancellationToken);

	}
}
