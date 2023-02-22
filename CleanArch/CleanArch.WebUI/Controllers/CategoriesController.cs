using Microsoft.AspNetCore.Mvc;

using CleanArch.Application.Interfaces;
using CleanArch.Application.DTOs;

namespace CleanArch.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return View(categories);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(categoryDTO);

                return RedirectToAction("Index");
            }

            return View(categoryDTO);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            CategoryDTO categoryDTO = await _categoryService.GetByIdAsync(id);

            if (categoryDTO is null) return NotFound();

            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(categoryDTO);
                }
                catch (Exception)
                {
                    throw;
                }
                
                return RedirectToAction("Index");
            }

            return View(categoryDTO);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            CategoryDTO categoryDTO = await _categoryService.GetByIdAsync(id);

            if (categoryDTO is null) return NotFound();
            
            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(id);

            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            CategoryDTO categoryDTO = await _categoryService.GetByIdAsync(id);

            if (categoryDTO is null) return NotFound();
            
            return View(categoryDTO);
        }
        #endregion
    }
}
