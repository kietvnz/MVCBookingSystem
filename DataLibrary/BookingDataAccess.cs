using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DataAccess
{
    public static class BookingDataAccess
    {
        public static int CreateBooking(BookingDomainModel data)
        {
            string sql = @"dbo.Booking_Insert @BookingId, @Name, @Phone, @Email, @CheckInDate, @CheckOutDate;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<BookingDomainModel> LoadBookings()
        {
            string sql = @"dbo.Booking_SelectAll";

            return SqlDataAccess.LoadData<BookingDomainModel>(sql);
        }

        public static BookingDomainModel GetBooking(int id)
        {
            string sql = @"dbo.Booking_SelectById @id";
            BookingDomainModel model = new BookingDomainModel { Id = id};
            return SqlDataAccess.SingleOrDefault(sql, model);
        }

        public static int CancelBooking(int id)
        {
            string sql = @"dbo.Booking_CancelById @Id, @CancelledDate";

            return SqlDataAccess.SaveData(sql, new { Id = id, CancelledDate = DateTime.Now});
        }
    }
}
