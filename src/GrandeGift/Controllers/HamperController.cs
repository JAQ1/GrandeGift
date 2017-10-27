using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Models.HamperViewModels;
using GrandeGift.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HamperController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IHamperRepository _hamperRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Gift> _giftRepo;
        private IRepository<HamperGift> _hamperGiftRepo;
        private IHostingEnvironment _hostingEnviro;

        public HamperController(
            UserManager<ApplicationUser> userManager,
            IHamperRepository hamperRepo,
            IRepository<Gift> giftRepo,
            IRepository<Category> categoryRepo,
            IRepository<HamperGift> hamperGiftRepo,
            IHostingEnvironment hostingEnviro
            )
        {
            _userManager = userManager;
            _hamperRepo = hamperRepo;
            _categoryRepo = categoryRepo;
            _giftRepo = giftRepo;
            _hamperGiftRepo = hamperGiftRepo;
            _hostingEnviro = hostingEnviro;
        }
        // GET: /<controller>/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {

            double maxPrice = 0;
            double minPrice = 0;
            IEnumerable<Hamper> activeHampers = _hamperRepo.GetActiveHampers();
            IEnumerable<Category> activeCategories = _categoryRepo.Query(c => c.Active == true);


            if (activeHampers.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            
            maxPrice = activeHampers.OrderByDescending(h => h.Price).ElementAt(0).Price;
            minPrice = activeHampers.OrderBy(h => h.Price).ElementAt(0).Price;
            

            HamperIndexViewModel vm = new HamperIndexViewModel();
            vm.Hampers = activeHampers;
            vm.Categories = activeCategories;
            vm.MaxPrice = maxPrice;
            vm.MinPrice = minPrice;

            return View(vm);
        }

        //Search
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(HamperIndexViewModel vm)
        {
            string query = vm.SearchQuery;
            double maxPrice = vm.MaxPrice;
            double minPrice = vm.MinPrice;
            string sortBy = vm.SortBy;
            int catId = vm.CategoryId;

            IEnumerable<Hamper> searchResult = _hamperRepo.SearchHampers(query, catId, maxPrice, minPrice, sortBy);

            IEnumerable<Category> activeCategories = _categoryRepo.Query(c => c.Active == true);

            vm.Categories = activeCategories;
            vm.Hampers = searchResult;

            return View(vm);
        }

        public IActionResult Create(int id)
        {
            Hamper hamper = new Hamper();

            if (id != 0)
            {
                hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            }

            CreateHamperViewModel vm = new CreateHamperViewModel();
            vm.HamperGifts = _hamperGiftRepo.Query(hg => hg.HamperId == hamper.HamperId);
            vm.Categories = _categoryRepo.Query(c => c.Active == true);
            vm.Hamper = hamper;

            return View(vm);
        }

        

        [HttpPost] 
        public IActionResult Create(CreateHamperViewModel vm, int id, IFormFile PhotoPath)
        {
            IEnumerable<HamperGift> hamperGifts = vm.HamperGifts;
            Category category = _categoryRepo.GetSingle(c => c.CategoryId == vm.SelectedCategoryId);
            string categoryName = _categoryRepo.GetSingle(c => c.CategoryId == vm.SelectedCategoryId).Name;

            Hamper hamper = new Hamper();

            if (id != 0)
            {
                hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            }

            hamper.Name = vm.Name;
            hamper.Price = vm.Price;
            hamper.HamperGifts = vm.HamperGifts;
            hamper.CategoryId = vm.SelectedCategoryId;
            hamper.Active = true;

            if (PhotoPath != null)
            {
                string uploadPath = Path.Combine(_hostingEnviro.WebRootPath, "Media\\Hamper");
                string filename = hamper.Name + Path.GetExtension(PhotoPath.FileName);
                uploadPath = Path.Combine(uploadPath, filename);


                using (FileStream fs = new FileStream(uploadPath, FileMode.Create))
                {
                    PhotoPath.CopyTo(fs);
                }
                string SaveFilename = Path.Combine("Media\\Hamper", filename);
                hamper.PhotoPath = SaveFilename;
            }
            else
            {
                hamper.PhotoPath = hamper.PhotoPath;
            }

            _hamperRepo.Create(hamper);


            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            IEnumerable<Category> categories = _categoryRepo.Query(c => c.Active == true);

            UpdateHamperViewModel vm = new UpdateHamperViewModel()
            {
                Name = hamper.Name,
                Price = hamper.Price,
                Categories = categories
            };

            return View(vm);
        }

        [HttpPost] 
        public IActionResult Update(UpdateHamperViewModel vm, int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);

            hamper.Name = vm.Name;
            hamper.Price = vm.Price;
            hamper.CategoryId = vm.SelectedCategoryId;
            hamper.Category = _categoryRepo.GetSingle(c => c.CategoryId == vm.SelectedCategoryId);

            _hamperRepo.Update(hamper);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Discontinue(int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            hamper.Active = false;

            _hamperRepo.Update(hamper);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            IEnumerable<Hamper> otherHampers = _hamperRepo.Query(h => h.HamperId != id && h.Active == true);
            IEnumerable<HamperGift> hamperGifts = _hamperGiftRepo.Query(hg => hg.HamperId == id);

            HamperDetailsViewModel vm = new HamperDetailsViewModel()
            {
                Hamper = hamper,
                OtherHampers = otherHampers,
                HamperGifts = hamperGifts
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult AddGift()
        {
            return RedirectToAction("Index", "Gift");
        }

        [HttpGet]
        public IActionResult GiftList(int id, Hamper hamper)
        {
            IEnumerable<Gift> gifts = _giftRepo.GetAll();
            IEnumerable<HamperGift> hamperGifts = _hamperGiftRepo.Query(hg => hg.HamperId == hamper.HamperId);
            Hamper _hamper = new Hamper();

            if (hamper.HamperId == 0)
            {
                _hamper = _hamperRepo.GetSingle(h => h.HamperId == id);
            }
            else
            {
                _hamper = hamper;
            }

            if (gifts.Count() == 0)
            {
                return RedirectToAction("Create", "Gift");
            }

            GiftListViewModel vm = new GiftListViewModel();

            if (hamperGifts.Count() == 0)
            {
                vm.HamperGifts = new List<HamperGift>();
            }

            vm.Gifts = gifts;
            vm.HamperGifts = hamperGifts;
            vm.Hamper = _hamper;

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateHamperGift(GiftListViewModel vm, int giftId, int hampId)
        {
            Gift gift = _giftRepo.GetSingle(g => g.GiftId == giftId);
            Hamper hamper = _hamperRepo.GetSingle(h => h.HamperId == hampId);

            HamperGift hamperGift = new HamperGift()
            {
                GiftId = giftId,
                GiftName = gift.Name,
                HamperId = hampId

            };
                //HamperName = hamper.Name
            _hamperGiftRepo.Create(hamperGift);

            return RedirectToAction("GiftList", "Hamper" , hamper);
        }

        private int getCategoryIdByName(string name)
        {
            int categoryId = _categoryRepo.GetSingle(c => c.Name == name).CategoryId;
            return categoryId;
        }
    }
}
