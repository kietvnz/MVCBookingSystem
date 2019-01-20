using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingSysModels = BookingSystem.Models;
using DataLibModels = DomainModel;

namespace BookingSystem.App_Start
{
    public class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BookingSysModels.BookingViewModel, DataLibModels.BookingDomainModel>();
            });
        }
    }
}