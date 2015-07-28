using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPAQoRdotnet.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

		public ActionResult Search()
		{
			return View();
		}

		public ActionResult Grid()
		{
			return View();
		}

		public ActionResult SampleGoogle()
		{
			return View();
		}
        
        
        // TODO    - this is old school bad to do ...
        // FormCollection is nasty, ... need to move to MODEL binding 
        // Also should move to API as well 
        [HttpPost]
        public ActionResult GridResults(FormCollection formCollection)
        {

            foreach (string formData in formCollection)
            {
                ViewData[formData] = formCollection[formData];
            }

            var metrics = ViewData["selectMetrics"].ToString();
            var kit = ViewData["kit"].ToString();
            var start = ViewData["start"].ToString();

            /////////


            foreach (var key in formCollection.AllKeys)
            {
                var value = formCollection[key];
                // etc.
            }

            foreach (var key in formCollection.Keys)
            {
                var value = formCollection[key.ToString()];
                // etc.
            }





            //
            return View();

        }
	}
}
