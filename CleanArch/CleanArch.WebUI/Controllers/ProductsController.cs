using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;

namespace CleanArch.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _environment = webHostEnvironment;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();

            return View(products);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(categoryDTO);

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

            ProductDTO productDTO = await _productService.GetByIdAsync(id);

            if (productDTO is null) return NotFound();

            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name", productDTO.CategoryId);

            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateAsync(productDTO);
                }
                catch (Exception)
                {
                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(productDTO);
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            ProductDTO productDTO = await _productService.GetByIdAsync(id);

            if (productDTO is null) return NotFound();

            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            ProductDTO productDTO = await _productService.GetByIdAsync(id);

            if (productDTO is null) return NotFound();

            string wwwroot = _environment.WebRootPath;
            
            string imagePath = Path.Combine(wwwroot, "images", productDTO.Image);

            bool exists = System.IO.File.Exists(imagePath);

            ViewBag.ImageExist = exists;

            return View(productDTO);
        }
        #endregion
    }
}
