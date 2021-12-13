using GraphQL.Types;
using TechDemo.MoviesDb.GraphQL.GraphQueries;

namespace TechDemo.MoviesDb.GraphQL.GraphSchema
{
	public class MovieSchema : Schema
	{
		public MovieSchema(IServiceProvider resolver) : base(resolver)
		{
			Query = (MovieQueries)resolver.GetService(typeof(MovieQueries));
		}
	}
}
