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
    public class TagRelationController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /TagRelation/

        public ActionResult Index()
        {
            return View(db.TagRelations.ToList());
        }

        //
        // GET: /TagRelation/Details/5

        public ActionResult Details(int id = 0)
        {
            TagRelation tagrelation = db.TagRelations.Find(id);
            if (tagrelation == null)
            {
                return HttpNotFound();
            }
            return View(tagrelation);
        }

        //
        // GET: /TagRelation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TagRelation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagRelation tagrelation)
        {
            if (ModelState.IsValid)
            {
                db.TagRelations.Add(tagrelation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tagrelation);
        }

        //
        // GET: /TagRelation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TagRelation tagrelation = db.TagRelations.Find(id);
            if (tagrelation == null)
            {
                return HttpNotFound();
            }
            return View(tagrelation);
        }

        //
        // POST: /TagRelation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TagRelation tagrelation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tagrelation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tagrelation);
        }

        //
        // GET: /TagRelation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TagRelation tagrelation = db.TagRelations.Find(id);
            if (tagrelation == null)
            {
                return HttpNotFound();
            }
            return View(tagrelation);
        }

        //
        // POST: /TagRelation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TagRelation tagrelation = db.TagRelations.Find(id);
            db.TagRelations.Remove(tagrelation);
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