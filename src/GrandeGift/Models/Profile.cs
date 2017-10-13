using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string DisplayName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string DisplayPhotoPath { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
