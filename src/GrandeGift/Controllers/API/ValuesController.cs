using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Services;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers.API
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IHamperRepository _hamperRepo;
        private IRepository<Category> _categoryRepo;


        public ValuesController(
            IHamperRepository hamperRepo,
            IRepository<Category> categoryRepo
            )
        {
            _hamperRepo = hamperRepo;
            _categoryRepo = categoryRepo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("getAllHampers")]
        public JsonResult GetAllHampers()
        {
            IEnumerable<Hamper> activeHampers = _hamperRepo.GetActiveHampers();

            foreach (var item in activeHampers)
            {
                item.Category = _categoryRepo.GetSingle(c => c.CategoryId == item.CategoryId);
            }

            return Json(activeHampers);
        }

        [HttpGet("getAllCategories")]
        public JsonResult GetAllCategories()
        {
            IEnumerable<Category> categories = _categoryRepo.Query(c => c.Active == true);

            return Json(categories);
        }

        [HttpGet("getHampersByCategoryName")]
        public JsonResult GetHampersByCategoryName(string categoryName)
            {
            IEnumerable<Hamper> hampers = _hamperRepo.Query(h => h.Category.Name == categoryName && h.Active == true);

            return Json(hampers);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
