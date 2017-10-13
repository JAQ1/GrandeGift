using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
