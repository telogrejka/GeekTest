using GeekTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GeekTest.Controllers
{
    public class TestingController : Controller
    {
        AnswerContext db = new AnswerContext();
        static int [] QuestionsIDs;     //Массив ID-шников вопросов
        static int[] answersArray;      //Массив ответов (отсчет начинается с 1)
        static int testNum = 0;
        static int questionsCount = 0;
        //
        // GET: /Testing/

        public ActionResult Index(int index)
        {
            testNum = index;
            
            GetTestInfo();
            GetQuestions();

            return View();
        }

        private void GetQuestions()
        {
            QuestionsIDs = (from q in db.questions
                            where q.parent_test == testNum
                            orderby Guid.NewGuid()
                            select q.id).Take(questionsCount).ToArray<int>();

            ViewBag.QuestionsIDs = QuestionsIDs;
            ViewBag.QuestionsCount = QuestionsIDs.Length;
        }

        private void GetTestInfo()
        {
            var test = (from t in db.tests
                        where t.id == testNum
                        select t);
            questionsCount = test.Single().questionsCount;
            answersArray = new int[questionsCount + 1];
            ViewBag.Duration = test.Single().duration;
            ViewBag.Title = Methods.GetTitle(testNum);
            ViewBag.Index = testNum;
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
