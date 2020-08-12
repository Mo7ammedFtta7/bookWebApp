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
    public class catogoriesController : ApiController
    {
        private booksEntities2 db = new booksEntities2();

        // GET: api/catogories
        public IQueryable<catogory> Getcatogories()
        {
            return db.catogories;
        }

        // GET: api/catogories/5
        [ResponseType(typeof(catogory))]
        public IHttpActionResult Getcatogory(long id)
        {
            catogory catogory = db.catogories.Find(id);
            if (catogory == null)
            {
                return NotFound();
            }

            return Ok(catogory);
        }

        // PUT: api/catogories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcatogory(long id, catogory catogory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catogory.catogory_id)
            {
                return BadRequest();
            }

            db.Entry(catogory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!catogoryExists(id))
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

        // POST: api/catogories
        [ResponseType(typeof(catogory))]
        public IHttpActionResult Postcatogory(catogory catogory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.catogories.Add(catogory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = catogory.catogory_id }, catogory);
        }

        // DELETE: api/catogories/5
        [ResponseType(typeof(catogory))]
        public IHttpActionResult Deletecatogory(long id)
        {
            catogory catogory = db.catogories.Find(id);
            if (catogory == null)
            {
                return NotFound();
            }

            db.catogories.Remove(catogory);
            db.SaveChanges();

            return Ok(catogory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool catogoryExists(long id)
        {
            return db.catogories.Count(e => e.catogory_id == id) > 0;
        }
    }
}