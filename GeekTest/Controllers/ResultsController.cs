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

        public ActionResult Index(int index)
        {
            ViewBag.Index = index;
            ViewBag.Title = "Результаты теста";
            ViewBag.TestName = Methods.GetTitle(index);
            var Date = DateTime.Now;
            ViewBag.Date = Date;
            return View();
        }

    }
}
