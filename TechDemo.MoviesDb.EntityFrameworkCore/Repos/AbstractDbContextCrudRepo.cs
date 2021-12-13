using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Core.Extentions;
using TechDemo.MoviesDb.EntityFrameworkCore.Context;

namespace TechDemo.MoviesDb.EntityFrameworkCore.Repos
{
	public class AbstractEFDbContextEntityRepo<TEntity> : IEntityRepo<TEntity>
		where TEntity : class, IEntity, new()
	{
		protected readonly TechDemoEntityContext Context;

		private DbSet<TEntity> GetDbSet()
		{
			return Context.Set<TEntity>();
		}
		
		public AbstractEFDbContextEntityRepo(TechDemoEntityContext context)
		{
			Context = context;
		}

		/// <summary>
		/// Runs a custom filter based on what you need
		/// </summary>
		/// <param name="filter">The filtering crtieria to use</param>
		/// <param name="orderBy">Order by params</param>
		/// <param name="includeProperties">If there are any specific properties you want to include</param>
		/// <param name="skip">How many record to skip</param>
		/// <param name="take">How many records to return</param>
		/// <returns>A list of T Entities</returns>
		public async Task<IEnumerable<TEntity>> GetByCustomParams(
			Expression<Func<TEntity, bool>>? filter = null,			
			Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>? orderBy = null,
			string? includeProperties = null,
			int? skip = null,
			int? take = null,
			CancellationToken cancellationToken = default)
		{
			includeProperties = includeProperties ?? string.Empty;
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (skip.HasValue)
			{
				query = query.Skip(skip.Value);
			}

			if (take.HasValue)
			{
				query = query.Take(take.Value);
			}

			return await query.ToListAsync(cancellationToken);
		}



		public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			var entry = await GetDbSet().AddAsync(entity, cancellationToken).ConfigureAwait(false);

			await Context.SaveChangesAsync(cancellationToken);

			return entry.Entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			var entry = Context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				var set = GetDbSet();

				var found = await set.FindAsync(new object?[] { entity.Id, cancellationToken }, cancellationToken: cancellationToken);

				if (found != null)
					entity.CopyPropertiesTo(found, "Id");
				else
				{
					set.Attach(entity);
					entry.State = EntityState.Modified;
				}

			}

			await Context.SaveChangesAsync(cancellationToken);

			return entry.Entity;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await GetDbSet().ToListAsync(cancellationToken);			
		}

		public Task<TEntity> GetByKeyAsync(long id, CancellationToken cancellationToken)
		{
			return GetDbSet().FindAsync(new object[] { id }, cancellationToken).AsTask();
		}
	}
}
