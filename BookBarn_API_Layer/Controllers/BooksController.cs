using BookBarn_API_Layer.DTOs;
using BookBarn_API_Layer.Services;
using BookBarn_DataLayer.Respositories;
using BookBarn_DomainLayer.Entities;
using BookBarn_DomainLayer.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn_API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookServices bookServices;

        public BooksController(BookServices bookServices)
        {
            this.bookServices = bookServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks([FromQuery] Pagination pagination )
        {
            try
            {
                var books = await bookServices.GetAllBooksAsync(pagination.PageNumber, pagination.PageSize);
                var totalBooks = await bookServices.GetTotalNumberOfBooks();
                var obj= new { TotalBooks = totalBooks, books };
                return Ok(obj);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var bookDTO = await bookServices.GetBookByIdAsync(id);
                if (bookDTO == null)
                    return NotFound($"Book with id:{id} not found");
                return Ok(bookDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("category/{categoryId:int}")]
        [ProducesResponseType(typeof(List<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooksByCategoryId(int categoryId,[FromQuery] Pagination pagination)
        {
            try
            {
                var books = await bookServices.GetBooksByCategoryAsync(categoryId, pagination.PageNumber,pagination.PageSize);
                var totalBooks = await bookServices.GetTotalNumberOfBooksByCategoryId(categoryId);
                var obj = new { TotalBooksByCategory = totalBooks, books };
                return Ok(obj);
            }
            catch (Exception ex)
            {
                // Log the exception 
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("title/{title}")]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            try
            {
                var books = await bookServices.GetBookByTitleAsync(title);
                if (books == null)
                    return NotFound($"No books found with title: {title}");
                return Ok(books);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("author/{authorId:int}")]
        [ProducesResponseType(typeof(List<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooksByAuthor(int authorId,[FromQuery] Pagination pagination)
        {
            try
            {
                List<BookDTO> books = await bookServices.GetBooksByAuthorAsync(authorId, pagination.PageNumber,pagination.PageSize);
                var totalBooks = await bookServices.GetTotalNumberOfBooksByAuthorId(authorId);
                var obj = new { TotalBooksByAuthor = totalBooks, books };
                return Ok(obj); 
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBook([FromBody]BookDTO bookDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            try
            {
                var result = await bookServices.AddBookAsync(bookDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                bool result = await bookServices.DeleteBookById(id);
                if (!result)
                    return NotFound($"Book with id {id} not found.");
                return Ok("Book deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook([FromBody]BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                return BadRequest("Invalid book data.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await bookServices.UpdateBook(bookDTO);

                if (result!=null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, "An error occurred while updating the book.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
    }
}
