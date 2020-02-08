using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemTicketing.Controllers
{
    public class DashboardController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            var index = myContext.User.ToList();
            return View();
        }
    }
}