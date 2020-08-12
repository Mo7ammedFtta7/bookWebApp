using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingManager.Models;

namespace BookingManager.Controllers.api
{
    public class sub_catrogorisController : ApiController
    {
        private booksEntities2 db = new booksEntities2();

        // GET: api/sub_catrogoris
        public IQueryable<sub_catrogoris> Getsub_catrogoris()
        {
            return db.sub_catrogoris;
        }

        // GET: api/sub_catrogoris/5
        [ResponseType(typeof(sub_catrogoris))]
        public IHttpActionResult Getsub_catrogoris(long id)
        {
            sub_catrogoris sub_catrogoris = db.sub_catrogoris.Find(id);
            if (sub_catrogoris == null)
            {
                return NotFound();
            }

            return Ok(sub_catrogoris);
        }

        public IHttpActionResult GetsubCatrogorisByMain(int id)
        {
            List<sub_catrogoris> sub_catrogoris = db.sub_catrogoris.Where(c => c.main_category == id).ToList();
            if (sub_catrogoris == null)
            {
                return NotFound();
            }

            return Ok(sub_catrogoris);
        }

        // PUT: api/sub_catrogoris/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsub_catrogoris(long id, sub_catrogoris sub_catrogoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sub_catrogoris.subCatogory_id)
            {
                return BadRequest();
            }

            db.Entry(sub_catrogoris).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sub_catrogorisExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/sub_catrogoris
        [ResponseType(typeof(sub_catrogoris))]
        public IHttpActionResult Postsub_catrogoris(sub_catrogoris sub_catrogoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sub_catrogoris.Add(sub_catrogoris);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (sub_catrogorisExists(sub_catrogoris.subCatogory_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sub_catrogoris.subCatogory_id }, sub_catrogoris);
        }

        // DELETE: api/sub_catrogoris/5
        [ResponseType(typeof(sub_catrogoris))]
        public IHttpActionResult Deletesub_catrogoris(long id)
        {
            sub_catrogoris sub_catrogoris = db.sub_catrogoris.Find(id);
            if (sub_catrogoris == null)
            {
                return NotFound();
            }

            db.sub_catrogoris.Remove(sub_catrogoris);
            db.SaveChanges();

            return Ok(sub_catrogoris);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sub_catrogorisExists(long id)
        {
            return db.sub_catrogoris.Count(e => e.subCatogory_id == id) > 0;
        }
    }
}