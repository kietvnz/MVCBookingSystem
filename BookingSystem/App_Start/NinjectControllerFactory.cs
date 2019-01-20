using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using BookingSystem.Models;
using BusinessLayer;

namespace BookingSystem.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        public IKernel Kernel { get; private set; }

        public NinjectControllerFactory(IKernel kernel)
        {
            this.Kernel = kernel;
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController controller = null;

            if (controllerType != null)
                controller = (IController)Kernel.Get(controllerType);

            return controller;
        }

        public void AddBindings()
        {
            Kernel.Bind<IBookingBusiness>().To<BookingBusiness>();
        }
    }
}