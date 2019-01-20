using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace BookingSystem.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Booking ID")]
        [Required(ErrorMessage = "You need to give us a booking ID")]
        public int BookingId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You need to give us a name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You need to give us an email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You need to give us a phone number")]
        public string Phone { get; set; }

        
        [Display(Name = "Check-in Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to give us a check-in date")]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "Check-out Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to give us a check-out date")]
        [GreaterThan("CheckInDate", ErrorMessage = "Check-out date should be greater than check-in date")]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Cancelled")]
        [DataType(DataType.DateTime)]
        public Boolean IsCancelled { get; set; }

        [Display(Name = "Cancelled Date")]
        [DataType(DataType.DateTime)]
        public DateTime? CancelledDate { get; set; }
    }
}