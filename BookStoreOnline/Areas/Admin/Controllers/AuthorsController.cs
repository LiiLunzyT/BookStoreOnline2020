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
    public class AuthorsController : BaseController
    {
        private BookStore db = new BookStore();

        // GET: Admin/Authors
        public ActionResult Index()
        {
            var authors = db.Authors.Include(u => u.Books);
            return View(authors.ToList());
        }
        [HttpPost]
        public ActionResult Index(string hoten)
        {

            //var nhanViens = db.NhanViens.SqlQuery("exec NhanVien_DS '"+maNV+"' ");
            /// var nhanViens = db.NhanViens.SqlQuery("SELECT * FROM NhanVien WHERE MaNV='" + maNV + "'");

            var authors = db.Authors.Where(abc => abc.AuthorName.Contains(hoten));
            return View(authors.ToList());
        }




        // GET: Admin/Authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Admin/Authors/Create
        public ActionResult Create()
        {
            Author author = new Author();
            author.AuthorID = getNewID();
            return View(author);
        }

        // POST: Admin/Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorID,AuthorName,PhoneNumber,Url")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Admin/Authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Admin/Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorID,AuthorName,PhoneNumber,Url")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Admin/Authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Admin/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
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
            var countOfRows = db.Authors.Count();
            if (countOfRows == 0) return "AU-001";
            var lastRow = db.Authors.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.AuthorID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "AU-" + str.PadLeft(3, '0');
        }
    }
}
