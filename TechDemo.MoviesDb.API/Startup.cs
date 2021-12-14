using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using TechDemo.MoviesDb.API.Models.Response;
using TechDemo.MoviesDb.Caching.Memory;
using TechDemo.MoviesDb.Core.Caching;
using TechDemo.MoviesDb.Core.DbEntities;
using TechDemo.MoviesDb.Core.Exceptions;
using TechDemo.MoviesDb.EntityFrameworkCore.Context;
using TechDemo.MoviesDb.EntityFrameworkCore.Repos;
using TechDemo.MoviesDb.GraphQL.GraphSchema;
using TechDemo.MoviesDb.Movies.Definitions;
using TechDemo.MoviesDb.Movies.Managers;
using TechDemo.MoviesDb.Orleans.Managers;

namespace TechDemo.MoviesDb.API
{
	public class Startup
    {
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		
		public IConfiguration Configuration { get; }


		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {			
			// Cache Manager
			services.AddSingleton<ICacheManager, MemoryCacheManager>();
			
			// Database stuff
			services.AddDbContext<TechDemoEntityContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("TechDemo")));

			// Genreic registration for entity repo
			services.AddScoped(typeof(IEntityRepo<>), typeof(AbstractEFDbContextEntityRepo<>));

			// Managers
			services.AddTransient<IMovieManger, MovieManager>();
			services.AddTransient<IGenreManager, GenreManager>();
			services.AddTransient<IUserMovieRatingManager, UserMovieRatingManager>();

			//graphql stuff			
			services.AddScoped<MovieSchema>();
			services.AddGraphQL((options, provider) =>
			{
				var logger = provider.GetRequiredService<ILogger<Startup>>();
				options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException);
			})
			.AddGraphTypes(typeof(MovieSchema), ServiceLifetime.Scoped)
			// Add required services for GraphQL request/response de/serialization
			.AddSystemTextJson();						
			services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);


			//Added swagger elements for eaiser debugging and demo
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
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			////I have built this custom error handler that will enable us to present outwards a standard format for error
			//// It will also help return specifc error codes based on what we want to signal back
			//// Here we would also use this to log any issues at this point
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


			// use HTTP middleware for ChatSchema at default path /graphql
			app.UseGraphQL<MovieSchema>();

			// use GraphiQL middleware at default path /ui/graphiql with default options
			app.UseGraphQLGraphiQL();

			app.UseGraphQLPlayground();

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
		}
    }
}
