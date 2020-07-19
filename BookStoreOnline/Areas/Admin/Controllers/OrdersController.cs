using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace BookStoreOnline.Areas.Admin.Controllers
{
    public class OrdersController : BaseController
    {
        private BookStore db = new BookStore();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Payment).Where(o => o.Status.Equals("Đã duyệt"));
            return View(orders.ToList());
        }

        [HttpPost]
        public ActionResult Index(string id, string trangthai, string dateMin = "", string dateMax = "")
        {
            DateTime min, max;

            if (dateMin == "")
            {
                min = DateTime.Now;
                ViewBag.dateMin = "";
                
            }
            else
            {
                ViewBag.dateMin = dateMin;
                min = DateTime.Parse(dateMin);
            }
            if (dateMax == "")
            {
                max = DateTime.MaxValue;
                ViewBag.dateMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.dateMax = dateMax;
                max = DateTime.Parse(dateMax);
            }


            
            var orders = db.Orders.Where(abc => abc.OrderID.Contains(id) && (abc.Status).Contains(trangthai) && (abc.OrderByDate >= min && abc.OrderByDate <= max));
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(string id)
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

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            Order order = new Order();
            order.OrderID = getNewID();
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName");
            return View(order);
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Address,OrderByDate,Status,Notes,CustomerID,PaymentID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress", order.CustomerID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName", order.PaymentID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress", order.CustomerID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName", order.PaymentID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Address,OrderByDate,Status,Notes,CustomerID,PaymentID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress", order.CustomerID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName", order.PaymentID);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public String getNewID()
        {
            var countOfRows = db.Orders.Count();
            if (countOfRows == 0) return "OD-000001";
            var lastRow = db.Orders.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.OrderID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "OD-" + str.PadLeft(6, '0');
        }
    }
}
