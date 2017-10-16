using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.HamperViewModels
{
    public class CreateHamperViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
