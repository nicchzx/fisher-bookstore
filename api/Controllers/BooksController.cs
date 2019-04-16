using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fisher.Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fisher.Bookstore.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Data.BookstoreContext db;

        public BooksController(Data.BookstoreContext db) => this.db = db;

        [HttpGet()]
        public IActionResult GetBooks()
        {
            return Ok(db.Books);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(int id)
        {
            //try to find the correct book
            var book = db.Books.FirstOrDefault (b => b.Id == id);
            // if no books is found with the id key, return HTTP 404 Not Found
            if (book == null)
            {
            return NotFound();
            }
            // return the Book inside HTTP 200 OK
            return Ok(book);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Book book)
        {
            if (book==null)
            {
                return BadRequest();
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book book)
        {
            //validate the incoming book
            if (book == null || book.Id != id){
                return BadRequest();
            }

            //verify the book is in database
            var bookToEdit = db.Books.FirstOrDefault(b => b.Id == id);
            if (bookToEdit == null)
            {
                return NotFound();
            }
            bookToEdit.Title = book.Title;
            bookToEdit.ISBN = book.ISBN;

            db.Books.Update(bookToEdit);
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var book = db.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return NoContent();
        }
    }
}