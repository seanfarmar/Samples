using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazon.InternalMessages.Sales;

namespace Amazon.ECommerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            var submitOrderMessage = new SubmitOrder {OrderId = Guid.NewGuid()};

            MvcApplication.Bus.Send(submitOrderMessage);

            ViewBag.Message = "Your app description page. order id: " + submitOrderMessage.OrderId.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
