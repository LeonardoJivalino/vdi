using System;
using System.Collections.Generic;

namespace DiscountCalculator_BackEnd.Data
{
    public partial class CustomerDiscount
    {
        public string TipeCustomer { get; set; } = null!;
        public int MinimumPointReward { get; set; }
        public int MaximumPointReward { get; set; }
        public string DiscountFormula { get; set; } = null!;
    }
}
