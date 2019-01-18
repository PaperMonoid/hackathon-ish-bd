using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hackathonishbd.Models;
using NHibernate;

namespace hackathonishbd.Controllers
{
    [RoutePrefix("Historial")]
    public class HistorialController : Controller
    {
        [HttpGet]
        [Route("Acceso")]
        public ActionResult Acceso()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ViewData["Accesos"] = session.Query<Acceso>().ToList();
            return View ();
        }

        [HttpGet]
        [Route("Clave")]
        public ActionResult Clave()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ViewData["Claves"] = session.Query<Clave>().ToList();
            return View();
        }
    }
}
