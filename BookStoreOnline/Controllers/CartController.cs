using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BookStoreOnline.Common;
using BookStoreOnline.Models;
using Model.DAO;
using Model.EF;

namespace BookStoreOnline.Controllers
{
    [RoutePrefix("gio-hang")]
    public class CartController : Controller
    {
        private BookStore db = new BookStore();

        private const string CartSession = "CartSession";

        [Route]
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.book.BookID == item.book.BookID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult AddItem(String bookID, int quantity)
        {
            var book = new BookDAO().getBookByID(bookID);
            var cart = Session[CartSession];
            int count = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.book.BookID == bookID))
                {
                    foreach (var item in list)
                    {
                        if (item.book.BookID == bookID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.book = book;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
                count = list.Count;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.book = book;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
                count = list.Count;
            }
            return Json(new { status = true, count = count });
        }

        public ActionResult Increase(String bookID)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                if (item.book.BookID == bookID)
                {
                    if(item.Quantity < item.book.Quantity)
                    {
                        item.Quantity++;
                    }
                }
            }
            Session[CartSession] = sessionCart;
            var list = (List<CartItem>)sessionCart;

            return PartialView("_CartPartial");
        }

        public ActionResult Decrease(String bookID)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];

            // Giảm số lượng của sách trong giỏ hàng
            foreach (var item in sessionCart)
            {
                if (item.book.BookID == bookID)
                {
                    if(item.Quantity == 1)
                    {
                        sessionCart.Remove(item);
                    } else
                    {
                        item.Quantity--;
                    }
                }
            }

            Session[CartSession] = sessionCart;
            var list = (List<CartItem>)sessionCart;

            return PartialView("_CartPartial");
        }

        public ActionResult Test()
        {
            return PartialView("_Cart");
        }

        [Route("thanh-toan")]
        public ActionResult Payment()
        {
            var customerSession = (CustomerLogin)Session["CUSTOMER_SESSION"];

            if (customerSession == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var customer = new CustomerDAO().GetCustomerFromUserID(customerSession.UserID);

            var cartSession = Session["CartSession"];
            if (Session["CartSession"] == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            var list = (List<CartItem>)cartSession;
            ViewData["Payment"] = db.Payments.ToList();
            ViewData["Customer"] = customer;

            return View(list);
        }

        [HttpPost]
        public ActionResult Submit(String address, String note, String payment)
        {
            var dao = new OrderDAO();

            var customerSession = (CustomerLogin)Session["CUSTOMER_SESSION"];

            if (customerSession == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var customer = new CustomerDAO().GetCustomerFromUserID(customerSession.UserID);

            var cartSession = Session["CartSession"];
            if (Session["CartSession"] == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            var list = (List<CartItem>)cartSession;

            var nOrder = new Order();
            nOrder.OrderID = dao.getNewID();
            nOrder.Address = address;
            nOrder.OrderByDate = DateTime.Now;
            nOrder.Status = "Chờ duyệt";
            nOrder.Notes = note;
            nOrder.CustomerID = customer.CustomerID;
            nOrder.PaymentID = payment;

            var listDetail = new List<OrderDetail>();
            int total = 0;
            foreach(var item in list)
            {
                var nOD = new OrderDetail();
                nOD.Quantity = item.Quantity;
                nOD.Price = item.book.Price * (100 - item.book.DiscountPercent) / 100;
                nOD.BookID = item.book.BookID;
                nOD.OrderID = nOrder.OrderID;
                listDetail.Add(nOD);
                total += nOD.Price * nOD.Quantity;
            }
            nOrder.Total = total;

            if(dao.NewOrder(nOrder, listDetail))
            {
                Session["CartSession"] = null;
            }

            return View(nOrder);
        }
    }
}