using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BookingSystem.Models;
using DomainModel;

namespace BookingSystem.Tests
{
    [SetUpFixture]
    public class Config
    {
        [OneTimeSetUp]
        public void Setup()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BookingViewModel, DomainModel.BookingDomainModel>();
            });
        }
    }
}
