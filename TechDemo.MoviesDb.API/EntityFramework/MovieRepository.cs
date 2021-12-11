using TechDemo.MoviesDb.EntityFrameworkCore;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Movies.Entities;
using Microsoft.EntityFrameworkCore;

namespace TechDemo.MoviesDb.API.EntityFramework
{
	public class MovieRepository : AbstractEFDbContextEntityRepo<Movie, TechDemoEntityContext>, IEntityRepo<Movie>
	{
		public MovieRepository(TechDemoEntityContext context) : base(context)
		{
		}
	
	}
}
