using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekTest.Models;

namespace GeekTest.Controllers
{
    public class UserResultsController : Controller
    {
        private ResultsContext db = new ResultsContext();

        //
        // GET: /UserResults/

        public ActionResult Index()
        {
            ViewBag.Title = "Результаты прохождения тестов";
            return View(db.results.ToList());
        }
    }
}