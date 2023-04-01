using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.GetCategoriesAsync();

            if (categories == null) return NotFound("Categories not found");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            CategoryDTO category = await _categoryService.GetByIdAsync(id);

            if (category == null) return NotFound("Category not found");
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest("Invalid Data");

            CategoryDTO newCategory = await _categoryService.AddAsync(categoryDTO);

            return Created($"~/api/GetCategoryById/{newCategory.Id}", newCategory);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest();

            await _categoryService.UpdateAsync(categoryDTO);

            return Ok("Category updated with success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest();

            if (_categoryService.GetByIdAsync(id) == null) return NotFound("Category not found");

            await _categoryService.RemoveAsync(id);

            return NoContent();
        }
    }
}
