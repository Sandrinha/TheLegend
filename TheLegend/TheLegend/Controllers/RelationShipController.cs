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
    public class RelationShipController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /RelationShip/

        public ActionResult Index()
        {
            var relationships = db.RelationShips.Include(r => r.Tag);
            
            return View(relationships.ToList());
        }

        //
        // GET: /RelationShip/Details/5

        public ActionResult Details(int id = 0)
        {
            RelationShip relationship = db.RelationShips.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        //
        // GET: /RelationShip/Create

        public ActionResult Create()
        {
            ViewBag.TagRelationId = new SelectList(db.TagRelations, "TagRelationId", "Name");
            ViewBag.UserId1 = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.UserId2 = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /RelationShip/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RelationShip relationship)
        {
            if (ModelState.IsValid)
            {
                db.RelationShips.Add(relationship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TagRelationId = new SelectList(db.TagRelations, "TagRelationId", "Name", relationship.TagRelationId);
            return View(relationship);
        }

        //
        // GET: /RelationShip/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RelationShip relationship = db.RelationShips.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            ViewBag.TagRelationId = new SelectList(db.TagRelations, "TagRelationId", "Name", relationship.TagRelationId);
            return View(relationship);
        }

        //
        // POST: /RelationShip/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RelationShip relationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TagRelationId = new SelectList(db.TagRelations, "TagRelationId", "Name", relationship.TagRelationId);
            return View(relationship);
        }

        //
        // GET: /RelationShip/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RelationShip relationship = db.RelationShips.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        //
        // POST: /RelationShip/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RelationShip relationship = db.RelationShips.Find(id);
            db.RelationShips.Remove(relationship);
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