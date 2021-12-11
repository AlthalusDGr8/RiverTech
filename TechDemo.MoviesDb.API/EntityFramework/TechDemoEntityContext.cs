using Microsoft.EntityFrameworkCore;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.API.EntityFramework
{
	public class TechDemoEntityContext : DbContext
	{
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Movie> Movies { get; set; }

		public TechDemoEntityContext(DbContextOptions<TechDemoEntityContext> options) : base(options)
		{
			Database.EnsureCreated();
			//Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Genre>().ToTable("LK_Genres", "Movies");
			modelBuilder.Entity<Movie>().ToTable("Movies", "Movies")
				.Navigation(e => e.Genres).AutoInclude();	
		}


	}
}
