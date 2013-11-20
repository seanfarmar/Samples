namespace Web.Controllers
{
	using Messages.Commands;
	using System;
	using System.Web.Mvc;
	using Models;

	public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
		public ActionResult Create(CreateMessageCommandModel message)
        {
            try
            {
                // TODO: Add insert logic here
				var command = new SayHallo
				{
					Guid = Guid.NewGuid(),
					What = "Say: " + message.Say
				};
				
				MvcApplication.Bus.Send(command);
                
				return RedirectToAction("Created");
            }
            catch (Exception e)
            {
                return View();
            }
        }

		public ActionResult Created()
		{
			return View();
		}
    }
}
