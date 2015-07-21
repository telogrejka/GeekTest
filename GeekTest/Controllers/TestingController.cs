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
        static int [] QuestionsIDs;     //Массив ID-шников вопросов
        static int[] answersArray = new int[21];     //Массив ответов (отсчет начинается с 1) костыль :(
        static int testNum = 0;
        static int questionsCount = 0;
        //
        // GET: /Testing/

        public ActionResult Index(int index)
        {
            testNum = index;
            var test = (from t in db.tests
                        where t.id == testNum
                        select t);
            questionsCount = test.First().questionsCount;
            ViewBag.Duration = test.First().duration;

            ViewBag.Title = Methods.GetTitle(index);
            ViewBag.Index = index;

            QuestionsIDs = (from q in db.questions
                            where q.parent_test == index
                            orderby Guid.NewGuid()
                            select q.id).Take(questionsCount).ToArray<int>();

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
        
        public void setAnswer(int id, int answerID)
        {
            answersArray[id] = answerID;
        }

        public ActionResult Finish()
        {
            TempData["index"] = testNum;
            TempData["QuestionsIDs"] = QuestionsIDs;
            TempData["answersArray"] = answersArray;
            TempData["questionsCount"] = questionsCount;
            return Redirect("../Results/Index");
        }               
    }
}
