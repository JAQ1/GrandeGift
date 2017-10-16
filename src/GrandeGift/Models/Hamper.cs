using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }
        IEnumerable<HamperGift> HamperGifts { get; set; } 
    }
}
