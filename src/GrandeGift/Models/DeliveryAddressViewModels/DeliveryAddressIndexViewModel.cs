using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.DeliveryAddressViewModels
{
    public class DeliveryAddressIndexViewModel
    {
        public IEnumerable<DeliveryAddress> DeliveryAddresses { get; set; }
        public Profile Profile { get; set; }
    }
}
