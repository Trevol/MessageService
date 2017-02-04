using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageService.Web.Controllers
{
    public class QueueController : Controller
    {
        public ActionResult Queue()
        {
            ViewBag.Title = "Message Queue";

            return View();
        }

        public ActionResult SendMessage()
        {
            ViewBag.Title = "Send new message";

            return View();
        }
    }
}
