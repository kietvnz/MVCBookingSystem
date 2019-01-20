CREATE PROCEDURE [dbo].[Booking_CancelById]
	@Id int,
	@CancelledDate DateTime
AS
	Update dbo.Booking
	set IsCancelled = 1,
		CancelledDate = @CancelledDate
	where Id = @Id
	and CheckOutDate > @CancelledDate;

RETURN 0
