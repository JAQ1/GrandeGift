using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Data;
using Microsoft.EntityFrameworkCore;

namespace GrandeGift.Services
{
    public class HamperRepository : BaseRepository<Hamper>, IHamperRepository
    {
        private ApplicationDbContext dbContext;
        private DbSet<Hamper> dbTable;

        public HamperRepository()
        {
            dbContext = new ApplicationDbContext();
            dbTable = dbContext.Set<Hamper>();
        }

        public IEnumerable<Hamper> GetActiveHampers()
        {
            return Query(h => h.Active == true);
        }

        public IEnumerable<Hamper> SearchHampers(string query, int catId, double maxPrice, double minPrice, string sortBy)
        {
            IEnumerable<Hamper> searchResult = GetActiveHampers();

            if (query == null)
            {
                query = "";
            }
            if (catId == 0)
            {
                searchResult = Query(
                    h => h.Active == true
                    && h.Name.Contains(query)
                    && h.Price <= maxPrice
                    && h.Price >= minPrice
                    );
            }
            else
            {
                searchResult = Query(
                    h => h.Active == true
                    && h.Name.Contains(query)
                    && h.CategoryId == catId
                    && h.Price <= maxPrice
                    && h.Price >= minPrice
                    );
            }

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

            return searchResult;
        }
    }
}
