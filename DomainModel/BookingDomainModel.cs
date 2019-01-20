using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class BookingDomainModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Boolean isCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
    }
}
