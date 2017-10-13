using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Models.CategoryViewModels;
using GrandeGift.Services;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class CategoryController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IRepository<Category> _categoryRepo;

        public CategoryController (
            UserManager<ApplicationUser> userManager,
            IRepository<Category> categoryRepo
            )
        {
            _userManager = userManager;
            _categoryRepo = categoryRepo;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            CategoryIndexViewModel vm = new CategoryIndexViewModel();
            IEnumerable<Category> categories = _categoryRepo.GetAll();

            if (categories.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            else
            {
                vm.Categories = categories;
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel vm)
        {
            var user = GetCurrentUserAsync().Result;

            Category category = new Category()
            {
                Name = vm.Name,
                Description = vm.Description,
                PhotoPath = vm.PhotoPath,
                UserId = user.Id
            };

            _categoryRepo.Create(category);

            return RedirectToAction("Index");
        }

        public IActionResult Update()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
