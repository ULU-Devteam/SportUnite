using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using SportUnite.BLL;
using SportUnite.BLL.Abstract;
using SportUnite.BLL.Concrete;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace SportUnite.API
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			var setup = new SetupBll(services, Configuration);
			setup.DependencyInjection();

			services.AddTransient<ISportComplexAccess, SportComplexAccess>();
			services.AddTransient<IInvoiceAccess, InvoiceAccess>();
			services.AddTransient<IOrderAccess, OrderAccess>();
			services.AddTransient<IReservationAccess, ReservationAccess>();
			services.AddTransient<ISportAccess, SportAccess>();
			services.AddTransient<ISportComplexAccess, SportComplexAccess>();
			services.AddTransient<IBankAccountAccess, BankAccountAccess>();
			services.AddTransient<IEventAccess, EventAccess>();

			services.AddCors();

			// Add framework services.
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});

			// Register the Swagger generator
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Title = "SportUnite API",
					Version = "v1",
					Description = "SportUnite ASP.NET Core 1.1 RESTful Web API"
				});

				//Enable filters provided by Swashbuckle.AspNetCore.Examples
				c.OperationFilter<DescriptionOperationFilter>();
				c.OperationFilter<ExamplesOperationFilter>();
				c.OperationFilter<AuthorizationInputOperationFilter>();

				// Set the comments path for the Swagger JSON and UI.
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				var xmlPath = Path.Combine(basePath, "API.xml");
				c.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
				builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
	
				);

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "SportUnite API V1");
			});

			app.UseMvc();
		}
	}
}
