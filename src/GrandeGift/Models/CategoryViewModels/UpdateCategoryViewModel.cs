using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.CategoryViewModels
{
    public class UpdateCategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public string Name { get; set; }
    }
}
