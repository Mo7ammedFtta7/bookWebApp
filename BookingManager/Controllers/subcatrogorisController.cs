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
    public class subcatrogorisController : Controller
    {
        private booksEntities2 db = new booksEntities2();

        // GET: subcatrogoris
        public ActionResult Index()
        {
            var sub_catrogoris = db.sub_catrogoris.Include(s => s.catogory);
            return View(sub_catrogoris.ToList());
        }

        // GET: subcatrogoris/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sub_catrogoris sub_catrogoris = db.sub_catrogoris.Find(id);
            if (sub_catrogoris == null)
            {
                return HttpNotFound();
            }
            return View(sub_catrogoris);
        }

        // GET: subcatrogoris/Create
        public ActionResult Create()
        {
            ViewBag.subCatogory_id = new SelectList(db.catogories, "catogory_id", "name");
            return View();
        }

        // POST: subcatrogoris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subCatogory_id,name,main_category")] sub_catrogoris sub_catrogoris)
        {
            if (ModelState.IsValid)
            {
                db.sub_catrogoris.Add(sub_catrogoris);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.subCatogory_id = new SelectList(db.catogories, "catogory_id", "name", sub_catrogoris.subCatogory_id);
            return View(sub_catrogoris);
        }

        // GET: subcatrogoris/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sub_catrogoris sub_catrogoris = db.sub_catrogoris.Find(id);
            if (sub_catrogoris == null)
            {
                return HttpNotFound();
            }
            ViewBag.subCatogory_id = new SelectList(db.catogories, "catogory_id", "name", sub_catrogoris.subCatogory_id);
            return View(sub_catrogoris);
        }

        // POST: subcatrogoris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subCatogory_id,name,main_category")] sub_catrogoris sub_catrogoris)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_catrogoris).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.subCatogory_id = new SelectList(db.catogories, "catogory_id", "name", sub_catrogoris.subCatogory_id);
            return View(sub_catrogoris);
        }

        // GET: subcatrogoris/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sub_catrogoris sub_catrogoris = db.sub_catrogoris.Find(id);
            if (sub_catrogoris == null)
            {
                return HttpNotFound();
            }
            return View(sub_catrogoris);
        }

        // POST: subcatrogoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            sub_catrogoris sub_catrogoris = db.sub_catrogoris.Find(id);
            db.sub_catrogoris.Remove(sub_catrogoris);
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
