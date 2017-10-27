using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using System.Linq.Expressions;

namespace GrandeGift.Services
{
    public interface IHamperRepository : IRepository<Hamper>
    {
        IEnumerable<Hamper> GetActiveHampers();
        IEnumerable<Hamper> SearchHampers(string query, int catId, double maxPrice, double minPrice, string sortBy);
    }
}
