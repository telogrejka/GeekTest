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
        private TestContext db = new TestContext();

        private void PopulateTestsDropDownList(object selectedQuestion = null)
        {
            var query = (from r in db.results
                         orderby r.id
                         select r).GroupBy(x => x.test_name).Select(y => y.FirstOrDefault()).ToList();

            ViewBag.test_name = new SelectList(query, "test_name", "test_name", selectedQuestion);
        }

        public ActionResult Index()
        {
            PopulateTestsDropDownList();
            ViewBag.Title = "Результаты прохождения тестов";
            return View(db.results.ToList());
        }

        public ActionResult userResults(string userName)
        {
            var model = (from r in db.results
                                       where r.user_name == userName
                                       select r).ToList();
            ViewBag.Title = "Результаты пользователя " + userName;

            return View("userResults", model);
        }

        public ActionResult testResults(string testName)
        {
            var model = (from r in db.results
                         where r.test_name.Equals(testName)
                         select r).ToList();
            ViewBag.Title = "Результаты по тесту " + testName;

            return View("testResults", model);
        }

        public ActionResult whoPassed()
        {
            var model = (from r in db.results
                         where r.point >= 75
                         select r).ToList();
            ViewBag.Title = "Пройденные";

            return View("whoPassed", model);
        }

        public ActionResult dateResults(DateTime firstDate, DateTime secondDate)
        {
            var model = (from r in db.results
                         where (r.date >= firstDate && r.date <= secondDate)
                         select r).ToList();
            ViewBag.Title = "Отчет с " + firstDate.ToString("dd.MM.yyyy") + " по " + secondDate.ToString("dd.MM.yyyy");

            return View("dateResults", model);
        }

        public ActionResult topResults(int topNumber)
        {
            //Очищаем таблицу рейтингов
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [ratings]");

            //Получаем список пользователей
            List<results> result = (from r in db.results
                             select r).GroupBy(x => x.user_name).Select(y => y.FirstOrDefault()).ToList();
            List<string> userNames = new List<string>();
            foreach(var item in result)
            {
                userNames.Add(item.user_name);
            }

            foreach(var item in userNames)
            {
                //Получаем список результатов конкретного юзера
                List<results> userResults = db.results.Where(r => r.user_name == item).ToList();
                //Считаем его рейтинг
                double rating = 0;
                foreach (var res in userResults)
                {
                    rating += res.point;
                }
                //Добавляем запись в таблицу рейтингов
                var userRating = new ratings();
                userRating.userName = item;
                userRating.rating = rating / userResults.Count;
                db.ratings.Add(userRating);
            }
            
            db.SaveChanges();

            //Выбираем топ N
            var model = (from r in db.ratings
                       orderby r.rating descending
                       select r).Take(topNumber);

            ViewBag.Title = "Топ " + topNumber;

            return View("topResults", model);
        }

    }
}