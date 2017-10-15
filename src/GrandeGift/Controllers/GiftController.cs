using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Models.GiftViewModels;
using GrandeGift.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class GiftController : Controller
    {
        private IRepository<Gift> _giftRepo;

        public GiftController(
            IRepository<Gift> giftRepo
            )
        {
            _giftRepo = giftRepo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Gift> gifts = _giftRepo.GetAll();

            if (gifts.Count() == 0)
            {
                return RedirectToAction("Create");
            }

            GiftIndexViewModel vm = new GiftIndexViewModel();
            vm.Gifts = gifts;

            return View();
        }
    }
}
