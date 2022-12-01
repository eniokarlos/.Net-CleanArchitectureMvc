using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IcategoryService categoryService;
        public CategoryController(IcategoryService categoryService)
        {
            this.categoryService = categoryService;
        } 

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetCategoriesAsync();
            return View(categories);
        }
    }
}