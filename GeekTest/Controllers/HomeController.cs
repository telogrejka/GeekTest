using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Сервис онлайн тестирования GeekTest";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";

            return View();
        }
    }
}
