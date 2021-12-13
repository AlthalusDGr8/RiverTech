using GraphQL.Types;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.GraphQL.GraphModels;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.GraphQL.GraphQueries
{
	public class MovieQueries : ObjectGraphType
	{
		private readonly IEntityRepo<Movie> _entityRepo;

		public MovieQueries(IEntityRepo<Movie> entityRepo)
		{			
			_entityRepo = entityRepo;
			Name = "Query";
			// Get all movies
			Field<ListGraphType<MovieGraphType>>("movies_all", "Returns a list of all movies",
				resolve:
				//context =>   _entityRepo.GetAllAsync());
				context => _entityRepo.GetAllAsync());

			//get movie by id
			Field<MovieGraphType>("movies_by_id", "Returns a single movie",
				new QueryArguments(new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "id", Description = "Movie Id"}),
					resolve: context =>   _entityRepo.GetByKeyAsync(long.Parse(context.Arguments["id"].Value.ToString())));
			
			// Get top rated movies
			Field<MovieGraphType>("top_rated_movies", "Returns a List of the top most rated movies",
				new QueryArguments(
					new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "skip", Description = "recs to skip" },
					new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "take", Description = "recs to take" }
					),
					resolve: 
						context => _entityRepo.GetByCustomParams(
							null, 
							x => x.OrderBy(y => y.CriticRating), 
							null,
							int.Parse(context.Arguments["skip"].Value.ToString()),
							int.Parse(context.Arguments["take"].Value.ToString())));					
		}
	}
}
