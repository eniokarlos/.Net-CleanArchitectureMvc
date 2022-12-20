
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _service.GetProductsAsync();

            if(products == null)
                return NotFound("Products Not Found");

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if(product == null)
                return NotFound("Product Not Found");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductDTO product)
        {
            if(product == null)
                return BadRequest("Invalid Product");

            await _service.AddAsync(product);

            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int id, ProductDTO product)
        {
            if(product == null)
                return BadRequest();
            if(id != product.Id)
                return BadRequest();

            await _service.UpdateAsync(product);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if(result == null)
                return NotFound("Product Not Found");

            await _service.RemoveAsync(id);

            return Ok();
        } 
    }
}