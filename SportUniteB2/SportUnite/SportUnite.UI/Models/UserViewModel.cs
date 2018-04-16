using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SportUnite.UI.Models
{
    public class UserViewModel
    {
        public IdentityUser User { get; set; }
        public IEnumerable<string> Roles { get; set; }

		public PagingViewModel PagingViewModel { get; set; }

	}
}
