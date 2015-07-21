using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekTest.Models;

namespace GeekTest.Controllers
{
    public class ResultsController : Controller
    {
        //
        // GET: /Results/

        public ActionResult Index()
        {
            ViewBag.Title = "Результат прохождения теста";
            ViewBag.Date = DateTime.Now;
            int[] QuestionsIDs = null;
            int[] userAnswers = null;
            int required = 0;
            int questionsCount = 0;
            AnswerContext db = new AnswerContext();
            List<List<answers>> answersModel = new List<List<answers>>();

            if(TempData["index"] != null)
            {
                ViewBag.Index = (int)TempData["index"];
                ViewBag.TestName = Methods.GetTitle((int)TempData["index"]);
            }
            if (TempData["questionsCount"] != null)
            {
                questionsCount = (int)TempData["questionsCount"];
                required = Convert.ToInt32(questionsCount * 0.75);
                ViewBag.questionsCount = questionsCount;
                ViewBag.required = required;
            }
            if (TempData["QuestionsIDs"] != null)
            {
                QuestionsIDs = TempData["QuestionsIDs"] as int[];
                List<string> questionList = new List<string>();
                for(int i = 0; i < QuestionsIDs.Length; i++)
                {
                    var tmp = QuestionsIDs[i];
                    questionList.Add((from q in db.questions
                                        where q.id == tmp
                                          select q).First().question);
                }
                ViewBag.questionList = questionList;
                    
            }
            if (TempData["answersArray"] != null)
            {
                userAnswers = TempData["answersArray"] as int[];
                ViewBag.userAnswers = userAnswers;

                for (int i = 0; i < QuestionsIDs.Length; i++)
                {
                    var tmp = QuestionsIDs[i];
                    answersModel.Add((from a in db.answers
                                            where a.parent_question == tmp
                                            select a).ToList<answers>());
                }

                int trueCount = 0;
                for (int i = 1; i < userAnswers.Length; i++)
                {
                    var tmp = userAnswers[i];
                    if (tmp != 0 &&
                            (from a in db.answers
                                where a.id == tmp
                                select a).Single().correct_answer)
                    {
                        trueCount++;
                    }
                }
                ViewBag.TrueAnswers = trueCount;
            
                if (trueCount >= required)
                    ViewBag.IsPassed = true;
                else ViewBag.IsPassed = false;

                List<List<string>> answersList = new List<List<string>>();
                for (int i = 0; i < QuestionsIDs.Length; i++)
                {
                    var tmp = QuestionsIDs[i];
                    answersList.Add((from a in db.answers
                                            where a.parent_question == tmp
                                            select a.answer).ToList<string>());
               }
                ViewBag.answersList = answersList;
            }
            return View(answersModel);
        }

    }
}
