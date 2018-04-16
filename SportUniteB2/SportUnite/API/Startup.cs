using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportUnite.BLL;
using SportUnite.BLL.Abstract;
using SportUnite.BLL.Concrete;

namespace API
{
    public class Startup
    {
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
