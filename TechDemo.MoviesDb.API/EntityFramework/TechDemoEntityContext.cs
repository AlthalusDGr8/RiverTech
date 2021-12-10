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
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Genre>().ToTable("LK_Genre", "Movies");
			//modelBuilder.Entity<Genre>().Property(e => e.Code).HasColumnType("NCHAR").HasMaxLength(10);
			//modelBuilder.Entity<Genre>().Property(e => e.Description).HasColumnType("Description").HasMaxLength(20);			
		}


	}
}
