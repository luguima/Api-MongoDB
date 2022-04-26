using Api.Database.Model;
using Api.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository) =>
            _bookRepository = bookRepository;

        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _bookRepository.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _bookRepository.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _bookRepository.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _bookRepository.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.Id = book.Id;

            await _bookRepository.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookRepository.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _bookRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}
