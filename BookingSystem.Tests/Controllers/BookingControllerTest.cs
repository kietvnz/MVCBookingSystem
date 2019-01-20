using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit;
using BookingSystem;
using BookingSystem.Models;
using DomainModel;
using BookingSystem.Controllers;
using NUnit.Framework;
using Moq;
using BusinessLayer;

namespace BookingSystem.Tests.Controllers
{
    [TestFixture]
    public class BookingControllerTest
    {
        [Test]
        public void Index_Returns_A_List_Of_BookingModel()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();

            List<BookingDomainModel> data = new List<BookingDomainModel>();
            data.Add(new BookingDomainModel() { Id = 1, BookingId = 0, Name = "Test", Phone = "123456789", Email = "test@test.com", CheckInDate = DateTime.Parse("2019-01-01 14:00"), CheckOutDate = DateTime.Parse("2019-01-02 14:00"), isCancelled = false, CancelledDate = null});

            bookingBusinessMock.Setup(b => b.LoadBookings()).Returns(data);
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            ViewResult actual = controller.Index();
            List<BookingViewModel> actualModel = (List<BookingViewModel>)actual.Model;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<List<BookingViewModel>>(actualModel);
            Assert.AreEqual(actualModel.Count, data.Count);
            Assert.AreEqual(actualModel[0].Id, data[0].Id);
            Assert.AreEqual(actualModel[0].BookingId, data[0].BookingId);
            Assert.AreEqual(actualModel[0].Name, data[0].Name);
            Assert.AreEqual(actualModel[0].Phone, data[0].Phone);
            Assert.AreEqual(actualModel[0].Email, data[0].Email);
            Assert.AreEqual(actualModel[0].CheckInDate, data[0].CheckInDate);
            Assert.AreEqual(actualModel[0].CheckOutDate, data[0].CheckOutDate);
            Assert.AreEqual(actualModel[0].IsCancelled, data[0].isCancelled);
            Assert.AreEqual(actualModel[0].CancelledDate, data[0].CancelledDate);

            // Make sure we actually called the LoadBookings() method on our mock.
            bookingBusinessMock.Verify(x => x.LoadBookings(), Times.Once());
        }

        [Test]
        public void Index_Returns_3_records()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();

            List<BookingDomainModel> data = new List<BookingDomainModel>();
            data.Add(new BookingDomainModel() { Id = 0, BookingId = 0, Name = "Test", Phone = "123456789", Email = "test@test.com", CheckInDate = DateTime.Parse("2019-01-01 14:00"), CheckOutDate = DateTime.Parse("2019-01-02 14:00"), isCancelled = false });
            data.Add(new BookingDomainModel() { Id = 0, BookingId = 0, Name = "Test2", Phone = "123456781", Email = "test2@test.com", CheckInDate = DateTime.Parse("2019-01-02 14:00"), CheckOutDate = DateTime.Parse("2019-01-03 14:00"), isCancelled = false });
            data.Add(new BookingDomainModel() { Id = 0, BookingId = 0, Name = "Test3", Phone = "123456782", Email = "test3@test.com", CheckInDate = DateTime.Parse("2019-01-03 14:00"), CheckOutDate = DateTime.Parse("2019-01-04 14:00"), isCancelled = false });

