using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace BookStoreOnline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            ViewData["Grouplist"] = new CategoryGroupDAO().listAll();
            ViewData["CategoryList"] = new CategoryDAO().listAll();

            return PartialView();
        }
    }
}