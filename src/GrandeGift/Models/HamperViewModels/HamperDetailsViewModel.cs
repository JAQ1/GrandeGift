using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.HamperViewModels
{
    public class HamperDetailsViewModel
    {
        public Hamper Hamper { get; set; }
        public IEnumerable<Hamper> OtherHampers { get; set; }
        public IEnumerable<HamperGift> HamperGifts { get; set; }
    }
}
