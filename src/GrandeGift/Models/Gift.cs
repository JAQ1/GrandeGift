using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string PhotoPath { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
