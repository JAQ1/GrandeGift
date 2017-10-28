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
            Profile profile = _profileRepo.GetSingle(p => p.UserId == user.Id);
            IEnumerable<DeliveryAddress> addresses = _addressRepo.Query(a => a.ProfileId == profile.ProfileId);

            DeliveryAddressIndexViewModel vm = new DeliveryAddressIndexViewModel()
            {
                DeliveryAddresses = addresses,
                Profile = profile
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDeliveryAddressViewModel vm)
        {
            ApplicationUser user = GetCurrentUserAsync().Result;
            Profile profile = _profileRepo.GetSingle(p => p.UserId == user.Id);

            DeliveryAddress address = new DeliveryAddress()
            {
                Name = vm.DeliveryAddress.Name,
                StreetAddress = vm.DeliveryAddress.StreetAddress,
                City = vm.DeliveryAddress.City,
                State = vm.DeliveryAddress.State,
                Postcode = vm.DeliveryAddress.Postcode,
                ProfileId = profile.ProfileId,
                Active = true
            };

            _addressRepo.Create(address);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            DeliveryAddress address = _addressRepo.GetSingle(a => a.DeliveryAddressId == id);

            UpdateDeliveryAddressViewModel vm = new UpdateDeliveryAddressViewModel()
            {
                DeliveryAddress = address
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(UpdateDeliveryAddressViewModel vm, int id)
        {
            DeliveryAddress address = _addressRepo.GetSingle(a => a.DeliveryAddressId == id);

            address.Name = vm.DeliveryAddress.Name;
            address.StreetAddress = vm.DeliveryAddress.StreetAddress;
            address.City = vm.DeliveryAddress.City;
            address.State = vm.DeliveryAddress.State;
            address.Postcode = vm.DeliveryAddress.Postcode;

            _addressRepo.Update(address);

            return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
