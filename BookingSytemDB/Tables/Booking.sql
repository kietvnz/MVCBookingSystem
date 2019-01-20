CREATE TABLE [dbo].[Booking]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BookingId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL, 
    [CheckInDate] DATETIME NOT NULL, 
    [CheckOutDate] DATETIME NOT NULL, 
    [IsCancelled] BIT NOT NULL DEFAULT(0), 
    [CancelledDate] DATETIME NULL
)
