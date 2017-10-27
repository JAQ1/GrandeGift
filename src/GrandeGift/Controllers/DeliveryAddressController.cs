using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.Models;
using GrandeGift.Models.DeliveryAddressViewModels;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class DeliveryAddressController : Controller
    {
        private IRepository<DeliveryAddress> _addressRepo;
        private IRepository<Profile> _profileRepo;
        private UserManager<ApplicationUser> _userManager;

        public DeliveryAddressController(
            IRepository<DeliveryAddress> addressRepo,
            IRepository<Profile> profileRepo,
            UserManager<ApplicationUser> userManager
            )
        {
            _addressRepo = addressRepo;
            _profileRepo = profileRepo;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ApplicationUser user = GetCurrentUserAsync().Result;
            IEnumerable<DeliveryAddress> addresses = _addressRepo.GetAll();
            Profile profile = _profileRepo.GetSingle(p => p.UserId == user.Id);

            DeliveryAddressIndexViewModel vm = new DeliveryAddressIndexViewModel()
            {
                DeliveryAddresses = addresses,
                Profile = profile
            };

            return View(vm);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
