using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using hackathonishbd.Models;
using NHibernate.Cfg;

namespace hackathonishbd.Controllers
{
    [RoutePrefix("Maestro")]
    public class MaestroController : Controller
    {
        [HttpGet]
        [Route("AltaAlumno")]
        public ActionResult AltaAlumno()
        {
            return View();
        }

        [HttpPost]
        [Route("AltaAlumno")]
        public ActionResult AltaAlumno(Usuario alumno)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    alumno.FechaRegistro = DateTime.Now;
                    alumno.FechaAcceso = DateTime.Now;
                    session.Save(alumno);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("ConsultaAlumno");
        }

        [HttpGet]
        [Route("ConsultaAlumno")]
        public ActionResult ConsultaAlumno(string Busqueda, string Valor)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var rol = session.Query<Rol>()
                               .Where(x => x.Descripcion == "ALUMNO")
                               .Select(x => x.IdRol)
                               .FirstOrDefault();

            IQueryable<Usuario> alumnos = session.Query<Usuario>()
                .Where(x => x.Rol == rol);
            switch (Busqueda)
            {
                case "Nombre":
                    ViewData["Alumnos"] = alumnos.Where(x => x.Nombre == Valor).ToList();
                    break;
                case "Apellido":
                    ViewData["Alumnos"] = alumnos.Where(x => x.Apellido == Valor).ToList();
                    break;
                default:
                    ViewData["Alumnos"] = alumnos.ToList();
                    break;
            }
            ViewData["Calificación más alta"] = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == 193440920)
                                          .OrderBy(x => x.Valor)
                                          .FirstOrDefault();
            ViewData["Calificación más baja"] = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == 193440920)
                                          .OrderByDescending(x => x.Valor)
                                          .FirstOrDefault();
            NHibernateHelper.CloseSession();
            return View();
        }
    }
}