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
				siloBuilder.AddAdoNetGrainStorage("Default",
					config =>
					{
						config.Invariant = "System.Data.SqlClient";
						config.ConnectionString = "Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=Test_Orleans;";
						config.UseJsonFormat = true;
					});
				siloBuilder.AddMemoryGrainStorageAsDefault();
				siloBuilder.ConfigureApplicationParts(parts => parts
							.AddApplicationPart(typeof(MovieGrainClient).Assembly)
							.WithReferences());
				
			})						
			.ConfigureLogging(logging => logging.AddConsole());			
    }
}
