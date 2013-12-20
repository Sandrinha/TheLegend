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
            AskDropDownList();

            //ViewBag.UserOrigin = new SelectList(db.UserProfiles, "UserId", "UserName");
            //ViewBag.UserDestiny = new SelectList(db.UserProfiles, "UserId", "UserName");
            //ViewBag.UserAsk = new SelectList(db.UserProfiles, "UserId", "UserName");
                    
            
            
            
            return View();

        }

        private void AskDropDownList(object userProfile = null)
        {
            var UserProfiles = from d in db.UserProfiles
                               orderby d.UserName
                               select d;
            ViewBag.UserAsk = new SelectList(UserProfiles, "UserID", "UserName", userProfile);
            ViewBag.UserOrigin = new SelectList(UserProfiles, "UserID", "UserName", userProfile);
            ViewBag.UserDestiny = new SelectList(UserProfiles, "UserID", "UserName", userProfile);
        }

        //
        // POST: /Ask/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "AskID,UserOrigin,UserDestiny,UserAsk")]
            Ask ask)
        {
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
            //if (ask == null)
            //{
            //    return HttpNotFound();
            //}

            AskDropDownList(ask.UserAsk);
            return View(ask);
        }

        //private void AskDropDownList(UserProfile userProfile)
        //{
        //    var UserProfiles = from d in db.Asks
        //                       orderby d.UserAsk
        //                       select d;
        //    ViewBag.UserAsk = new SelectList(UserProfiles, "UserID", selectUser);
        //}

        //
        // POST: /Ask/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ask ask)
        {
            try { 
                    if (ModelState.IsValid)
                    {
                        db.Entry(ask).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    } 
            }
            catch (DataException /* dex */)
               {
                  //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                  ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               }
                             AskDropDownList(ask.UserAsk);
                
        
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

        public string selectUser { get; set; }
    }
}