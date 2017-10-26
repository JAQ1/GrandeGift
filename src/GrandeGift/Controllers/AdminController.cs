using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Models.AdminViewModels;
using GrandeGift.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IRepository<Category> _categoryRepo;
        private IRepository<Hamper> _hamperRepo;
        private IRepository<Gift> _giftRepo;

        public AdminController(
            IRepository<Category> categoryRepo,
            IRepository<Hamper> hamperRepo,
            IRepository<Gift> giftRepo
            )
        {
            _categoryRepo = categoryRepo;
            _hamperRepo = hamperRepo;
            _giftRepo = giftRepo;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepo.Query(c => c.Active == true);
            IEnumerable<Hamper> hampers = _hamperRepo.Query(h => h.Active == true);
            IEnumerable<Gift> gifts = _giftRepo.GetAll();

            AdminIndexViewModel vm = new AdminIndexViewModel()
            {
                Categories = categories,
                Hampers = hampers,
                Gifts = gifts
            };

            return View(vm);
        }
    }
}
