using System.Linq.Expressions;

namespace TechDemo.MoviesDb.Core.DbEntities
{
	/// <summary>
	/// This is the generic Entity Repository
	/// </summary>
	public interface IEntityRepo<TEntity> where TEntity : IEntity
	{
		/// <summary>
		/// Returns a list of entities based on custom filters
		/// </summary>
		/// <param name="filter">By what to filter out crtieria</param>
		/// <param name="orderBy">Order by a specific order</param>
		/// <param name="includeProperties">What properties to include</param>
		/// <param name="skip">How many records to skip</param>
		/// <param name="take">How many records tor eturn</param>		
		Task<IEnumerable<TEntity>> GetByCustomParams(
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>,
			IOrderedQueryable<TEntity>>? orderBy = null,
			string? includeProperties = null,
			int? skip = null,
			int? take = null,
			CancellationToken cancellationToken = default);


		/// <summary>
		/// Return all entities
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Get entity by primary key
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> GetByKeyAsync(long id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a new entity
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Update Entity
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
		
	}
}
