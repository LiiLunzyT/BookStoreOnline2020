using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreOnline.Areas.Admin.Controllers
{
    public class ApprovalOrderController : BaseController
    {
        BookStore db = new BookStore();

        // GET: Admin/ApprovalOrder
        public ActionResult Index()
        {
            var orders = db.Orders.Where(o => o.Status.Equals("Chờ duyệt"));
            return View(orders.ToList());
        }

        public ActionResult Details(String OrderID)
        {
            return View();
        }
    }
}