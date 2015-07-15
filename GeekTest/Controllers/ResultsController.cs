using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekTest.Controllers
{
    public class ResultsController : Controller
    {
        //
        // GET: /Results/

        public ActionResult Index()
        {
            ViewBag.Title = "Результаты теста";
            return View();
        }

    }
}
