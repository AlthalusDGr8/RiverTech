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
			Field<ListGraphType<MovieGraphType>>("movies_all", "Returns a list of all movies", resolve: context =>   _entityRepo.GetAllAsync());
			
			//get movie by id
			Field<MovieGraphType>("movies_by_id", "Returns a single movie",
				new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Movie Id"}),
					context =>   _entityRepo.GetByKeyAsync(long.Parse(context.Arguments["id"].Value.ToString())));
			
		//	// Get top rated movies
		//	Field<MovieGraphType>("top_rated_movies", "Returns a List of the top most rated movies",
		//		new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "listCount", Description = "The number of elements to return" }),
		//			context => _entityRepo.GetAllAsync().Result.OrderBy(x => x.CriticRating).Take(int.Parse(context.Arguments["listCount"].Value.ToString())));					
		//}
	}
}
