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
                    var rol = session.Query<Rol>()
                               .Where(x => x.Descripcion == "ALUMNO")
                               .Select(x => x.IdRol)
                               .FirstOrDefault();
                    alumno.Rol = rol;
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
                    alumnos = alumnos.Where(x => x.Nombre == Valor);
                    break;
                case "Apellido":
                    alumnos = alumnos.Where(x => x.Apellido == Valor);
                    break;
            }
            ViewData["Alumnos"] = alumnos.ToList();
            ViewData["Total"] = session.Query<Calificacion>()
                              .Where(x => x.IdMaestro == 193440920 && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
                              .Count();
            ViewData["Promedio"] = session.Query<Calificacion>()
                              .Where(x => x.IdMaestro == 193440920 && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
                              .Select(x => x.Valor)
                              .Average();
            ViewData["Calificación más alta"] = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == 193440920 && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
                                          .OrderByDescending(x => x.Valor)
                                          .FirstOrDefault();
            ViewData["Calificación más baja"] = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == 193440920)
                                          .OrderBy(x => x.Valor)
                                          .FirstOrDefault();
            NHibernateHelper.CloseSession();
            return View();
        }

        [HttpGet]
        [Route("BajaAlumno/{IdAlumno}")]
        public ActionResult BajaAlumno(int IdAlumno)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Usuario alumno = session.Query<Usuario>()
                        .Where(x => x.IdUsuario == IdAlumno)
                        .FirstOrDefault();
                    alumno.Activo = false;
                    session.Update(alumno);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("ConsultaAlumno");
        }
    }
}