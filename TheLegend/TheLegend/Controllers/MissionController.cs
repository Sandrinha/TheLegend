﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheLegend.Models;

namespace TheLegend.Controllers
{
    public class MissionController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Mission/

        public ActionResult Index()
        {
            var missions = db.Missions.Include(m => m.TargetUser).Include(m => m.UserMission);
            return View(missions.ToList());
        }

        //
        // GET: /Mission/Details/5

        public ActionResult Details(int id = 0)
        {
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        //
        // GET: /Mission/Create

        public ActionResult Create()
        {
            ViewBag.TargetUserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Mission/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Missions.Add(mission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TargetUserId = new SelectList(db.UserProfiles, "UserId", "UserName", mission.TargetUserId);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", mission.UserMissionId);
            return View(mission);
        }

        //
        // GET: /Mission/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            ViewBag.TargetUserId = new SelectList(db.UserProfiles, "UserId", "UserName", mission.TargetUserId);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", mission.UserMissionId);
            return View(mission);
        }

        //
        // POST: /Mission/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TargetUserId = new SelectList(db.UserProfiles, "UserId", "UserName", mission.TargetUserId);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", mission.UserMissionId);
            return View(mission);
        }

        //
        // GET: /Mission/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        //
        // POST: /Mission/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mission mission = db.Missions.Find(id);
            db.Missions.Remove(mission);
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