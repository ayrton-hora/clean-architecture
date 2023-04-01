using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            IEnumerable<ProductDTO> products = await _productService.GetProductsAsync();

            if (products == null) return NotFound("Products not found");

            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            ProductDTO product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound("Product not found");

            return Ok(product);
        }
        
        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest("Invalid Data");

            ProductDTO newProduct = await _productService.AddAsync(productDTO);

            return Created($"~/api/GetProductById/{newProduct.Id}", newProduct);
        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest();

            await _productService.UpdateAsync(productDTO);

            return Ok("Product updated with success");
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest();

            if (_productService.GetByIdAsync(id) == null) return NotFound("Product not found");

            await _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
