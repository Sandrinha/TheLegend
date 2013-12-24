using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheLegend.Models;

namespace TheLegend.Controllers
{
    public class HumorController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Humor/

        public ActionResult Index()
        {
            return View(db.Humors.ToList());
        }

        //
        // GET: /Humor/Details/5

        public ActionResult Details(int id = 0)
        {
            Humor humor = db.Humors.Find(id);
            if (humor == null)
            {
                return HttpNotFound();
            }
            return View(humor);
        }

        //
        // GET: /Humor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Humor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Humor humor)
        {
            if (ModelState.IsValid)
            {
                db.Humors.Add(humor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(humor);
        }

        //
        // GET: /Humor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Humor humor = db.Humors.Find(id);
            if (humor == null)
            {
                return HttpNotFound();
            }
            return View(humor);
        }

        //
        // POST: /Humor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Humor humor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(humor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(humor);
        }

        //
        // GET: /Humor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Humor humor = db.Humors.Find(id);
            if (humor == null)
            {
                return HttpNotFound();
            }
            return View(humor);
        }

        //
        // POST: /Humor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Humor humor = db.Humors.Find(id);
            db.Humors.Remove(humor);
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