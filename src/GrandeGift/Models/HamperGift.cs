using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class HamperGift
    {
        public int HamperGiftId { get; set; }

        public int GiftId { get; set; }
        public Gift Gift { get; set; }
        public string GiftName { get; set; }
        public int HamperId { get; set; }
        public Hamper Hamper { get; set; }
        public string HamperName { get; set; }
    }
}
