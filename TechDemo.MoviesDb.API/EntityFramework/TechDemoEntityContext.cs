using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
			//modelBuilder.Entity<Genre>().ToTable("LK_Genre");

			//modelBuilder.Entity<Movie>().ToTable("Movie", "Movies");
								//.HasMany(g => g.MovieGenres)
								//.WithMany(m => m.MovieGenres)
								//.UsingEntity<Dictionary<string, object>>("MovieGenre",
								//	j => j.HasOne<Genre>()
								//			.WithMany()
								//			.HasForeignKey("GenreId")
								//			.HasConstraintName("FK_MovieGenre_LK_Genre")
								//			,
								//	j => j
								//		.HasOne<Movie>()
								//		.WithMany()
								//		.HasForeignKey("MovieId")
								//		.HasConstraintName("FK_MovieGenre_Movie"));			
		}


	}
}
