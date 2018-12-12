using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate;
using NHibernate.Cfg;
using hackathonishbd.Models;


namespace hackathonishbd.Controllers
{
    [RoutePrefix("Alumno")]
    public class AlumnoController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index(int id = 593440121)
        {
            ISession sesion = NHibernateHelper.GetCurrentSession();
            IQueryable<T_usuarios> alumnos = sesion.Query<T_usuarios>();
            ViewData["alumnos"] = alumnos.Where(x => x.ID_usuario == id).ToList();
            NHibernateHelper.CloseSession();
            return View();
        }
    }
}