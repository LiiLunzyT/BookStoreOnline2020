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
    public class BooksController : BaseController
    {
        private BookStore db = new BookStore();

        // GET: Admin/Books

        public ActionResult Index()
        {
            var books = db.Books.Include(n => n.Author);
            return View(db.Books.ToList());
        }

        [HttpPost]
        public ActionResult Index(string ma, string ten)
        {
            var books = db.Books.Where(abc => abc.BookID.Equals(ma) || ( ma.Equals("") && (abc.BookName).Contains(ten)));

            return View(books.ToList());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            Book book = new Book();
            book.BookID = getNewID();
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName");
            return View(book);
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BookName,Price,DiscountPercent,Quantity,TotalSell,Avatar,CreateByDate,Url,Publisher,PublicByDate,BookCover,Pages,BookDescription,AuthorID,ProducerID")] Book book)
        {
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = Request.Form["BookID"] + ".jpg";
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/images/books/" + postedFileName);
            imgNV.SaveAs(path);


            if (ModelState.IsValid)
            {
                book.Avatar = postedFileName;

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", book.ProducerID);
            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(string id, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", book.ProducerID);
            ViewBag.Category = db.Categories.ToList();
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BookName,Price,DiscountPercent,Quantity,Avatar,CreateByDate,Url,Publisher,PublicByDate,BookCover,Pages,BookDescription,AuthorID,ProducerID")] Book book, string returnUrl)
        {
            string decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
                decodedUrl = Server.UrlDecode(returnUrl);

            var imgNV = Request.Files["Avatar"];
            if(imgNV.ContentLength != 0)
            {
                try
                {
                    //Lấy thông tin từ input type=file có tên Avatar
                    string postedFileName = Request.Form["BookID"] + ".jpg";
                    //Lưu hình đại diện về Server
                    var path = Server.MapPath("/images/books/" + postedFileName);
                    imgNV.SaveAs(path);
                }
                catch
                { }
            }

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Books");
                }
            }

            if (ModelState.IsValid)
            {
                book.Avatar = book.BookID + ".jpg";
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", book.ProducerID);
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
            var countOfRows = db.Books.Count();
            if (countOfRows == 0) return "BK-00001";
            var lastRow = db.Books.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.BookID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "BK-" + str.PadLeft(5, '0');
        }
    }
}
