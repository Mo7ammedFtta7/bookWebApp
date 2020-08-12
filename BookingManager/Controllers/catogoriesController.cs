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
    public class catogoriesController : Controller
    {
        private booksEntities2 db = new booksEntities2();

        // GET: catogories
        public ActionResult Index()
        {
            var catogories = db.catogories.Include(c => c.sub_catrogoris);
            return View(catogories.ToList());
        }

        // GET: catogories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catogory catogory = db.catogories.Find(id);
            if (catogory == null)
            {
                return HttpNotFound();
            }
            return View(catogory);
        }

        // GET: catogories/Create
        public ActionResult Create()
        {
            ViewBag.catogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name");
            return View();
        }

        // POST: catogories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "catogory_id,name")] catogory catogory)
        {
            if (ModelState.IsValid)
            {
                db.catogories.Add(catogory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.catogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name", catogory.catogory_id);
            return View(catogory);
        }

        // GET: catogories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catogory catogory = db.catogories.Find(id);
            if (catogory == null)
            {
                return HttpNotFound();
            }
            ViewBag.catogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name", catogory.catogory_id);
            return View(catogory);
        }

        // POST: catogories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "catogory_id,name")] catogory catogory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catogory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.catogory_id = new SelectList(db.sub_catrogoris, "subCatogory_id", "name", catogory.catogory_id);
            return View(catogory);
        }

        // GET: catogories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catogory catogory = db.catogories.Find(id);
            if (catogory == null)
            {
                return HttpNotFound();
            }
            return View(catogory);
        }

        // POST: catogories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            catogory catogory = db.catogories.Find(id);
            db.catogories.Remove(catogory);
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
