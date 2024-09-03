namespace backend.TransactionDetailModule.Model
{
    public class TransactionDetailDTO
    {
        public long TransactionId { get; set; }
        public string TransactionNo { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Subtotal { get; set; }
    }
}
