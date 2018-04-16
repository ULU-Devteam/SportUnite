using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportUnite.DAL;
using SportUnite.DAL.Abstract;
using SportUnite.DAL.Concrete;
using SportUnite.DAL.Identity;

namespace SportUnite.BLL
{
	public class SetupBll
	{

		private readonly IConfiguration _config;
		private readonly IServiceCollection _services;

		public SetupBll(IServiceCollection services, IConfiguration config)
		{
			_config = config;
			_services = services;
		}

		public void DependencyInjection()
		{
			_services.AddTransient<IEventRepository, EfEventRepository>();
			_services.AddTransient<IInvoiceRepository, EfInvoiceRepository>();
			_services.AddTransient<IOrderRepository, EfOrderRepository>();
			_services.AddTransient<IReservationRepository, EfReservationRepository>();
			_services.AddTransient<ISportComplexRepository, EfSportComplexRepository>();
			_services.AddTransient<ISportRepository, EfSportRepository>();
			_services.AddTransient<IBankAccountRepository, EfBankAccountRepository>();

			_services.AddDbContext<DatabaseContext>(options =>
				options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

			_services.AddDbContext<IdentityContext>(options =>
				options.UseSqlServer(_config.GetConnectionString("IdentityConnection")));

			_services.AddIdentity<IdentityUser, IdentityRole>(options =>
				{
					options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
					options.Cookies.ApplicationCookie.AutomaticChallenge = true;
					options.Cookies.ApplicationCookie.LoginPath = "/Home/Login";
				})
				.AddEntityFrameworkStores<IdentityContext>().AddEntityFrameworkStores<IdentityDbContext>()
				.AddDefaultTokenProviders();
		}
	}
}
