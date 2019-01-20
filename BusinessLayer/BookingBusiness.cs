using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using DomainModel;


namespace BusinessLayer
{
    public class BookingBusiness: IBookingBusiness
    {
        public List<BookingDomainModel> LoadBookings()
        {
            var bookings = BookingDataAccess.LoadBookings();
            return bookings;
        }

        public BookingDomainModel GetBooking(int id)
        {
            return BookingDataAccess.GetBooking(id);
        }

        public int CreateBooking(int bookingId, string name, string phone, string email, DateTime checkInDate, DateTime checkOutDate)
        {
            BookingDomainModel booking = new BookingDomainModel() {
                    BookingId = bookingId,
                    Name = name,
                    Phone = phone,
                    Email = email,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate
            };

            int recordsCreated = BookingDataAccess.CreateBooking(booking);

            return recordsCreated;
        }

        public int CancelBooking(int id)
        {
            return BookingDataAccess.CancelBooking(id);
        }
    }
}