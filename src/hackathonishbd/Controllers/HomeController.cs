﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using hackathonishbd.Models;
using hackathonishbd.Models.Identity;


namespace hackathonishbd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(CurrentUser); ;
        }

        private User currentUser = null;
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    string userName = User.Identity.Name;
                    if (userName != null)
                    {
                        currentUser = new NHibernateHelper().Users.FindByNameAsync(userName).Result;
                    }
                }
                return currentUser;
            }

        }
    }
}