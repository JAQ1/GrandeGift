using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class DeliveryAddress
    {
        public int DeliveryAddressId { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public bool Active { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
