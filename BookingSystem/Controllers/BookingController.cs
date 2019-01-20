using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingSystem.Models;
using BusinessLayer;
using DomainModel;
using AutoMapper;

namespace BookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private IBookingBusiness bookingBusiness;
        public BookingController(IBookingBusiness bookingRepository)
        {
            this.bookingBusiness = bookingRepository;
        }
        // GET: Booking
        public ViewResult Index()
        {
            ViewBag.Message = "Boooking List";
            var data = bookingBusiness.LoadBookings();

            //Map BookingModel in DataLibrary to BookingModel in MVC project
            List<BookingViewModel> bookings = Mapper.Map<List<BookingViewModel>>(data);
            return View(bookings);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.Message = "Booking Creation";

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = bookingBusiness.CreateBooking(model.BookingId,
                    model.Name,
                    model.Phone,
                    model.Email,
                    model.CheckInDate,
                    model.CheckOutDate);

                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: Booking/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            if (id < 1)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                BookingDomainModel model = bookingBusiness.GetBooking(id);
                DateTime currentTime = DateTime.Now;
                if(model == null)
                {
                    return HttpNotFound();
                }

                //Cancel booking if it hasn't gone past check-out time
                if (model != null && !model.isCancelled && currentTime < model.CheckOutDate)
                {

                    int recordCancelled = bookingBusiness.CancelBooking(id);
                    return RedirectToAction("Index");
                }              
            }

            return RedirectToAction("Index");
        }
    }
}
