CREATE PROCEDURE [dbo].[Booking_SelectAll]
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
    from dbo.Booking (nolock);

RETURN 0
