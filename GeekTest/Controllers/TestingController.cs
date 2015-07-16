using GeekTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekTest.Controllers
{
    public class TestingController : Controller
    {
        //
        // GET: /Testing/

        public ActionResult Index(int index)
        {
            GoTest(index);

            return View();
        }

        private void GoTest(int index)
        {
            ViewBag.Title = Methods.GetTitle(index);
            
            //Получение данных теста
            AnswerContext db = new AnswerContext();
            //var query = (from q in db.questions
            //             join a in db.answers on new { id = q.id } equals new { id = (int)a.parent_question } into a_join
            //             from a in a_join.DefaultIfEmpty()
            //             where
            //               q.parent_test == index
            //             select new
            //             {
            //                 q.question,
            //                 q.parent_test,
            //                 id = (int?)a.id,
            //                 answer = a.answer,
            //                 parent_question = (int?)a.parent_question,
            //                 //correct_answer = (bool?)a.correct_answer
            //             }).ToArray();

            var Questions = (from q in db.questions
                                 where q.parent_test == index
                             select new { q.id, q.question }).ToDictionary(q => q.id, q => q.question);

            List<List<string>> AnswerList = new List<List<string>>();
            foreach(var q in Questions)
            {
                AnswerList.Add((from a in db.answers
                             where a.parent_question == q.Key
                             select a.answer).ToList<string>()); 
            }

            ViewBag.AnswerList = AnswerList;
            ViewBag.QuestionsList = Questions.Values.ToList<string>();
            ViewBag.Index = index;
            ViewBag.Duration = (from t in db.tests
                                where t.id == index
                                select t.duration).First();
        }
    }
}
