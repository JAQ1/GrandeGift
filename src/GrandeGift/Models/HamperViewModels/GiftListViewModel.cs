using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.HamperViewModels
{
    public class GiftListViewModel
    {
        public IEnumerable<Gift> Gifts { get; set; }
        public IEnumerable<HamperGift> HamperGifts { get; set; }
        public Hamper Hamper { get; set; }
    }
}
