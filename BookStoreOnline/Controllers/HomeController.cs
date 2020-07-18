using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

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
            ViewData["ForKid"] = new BookDAO().ForKid();

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult NewBooks()
        {
            var books = new BookDAO().listNewBook(6);
            return PartialView(books);
        }

        [ChildActionOnly]
        public ActionResult BestSeller()
        {
            var books = new BookDAO().listBestSeller(6);
            return PartialView(books);
        }
    }
}