CREATE PROCEDURE [dbo].[Booking_SelectById]
	@Id int
AS
	Select Id, 
		BookingId, 
		Name, 
		Phone, 
		Email, 
		CheckInDate, 
		CheckOutDate, 
		IsCancelled, 
		CancelledDate
    from dbo.Booking (nolock)
	where Id = @Id;

RETURN 0
