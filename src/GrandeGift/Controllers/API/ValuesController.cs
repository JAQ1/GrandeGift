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
    [EnableCors("SitePolicy")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IRepository<Hamper> _hamperRepo;

        public ValuesController(
            IRepository<Hamper> hamperRepo
            )
        {
            _hamperRepo = hamperRepo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("getAll")]
        public JsonResult GetAll()
        {
            IEnumerable<Hamper> activeHampers = _hamperRepo.Query(h => h.Active == false);

            return Json(activeHampers);
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
