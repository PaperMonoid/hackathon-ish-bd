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
        [Route("CalificacionAlumno/{IdAlumno}")]
        public ActionResult CalificacionAlumno(int IdAlumno)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Calificacion calificacion = session.Query<Calificacion>()
                                                       .Where(x => x.IdAlumno == IdAlumno)
                                                       .FirstOrDefault();
                    Usuario alumno = session.Query<Usuario>()
                                            .Where(x => x.IdUsuario == IdAlumno)
                                            .FirstOrDefault();
                    Usuario maestro = session.Query<Usuario>()
                                            .Where(x => x.IdUsuario == Global._id)
                                            .FirstOrDefault();
                    @ViewData["alumno"] = alumno;
                    @ViewData["maestro"] = maestro;
                    @ViewData["calificacion"] = calificacion;
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return View();
        }

        [HttpPost]
        [Route("CalificacionAlumno")]
        public ActionResult CalificacionAlumno(Calificacion calificacion)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    calificacion.Final = false;
                    session.Save(calificacion);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("ConsultarAlumno");
        }

        [HttpGet]
        [Route("ModificarCalificacionAlumno/{IdCalificaciones}")]
        public ActionResult ModificarCalificacionAlumno(int IdCalificaciones)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Calificacion calificacion = session.Query<Calificacion>()
                                                       .Where(x => x.IdCalificaciones == IdCalificaciones)
                                                       .FirstOrDefault();
                    ViewData["calificacion"] = calificacion;
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return View();
        }

        [HttpPost]
        [Route("ModificarCalificacionAlumno")]
        public ActionResult CalificacionAlumno(int IdCalificaciones, int Valor)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Calificacion calificacion = session.Query<Calificacion>()
                                                       .Where(x => x.IdCalificaciones == IdCalificaciones && !x.Final)
                                                       .FirstOrDefault();
                    calificacion.Valor = Valor;
                    calificacion.Final = true;
                    calificacion.FechaFinal = DateTime.Now;
                    session.Update(calificacion);
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
            ViewData["AlumnosCalificaciones"] = alumnos
                .Select(x => new AlumnoCalificacion
                {
                    Alumno = x,
                    Calificacion = session.Query<Calificacion>()
                                          .Where(y => y.IdMaestro == Global._id && x.IdUsuario == y.IdAlumno)
                                          .FirstOrDefault()
                })
                .ToList();
            ViewData["Total"] = session.Query<Calificacion>()
                              .Where(x => x.IdMaestro == Global._id && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
                              .Count();
            ViewData["Promedio"] = session.Query<Calificacion>()
                              .Where(x => x.IdMaestro == Global._id && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
                              .Select(x => x.Valor)
                              .Average();
            ViewData["Calificación más alta"] = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == Global._id && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
                                          .OrderByDescending(x => x.Valor)
                                          .FirstOrDefault();
            ViewData["Calificación más baja"] = session.Query<Calificacion>()
                                          .Where(x => x.IdMaestro == Global._id && alumnos.Any(y => y.IdUsuario == x.IdAlumno))
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