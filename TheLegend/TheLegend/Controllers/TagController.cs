using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheLegend.Filters;
using TheLegend.Models;
using WebMatrix.WebData;

namespace TheLegend.Controllers
{
    public class TagController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Tag/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        //
        // GET: /Tag/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        //
        // GET: /Tag/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tag/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                int iduser = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile u = db.UserProfiles.Find(iduser);
                u.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        //
        // GET: /Tag/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        //
        // POST: /Tag/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        //
        // GET: /Tag/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        //
        // POST: /Tag/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //Adicionar as Tags a lista do user logado
        [InitializeSimpleMembership]
        public ActionResult Adicionar(int id = 0)
        {
            Tag tag = db.Tags.Find(id);
            int userid = WebSecurity.GetUserId(User.Identity.Name);
            UserProfile user = db.UserProfiles.Find(userid);
            user.Tags.Add(tag);
            db.SaveChanges();

            return RedirectToAction("Index","Perfil");
        }
    }
}