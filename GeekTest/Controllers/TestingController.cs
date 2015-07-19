using GeekTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GeekTest.Controllers
{
    public class TestingController : Controller
    {
        AnswerContext db = new AnswerContext();
        static int [] QuestionsIDs;
        //
        // GET: /Testing/

        public ActionResult Index(int index)
        {
            ViewBag.Title = Methods.GetTitle(index);
            ViewBag.Index = index;
            ViewBag.Duration = (from t in db.tests
                                where t.id == index
                                select t.duration).First();

            QuestionsIDs = (from q in db.questions
                            where q.parent_test == index
                            orderby Guid.NewGuid()
                            select q.id).Take(2).ToArray<int>();

            ViewBag.QuestionsIDs = QuestionsIDs;
            
            ViewBag.QuestionsCount = QuestionsIDs.Length;
            return View();
        }

        public PartialViewResult GetQuestion(int parentQuestion)
        {
            List<answers> model = (from a in db.answers
                                   where a.parent_question == parentQuestion
                                   select a).ToList();
            
            return PartialView("_Answers", model);
        }

    }
}
