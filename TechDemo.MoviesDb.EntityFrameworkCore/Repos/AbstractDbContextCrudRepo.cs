using Microsoft.EntityFrameworkCore;
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

				var found = await set.FindAsync(entity.Id, cancellationToken);

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
			var list = await GetDbSet().ToListAsync(cancellationToken);

			return list;
		}

		public Task<TEntity> GetByKeyAsync(long id, CancellationToken cancellationToken)
		{
			return GetDbSet().FindAsync(new object[] { id }, cancellationToken).AsTask();
		}
	}
}
