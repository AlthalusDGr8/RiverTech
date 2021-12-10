using Microsoft.EntityFrameworkCore;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Core.Extentions;

namespace TechDemo.MoviesDb.EntityFrameworkCore
{
	public class AbstractEFDbContextEntityRepo<TEntity, TDbContext> : IEntityRepo<TEntity>
		where TDbContext : DbContext
		where TEntity : class, IEntity, new()
	{
		protected readonly TDbContext Context;

		private DbSet<TEntity> GetDbSet()
		{
			return Context.Set<TEntity>();
		}

		public AbstractEFDbContextEntityRepo(TDbContext context)
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
				{
					entity.CopyPropertiesTo(found, "Id");
				}
				else
				{
					set.Attach(entity);
					entry.State = EntityState.Modified;
				}

			}

			await Context.SaveChangesAsync(cancellationToken);

			return entry.Entity;
		}

		public async Task DeleteAsync(int id, CancellationToken cancellationToken)
		{
			var entity = new TEntity
			{
				Id = id
			};

			var entry = Context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				var set = GetDbSet();

				var found = await set.FindAsync(id, cancellationToken);

				if (found != null)
				{
					set.Remove(found);
				}
				else
				{
					set.Attach(entity);
					entry.State = EntityState.Deleted;
				}

			}

			await Context.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			var list = await GetDbSet().ToListAsync(cancellationToken);

			return list;
		}

		public Task<TEntity> GetByKeyAsync(int id, CancellationToken cancellationToken)
		{
			return GetDbSet().FindAsync(new object[] { id }, cancellationToken).AsTask();
		}
	}
}
