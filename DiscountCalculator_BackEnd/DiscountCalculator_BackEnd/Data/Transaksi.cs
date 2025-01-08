using System;
using System.Collections.Generic;

namespace DiscountCalculator_BackEnd.Data
{
    public partial class Transaksi
    {
        public string TransaksiId { get; set; } = null!;
        public string TipeCustomer { get; set; } = null!;
        public int PointReward { get; set; }
        public int TotalBelanja { get; set; }
        public int Diskon { get; set; }
        public int TotalBayar { get; set; }
    }
}
