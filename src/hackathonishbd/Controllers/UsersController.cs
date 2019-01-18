using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate;
using hackathonishbd.Models;
using System;

namespace hackathonishbd.Controllers
{
    [RoutePrefix("user")]
    public class UsersController : Controller
    {

        [HttpPost]
        [Route("index")]
        public ActionResult Index(int IdUsuario, string Clave)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {

                var authUser = session.Query<Usuario>()
                                      .Where(x => x.IdUsuario == IdUsuario && x.Clave == Clave)
                                       .FirstOrDefault();
                if (authUser != null)
                {
                    authUser.FechaAcceso = DateTime.Now;
                    session.Update(authUser);
                    tx.Commit();
                    var rolstring = session.Query<Rol>()
                                       .Where(x => x.IdRol == authUser.Rol)
                                       .FirstOrDefault();
                    Global._rol = rolstring.Descripcion;
                    Global._id = authUser.IdUsuario;
                    return RedirectToAction("", "Dashboard");
                }

                else
                {
                    Global._rol = " ";
                    Global._id = 0;
                return RedirectToAction("", "");
                }

            }

        }

        [HttpGet]
        [Route("CambioClave")]
        public ActionResult CambioClave()
        {
            return View();
        }

        [HttpPost]
        [Route("CambioClave")]
        public ActionResult CambioClave(string ClaveVieja, string ClaveNueva)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            using (ITransaction tx = session.BeginTransaction())
            {

                var authUser = session.Query<Usuario>()
                                      .Where(x => x.IdUsuario == Global._id && x.Clave == ClaveVieja)
                                       .FirstOrDefault();
                if (authUser != null)
                {
                    authUser.Clave = ClaveNueva;
                    session.Update(authUser);
                    tx.Commit();
                    return RedirectToAction("", "Dashboard");
                }
                else
                {
                    Global._rol = "";
                    Global._id = 0;
                    return RedirectToAction("", "");
                }
            }
        }
    }
}