using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.HamperViewModels
{
    public class CreateHamperViewModel
    {
        public Hamper Hamper { get; set; }
        public string Name { get; set; }

        //details
        public IEnumerable<HamperGift> HamperGifts { get; set; }
        public double Price { get; set; }
        public string PhotoPath { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
