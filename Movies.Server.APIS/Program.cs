using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.GrainClients;
using Orleans;
using Orleans.Hosting;

namespace Movies.Server.APIS
{
	public class Program
    {

		private string invariant = "System.Data.SqlClient"; // for Microsoft SQL Server
		private string connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;";

		public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
			.UseOrleans((ctx, siloBuilder) =>
			{
				siloBuilder.UseLocalhostClustering();
				//siloBuilder.use("LocalStuff",
				//	config =>
				//	{
				//		config.ConnectionString = "Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;";
				//		config.UseJsonFormat = true;
				//	});
				siloBuilder.AddMemoryGrainStorageAsDefault();
				siloBuilder.ConfigureApplicationParts(parts => parts
							.AddApplicationPart(typeof(MovieGrainClient).Assembly)
							.WithReferences());
				
			})						
			.ConfigureLogging(logging => logging.AddConsole());			
    }
}
