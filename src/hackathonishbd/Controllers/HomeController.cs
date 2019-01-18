using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using hackathonishbd.Models;


namespace hackathonishbd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Global._rol = " ";
            Global._id = 0;
            return View(); ;
        }

    }
}