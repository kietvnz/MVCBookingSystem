using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace BusinessLayer
{
    public interface IBookingBusiness
    {
        List<BookingDomainModel> LoadBookings();
        BookingDomainModel GetBooking(int id);
        int CreateBooking(int bookingId, string name, string phone, string email, DateTime checkInDate, DateTime checkOutDate);
        int CancelBooking(int id);
        

    }
}
