using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate;
using hackathonishbd.Models;

namespace hackathonishbd.Controllers
{
     [RoutePrefix("Administrador")]
    public class AdministradorController : Controller
    {
        [HttpGet]
        [Route("alta")]
        public ActionResult AltaUsuario()
        {
            return view();
        }
        [HttpPost]
        [Route("alta")]
        public ActionResult AltaUsuario(T_usuarios usuario)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    alumno.FechaRegistro = DateTime.Now;
                    session.Save(alumno);
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
