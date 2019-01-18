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
        [Route("ConsultaCalificaciones")]
        public ActionResult ConsultaCalificaciones(string Busqueda, string Valor)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            IQueryable<Usuario> alumnos = session.Query<Usuario>()
                                                 .Where(x => x.IdUsuario == Global._id);
            ViewData["AlumnosCalificaciones"] = alumnos.Select(
                x => new AlumnoCalificacion
                {
                    Alumno = x,
                    Calificacion = session.Query<Calificacion>()
                                          .Where(y => y.IdAlumno == Global._id && x.IdUsuario == y.IdAlumno)
                                          .FirstOrDefault()
                }).ToList();
            NHibernateHelper.CloseSession();
            return View();

        }
    }
}