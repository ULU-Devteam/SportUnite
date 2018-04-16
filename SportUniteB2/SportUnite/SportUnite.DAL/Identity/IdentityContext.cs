using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportUnite.DAL.Identity
{
	public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options) { }
	}
}
