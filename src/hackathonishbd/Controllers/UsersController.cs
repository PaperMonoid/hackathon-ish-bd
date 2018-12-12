using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate;
using hackathonishbd.Models;

namespace hackathonishbd.Controllers
{
    public class UsersController : Controller
    {

        [HttpPost]
        public ActionResult Index(T_usuarios users)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    var authUser = session.Query<T_usuarios>()
                                           .Where(x => x.ID_usuario == users.ID_usuario && x.Clave == users.Clave)
                                           .First();
                    if (authUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(users.Nombre, false);
                        return RedirectToAction("", "Dashboard");
                    }
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
              
            }
            return RedirectToAction("", "/Home");

        }
    }
}
