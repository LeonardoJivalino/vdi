namespace DiscountCalculator_FrontEnd.Models
{
    public class TransactionModel
    {
        public string TransactionId { get; set; } = null!;
        public string CustomerType { get; set; } = null!;
        public int PointReward { get; set; }
        public int Discount { get; set; }
        public string TotalBelanja { get; set; }
    }
}
