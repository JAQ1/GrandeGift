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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class HamperController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IRepository<Hamper> _hamperRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Gift> _giftRepo;
        private IRepository<HamperGift> _hamperGiftRepo;
        private IHostingEnvironment _hostingEnviro;

        public HamperController(
            UserManager<ApplicationUser> userManager,
            IRepository<Hamper> hamperRepo,
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
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Hamper> activeHampers = _hamperRepo.Query(
                h => h.Active == true
                );

            HamperIndexViewModel vm = new HamperIndexViewModel();
            vm.Hampers = activeHampers;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(HamperIndexViewModel vm)
        {
            string query = vm.SearchQuery;
            double maxPrice = vm.MaxPrice;
            double minPrice = vm.MinPrice;
            string sortBy = vm.SortBy;
            IEnumerable<Hamper> searchResult = new List<Hamper>();

            //Search by name
            if (query != null)
            {
                searchResult = _hamperRepo.Query(
                     h => h.Active == true &&
                     h.Name.Contains(query)
                     );
            }
            else
            {
                searchResult = _hamperRepo.Query(h => h.Active == true);
            }

            //Sort By options
            switch (sortBy)
            {
                case "Name(A - Z)":
                    searchResult = searchResult.OrderBy(p => p.Name);
                    break;
                case "Price(High - Low)":
                    searchResult = searchResult.OrderByDescending(p => p.Price);
                    break;
                case "Price(Low - High)":
                    searchResult = searchResult.OrderBy(p => p.Price);
                    break;
                default:
                    break;
            }

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
            vm.Categories = _categoryRepo.GetAll();
            vm.Hamper = hamper;

            return View(vm);
        }

        

        [HttpPost] 
        public IActionResult Create(CreateHamperViewModel vm, int id, IFormFile PhotoPath)
        {
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

            UpdateHamperViewModel vm = new UpdateHamperViewModel()
            {
                Name = hamper.Name,
                Price = hamper.Price,
                Categories = _categoryRepo.GetAll()
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

        [HttpPost]
        public IActionResult Search(HamperIndexViewModel vm)
        {
            // double check query specs
            IEnumerable<Hamper> searchHampers = _hamperRepo.Query(

                h => h.Active == true
                && h.Name.Contains(vm.SearchQuery)
                );

            vm.Hampers = searchHampers;

            return RedirectToAction("Index", vm);
        }

        public IActionResult Order()
        {
            return View();
        }
    }
}
