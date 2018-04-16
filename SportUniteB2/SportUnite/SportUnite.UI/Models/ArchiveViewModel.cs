using System.Collections.Generic;
using SportUnite.Domain.Models;


namespace SportUnite.UI.Models
{
    public class ArchiveViewModel
    {
        public IEnumerable<Event> Events;
        public IEnumerable<Hall> Halls;
        public IEnumerable<Invoice> Invoices;
        public IEnumerable<Reservation> Reservations;
        public IEnumerable<Sport> Sports;
        public IEnumerable<SportAttribute> SportAttributes;
        public IEnumerable<SportComplex> SportComplexes;
        public IEnumerable<Order> Orders;

    }
}
