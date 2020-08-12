using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingManager.Models;

namespace BookingManager.Controllers
{
    public class booksController : Controller
    {
        private booksEntities2 db = new booksEntities2();

        // GET: books
        public ActionResult Index()
        {
            var books = db.books.Include(b => b.sub_catrogoris);
            return View(books.ToList());
        }


        // GET: /GetsubCatrogorisByMain

        public ActionResult GetsubCatrogorisByMain(int id)
        {
            var sub_catrogoris = db.subs(id);
            return Json(sub_catrogoris, JsonRequestBehavior.AllowGet); ;
        }

        // GET: books/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.catogory_id = new SelectList(db.catogories, "catogory_id", "name");
            ViewBag.subcatogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name");
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "book_id,title,auther,subcatogory_id,created_at")] book book)
        {
            if (ModelState.IsValid)
            {
                book.created_at = DateTime.Now;
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.subcatogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name", book.subcatogory_id);
            return View(book);
        }

        // GET: books/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.subcatogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name", book.subcatogory_id);
            ViewBag.catogory_id = new SelectList(db.catogories, "catogory_id", "name");

            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_id,title,auther,subcatogory_id,created_at")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.subcatogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name", book.subcatogory_id);
            return View(book);
        }

        // GET: books/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
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
    }
}
