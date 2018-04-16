using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
