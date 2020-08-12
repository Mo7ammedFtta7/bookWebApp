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
    public class booksController : ApiController
    {
        private booksEntities2 db = new booksEntities2();

        // GET: api/books
        public IQueryable<book> Getbooks()
        {
            return db.books;
        }

        // GET: api/books/5
        [ResponseType(typeof(book))]
        public IHttpActionResult Getbook(long id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbook(long id, book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.book_id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookExists(id))
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

        // POST: api/books
        [ResponseType(typeof(book))]
        public IHttpActionResult Postbook(book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.book_id }, book);
        }

        // DELETE: api/books/5
        [ResponseType(typeof(book))]
        public IHttpActionResult Deletebook(long id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookExists(long id)
        {
            return db.books.Count(e => e.book_id == id) > 0;
        }
    }
}