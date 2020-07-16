using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace BookStoreOnline.Controllers
{
    [RoutePrefix("danh-muc")]
    public class CategoryController : Controller
    {
        // GET: Category
        [Route]
        public ActionResult Index()
        {
            var dao = new BookDAO();
            ViewData["listBook"] = dao.listAll();
            return View();
        }

        [Route("{Url?}")]
        public ActionResult Index(String Url)
        {
            var dao = new BookDAO();
            var cDao = new CategoryDAO();
            var category = cDao.getCategoryByUrl(Url);
            ViewData["listBook"] = category.Books.ToList();
            return View();
        }

        [ChildActionOnly]
        public ActionResult CategorySideBar(String cateName = "")
        {
            var dao = new CategoryDAO();
            ViewData["listCategory"] = dao.listAll();
            ViewData["selectedCategory"] = cateName;
            return PartialView();
        }
    }
}