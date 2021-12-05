using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Movies.CentralCore.Exceptions;
using Movies.Entities.Movie.Definition;
using Movies.GrainClients;
using Movies.Server.APIS.Models.Response;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace Movies.Server.APIS
{
	public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddSingleton<IMovieGrainClient, MovieGrainClient>();

			//Added swagger elements for eaiser 
			services.AddMvcCore().AddApiExplorer();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tech Demo", Version = "v1" });
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {

			IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

			//I have built this custom error handler that will enable us to present outwards a standard format for error
			// It will also help return specifc error codes based on what we want to signal back
			// Here we would also use this to log any issues at this point
			app.UseExceptionHandler(errorHander =>
			{
				errorHander.Run(
					async context =>
					{
						var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
						var exception = errorFeature.Error;

						// ALl exceptions follow standard output
						BaseErrorResponseModel errorModel = new BaseErrorResponseModel() { ErrorMessage = exception.Message, UniqueRequestId = context.TraceIdentifier };

						// If it is an exception that is ours, then handle one way and present our unqiue error code
						if (exception is CentralCoreException coreException)
						{
							errorModel.UniqueErrorCode = coreException.UniqueErrorCode;
							context.Response.StatusCode = (int)HttpStatusCode.Conflict;
						}
						else
						{
							// Otherwise its an unhandled system error 
							errorModel.UniqueErrorCode = "INTERNAL_ERROR";
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						}

						context.Response.ContentType = "application/json";
						await context.Response.WriteAsync(JsonSerializer.Serialize(errorModel));
					}
					);
			});


			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

			//swagger for easier viewing of stuff
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("v1/swagger.json", "Tech Demo");
			});
			
			PrepareDatabase(config);			
		}
		
		private void PrepareDatabase(IConfiguration config)
		{			 
			if (bool.Parse(config["DbConnectivity:CreateDbOnStartup"]))
			{
				// Create the Database
				ExecuteScripts(new string[] { $"CREATE DATABASE {config["DbConnectivity:DatabaseInstanceName"]}" }, config["DbConnectivity:DBConnectionString"]);
				
				var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
				var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
				var dirPath = Path.GetDirectoryName(codeBasePath);
				var sqlFilesPath = $"{dirPath}\\Setup";

				if (Directory.Exists(sqlFilesPath))
				{
					foreach (string file in Directory.EnumerateFiles(sqlFilesPath, "*.sql"))
					{
						string contents = File.ReadAllText(file);
						ExecuteScripts(contents.Split("GO", StringSplitOptions.RemoveEmptyEntries), config["DbConnectivity:FullConnString"]);
					}
				}
			}
		}

		private void ExecuteScripts(string[] scripts, string connectionString, SqlParameterCollection parameters = null)
		{
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				foreach (var script in scripts)
				{
					if (string.IsNullOrWhiteSpace(script)) continue;

					using (SqlCommand sqlCommand = new SqlCommand(script, sqlConnection))
					{
						if (parameters != null)
						{
							foreach (var item in parameters)
							{
								sqlCommand.Parameters.Add(item);
							}
						}

						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}



	}
}
