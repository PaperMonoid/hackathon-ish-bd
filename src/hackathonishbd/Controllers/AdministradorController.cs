using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate;
using hackathonishbd.Models;
using System;

namespace hackathonishbd.Controllers
{
     [RoutePrefix("Administrador")]
    public class AdministradorController : Controller
    {
        [HttpGet]
        [Route("AltaUsuario")]
        public ActionResult AltaUsuario()
        {
            return View();
        }

        [HttpPost]
        [Route("AltaUsuario")]
        public ActionResult AltaUsuario(T_usuarios usuario)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    usuario.Fecha_registro = DateTime.Now;
                    session.Save(usuario);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("Index");
        }
    }
}
