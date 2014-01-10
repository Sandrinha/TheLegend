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
    public class IntrodutionController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Introdution/

        public ActionResult Index()
        {
            var introdutions = db.Introdutions.Include(i => i.mission).Include(i => i.UserOrigin).Include(i => i.UserDestin).Include(i => i.game).Include(i => i.state);
            return View(introdutions.ToList());
        }

        //
        // GET: /Introdution/Details/5

        public ActionResult Details(int id = 0)
        {
            Introdution introdution = db.Introdutions.Find(id);
            if (introdution == null)
            {
                return HttpNotFound();
            }
            return View(introdution);
        }

        //
        // GET: /Introdution/Create

        public ActionResult Create()
        {
            ViewBag.MissionId = new SelectList(db.Missions, "MissionId", "MissionId");
            ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name");
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name");
            return View();
        }

        //
        // POST: /Introdution/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Introdution introdution)
        {
            if (ModelState.IsValid)
            {
                db.Introdutions.Add(introdution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MissionId = new SelectList(db.Missions, "MissionId", "MissionId", introdution.MissionId);
            ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName", introdution.UserOriginId);
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName", introdution.UserDestinId);
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name", introdution.GameId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", introdution.StateId);
            return View(introdution);
        }

        //
        // GET: /Introdution/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Introdution introdution = db.Introdutions.Find(id);
            if (introdution == null)
            {
                return HttpNotFound();
            }
            ViewBag.MissionId = new SelectList(db.Missions, "MissionId", "MissionId", introdution.MissionId);
            ViewBag.UserOriginId = new SelectList(db.UserProfiles, "UserId", "UserName", introdution.UserOriginId);
            ViewBag.UserDestinId = new SelectList(db.UserProfiles, "UserId", "UserName", introdution.UserDestinId);
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name", introdution.GameId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", introdution.StateId);
            return View(introdution);
        }

        //
        // POST: /Introdution/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Introdution introdution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(introdution).State = EntityState.Modified;
                db.SaveChanges();
                if (introdution.state.Name == "Aceite")
                {
                    RelationShip relationship = new RelationShip();
                    //Adicinar os utilizadores a relação
                    relationship.UserId1 = introdution.UserOriginId;
                    relationship.User1 = db.UserProfiles.Find(relationship.UserId1);
                    relationship.UserId2 = introdution.UserDestinId;
                    relationship.User2 = db.UserProfiles.Find(relationship.UserId2);
                    //Por definição ficam como conhecidos
                    relationship.TagRelationId = 1;
                    relationship.Tag = db.TagRelations.Find(1);
                    db.RelationShips.Add(relationship);
                    db.SaveChanges();

                    //Trocar as relações
                    relationship.UserId2 = introdution.UserOriginId;
                    relationship.User2 = db.UserProfiles.Find(relationship.UserId2);
                    relationship.UserId1 = introdution.UserDestinId;
                    relationship.User1 = db.UserProfiles.Find(relationship.UserId1);

                    //Por definição ficam como conhecidos
                    relationship.TagRelationId = 1;
                    relationship.Tag = db.TagRelations.Find(1);
                    db.RelationShips.Add(relationship);
                    db.SaveChanges();

                    db.Introdutions.Remove(introdution);

                    return RedirectToAction("RelatioShip", "Index");
                }
                
                return RedirectToAction("Index");
            }
            ViewBag.MissionId = new SelectList(db.Missions, "MissionId", "MissionId", introdution.MissionId);
            ViewBag.GameId = new SelectList(db.Games, "GameId", "Name", introdution.GameId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", introdution.StateId);
            return View(introdution);
        }

        //
        // GET: /Introdution/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Introdution introdution = db.Introdutions.Find(id);
            if (introdution == null)
            {
                return HttpNotFound();
            }
            return View(introdution);
        }

        //
        // POST: /Introdution/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Introdution introdution = db.Introdutions.Find(id);
            db.Introdutions.Remove(introdution);
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