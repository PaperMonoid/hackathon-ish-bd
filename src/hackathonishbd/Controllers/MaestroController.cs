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
        public ActionResult Index(string Busqueda, string Valor)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var rol = session.Query<T_rol>()
                               .Where(x => x.Descripcion == "Alumno")
                               .Select(x => x.ID_rol)
                               .First();

            IQueryable<Usuario> alumnos = session.Query<Usuario>()
                .Where(x => x.Rol == rol);
            var calificacionalta = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == 2)
                                          .Max(x =>x.Valor);
            var calificacionbaja = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == 2)
                                          .Max(x => x.Valor);
            switch (Busqueda)
            {
                case "Nombre":
                    ViewData["Alumnos"] = alumnos.Where(x => x.Nombre == Valor).ToList();
                    break;
                case "Apellido":
                    ViewData["Alumnos"] = alumnos.Where(x => x.Apellido == Valor).ToList();
                    break;
                case "Maximo":
                    ViewData["Calificación más alta"] = calificacionalta.ToString();
                    break;
                case "Minimo":
                    ViewData["Calificación más baja"] = calificacionbaja.ToString();
                    break;
                default:
                    ViewData["Alumnos"] = alumnos.ToList();
                    break;
            }
            NHibernateHelper.CloseSession();
            return View();
        }
    }
}