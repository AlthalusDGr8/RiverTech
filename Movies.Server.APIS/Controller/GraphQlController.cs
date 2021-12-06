using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Movies.Server.APIS.GraphQL;
using Movies.Entities.Movie.Definition;

namespace Movies.Server.APIS.Controller
{
	public class GraphQlQuery
	{
		public string OperationName { get; set; }
		public string NamedQuery { get; set; }
		public string Query { get; set; }
		public Inputs Variables { get; set; }
	}


	[Route(Startup.GraphQlPath)]
	[Route(Startup.CustomGraphQlPath)]
	public class GraphQlController : ControllerBase
	{
		private readonly IMovieGrainClient _movieGrainClient;
		public GraphQlController(IMovieGrainClient movieGrainClient)
		{
			_movieGrainClient = movieGrainClient;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
		{
			var schema = new Schema { Query = new MovieGraphQuery(_movieGrainClient) };

			var result = await new DocumentExecuter().ExecuteAsync(x =>
			{
				x.Schema = schema;
				x.Query = query.Query;
				x.Inputs = query.Variables;
			});

			if (result.Errors?.Count > 0)
			{
				return BadRequest();
			}

			return Ok(result);
		}
	}
}
