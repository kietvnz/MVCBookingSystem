CREATE PROCEDURE [dbo].[Booking_Insert]
	@BookingId int = 0,
	@Name nvarchar(50),
	@Phone nvarchar(50),
	@Email nvarchar(100),
	@CheckInDate DateTime,
	@CheckOutDate DateTime
AS
	Insert into dbo.Booking (BookingId, Name, Phone, Email, CheckInDate, CheckOutDate) 
    values (@BookingId, @Name, @Phone, @Email, @CheckInDate, @CheckOutDate);

RETURN 0
