using Microsoft.EntityFrameworkCore;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.API.EntityFramework
{
	public class TechDemoEntityContext : DbContext
	{
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Movie> Movies { get; set; }
		
		public DbSet<MovieGenre> MovieGenres { get; set; }

		public TechDemoEntityContext(DbContextOptions<TechDemoEntityContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Genre>().ToTable("LK_Genre", "Movies");

			modelBuilder.Entity<MovieGenre>()
				.HasKey(mg => new { mg.MovieId, mg.GenreId});


			modelBuilder.Entity<MovieGenre>()
			.HasOne<Movie>(mov => mov.Movie)
			.WithMany(movGenres => movGenres.MovieGenres)
			.HasForeignKey(movieGenre => movieGenre.MovieId);

			modelBuilder.Entity<MovieGenre>()
			.HasOne<Genre>(genre => genre.Genre)
			.WithMany(movGenres => movGenres.MovieGenres)
			.HasForeignKey(movieGenre => movieGenre.GenreId);
		}


	}
}
