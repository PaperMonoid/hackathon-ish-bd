using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate;
using hackathonishbd.Models;

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
                                       .Where(x => x.IdUsuario == IdUsuario)
                                       .FirstOrDefault();
                if (authUser != null)
                {
                    FormsAuthentication.SetAuthCookie(authUser.Nombre, false);
                    NHibernateHelper.CloseSession();
                    return RedirectToAction("", "Dashboard");
                }

                return RedirectToAction("", "");
            }

        }
    }
}