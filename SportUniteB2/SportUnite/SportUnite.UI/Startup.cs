using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportUnite.BLL;
using SportUnite.BLL.Abstract;
using SportUnite.BLL.Concrete;
using SportUnite.DAL.Identity;

namespace SportUnite.UI
{
	public class Startup
	{

		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var setup = new SetupBll(services, Configuration);
			setup.DependencyInjection();



			services.AddTransient<IEventAccess, EventAccess>();
			services.AddTransient<IInvoiceAccess, InvoiceAccess>();
			services.AddTransient<IOrderAccess, OrderAccess>();
			services.AddTransient<IReservationAccess, ReservationAccess>();
			services.AddTransient<ISportAccess, SportAccess>();
			services.AddTransient<ISportComplexAccess, SportComplexAccess>();

			// Add framework services.
			services.AddMvc();
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
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseIdentity();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Admin}/{action=Dashboard}/{id?}");
			});

			SeedDataIdentity.EnsurePopulated(app);
		}
	}
}