            bookingBusinessMock.Setup(b => b.LoadBookings()).Returns(data);
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            ViewResult actual = controller.Index();
            List<BookingViewModel> actualModel = (List<BookingViewModel>)actual.Model;

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actualModel.Count, data.Count);

            // Make sure we actually called the LoadBookings() method on our mock.
            bookingBusinessMock.Verify(x => x.LoadBookings(), Times.Once());
        }

        [Test]
        public void Index()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();
            bookingBusinessMock.Setup(b => b.LoadBookings()).Returns(new List<BookingDomainModel>());
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            ViewResult actual = controller.Index() as ViewResult;
            List<BookingViewModel> actualModel = (List<BookingViewModel>)actual.Model;

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actualModel.Count, 0);

            // Make sure we actually called the LoadBookings() method on our mock.
            bookingBusinessMock.Verify(x => x.LoadBookings(), Times.Once());
        }

        [Test]
        public void Create_A_Booking_And_CreateBooking_Method_Is_Called()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();
            BookingController controller = new BookingController(bookingBusinessMock.Object);
            BookingViewModel booking = new BookingViewModel()
            {
                BookingId = 123,
                Name = "Test",
                Phone = "123456789",
                Email = "test@test.com",
                CheckInDate = DateTime.Parse("2019-01-01 14:00"),
                CheckOutDate = DateTime.Parse("2019-01-02 14:00")
            };

            // Act
            ActionResult actual = controller.Create(booking);

            // Assert
            Assert.IsNotNull(actual);
            bookingBusinessMock.Verify(bookingBusiness => bookingBusiness.CreateBooking(
                It.Is<int>(bookingId => bookingId == 123),
                It.Is<string>(Name => Name == "Test"),
                It.Is<string>(Phone => Phone == "123456789"),
                It.Is<string>(Email => Email == "test@test.com"),
                It.Is<DateTime>(CheckInDate => CheckInDate == DateTime.Parse("2019-01-01 14:00")),
                It.Is<DateTime>(CheckOutDate => CheckOutDate == DateTime.Parse("2019-01-02 14:00"))), Times.Once());
        }

        [Test]
        public void Cancel_A_Booking_And_CancelBooking_Method_Is_Called()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            bookingBusinessMock.Setup(b => b.GetBooking(1)).Returns(new BookingDomainModel() {
                Id = 1,
                BookingId = 123,
                Name = "Test",
                Phone = "123456789",
                Email = "test@test.com",
                CheckInDate = DateTime.Parse("2019-01-01 14:00"),
                CheckOutDate = DateTime.Parse("2040-01-02 14:00"),
                isCancelled = false
            });

            ActionResult actual = controller.Cancel(1);

            // Assert
            Assert.IsNotNull(actual);
            bookingBusinessMock.Verify(bookingBusiness => bookingBusiness.CancelBooking(
                It.Is<int>(id => id == 1)
             ), Times.Once());
        }

        [Test]
        public void Cancel_A_Booking_And_CancelBooking_Method_Is_Not_Called()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            bookingBusinessMock.Setup(b => b.GetBooking(1)).Returns(new BookingDomainModel()
            {
                Id = 1,
                BookingId = 123,
                Name = "Test",
                Phone = "123456789",
                Email = "test@test.com",
                CheckInDate = DateTime.Parse("2019-01-01 14:00"),
                CheckOutDate = DateTime.Parse("2019-01-02 14:00"),
                isCancelled = false
            });

            ActionResult actual = controller.Cancel(1);

            // Assert
            Assert.IsNotNull(actual);
            bookingBusinessMock.Verify(bookingBusiness => bookingBusiness.CancelBooking(
                It.Is<int>(id => id == 1)
             ), Times.Never());
        }

        [Test]
        public void Cancel_A_Booking_When_Id_Is_Not_Found()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            bookingBusinessMock.Setup(b => b.GetBooking(1)).Returns((BookingDomainModel)null);

            ActionResult actual = controller.Cancel(1);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<HttpNotFoundResult>(actual);

            bookingBusinessMock.Verify(bookingBusiness => bookingBusiness.CancelBooking(
                It.Is<int>(id => id == 1)
             ), Times.Never());
        }

        [Test]
        public void Cancel_A_Booking_When_Id_Is_0()
        {
            // Arrange
            Mock<IBookingBusiness> bookingBusinessMock = new Mock<IBookingBusiness>();
            BookingController controller = new BookingController(bookingBusinessMock.Object);

            // Act
            ActionResult actual = controller.Cancel(0);

            // Assert
            Assert.IsNotNull(actual);
            bookingBusinessMock.Verify(bookingBusiness => bookingBusiness.CancelBooking(
                It.Is<int>(id => id == 1)
             ), Times.Never());
        }
    }
}
