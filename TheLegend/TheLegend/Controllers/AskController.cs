using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var asks = db.Asks.Include(a => a.UserAsk).Include(a => a.UserOrigin).Include(a => a.UserDestin);
            return View(asks.ToList());
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
           // ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Ask/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ask ask)
        {
            ask.UserOriginId = WebSecurity.GetUserId(User.Identity.Name);
            ask.UserOrigin = db.UserProfiles.Find(ask.UserOriginId);
            if (ModelState.IsValid)
            {
                db.Asks.Add(ask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserAskId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserAskId);
           // ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserOriginId);
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserDestinId);
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
            ViewBag.UserAskId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserAskId);
            ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserOriginId);
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserDestinId);
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
            ViewBag.UserAskId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserAskId);
            ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserOriginId);
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName", ask.UserDestinId);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Responder(int id = 0)
        {
            Ask ask = db.Asks.Find(id);
            Introdution introdution = new Introdution();

            //User que Originou o pedido
            UserProfile user = db.UserProfiles.Find(ask.UserOriginId);


            //Procurar na lista de missions a missom que esta activa para o User que Originou o pedido
            Mission[] auxmisson = db.Missions.ToArray();

            Mission mission = new Mission();

            for (int i = 0; i < auxmisson.Length; i++)
            {
                if (auxmisson[i].UserId == ask.UserOriginId)
                {
                    if (!auxmisson[i].IsComplete)
                    {
                        mission = auxmisson[i];
                    }
                }
            }

            //Preencher a Introdution
            introdution.MissionId = mission.MissionId;
            introdution.mission = mission;
            introdution.UserOriginId = ask.UserOriginId;
            introdution.UserOrigin = db.UserProfiles.Find(ask.UserOriginId);
            introdution.UserDestinId = ask.UserDestinId;
            introdution.UserDestin = db.UserProfiles.Find(ask.UserDestinId);
            introdution.StateId = 1;
            introdution.state = db.States.Find(1);
            introdution.GameId = 4;
            introdution.game = db.Games.Find(4);
            introdution.GameResult = false;

            db.Asks.Remove(ask);
            db.SaveChanges();

            db.Introdutions.Add(introdution);
            db.SaveChanges();

            return RedirectToAction("Index", "Introdution");
        }
    }
}