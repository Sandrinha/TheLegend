using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheLegend.Filters;
using TheLegend.Models;
using WebMatrix.WebData;



namespace TheLegend.Controllers
{
    public class PerfilController : Controller
    {
         private UsersContext bd = new UsersContext();

        public ActionResult Index()
        {
            return View(bd.UserProfiles.ToList());
        }

        //metodo para mostrar os dados 
        public ActionResult Edit(int id = 0)
        {
            UserProfile user = bd.UserProfiles.Find(id);
            ViewBag.HumorId = new SelectList(bd.Humors, "HumorId", "EstadoHumor");
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // metodo de ediçao

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile user)
        {
            if (ModelState.IsValid)
            {
                bd.Entry(user).State = EntityState.Modified;
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
         [InitializeSimpleMembership]
        public ActionResult Eliminar(int id=0)
        {
           UserProfile user = bd.UserProfiles.Find(WebSecurity.GetUserId(User.Identity.Name));
            Tag t = bd.Tags.Find(id);
            user.Tags.Remove(t);
            t.count--;
            bd.SaveChanges();
                return RedirectToAction("Index");
           
        }

    }
}
