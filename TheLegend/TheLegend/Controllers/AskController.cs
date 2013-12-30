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
    public class AskController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Ask/

        public ActionResult Index()
        {

            return View(db.Asks.ToList());
        }

        //
        // GET: /Ask/Details/5

        public ActionResult Details(int id = 0)
        {
            Ask ask = db.Asks.Find(id);
            if (ask == null)
            {
                return HttpNotFound();
            }
            return View(ask);
        }

        //
        // GET: /Ask/Create

        public ActionResult Create()
        {
            ViewBag.UserAskId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Ask/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(Ask ask)
        {
            ask.UserOriginId = WebSecurity.GetUserId(User.Identity.Name);
            if (ModelState.IsValid)
            {
                db.Asks.Add(ask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ask);
        }

        //
        // GET: /Ask/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ask ask = db.Asks.Find(id);
            if (ask == null)
            {
                return HttpNotFound();
            }
            return View(ask);
        }

        //
        // POST: /Ask/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ask ask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ask);
        }

        //
        // GET: /Ask/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ask ask = db.Asks.Find(id);
            if (ask == null)
            {
                return HttpNotFound();
            }
            return View(ask);
        }

        //
        // POST: /Ask/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ask ask = db.Asks.Find(id);
            db.Asks.Remove(ask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Responder(int id = 0)
        {
            Ask ask = db.Asks.Find(id);
            Introdution introdution = new Introdution();
            
            UserProfile user = db.UserProfiles.Find(ask.UserOriginId);

            Mission[] auxmisson = db.Missions.ToArray();

            Mission mission = new Mission();

            for (int i = 0; i < auxmisson.Length; i++)
            {
                if (auxmisson[i].UserId == WebSecurity.GetUserId(User.Identity.Name))
                {
                    if (!auxmisson[i].IsComplete)
                    {
                        mission = auxmisson[i];
                    }
                }
            }


            introdution.MissionId = mission.MissionId;
            introdution.mission = mission;
            introdution.UserOriginId = ask.UserOriginId;
            introdution.UserDestinId = ask.UserDestinId;
            introdution.StateId = 1;
            introdution.state = db.States.Find(1);
            introdution.GameId = 4;
            introdution.game = db.Games.Find(4);
            introdution.GameResult = false;

            db.Asks.Remove(ask);
            db.SaveChanges();

            db.Introdutions.Add(introdution);
            db.SaveChanges();

            return RedirectToAction("Index","Introdution");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}