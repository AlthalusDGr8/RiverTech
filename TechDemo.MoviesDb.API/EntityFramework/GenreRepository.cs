using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.EntityFrameworkCore;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.API.EntityFramework	
{
	public class GenreRepository : AbstractEFDbContextEntityRepo<Genre, TechDemoEntityContext>, IEntityRepo<Genre>
	{
		public GenreRepository(TechDemoEntityContext context) : base(context)
		{
		}
	}
}
