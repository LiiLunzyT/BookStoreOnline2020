using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;

namespace BookStoreOnline.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [Route("chi-tiet/{Url?}")]
        public ActionResult Detail(String Url)
        {
            var dao = new BookDAO();
            var book = dao.getBookByUrl(Url);

            return View(book);
        }

        public JsonResult ListName(string q)
        {
            var data = new BookDAO().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult BookCard(Book enity)
        {
            return PartialView(enity);
        }

        [ChildActionOnly]
        public ActionResult listCategoryOfBook(int id)
        {
            return PartialView();
        }
    }
}