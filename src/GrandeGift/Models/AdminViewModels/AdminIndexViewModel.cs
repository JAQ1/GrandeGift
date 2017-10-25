using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.AdminViewModels
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Gift> Gifts { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
    }
}
