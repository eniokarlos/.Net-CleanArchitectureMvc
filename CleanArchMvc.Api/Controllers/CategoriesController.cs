using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchMvc.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<CategoryDTO>> GetCategories()
        {
            var categories = await _service.GetCategoriesAsync();

            if(categories == null) return NotFound("Categories Not Found");

            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if(category == null) return NotFound("Category Not Found");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryDTO category)
        {
            if(category == null) return BadRequest("Invalid Category");

            await _service.AddAsync(category);

            return new CreatedAtRouteResult("GetCategory", new { Id = category.Id }, category);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDTO category)
        {
            if(id != category.Id)
                return BadRequest();

            if(category == null)
                return BadRequest();

            await _service.UpdateAsync(category);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if(result == null)
                return NotFound("Category Not Found");

            await _service.RemoveAsync(id);
            return Ok(result);
        }
    }
}