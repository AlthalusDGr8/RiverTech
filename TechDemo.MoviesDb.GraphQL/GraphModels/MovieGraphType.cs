using GraphQL.Types;
using TechDemo.MoviesDb.Movies.Entities;

namespace TechDemo.MoviesDb.GraphQL.GraphModels
{
	public class MovieGraphType : ObjectGraphType<Movie>
    {
		public MovieGraphType()
		{
			Name = "Movie";
			Field(x => x.Id, type: typeof(IdGraphType)).Description("Movie Id");
			Field(x => x.Name).Description("Movie Name");
			Field(x => x.Description).Description("Movie Description");
			Field(x => x.CriticRating).Description("Movie Critic Rating");
			Field(x => x.ImgUrl).Description("Movie Image URL");
			Field(x => x.Length).Description("Movie Length in Minutes");			
		}
    }
}
