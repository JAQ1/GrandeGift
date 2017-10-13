using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.Models;
using GrandeGift.Models.ProfileViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IHostingEnvironment _hostingEnviro;
        private UserManager<ApplicationUser> _userManager;
        private IRepository<Profile> _profileRepo;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnviro,
            IRepository<Profile> profileRepo
            )
        {
            _userManager = userManager;
            _hostingEnviro = hostingEnviro;
            _profileRepo = profileRepo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var user = GetCurrentUserAsync().Result;
            var profile = _profileRepo.GetSingle(p => p.UserId == user.Id);

            if (profile == null)
            {
                profile = new Profile()
                {
                    DisplayName = "",
                    Firstname = "",
                    Lastname = "",
                    Phone = "",
                    DisplayPhotoPath = "images/user.png",
                    UserId = user.Id
                };

                _profileRepo.Create(profile);

            }

            ProfileIndexViewModel vm = new ProfileIndexViewModel()
            {
                DisplayName = profile.DisplayName,
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                Phone = profile.Phone,
                DisplayPhotoPath = profile.DisplayPhotoPath
            };

            return View(vm);
        }

        public IActionResult UpdateProfile()
        {
            var user = GetCurrentUserAsync().Result;
            var profile = _profileRepo.GetSingle(p => p.UserId == user.Id);

            UpdateProfileViewModel vm = new UpdateProfileViewModel()
            {
                DisplayName = profile.DisplayName,
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                Phone = profile.Phone,
                DisplayPhotoPath = profile.DisplayPhotoPath
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileViewModel vm, IFormFile DisplayPhotoPath)
        {
            var user = GetCurrentUserAsync().Result;
            var profile = _profileRepo.GetSingle(p => p.UserId == user.Id);

            profile.DisplayName = vm.DisplayName;
            profile.Firstname = vm.Firstname;
            profile.Lastname = vm.Lastname;
            profile.Phone = vm.Phone;

            if (DisplayPhotoPath != null)
            {
                string uploadPath = Path.Combine(_hostingEnviro.WebRootPath, "Media\\User");
                //uploadPath = Path.Combine(uploadPath, User.Identity.Name);
                //Directory.CreateDirectory(Path.Combine(uploadPath, tp.PackageName));
                string filename = User.Identity.Name + "-" + User.Identity.Name + "-1" + Path.GetExtension(DisplayPhotoPath.FileName);
                uploadPath = Path.Combine(uploadPath, filename);


                using (FileStream fs = new FileStream(uploadPath, FileMode.Create))
                {
                    DisplayPhotoPath.CopyTo(fs);
                }
                string SaveFilename = Path.Combine("Media\\User", filename);
                profile.DisplayPhotoPath = SaveFilename;
            }
            else
            {
                profile.DisplayPhotoPath = profile.DisplayPhotoPath;
            }

            _profileRepo.Update(profile);

            return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
