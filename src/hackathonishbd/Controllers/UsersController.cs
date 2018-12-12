using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using NHibernate;

namespace hackathonishbd.Controllers
{
    public class UsersController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Users users)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    var authUser = session.Query<Users>()
                                           .Where(x => x.ID_usuario == ID_usuario && x.Clave == Clave)
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

            }
            return View();
        }
    }
}
