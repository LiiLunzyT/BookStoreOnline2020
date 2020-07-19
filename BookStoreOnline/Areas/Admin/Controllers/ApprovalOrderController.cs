using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
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

        public ActionResult Details(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult Approve(String id)
        {
            Order order = db.Orders.Find(id);

            bool flag = true;

            // Kiểm tra có đủ hàng hay không
            foreach(OrderDetail detail in order.OrderDetails)
            {
                if(detail.Quantity > detail.Book.Quantity)
                {
                    flag = false;
                    break;
                }
            }

            // Nếu đủ thì bắt đầu trừ số lượng tồn của sản phẩm
            if(flag)
            {
                // Chuyển trạng thái đơn hàng từ chờ duyệt sang đã duyệt
                order.Status = "Đã duyệt";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                foreach (OrderDetail detail in order.OrderDetails)
                {
                    Book book = db.Books.Find(detail.Book.BookID);
                    book.Quantity -= detail.Quantity;
                    book.TotalSell += detail.Quantity;
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }
            } else
            {
                TempData["ApproveMessage"] = "Có Sản phẩm không đủ số lượng, không thể duyệt";
                return RedirectToAction("Details", "ApprovalOrder", new { id = id});
            }

            return RedirectToAction("Index");
        }
    }
}