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
    public class UsersController : Controller
    {
        private TestContext db = new TestContext();
        
        //
        // GET: /Users/

        public ActionResult Index()
        {
            MakeRolesDic();

            return View(db.UserProfiles.ToList());
        }

        private void MakeRolesDic()
        {
            var userProfiles = db.UserProfiles.ToList();
            var roles = db.roles.ToList();
            var usersInRoles = db.UsersInRoles.ToList();

            Dictionary<int, string> rolesDic = new Dictionary<int, string>();
            foreach (var user in userProfiles)
            {
                var roleId = (from r in usersInRoles
                              where r.UserId == user.UserId
                              select r).Single().RoleId;
                var roleName = (from r in roles
                                where r.RoleId == roleId
                                select r).Single().RoleName;
                rolesDic.Add(user.UserId, roleName);
            }
            ViewBag.rolesDic = rolesDic;
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PopulateRolesDropDownList();
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //Построение выпадающего списка ролей
        private void PopulateRolesDropDownList(object selected = null)
        {
            var Query = (from d in db.roles
                         orderby d.RoleId
                         select d).ToList();

            var rl = new SelectList(Query, "RoleId", "RoleName", selected);
            
            ViewBag.rolesList = rl;
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;

                db.UsersInRoles.Find(userprofile.UserId).RoleId = Convert.ToInt32(Request["rolesList"]);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MakeRolesDic();
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
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