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
    public class ReviewsController : Controller
    {
        private BookStore db = new BookStore();

        // GET: Admin/Reviews

     

        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(n => n.Book);
            return View(db.Reviews.ToList());
        }

        [HttpPost]
        public ActionResult Index(string maNX, string hoten)
        {

            //var nhanViens = db.NhanViens.SqlQuery("exec NhanVien_DS '"+maNV+"' ");
            /// var nhanViens = db.NhanViens.SqlQuery("SELECT * FROM NhanVien WHERE MaNV='" + maNV + "'");

            var reviews = db.Reviews.Where(abc => abc.ReviewID.Contains(maNX) && (abc.Book.BookID).Contains(hoten));

            return View(reviews.ToList());
        }

        // GET: Admin/Reviews/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Admin/Reviews/Create
        public ActionResult Create()
        {
            Review review = new Review();
            review.ReviewID = getNewID();
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress");
            return View(review);
        }

        // POST: Admin/Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,ReviewByDate,Content,BookID,CustomerID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", review.BookID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress", review.CustomerID);
            return View(review);
        }

        // GET: Admin/Reviews/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", review.BookID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress", review.CustomerID);
            return View(review);
        }

        // POST: Admin/Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,ReviewByDate,Content,BookID,CustomerID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", review.BookID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerAddress", review.CustomerID);
            return View(review);
        }

        // GET: Admin/Reviews/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
            var countOfRows = db.Reviews.Count();
            if (countOfRows == 0) return "RV-0001";
            var lastRow = db.Reviews.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.ReviewID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "RV-" + str.PadLeft(4, '0');
        }
    }
}
