using TechDemo.MoviesDb.EntityFrameworkCore;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.API.EntityFramework	
{
	public class GenreRepository : AbstractEFDbContextEntityRepo<Genre, TechDemoEntityContext>
	{
		public GenreRepository(TechDemoEntityContext context) : base(context)
		{
		}
	}
}
