using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Models.GiftViewModels;
using GrandeGift.Services;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class GiftController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IRepository<Gift> _giftRepo;

        public GiftController(
            UserManager<ApplicationUser> userManager,
            IRepository<Gift> giftRepo
            )
        {
            _userManager = userManager;
            _giftRepo = giftRepo;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            GiftIndexViewModel vm = new GiftIndexViewModel();
            IEnumerable<Gift> gifts = _giftRepo.GetAll();

            if (gifts.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            else
            {
                vm.Gifts = gifts;
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateGiftViewModel vm)
        {
            var user = GetCurrentUserAsync().Result;

            Gift gift = new Gift()
            {
                Name = vm.Name,
                Price = vm.Price,
                PhotoPath = vm.PhotoPath,
                UserId = user.Id
            };

            _giftRepo.Create(gift);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Update(int id)
        //{
        //    var gift = _giftRepo.GetSingle(g => g.GiftId == id);

        //    UpdateCategoryViewModel vm = new UpdateCategoryViewModel()
        //    {
        //        Name = category.Name,
        //        Description = category.Description,
        //        PhotoPath = category.PhotoPath,
        //        CategoryId = id
        //    };

        //    return View(vm);
        //}

        //[HttpPost]
        //public IActionResult Update(UpdateCategoryViewModel vm, int id)
        //{
        //    var user = GetCurrentUserAsync().Result;
        //    var category = _categoryRepo.GetSingle(c => c.CategoryId == id);

        //    category.Name = vm.Name;
        //    category.Description = vm.Description;
        //    category.PhotoPath = vm.PhotoPath;
        //    category.UserId = user.Id;

        //    _categoryRepo.Update(category);

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    var category = _categoryRepo.GetSingle(c => c.CategoryId == id);

        //    _categoryRepo.Delete(category);

        //    return RedirectToAction("Index");
        //}

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
