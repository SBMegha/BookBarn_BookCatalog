using BookBarn_API_Layer.DTOs;
using BookBarn_API_Layer.Services;
using BookBarn_DomainLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn_API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly AuthorServices authorServices;

        public AuthorController(AuthorServices authorServices)
        {
            this.authorServices = authorServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await authorServices.GetAllAuthor();
                var obj = new { TotalAuthors = authors.Count, authors };
                return Ok(obj);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
