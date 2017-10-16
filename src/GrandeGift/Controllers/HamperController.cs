using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Models.HamperViewModels;
using GrandeGift.Services;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class HamperController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IRepository<Hamper> _hamperRepo;
        private IRepository<Category> _categoryRepo;


        public HamperController(
            UserManager<ApplicationUser> userManager,
            IRepository<Hamper> hamperRepo,
            IRepository<Category> categoryRepo
            )
        {
            _userManager = userManager;
            _hamperRepo = hamperRepo;
            _categoryRepo = categoryRepo;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Hamper> hampers = _hamperRepo.Query(h => h.Active != true);

            if (hampers.Count() == 0)
            {
                return RedirectToAction("Create");
            }

            HamperIndexViewModel vm = new HamperIndexViewModel();
            vm.Hampers = hampers;
            
            
            return View(vm);
        }

        public IActionResult Create()
        {
            CreateHamperViewModel vm = new CreateHamperViewModel();
            vm.Categories = _categoryRepo.GetAll();

            return View(vm);
        }

        [HttpPost] 
        public IActionResult Create(CreateHamperViewModel vm)
        {
            Hamper hamper = new Hamper()
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                CategoryId = vm.SelectedCategoryId,
                CategoryName = _categoryRepo.GetSingle(c => c.CategoryId == vm.SelectedCategoryId).Name
            };

            _hamperRepo.Create(hamper);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);

            UpdateHamperViewModel vm = new UpdateHamperViewModel()
            {
                Name = hamper.Name,
                Description = hamper.Description,
                Price = hamper.Price,
                Categories = _categoryRepo.GetAll(),
                SelectedCategoryName = hamper.CategoryName
            };

            return View(vm);
        }

        [HttpPost] 
        public IActionResult Update(UpdateHamperViewModel vm, int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);

            hamper.Name = vm.Name;
            hamper.Description = vm.Description;
            hamper.Price = vm.Price;
            hamper.CategoryId = vm.SelectedCategoryId;
            hamper.CategoryName = _categoryRepo.GetSingle(c => c.CategoryId == vm.SelectedCategoryId).Name;

            _hamperRepo.Update(hamper);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Discontinue(int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            hamper.Active = true;

            _hamperRepo.Update(hamper);

            return RedirectToAction("Index");
        }

        public IActionResult Order()
        {
            return View();
        }
    }
}
