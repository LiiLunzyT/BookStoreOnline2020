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
    public class CategoriesController : BaseController
    {
        private BookStore db = new BookStore();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.CategoryGroup);
            return View(categories.ToList());
        }

        [HttpPost]
        public ActionResult Index(string id, string tentl)
        {

            //var nhanViens = db.NhanViens.SqlQuery("exec NhanVien_DS '"+maNV+"' ");
            /// var nhanViens = db.NhanViens.SqlQuery("SELECT * FROM NhanVien WHERE MaNV='" + maNV + "'");

            var categories = db.Categories.Where(abc => abc.CategoryID.Contains(id) && (abc.CategoryName).Contains(tentl));
            return View(categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            Category category = new Category();
            category.CategoryID = getNewID();
            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName");
            return View(category);
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,CategoryDescription,Url,CateGroupID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName", category.CateGroupID);
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName", category.CateGroupID);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryDescription,Url,CateGroupID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateGroupID = new SelectList(db.CategoryGroups, "GroupID", "GroupName", category.CateGroupID);
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
            var countOfRows = db.Categories.Count();
            if (countOfRows == 0) return "CT-001";
            var lastRow = db.Categories.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.CategoryID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "CT-" + str.PadLeft(3, '0');
        }
    }
}
