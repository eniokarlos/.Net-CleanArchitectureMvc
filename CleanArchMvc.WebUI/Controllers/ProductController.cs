using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment environment;

        public ProductController(IProductService productService, ICategoryService categoryService,
        IWebHostEnvironment enviroment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.environment = enviroment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
            new SelectList(await categoryService.GetCategoriesAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await productService.AddAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null) return NotFound();

            var productDTO = await productService.GetByIdAsync(id);

            if(productDTO == null) return NotFound();

            var categories = await categoryService.GetCategoriesAsync();

            ViewBag.CategoryId =
            new SelectList(categories, "Id", "Name", productDTO.CategoryId);

            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if(ModelState.IsValid)
            {
                await productService.UpdateAsync(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return NotFound();

            var product = await productService.GetByIdAsync(id);

            if(product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await productService.RemoveAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();

            var product = await productService.GetByIdAsync(id);

            if(product == null) return NotFound();

            var root = environment.WebRootPath;
            var image = Path.Combine(root, "Images", product.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExists = exists;

            return View(product);
        }
    }
}