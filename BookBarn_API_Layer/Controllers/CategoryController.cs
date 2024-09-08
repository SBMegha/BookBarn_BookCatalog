using BookBarn_API_Layer.DTOs;
using BookBarn_API_Layer.Services;
using BookBarn_DomainLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn_API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices categoryServices=null;

        public CategoryController(CategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await categoryServices.GetAllCategories();
                var obj = new { TotalCategories = categories.Count, categories };
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
