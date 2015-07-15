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
    public class AnswerController : Controller
    {
        private AnswerContext db = new AnswerContext();

        //Построение выпадающего списка
        private void PopulateQuestionsDropDownList(object selectedQuestion = null)
        {
            var query = (from d in db.questions
                                    orderby d.id
                                    select d).ToList<questions>();

            ViewBag.parent_question = new SelectList(query, "id", "question", selectedQuestion);
        }

        //
        // GET: /Answer/

        public ActionResult Index()
        {
            return View(db.answers.ToList());
        }

        //
        // GET: /Answer/Details/5

        public ActionResult Details(int id = 0)
        {
            getQuestionByID(id);
            answers answers = db.answers.Find(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        private void getQuestionByID(int id)
        {
            var query = (from d in db.questions where d.id == id select d.question);
            ViewBag.parent_question = query.Single();
        }

        //
        // GET: /Answer/Create

        public ActionResult Create()
        {
            PopulateQuestionsDropDownList();
            return View();
        }

        //
        // POST: /Answer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(answers answers)
        {
            if (ModelState.IsValid)
            {
                db.answers.Add(answers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(answers);
        }

        //
        // GET: /Answer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PopulateQuestionsDropDownList();
            answers answers = db.answers.Find(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        //
        // POST: /Answer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(answers answers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(answers);
        }

        //
        // GET: /Answer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            answers answers = db.answers.Find(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        //
        // POST: /Answer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            answers answers = db.answers.Find(id);
            db.answers.Remove(answers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}