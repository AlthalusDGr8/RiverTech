using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Movies.Entities.Movie.DataModels.Movies;
using Movies.Entities.Movie.Definition;
using System;

namespace Movies.Server.APIS.GraphQL
{
	public class MovieSchema : Schema
	{
		public MovieSchema(IServiceProvider provider) : base(provider)
		{
			Query = provider.GetRequiredService<MovieGraphQuery>();
			Mutation = provider.GetRequiredService<MovieGraphMutation>();
		}
	}

	public class MovieGraphQuery : ObjectGraphType
	{
		public MovieGraphQuery(IMovieGrainClient movieGrainClient)
		{
			Name = "MovieQueries";
			Field<MovieDataModelGraphType>("movies",
				arguments: new QueryArguments(new QueryArgument<StringGraphType>
				{
					Name = "id",
					DefaultValue = Guid.Empty,
					Description = "The primary Key of the movie"

				}),
				resolve: ctx => movieGrainClient.Get(Guid.Parse(ctx.Arguments["id"].ToString()))
			);
		}
	}

	public class MovieGraphMutation : ObjectGraphType
	{
		public MovieGraphMutation()
		{
		}
	}
}
