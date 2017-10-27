using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.HamperViewModels
{
    public class HamperIndexViewModel
    {
        public IEnumerable<Hamper> Hampers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string SearchQuery { get; set; }
        public int CategoryId { get; set; }
        public string SortBy { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
    }
}
