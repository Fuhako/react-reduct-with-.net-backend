using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.TransactionDetailModule.Model
{
    [Table("m_transaction_detail")]
    public class TransactionDetail
    {
        [Key]
        public long id { get; set; }
        public long transaction_id { get; set; } 
        public long product_variant_id { get; set; } 
        public decimal price { get; set; } 
        public int qty { get; set; } 
        public decimal subtotal { get; set; }
        public  bool active { get; set; } = true;
        public string created_user { get; set; } = "";
        public DateTime created_date { get; set; }
        public string updated_user { get; set; } = "";
        public DateTime updated_date { get; set; }
    }
}
