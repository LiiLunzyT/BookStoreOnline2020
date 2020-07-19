using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using PagedList;

namespace BookStoreOnline.Controllers
{
    [RoutePrefix("danh-muc")]
    public class CategoryController : Controller
    {
        // GET: Category
        [Route]
        public ActionResult Index(int? page, String searchString = "")
        {
            var dao = new BookDAO();
            var books = dao.listByName(searchString);

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(books.ToPagedList(pageNumber, pageSize));
        }

        [Route("{Url?}")]
        public ActionResult Index(String Url, int? page)
        {
            var cDao = new CategoryDAO();
            var category = cDao.getCategoryByUrl(Url);
            var books = category.Books.ToList();

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            TempData["selCate"] = category;

            return View(books.ToPagedList(pageNumber, pageSize));
        }

        [ChildActionOnly]
        public ActionResult CategorySideBar(String cateName = "")
        {
            var dao = new CategoryDAO();
            ViewData["listCategory"] = dao.listAll();
            ViewData["CateName"] = cateName;
            return PartialView();
        }
    }
}