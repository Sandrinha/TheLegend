﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheLegend.Models;



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

    }
}