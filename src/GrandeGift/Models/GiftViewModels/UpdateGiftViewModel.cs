﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models.GiftViewModels
{
    public class UpdateGiftViewModel
    {
        public int GiftId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PhotoPath { get; set; }
    }
}
