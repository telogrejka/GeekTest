using GeekTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekTest.Controllers
{
    public class BeforeStartController : Controller
    {
        //
        // GET: /BeforeStart/

        public ActionResult Index(int index)
        {
            GoTest(index);
            ViewBag.index = index;
            return View();
        }

        private void GoTest(int index)
        {
            ViewBag.Title = Methods.GetTitle(index);

            QuestionContext db = new QuestionContext();

            int QuestionsCount = (from q in db.questions
                                      where q.parent_test == index 
                                      select q.id).Count();

            ViewBag.QuestionsCount = QuestionsCount;

            int duration = (from t in db.tests
                            where t.id == index
                            select t.duration).First();

            ViewBag.Duration = duration;

            string TestInfo = (from t in db.tests
                                   where t.id == index
                                   select t.test_info).First();

            ViewBag.TestInfo = TestInfo;
        }
    }
}
