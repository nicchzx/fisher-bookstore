using System.Linq;
using Fisher.Bookstore.Api.Data;
using Fisher.Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fisher.Bookstore.Api.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookstoreContext db;

        public AuthorsController(BookstoreContext db) => this.db = db;

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            return Ok(db.Authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(int id)
        {
            var author = db.Authors.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            db.Authors.Add(author);
            db.SaveChanges();
            return CreatedAtRoute("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Author author)
        {
            if (author == null || author.Id != id)
            {
                return BadRequest();
            }

            var authorToEdit = db.Authors.Find(id);
            if (authorToEdit == null)
            {
                return NotFound();
            }

            authorToEdit.Name = author.Name;
            authorToEdit.Bio = author.Bio;

            db.Authors.Update(authorToEdit);
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = db.Authors.FirstOrDefault(b => b.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();
            return NoContent();
        }
    }
}