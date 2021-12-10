using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

			return Host.CreateDefaultBuilder(args)
					.ConfigureAppConfiguration(builder =>
					{
						builder.Sources.Clear();
						builder.AddConfiguration(configuration);
					})
					.ConfigureWebHostDefaults(webBuilder =>
					{
						webBuilder.UseStartup<Startup>();
					})
					.ConfigureLogging(logging => logging.AddConsole()); ;
		}
	}
}
