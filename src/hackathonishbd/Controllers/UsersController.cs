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
        public ActionResult Index(int ID_usuario,string Clave)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
                using (ITransaction tx = session.BeginTransaction())
                {

                    var authUser = session.Query<T_usuarios>()
                                           .Where(x => x.ID_usuario == ID_usuario )
                                           .FirstOrDefault();
                    if (authUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(authUser.Nombre, false);
                NHibernateHelper.CloseSession();
                        return RedirectToAction("", "Dashboard");
                    }

                    return RedirectToAction("", "");
                }
<<<<<<< HEAD

            }
            return View();
=======
>>>>>>> 69b0a263fa5fca74ca2a38920d2faeb8b4bfbec2
        }
    }
}
