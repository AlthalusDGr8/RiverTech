using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using System.Text.Json;
using TechDemo.MoviesDb.Orleans.Grains;

namespace TechDemo.MoviesDb.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			var configuration = new ConfigurationBuilder().AddEnvironmentVariables().AddCommandLine(args).AddJsonFile("appsettings.json").Build();

			JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
			{
				WriteIndented = true,
				NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals
			};



			return Host.CreateDefaultBuilder(args)
				
				// Configuration
				.ConfigureAppConfiguration(builder =>
					{
						builder.Sources.Clear();
						builder.AddConfiguration(configuration);
					})
					// Startup
					.ConfigureWebHostDefaults(webBuilder =>
					{
						webBuilder.UseStartup<Startup>();
					})					
					// Logging
					.ConfigureLogging(logging => logging.AddConsole())

					// Orleans COnfig
					.UseOrleans((ctx, siloBuilder) =>
					{
						siloBuilder.UseLocalhostClustering();
						siloBuilder.AddMemoryGrainStorageAsDefault();
						siloBuilder.ConfigureApplicationParts(parts => parts
									.AddApplicationPart(typeof(UserMovieRatingGrain).Assembly)
									.WithReferences());

					});
		}
	}
}
