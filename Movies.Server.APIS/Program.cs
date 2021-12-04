using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.Entities.Implementations;
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
				siloBuilder.AddMemoryGrainStorageAsDefault();
				siloBuilder.ConfigureApplicationParts(parts => parts
							.AddApplicationPart(typeof(MovieManagerGrain).Assembly)
							.WithReferences());
				
			})						
			.ConfigureLogging(logging => logging.AddConsole());			
    }
}