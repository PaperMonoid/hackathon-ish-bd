﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using NHibernate;
using hackathonishbd.Models;

namespace hackathonishbd.Controllers
{
    public class UsersController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(T_usuarios users)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    var authUser = session.Query<T_usuarios>()
                                           .Where(x => x.ID_usuario == users.ID_usuario && x.Clave == users.Clave)
                                           .First();
                    if (authUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(users.Nombre, false);
                        return RedirectToAction("", "Dashboard");
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();


                if (authUser != null)
                {
                    Session["role"] = authUser.Role;

                    FormsAuthentication.SetAuthCookie(users.userName, false);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }

                return View();
            }

        }
    }
