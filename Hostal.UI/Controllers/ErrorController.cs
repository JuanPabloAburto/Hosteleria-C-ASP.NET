using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hostal.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
      
        public ActionResult Unauthorized()
        {
            return View();
        }
        public ActionResult Error()
        {
            ViewBag.Error = Session["Error"];
            ViewBag.Descripcion = Session["Descripcion"];
            return View();
        }
    }
}